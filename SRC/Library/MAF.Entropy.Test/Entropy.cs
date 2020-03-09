﻿namespace MAF.Entropy.Test
{
    class Entropy : IEntropy
    {
        public string SourceDataFile { get; } = string.Empty;

        public int ThreadCount { get; } = 1;

        public int RunningThreadCount { get; } = 1;

        public bool IsRunCalculation { get; } = false;
    }
}
