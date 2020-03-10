using System;
using System.IO;

namespace MAF.Entropy
{
    /// <summary>Entrópia számítást végző osztály.
    /// </summary>
    public class EntropyCalculator
    {
        /// <summary>Alapértelmezett logika.</summary>
        public const string C_DefaultLogicFile = "Logic\\ecl.xml";

        /// <summary>Feldolgozás indításakor, ha a feldolgozandó fájl nem létezik, akkor ezt a hibaüzenetet dobja.</summary>
        public const string C_SourceFileNotExist = "Nem létezik a következő feldolgozandó fájl: {0} !";

        /// <summary>Feldolgozás indításakor, ha váratlan hiba esemény következik be, akkor ezt a hibaüzenetet dobja, a teljes hibalánccal.</summary>
        public const string C_CalculationError = "A következő fájl feldolgozásakor hiba történt: {0} !";

        /// <summary>Forrás fájl, amit legutoljára kíséreltek meg feldolgozni.</summary>
        public string SourceDataFile { get; private set; } = string.Empty;

        /// <summary>Feldolgozási logikát leíró fájl.</summary>
        public string CalculationLogic { get; private set; } = C_DefaultLogicFile;

        /// <summary>Ennyi szálon fogunk próbálkozni a feldolgozással, ha a szöveg mérete ezt igényli.</summary>
        public int ThreadCount { get; private set; } = 1;

        /// <summary>Ennyi szál indult el végül.</summary>
        public int RunningThreadCount { get; private set; } = 0;

        /// <summary>Igaz, ha fut a feldolgozás és hamis, ha nem.</summary>
        public bool IsRunCalculation { get; private set; } = false;

        /// <summary>Eredményfájl. Ide kerülnek be az entrópia számítás eredményei.</summary>
        public string ResultFile { get; private set; } = string.Empty;

        /// <summary>Entrópia feldolgozó osztály konstruktora, ami betölti a feldolgozás logikáját.</summary>
        public EntropyCalculator()
        {
            LoadLogic();
        }


        /// <summary>Entrópia számítást elindító függvény. 
        /// Figyelem! A függvény saját szálat vagy szálakat indít és a futást visszaadja a hívónak!
        /// A feldolgozás végét és a szálak leállását a <see cref="IsRunCalculation"/> jelzi.
        /// Amíg ez igaz, addig folyamatban van a feldolgozás.
        /// A szálak bevárásához a <see cref="WaitForAll"/> metódus szolgál.</summary>
        public void RunCalculation(string pSourceDataFile)
        {
            try
            {
                SourceDataFile = pSourceDataFile;
                InitCalc();
                CheckSourceDataFile();
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


        #region Privát tertület!

        private void LoadLogic()
        {
            
        }

        private void InitCalc()
        {
            ResultFile = string.Empty;
            IsRunCalculation = true;
            ThreadCount = 1;
            RunningThreadCount = 0;
        }

        private void CheckSourceDataFile()
        {
            if (!File.Exists(SourceDataFile))
                throw new EntropyCalculatorException(string.Format(C_SourceFileNotExist, SourceDataFile));
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
