using MAF.Collection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAF.TextAnalytics.Test.Collection
{
    [TestClass]
    public class CryptographyTest
    {
        [TestMethod]
        public void TestFileMD5Calculator()
        {
            const string C_TestSourceFile = @"TestFiles\test1.txt";
            Assert.IsTrue(Cryptography.FileMD5Calculator(C_TestSourceFile) == "313a8ae8b2f582523e18fb6a8df48897");
        }
    }
}
