using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAF.Entropy.Test
{
    [TestClass]
    public class EntropyResultTest
    {
        [TestMethod]
        public void TestEntropyResult()
        {
            EntropyResult er = new EntropyResult();
            Assert.IsTrue(er.ShannonEntropy == 0);
            Assert.IsTrue(er.I == 0);
            Assert.IsTrue(er.Hmax == 0);
        }
    }
}
