using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAF.Entropy.Test
{
    [TestClass]
    public class EntropyResultTest
    {
        EntropyResult er;

        [TestMethod]
        public void TestEntropyResult()
        {
            er = new EntropyResult();
            CheckEntropyDefaultData();
        }

        private void CheckEntropyDefaultData()
        {
            CheckDefaultData();
            Assert.IsNull(er.Logic);
            Assert.IsTrue(er.ItemList.Count == 0);
        }

        private void CheckDefaultData()
        {
            Assert.IsTrue(er.ShannonEntropy == 0);
            Assert.IsTrue(er.I == 0);
            Assert.IsTrue(er.Hmax == 0);
            Assert.IsTrue(er.SignCount == 0);
            Assert.IsTrue(er.DifferentSignsCount == 0);
        }
    }
}
