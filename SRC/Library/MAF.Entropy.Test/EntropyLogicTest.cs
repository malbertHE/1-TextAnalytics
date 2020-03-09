using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAF.Entropy.Test
{
    [TestClass]
    public class EntropyLogicTest
    {
        [TestMethod]
        public void TestEntropyLogic()
        {
            EntropyLogic el = new EntropyLogic();
            Assert.IsTrue(el.Name == string.Empty);
            Assert.IsTrue(el.RegexList.Count == 0);
            Assert.IsTrue(el.Trim == string.Empty);
            Assert.IsTrue(el.NoEmpty == false);
        }
    }
}
