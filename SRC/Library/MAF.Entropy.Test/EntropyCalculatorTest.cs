using MAF.Collection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace MAF.Entropy.Test
{
    [TestClass]
    public class EntropyCalculatorTest
    {
        EntropyCalculator ec;
        const string c_TestSourceFile = @"TestFiles\test1.txt";


        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {
            if (Directory.Exists(EntropyCalculator.C_DefaultResultDir))
                Directory.Delete(EntropyCalculator.C_DefaultResultDir, true);
        }

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
            const string c_SampleResultFile = @"SampleFiles\Test1.xml";
            RunCalculation(c_TestSourceFile);
            ChekcResult();
            CheckContent(c_SampleResultFile);
        }

        private void CheckContent(string c_SampleResultFile)
        {
            List<EntropyResult> erl1 = (List<EntropyResult>)XMLObject.XMLToObject(c_SampleResultFile, typeof(List<EntropyResult>));
            List<EntropyResult> erl2 = (List<EntropyResult>)XMLObject.XMLToObject(ec.ResultFile, typeof(List<EntropyResult>));
            Assert.IsTrue(erl1.Count == erl2.Count);
            foreach (EntropyResult er1 in erl1)
                CheckContentItem(erl1, er1);
        }

        private static void CheckContentItem(List<EntropyResult> erl1, EntropyResult er1)
        {
            EntropyResult er2 = erl1.Find(x => x.Logic.Name == er1.Logic.Name);
            ChekcLogic(er1, er2);
            for (int j = 0; j < er1.Logic.RegexList.Count; j++)
                CheckLogicItem(er1, er2, j);
            ChekcEntropyData(er1, er2);
            foreach (EntropyItem ei1 in er1.ItemList)
                CheckEntropyData(er2, ei1);
        }

        private static void CheckEntropyData(EntropyResult er2, EntropyItem ei1)
        {
            EntropyItem ei2 = er2.ItemList.Find(x => x.Value == ei1.Value);
            Assert.IsTrue(ei1.Count == ei2.Count);
            Assert.IsTrue(ei1.P == ei2.P);
            Assert.IsTrue(ei1.I == ei2.I);
        }

        private static void ChekcEntropyData(EntropyResult er1, EntropyResult er2)
        {
            Assert.IsTrue(er1.ShannonEntropy == er2.ShannonEntropy);
            Assert.IsTrue(er1.I == er2.I);
            Assert.IsTrue(er1.Hmax == er2.Hmax);
            Assert.IsTrue(er1.SignCount == er2.SignCount);
            Assert.IsTrue(er1.DifferentSignsCount == er2.DifferentSignsCount);
            Assert.IsTrue(er1.ItemList.Count == er2.ItemList.Count);
        }

        private static void CheckLogicItem(EntropyResult er1, EntropyResult er2, int j)
        {
            RegEx re1 = er1.Logic.RegexList[j];
            RegEx re2 = er2.Logic.RegexList[j];
            Assert.IsTrue(re1.Pattern == re2.Pattern);
            Assert.IsTrue(re1.RegexOptions == re2.RegexOptions);
            Assert.IsTrue(re1.Replace == re2.Replace);
            Assert.IsTrue(re1.ToLower == re2.ToLower);
        }

        private static void ChekcLogic(EntropyResult er1, EntropyResult er2)
        {
            Assert.IsTrue(er1.Logic.Name == er2.Logic.Name);
            Assert.IsTrue(er1.Logic.Trim == er2.Logic.Trim);
            Assert.IsTrue(er1.Logic.NoEmpty == er2.Logic.NoEmpty);
            Assert.IsTrue(er1.Logic.RegexList.Count == er2.Logic.RegexList.Count);
        }

        private void ChekcResult()
        {
            Assert.IsFalse(ec.IsRunCalculation);
            Assert.IsTrue(ec.CalculationLogic == EntropyCalculator.C_DefaultLogicFile);
            Assert.IsTrue(File.Exists(ec.ResultFile));
            Assert.IsTrue(ec.ResultFile != string.Empty);
        }

        private void RunCalculation(string c_TestSourceFile)
        {
            ec.RunCalculation(c_TestSourceFile);
            Assert.IsTrue(ec.SourceDataFile == c_TestSourceFile);
            ec.WaitForAll();
            Assert.IsTrue(ec.SourceDataFile == c_TestSourceFile);
        }

        [TestMethod]
        public void TestIsRunCalculationError()
        {
            try
            {
                ec.RunCalculation(c_TestSourceFile);
                ec.RunCalculation(c_TestSourceFile);
                Assert.Fail();
            }
            catch(EntropyCalculatorException ex)
            {
                Assert.IsTrue(ex.Message == EntropyCalculator.C_IsRunCalculationError);
            }
            Assert.IsTrue(ec.SourceDataFile == c_TestSourceFile);
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
            Assert.IsTrue(ec.ResultFile != string.Empty);
            InitTest();
        }

        private void InitTest()
        {
            Assert.IsTrue(ec.CalculationLogic == EntropyCalculator.C_DefaultLogicFile);
            Assert.IsFalse(ec.IsRunCalculation);
            Assert.IsTrue(ec.ThreadCount > 0);
            Assert.IsTrue(ec.RunningThreadCount == 0);
        }
    }
}
