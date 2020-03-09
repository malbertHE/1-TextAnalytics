using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAF.Entropy.Test
{
    [TestClass]
    public class RegExTest
    {
        [TestMethod]
        public void TestRegEx()
        {
            RegEx re = new RegEx();
            Assert.IsTrue(re.Pattern == string.Empty);
            Assert.IsTrue(re.Replace == string.Empty);
            Assert.IsTrue(re.RegexOptions == string.Empty);
            Assert.IsTrue(re.ToLower == string.Empty);
        }
    }
}
