using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAF.Entropy.Test
{
    [TestClass]
    public class IEntropyTest
    {
        [TestMethod]
        public void TestIEntropy()
        {
            Entropy e = new Entropy();
            Assert.IsNotNull(e);
            Assert.IsTrue(e.SourceDataFile == Entropy.C_DefaultSourceDataFile);
            Assert.IsTrue(e.ThreadCount == Entropy.C_DefaultThreadCount);
            Assert.IsTrue(e.RunningThreadCount == Entropy.C_DefaultRunningThreadCount);
            Assert.IsFalse(e.IsRunCalculation);
            Assert.IsTrue(e.ResultFile == Entropy.C_TestResultFile);
            Assert.IsTrue(e.CalculationLogic == Entropy.C_DefaultCalculationLogic);
            
            e.RunCalculation("a");
            Assert.IsTrue(e.IsRunCalculation);
            e.WaitForAll();
            Assert.IsTrue(e.ResultFile == Entropy.C_TestResultFile);
            Assert.IsFalse(e.IsRunCalculation);
        }
    }
}
