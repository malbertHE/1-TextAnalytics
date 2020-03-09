namespace MAF.Entropy.Test
{
    class Entropy : IEntropy
    {
        public const string C_TestResultFile = "test.xml";

        public string SourceDataFile { get; } = string.Empty;

        public int ThreadCount { get; } = 1;

        public int RunningThreadCount { get; } = 1;

        public bool IsRunCalculation { get { return isRunCalculation; } }

        public string ResultFile { get { return resultFile; } }

        public string CalculationLogic { get; } = string.Empty;

        public void RunCalculation()
        {
            isRunCalculation = true;
        }

        public void WaitForAll()
        {
            isRunCalculation = false;
            resultFile = C_TestResultFile;
        }

        string resultFile = string.Empty;
        bool isRunCalculation = false;
    }
}
