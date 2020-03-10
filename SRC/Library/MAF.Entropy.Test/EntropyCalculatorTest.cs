using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAF.Entropy.Test
{
    [TestClass]
    public class EntropyCalculatorTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            const string c_ScrFile = "testSRC.xml";
            EntropyCalculator ec = new EntropyCalculator(c_ScrFile);
            Assert.IsTrue(ec.SourceDataFile == c_ScrFile);
        }
    }
}
