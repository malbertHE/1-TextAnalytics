namespace MAF.Entropy
{
    /// <summary>Entrópia számítás eredményének tételei.</summary>
    public class EntropyItem
    {
        /// <summary>A jel.</summary>
        public string Value = string.Empty;

        /// <summary>A jel előfordulása.</summary>
        public uint Count = 0;

        /// <summary>Relatív gyakoriság értéke.</summary>
        public double P = 0;

        /// <summary>Információ mennyiség</summary>
        public double I = 0;
    }
}
