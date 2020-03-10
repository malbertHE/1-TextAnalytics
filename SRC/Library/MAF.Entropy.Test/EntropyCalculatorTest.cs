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
        }
    }
}
