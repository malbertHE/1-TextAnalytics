using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAF.Entropy.Test
{
    [TestClass]
    public class EntropyItemTest
    {
        [TestMethod]
        public void TestEntropyItem()
        {
            EntropyItem ei = new EntropyItem();
            Assert.IsTrue(ei.Value == string.Empty);
            Assert.IsTrue(ei.Count == 0);
            Assert.IsTrue(ei.P == 0);
            Assert.IsTrue(ei.I == 0);
        }
    }
}
