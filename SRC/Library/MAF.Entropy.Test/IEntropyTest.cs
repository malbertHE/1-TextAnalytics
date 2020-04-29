using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAF.Entropy.Test
{
    [TestClass]
    public class IEntropyTest
    {
        Entropy e;

        [TestMethod]
        public void TestIEntropy()
        {
            e = new Entropy();
            CheckEntropyData();
            RunAndCheckCalculation();
        }

        private void RunAndCheckCalculation()
        {
            RunClaculation();
            Assert.IsTrue(e.ResultFile == Entropy.C_TestResultFile);
            Assert.IsFalse(e.IsRunCalculation);
        }

        private void RunClaculation()
        {
            e.RunCalculation("a");
            Assert.IsTrue(e.IsRunCalculation);
            e.WaitForAll();
        }

        private void CheckEntropyData()
        {
            Assert.IsNotNull(e);
            Assert.IsTrue(e.SourceDataFile == Entropy.C_DefaultSourceDataFile);
            Assert.IsTrue(e.ThreadCount == Entropy.C_DefaultThreadCount);
            Assert.IsTrue(e.RunningThreadCount == Entropy.C_DefaultRunningThreadCount);
            Assert.IsFalse(e.IsRunCalculation);
            Assert.IsTrue(e.ResultFile == Entropy.C_TestResultFile);
            Assert.IsTrue(e.CalculationLogic == Entropy.C_DefaultCalculationLogic);
        }
    }
}
