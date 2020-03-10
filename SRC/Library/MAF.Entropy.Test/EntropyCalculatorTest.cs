using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAF.Entropy.Test
{
    [TestClass]
    public class EntropyCalculatorTest
    {
        EntropyCalculator ec;

        [TestInitialize]
        public void ClassInitialize()
        {
            ec = new EntropyCalculator();
            Assert.IsTrue(ec.SourceDataFile == string.Empty);
            Assert.IsTrue(ec.ResultFile == string.Empty);
            Assert.IsTrue(ec.CalculationLogic == EntropyCalculator.C_DefaultLogicFile);
            Assert.IsFalse(ec.IsRunCalculation);
            Assert.IsTrue(ec.ThreadCount == 1);
            Assert.IsTrue(ec.RunningThreadCount == 0);
        }

        [TestMethod]
        public void TestRunCalculation()
        {
            const string C_TestSourceFile = "test1.xml";
            ec.RunCalculation(C_TestSourceFile);
            Assert.IsTrue(ec.SourceDataFile == C_TestSourceFile);
        }
    }
}
