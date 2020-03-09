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
            Assert.IsTrue(e.SourceDataFile == string.Empty);
        }
    }
}
