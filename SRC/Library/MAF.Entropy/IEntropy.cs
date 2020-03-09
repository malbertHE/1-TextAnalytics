namespace MAF.Entropy
{
    /// <summary>Entrópia számítás modul (MAF.Entropy.dll) publikált szolgáltatásait leíró interfész.</summary>
    public interface IEntropy
    {
        /// <summary>A forrás adat, ami feldolgozásra került.</summary>
        string SourceDataFile { get; }

        /// <summary>Ennyi szálon fogunk próbálkozni a feldolgozással, ha a szöveg mérete ezt igényli.</summary>
        int ThreadCount { get; }

    }
}
