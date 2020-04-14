namespace MAF.Entropy.Test
{
    class Entropy : IEntropy
    {
        public const string C_DefaultSourceDataFile = "";
        public const int C_DefaultThreadCount = 1;
        public const int C_DefaultRunningThreadCount = 1;
        public const string C_DefaultCalculationLogic = "";
        public const string C_DefaultResultFile = "";
        public const bool C_DefaultIsRunCalculation = false;


        public const string C_TestResultFile = "test.xml";

        public string SourceDataFile { get; } = C_DefaultSourceDataFile;

        public int ThreadCount { get; } = C_DefaultThreadCount;

        public int RunningThreadCount { get; } = C_DefaultRunningThreadCount;

        public bool IsRunCalculation { get; private set; } = C_DefaultIsRunCalculation;

        public string ResultFile { get; private set; } = C_TestResultFile;

        public string CalculationLogic { get; } = C_DefaultCalculationLogic;

        public void RunCalculation(string pSourceDataFile)
        {
            IsRunCalculation = true;
        }

        public void WaitForAll()
        {
            IsRunCalculation = false;
            ResultFile = C_TestResultFile;
        }
    }
}
