namespace MAF.Entropy.Test
{
    class Entropy : IEntropy
    {
        public string SourceDataFile { get; } = string.Empty;

        public int ThreadCount { get; } = 1;

    }
}
