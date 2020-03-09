using System;
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
            Assert.IsTrue(Name == string.Empty);
        }
    }
}
