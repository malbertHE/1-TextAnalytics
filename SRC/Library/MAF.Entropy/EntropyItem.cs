namespace MAF.Entropy
{
    /// <summary>Entrópia számítás eredményének tételei.</summary>
    public class EntropyItem
    {
        /// <summary>A jel.</summary>
        public string Value { get; private set; } = string.Empty;

        /// <summary>A jel előfordulása.</summary>
        public uint Count;

        /// <summary>Relatív gyakoriság értéke.</summary>
        public double P;
    }
}
