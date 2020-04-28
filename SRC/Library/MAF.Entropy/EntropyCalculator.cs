using MAF.Collection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MAF.Entropy
{
    /// <summary>Entrópia számítást végző osztály. Egyetlen célja van, szövegből entrópiát számít.</summary>
    public class EntropyCalculator : IEntropy
    {
        /// <summary>Az eredmények ebbe a mappába kerülenk bele.</summary>
        public const string C_DefaultResultDir = "Result";

        /// <summary>Alapértelmezett logika.</summary>
        public const string C_DefaultLogicFile = "Logic\\ecl.xml";

        /// <summary>Alapértelmezett minimum feldolgozási blokk méret. Maximum ekkora szövegblokk dolgozódik fel egy szálon.</summary>
        public const int C_DefaultMinBlockSize = 640 * c_KByte; //Mert ez elég kell legyen mindenkinek :) !

        /// <summary>A konstruktor ezt a hibát dobja, ha nem létezik a logikát leíró fájl!</summary>
        public const string C_LogicFileNotExist = "Nem létezik a következő logikát leíró fájl: {0} !";

        /// <summary>A konstruktor, amennyiben nem tudja betölteni a feldolgozási logikát, akkor ezt a hibát dobja, a teljes hibalánccal!</summary>
        public const string C_LoadLogicError = "A következő logikai fájl betöltés közben hiba történt: {0} !";

        /// <summary>Feldolgozás indításakor, ha a feldolgozandó fájl nem létezik, akkor ezt a hibaüzenetet dobja!</summary>
        public const string C_SourceFileNotExist = "Nem létezik a következő feldolgozandó fájl: {0} !";

        /// <summary>Feldolgozás indításakor, ha váratlan hiba esemény következik be, akkor ezt a hibaüzenetet dobja, a teljes hibalánccal!</summary>
        public const string C_CalculationError = "A következő fájl feldolgozásakor hiba történt: {0} !";

        /// <summary>Feldolgozás futása közben, ha új feldolgozást indítana a hívó, akkor ezt a hibaüzenetet dobja a feldolgozó!</summary>
        public const string C_IsRunCalculationError = "Éppen fut egy feldolgozás!";

        /// <summary>Ha a fájl nagyobb mint 10MB és a mérete nagyobb mint a felhasználható memória 65%-a, akkor ezt a hibaüzenetet dobja!</summary>
        public const string C_MemoryError = "A kért műveletet sajnos nem tudjuk végrehajtani mert túlságosan leterhelné a gépet!";

        /// <summary>Entrópia számításkor az egyes szálak részeredményeinek összesítése közben, ha hiba történik, akkor ezt a hibaüzenetet
        /// dobja a feldolgozó összesítő szála. </summary>
        public const string C_SumResultError = "A feldolgozás részeredményeinek összesítése közben hiba történt!";

        /// <summary>Ha valamelyik szál hibára fut, akkor ezt a hibaüzenetet mentjük a hibalistába, a teljes hibalánccal együtt!</summary>
        public const string C_CalcError = "Entrópia számítás közben hiba történt!";

        /// <summary>Forrás fájl, amit legutoljára kíséreltek meg feldolgozni.</summary>
        public string SourceDataFile { get; private set; } = string.Empty;

        /// <summary>Feldolgozási logikát leíró fájl.</summary>
        public string CalculationLogic { get; private set; } = C_DefaultLogicFile;

        /// <summary>Ennyi szálon fogunk próbálkozni a feldolgozással, ha a szöveg mérete ezt igényli.
        /// A maximum szálak száma a processzor szálainak száma - 1.</summary>
        public int ThreadCount { get; private set; }

        /// <summary>Ennyi szál indult el végül.</summary>
        public int RunningThreadCount { get { return threadObjects == null ? 0 : threadObjects.Length; } }

        /// <summary>Igaz, ha fut a feldolgozás és hamis, ha nem.</summary>
        public bool IsRunCalculation { get; private set; } = false;

        /// <summary>Eredményfájl. Ide kerülnek be az entrópia számítás eredményei.</summary>
        public string ResultFile { get; private set; } = string.Empty;

        /// <summary>Maximum feldolgozási blokk méret.</summary>
        public long MaxBlockSize { get; private set; } = C_DefaultMinBlockSize;

        /// <summary>Feldolgozás közben előjött hibák.</summary>
        public List<Exception> CalculationExceptionList { get; private set; } = null;

        /// <summary>Ha a feldolgozás hiba nélkül lefutott, akkor igaz, különben hamis.</summary>
        public bool CalcWithoutError { get { return CalculationExceptionList == null || CalculationExceptionList.Count == 0; } }

        /// <summary>A feldolgozandó fájl karakter kódolása.</summary>
        public Encoding TextEncoding { get; private set; } = Encoding.UTF8;

        /// <summary>Munka könyvtár. Ide kerülnek az entrópia számítások eredményei.</summary>
        public string ResultDir { get; set; } = C_DefaultResultDir;

        /// <summary>Entrópia feldolgozó osztály konstruktora, ami betölti a feldolgozás logikáját.</summary>
        public EntropyCalculator()
        {
            LoadLogic();
            ThreadCount = Environment.ProcessorCount > 1 ? Environment.ProcessorCount - 1 : 1;
        }

        /// <summary>Entrópia számítást elindító függvény. 
        /// Figyelem! A függvény saját szálat vagy szálakat indít és a futást visszaadja a hívónak!
        /// A feldolgozás végét és a szálak leállását a <see cref="IsRunCalculation"/> jelzi.
        /// Amíg ez igaz, addig folyamatban van a feldolgozás.
        /// A szálak bevárásához a <see cref="WaitForAll"/> metódus szolgál.</summary>
        public void RunCalculation(string pSourceDataFile)
        {
            if (IsRunCalculation)
                throw new EntropyCalculatorException(C_IsRunCalculationError);
            try
            {
                SourceDataFile = pSourceDataFile;
                InitCalc();
                CheckParams();
                RunThreads();
            }
            catch(Exception ex)
            {
                IsRunCalculation = false;
                if (ex is EntropyCalculatorException)
                    throw ex;
                else
                    throw new EntropyCalculatorException(string.Format(C_CalculationError, SourceDataFile), ex);
            }
        }

        /// <summary>Ha ezt elindítják, akkor bevárja az összes szálat.</summary>
        public void WaitForAll()
        {
            threadMain.Join();
        }


        #region Privát tertület!

        const int c_KByte = 1024;

        EntropyCalculationLogic logic;
        Thread threadMain;
        ThreadObject[] threadObjects;
        List<EntropyResult> resultList;

        private void LoadLogic()
        {
            if (!File.Exists(C_DefaultLogicFile))
                throw new EntropyCalculatorException(string.Format(C_LogicFileNotExist, C_DefaultLogicFile));
            try
            {
                logic = (EntropyCalculationLogic)XMLObject.XMLToObject(
                    CalculationLogic, typeof(EntropyCalculationLogic));
            }
            catch (Exception ex)
            {
                throw new EntropyCalculatorException(string.Format(C_LoadLogicError, C_DefaultLogicFile), ex);
            }
        }

        private void InitCalc()
        {
            IsRunCalculation = true;
            if (!Directory.Exists(ResultDir))
                Directory.CreateDirectory(ResultDir);
            ResultFile = Path.Combine(ResultDir, string.Concat(Path.GetFileNameWithoutExtension(SourceDataFile),
                "_", DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss"), ".xml"));
            ThreadCount = 1;
            CalculationExceptionList = new List<Exception>();
        }

        private void CheckParams()
        {
            CheckSourceDataFile();
            CheckMemoryAndSetBlockSize();
        }

        private void CheckSourceDataFile()
        {
            if (!File.Exists(SourceDataFile))
                throw new EntropyCalculatorException(string.Format(C_SourceFileNotExist, SourceDataFile));
        }

        private void CheckMemoryAndSetBlockSize()
        {
            long fileSize = GetFileSize();
            MaxBlockSize = fileSize / ThreadCount;
            ThrowExceptionIfNotEnoughtMemory(fileSize);
        }

        private long GetFileSize()
        {
            FileInfo fi = new FileInfo(SourceDataFile);
            if (fi.Length < c_KByte)
                ThreadCount = 1;
            return fi.Length;
        }

        private void ThrowExceptionIfNotEnoughtMemory(long pFileSize)
        {
            if (c_KByte * c_KByte * 10 < pFileSize)
            {
                PerformanceCounter pCntr = new PerformanceCounter("Memory", "Available KBytes");
                long memAvailable = (long)(pCntr.NextValue() * c_KByte);
                long mem65 = (long)((memAvailable / 0.65));
                if (mem65 < (long)pFileSize)
                    throw new EntropyCalculatorException(C_MemoryError);
            }
        }

        private void RunThreads()
        {
            CreateAndRunThreadObjects();
            CreateAndRunMainThread();
        }

        private void CreateAndRunMainThread()
        {
            threadMain = new Thread(RunMain);
            threadMain.Start();
        }

        private void CreateAndRunThreadObjects()
        {
            threadObjects = new ThreadObject[ThreadCount];
            using (StreamReader srSource = new StreamReader(SourceDataFile, TextEncoding))
                for (int i = 0; i < ThreadCount; i++)
                {
                    threadObjects[i] = CreateThread(srSource);
                    SetThreadParams(srSource, threadObjects, i);
                    threadObjects[i].SetAndStartThread(new Thread(new ParameterizedThreadStart(RunBlock)));
                }
        }

        private ThreadObject CreateThread(StreamReader pSRSource)
        {
            if (pSRSource.Peek() < 0)
                return null;
            else
                return new ThreadObject();
        }

        private void SetThreadParams(StreamReader pSRSource, ThreadObject[] pThreadObjects, int pThreadIDX)
        {
            long textSize = 0;
            while (pSRSource.Peek() >= 0 && (textSize < MaxBlockSize || pThreadIDX == ThreadCount - 1))
            {
                string line = pSRSource.ReadLine();
                pThreadObjects[pThreadIDX].AddTextList(line);
                textSize += line.Length;
            }
        }

        private void RunMain()
        {
            try
            {
                for (int i = 0; i < threadObjects.Length; i++)
                    threadObjects[i].ThreadJoin();
                SumThreadResult();
                threadObjects = null;
            }
            catch (Exception ex)
            {
                CalculationExceptionList.Add(new EntropyCalculatorException(C_CalcError, ex));
            }
        }

        private void RunBlock(object pThreadParams)
        {
            try
            {
                ThreadParams blockPars = (ThreadParams)pThreadParams;
                RunLogicBlock(blockPars);
            }
            catch (Exception ex)
            {
                CalculationExceptionList.Add(new EntropyCalculatorException(C_CalcError, ex));
            }
        }

        private void RunLogicBlock(ThreadParams pThreadParams)
        {
            Parallel.ForEach(logic.EntropyLogicList, (el) =>
            {
                for(int i = 0; i < pThreadParams.TextListCount; i++)
                {
                    MatchCollection mc = FindSigns(el, pThreadParams.GetTextList(i));
                    if (mc != null)
                        AddEntropyResults(pThreadParams, el, mc);
                }
            });
        }

        private void AddEntropyResults(ThreadParams pThreadParams, EntropyLogic pEntropyLogic, MatchCollection pMatchCollection)
        {
            EntropyResult er2 = pThreadParams.FindEntropyResult(pEntropyLogic.Name);
            if (er2 == null)
            {
                er2 = new EntropyResult() { Logic = pEntropyLogic };
                pThreadParams.AddEntropyResult(er2);
            }
            AddEntropyResultItems(pEntropyLogic, pMatchCollection, er2);
        }

        private void AddEntropyResultItems(EntropyLogic pEntropyLogic, MatchCollection pMatchCollection, EntropyResult pEntropyResult)
        {
            foreach (Match m in pMatchCollection)
            {
                string value = m.Value;
                if (pEntropyLogic.Trim != string.Empty)
                    value = value.Trim();
                if (pEntropyLogic.NoEmpty && value == string.Empty)
                    continue;
                EntropyItem it = pEntropyResult.ItemList.Find(x => x.Value == value);
                if (it == null)
                    pEntropyResult.ItemList.Add(new EntropyItem() { Value = value, Count = 1 });
                else
                    it.Count++;
                pEntropyResult.SignCount++;
            }
        }


        private MatchCollection FindSigns(EntropyLogic pEntropyLogic, string pText)
        {
            string tmp = pText;
            MatchCollection mc = null;
            foreach (RegEx re in pEntropyLogic.RegexList)
            {
                if (re.ToLower)
                    tmp = tmp.ToLower();
                RegexOptions ro = re.RegexOptions == string.Empty ? RegexOptions.None :
                    (RegexOptions)Enum.Parse(typeof(RegexOptions), re.RegexOptions);
                if (re.Replace != string.Empty)
                    tmp = Regex.Replace(tmp, re.Pattern, re.Replace, ro);
                else
                    mc = Regex.Matches(tmp, re.Pattern, ro);
            }
            return mc;
        }

        private void SumThreadResult()
        {
            try
            {
                SumPartialResults();
                CalcEntropy();
                XMLObject.ObjectToXML(ResultFile, resultList);
                IsRunCalculation = false;
            }
            catch (Exception ex)
            {
                IsRunCalculation = false;
                throw new EntropyCalculatorException(C_SumResultError, ex);
            }
        }

        private void SumPartialResults()
        {
            resultList = threadObjects[0].EntropyResultList;
            for (int i = 1; i < threadObjects.Length; i++)
            {
                if (threadObjects[i] == null)
                    break;
                SumLogicPartial(threadObjects[i]);
            }
        }

        private void SumLogicPartial(ThreadObject pThreadObject)
        {
            Parallel.ForEach(pThreadObject.EntropyResultList, (er) =>
            {
                EntropyResult er2 = resultList.Find(x => x.Logic.Name == er.Logic.Name);
                foreach (EntropyItem it in er.ItemList)
                    SumItem(er2, it);
                er2.SignCount += er.SignCount;
            });
        }

        private void SumItem(EntropyResult pEntropyResult, EntropyItem pEntropyItem)
        {
            EntropyItem it2 = pEntropyResult.ItemList.Find(x => x.Value == pEntropyItem.Value);
            if (it2 == null)
                pEntropyResult.ItemList.Add(new EntropyItem() { Value = pEntropyItem.Value, Count = pEntropyItem.Count });
            else
                it2.Count += pEntropyItem.Count;
        }


        private void CalcEntropy()
        {
            Parallel.ForEach(resultList, (er) =>
            {
                foreach (EntropyItem it in er.ItemList)
                    CalcItem(er, it);
                er.I = er.SignCount * er.ShannonEntropy;
                er.Hmax = Math.Log(er.SignCount, 2);
                er.DifferentSignsCount = er.ItemList.Count;
            });
        }

        private void CalcItem(EntropyResult pEntropyResult, EntropyItem pEntropyItem)
        {
            pEntropyItem.P = (double)pEntropyItem.Count / pEntropyResult.SignCount;
            double pI = 1 / pEntropyItem.P;
            pEntropyItem.I = Math.Log(pI, 2);
            pEntropyResult.ShannonEntropy += pEntropyItem.P * pEntropyItem.I;
        }

        #endregion
    }

    [Serializable]
    public class EntropyCalculatorException : Exception
    {
        public EntropyCalculatorException(string message) : base(message)
        {
        }

        public EntropyCalculatorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
