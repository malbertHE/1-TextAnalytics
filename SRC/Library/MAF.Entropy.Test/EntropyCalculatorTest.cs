using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAF.Entropy.Test
{
    [TestClass]
    public class EntropyCalculatorTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            EntropyCalculator ec = new EntropyCalculator();
            Assert.IsTrue(ec.SourceDataFile == string.Empty);
            Assert.IsTrue(ec.ResultFile == string.Empty);
            Assert.IsTrue(ec.CalculationLogic == EntropyCalculator.C_DefaultLogicFile);
            Assert.IsFalse(ec.IsRunCalculation);
            Assert.IsTrue(ec.ThreadCount == 1);
            Assert.IsTrue(ec.RunningThreadCount == 0);
        }
    }
}
