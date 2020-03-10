using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAF.Entropy.Test
{
    [TestClass]
    public class EntropyCalculatorTest
    {
        EntropyCalculator ec;

        [TestInitialize]
        public void ClassInitialize()
        {
            ec = new EntropyCalculator();
            Assert.IsTrue(ec.SourceDataFile == string.Empty);
            InitTest();
        }

        [TestMethod]
        public void TestRunCalculation()
        {
            const string C_TestSourceFile = @"TestFiles\test1.txt";
            ec.RunCalculation(C_TestSourceFile);
            Assert.IsTrue(ec.SourceDataFile == C_TestSourceFile);
        }

        [TestMethod]
        public void TestRunCalculation_NotExistSourceFile()
        {
            const string C_TestSourceFile = "notExist.file";
            try
            {
                ec.RunCalculation(C_TestSourceFile);
                Assert.Fail();
            }
            catch(EntropyCalculatorException ex)
            {
                Assert.IsTrue(ex.Message == string.Format(EntropyCalculator.C_SourceFileNotExist, C_TestSourceFile));
            }
            Assert.IsTrue(ec.SourceDataFile == C_TestSourceFile);
            InitTest();
        }

        private void InitTest()
        {
            Assert.IsTrue(ec.ResultFile == string.Empty);
            Assert.IsTrue(ec.CalculationLogic == EntropyCalculator.C_DefaultLogicFile);
            Assert.IsFalse(ec.IsRunCalculation);
            Assert.IsTrue(ec.ThreadCount == 1);
            Assert.IsTrue(ec.RunningThreadCount == 0);
        }
    }
}
