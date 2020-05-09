using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAF.Entropy.WPF.Test
{
    [TestClass]
    public class MainWindowTest : EntropySession
    {
        /*[TestMethod]
        public void TestAboutButton()
        {
            /* ToDo: 
            session.FindElementByName("AboutButton").Click();
            ...
            * /
        }*/    

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Setup(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }

    }
}
