namespace MAF.Entropy.Console
{
    /// <summary>Az entrópia számító konzolos alkalmazás kilépési kódjai.</summary>
    public enum ExitCode
    {
        /// <summary>A program futása közben ismeretlen hiba törtét.</summary>
        UnknownError = -1,

        /// <summary>A program sikeresen lefutott, a kért műveletet végrehajtotta.</summary>
        Ok = 0,

        /// <summary>A README.md fájl módosult!</summary>
        HelpFileMD5Error = 1,

        /// <summary>Információs fájl kiírása közben hiba történt.</summary>
        ShowHelpError = 2,

        /// <summary>Entrópia számítás indítása közben hiba történt.</summary>
        CalculationError = 3,

        /// <summary>Entrópia számítás számítás szál hiba történt.</summary>
        CalculationThredError = 4
    }
}
