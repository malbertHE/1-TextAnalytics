namespace MAF.Entropy
{
    /// <summary>Entrópia számítás modul (MAF.Entropy.dll) publikált szolgáltatásait leíró interfész.</summary>
    public interface IEntropy
    {
        /// <summary>A forrás adat, ami feldolgozásra került.</summary>
        string SourceDataFile { get; }

    }
}
