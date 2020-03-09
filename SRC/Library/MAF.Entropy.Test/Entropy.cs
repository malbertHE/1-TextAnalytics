namespace MAF.Entropy.Test
{
    class Entropy : IEntropy
    {
        public const string C_TestResultFile = "test.xml";

        public string SourceDataFile { get; } = string.Empty;

        public int ThreadCount { get; } = 1;

        public int RunningThreadCount { get; } = 1;

        public bool IsRunCalculation { get; } = false;

        public string ResultFile { get; } = resultFile;

        public string CalculationLogic { get; } = string.Empty;


        string resultFile = string.Empty;
    }
}
