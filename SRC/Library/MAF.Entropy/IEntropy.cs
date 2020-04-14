namespace MAF.Entropy
{
    /// <summary>Entrópia számítás modul (MAF.Entropy.dll) publikált szolgáltatásait leíró interfész.</summary>
    public interface IEntropy
    {
        /// <summary>A forrás adat, ami feldolgozásra került.</summary>
        string SourceDataFile { get; }

        /// <summary>Ennyi szálon fogunk próbálkozni a feldolgozással, ha a szöveg mérete ezt igényli.</summary>
        int ThreadCount { get; }

        /// <summary>Ennyi szál indult el végül.</summary>
        int RunningThreadCount { get; }

        /// <summary>Igaz, ha éppen fut a feldolgozás és hamis, ha nem.</summary>
        bool IsRunCalculation { get; }

        /// <summary>Eredményfájl. Ide kerülnek be az entrópia számítás eredményei.</summary>
        string ResultFile { get; }

        /// <summary>Feldolgozási logikát leíró fájl.</summary>
        string CalculationLogic { get; }

        /// <summary>Entrópia számítást elindító függvény. 
        /// Figyelem! A függvény saját szálat vagy szálakat indít és a futást visszaadja a hívónak!
        /// A feldolgozás végét és a szálak leállását a <see cref="IsRunCalculation"/> jelzi.
        /// Amíg ez igaz, addig folyamatban van a feldolgozás.
        /// A szálak bevárásához a <see cref="WaitForAll"/> metódus szolgál.</summary>
        void RunCalculation(string pSourceDataFile);

        /// <summary>Ha ezt elindítják, akkor bevárja az összes szálat.</summary>
        void WaitForAll();
    }
}
