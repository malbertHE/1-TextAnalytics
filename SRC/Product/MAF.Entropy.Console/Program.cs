using MAF.Entropy.Collection;
using System;
using System.IO;
using System.Timers;

namespace MAF.Entropy.Console
{
    /// <summary>Entrópia számító konzolos alkalmazás.</summary>
    public class Program
    {
        /// <summary>Program információka tartalmazó fájl.</summary>
        public const string C_HelpFile = "README.md";

        /// <summary>Program indulásakor tesztelésre kerül, az információs fájl (<see cref="C_HelpFile"/>). Ha
        /// sérült vagy hiányzik, akkor ezzel a hibaüzenettel leáll a program. </summary>
        public const string C_HelpFileMD5Error = "Az információs fájl sérült ({0})!";

        /// <summary>A program futása közben, ha hiba történik, akkor a hibaüzenetet ezzel a szöveggel zárja.</summary>
        public const string C_ErrorEndText = "Súgó kiiratásához indítsa el a programot paraméterek nélkül!";

        /// <summary>Entrópia számítás kezdetekor a következő információ kerül kiírásra.</summary>
        public const string C_StartEntropyText = "Entrópia számítás elkezdődött.";

        /// <summary>Entrópia számítás befejezésekor a következő információ kerül kiírásra.</summary>
        public const string C_EndEntropyText = "Entrópia számítás befejeződött.";

        /// <summary>Sikeres entrópia számítás végén kiírt információ első sora.</summary>
        public const string C_SourceFileInfo = "Feldolgozott adat fájl: {0}";

        /// <summary>Sikeres entrópia számítás végén kiírt információ második sora.</summary>
        public const string C_ResultFileInfo = "Eredmény fájl: {0}";

        /// <summary>Sikeres entrópia számítás végén kiírt információ harmadik sora.</summary>
        public const string C_ResultEndInfo = "A feldolgozás sikeres!";

        /// <summary>Sikertelen feldolgozás esetén a zárószöveg.</summary>
        public const string C_CalculationError = "A feldolgozás sikertelen!";

        /// <summary>BELÉPÉSI PONT!</summary>
        /// <param name="args">Paraméterek.</param>
        public static void Main(string[] args)
        {
            try
            {
                Environment.ExitCode = (int)ExitCode.UnknownError;
                CheckHelpFileIsCorrect();
                if (args == null || args.Length == 0)
                    ShowHelp();
                else
                    RunCalculation(string.Join(" ", args));
                Environment.ExitCode = (int)ExitCode.Ok;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine(C_ErrorEndText);
            }
        }

        /// <summary>A súgó fájl MD5 összegének ellenőrzése. </summary>
        public static void CheckHelpFileIsCorrect()
        {
            if (!File.Exists(C_HelpFile) || Cryptography.FileMD5Calculator(C_HelpFile) != "0b683905e901920e9592ca179b6e3d42")
            {
                Environment.ExitCode = (int)ExitCode.HelpFileMD5Error;
                throw new Exception(string.Format(C_HelpFileMD5Error, C_HelpFile));
            }
        }

        /// <summary>Súgó kiiratása.</summary>
        public static void ShowHelp()
        {
            try
            {
                System.Console.WriteLine(File.ReadAllText("README.md"));
            }
            catch(Exception ex)
            {
                Environment.ExitCode = (int)ExitCode.ShowHelpError;
                throw ex;
            }
        }

        /// <summary>Entrópia számítás.</summary>
        /// <param name="pSourceFile">Feldolgozandó adatfájl.</param>
        public static void RunCalculation(string pSourceFile)
        {
            try
            {
                ec = new EntropyCalculator();
                System.Console.Write(C_StartEntropyText);
                ec.RunCalculation(pSourceFile);
                SetAndStartDotTimer();
                ec.WaitForAll();
                timer.Stop();
                WriteEndInfo();
            }
            catch (Exception ex)
            {
                Environment.ExitCode = (int)ExitCode.CalculationError;
                System.Console.WriteLine();
                System.Console.WriteLine($"{C_EndEntropyText} {C_CalculationError}");
                throw ex;
            }
        }

        /// <summary>Üres string, ha nem volt feldolgozás és eredményfájl, ha volt és sikeres.
        /// A teszt rendszer támogatását szolgálja.</summary>
        public static string ResultFile
        {
            get
            {
                return ec == null ? string.Empty : ec.ResultFile;
            }
        }

        #region Privát terület!

        static EntropyCalculator ec;
        static Timer timer;

        private static void SetAndStartDotTimer()
        {
            timer = new Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            System.Console.Write(".");
        }

        private static void WriteEndInfo()
        {
            System.Console.WriteLine();
            System.Console.WriteLine(C_EndEntropyText);
            if (!ec.CalcWithoutError)
                WriteErrorInfo();
            WriteResultInfo();
        }

        private static void WriteErrorInfo()
        {
            Environment.ExitCode = (int)ExitCode.CalculationThredError;
            foreach(Exception ex in ec.CalculationExceptionList)
                System.Console.WriteLine(ex.Message);
            throw new Exception(C_CalculationError);
        }

        private static void WriteResultInfo()
        {
            System.Console.WriteLine(C_SourceFileInfo, ec.SourceDataFile);
            System.Console.WriteLine(C_ResultFileInfo, ec.ResultFile);
            System.Console.WriteLine(C_ResultEndInfo);
        }

        #endregion
    }
}
