using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAF.Entropy.Console.Test
{
    [TestClass]
    public class ProgramTest
    {
        [TestInitialize]
        public void ClassInitialize()
        {
            File.Delete(c_CopyHelpFile);
            File.Copy(Program.C_HelpFile, c_CopyHelpFile);
        }

        [TestMethod]
        public void TestHelpFileIsCorrect()
        {
            Program.CheckHelpFileIsCorrect();
        }

        [TestMethod]
        public void TestHelpFileIsNotExist()
        {
            try
            {
                File.Delete(Program.C_HelpFile);
                CheckHelpFileIsCorrect();
            }
            finally
            {
                File.Copy(c_CopyHelpFile, Program.C_HelpFile);
            }
        }

        [TestMethod]
        public void TestHelpFileIsBad()
        {
            try
            {
                using (StreamWriter sw = File.AppendText(Program.C_HelpFile))
                    sw.WriteLine("");
                CheckHelpFileIsCorrect();
            }
            finally
            {
                File.Delete(Program.C_HelpFile);
                File.Copy(c_CopyHelpFile, Program.C_HelpFile);
            }
        }

        [TestMethod]
        public void TestShowHelp()
        {
            TextWriter tmp = System.Console.Out;
            try
            {
                const string c_TestHelpFile = "TestShowHelp.md";
                using (StreamWriter writer = new StreamWriter(c_TestHelpFile))
                {
                    System.Console.SetOut(writer);
                    Program.ShowHelp();
                }
                Assert.IsTrue(string.Concat(File.ReadAllText(Program.C_HelpFile), Environment.NewLine) == 
                    File.ReadAllText(c_TestHelpFile));
            }
            finally
            {
                System.Console.SetOut(tmp);
            }
        }

        [TestMethod]
        public void TestShowHelpError()
        {
            try
            {
                File.Delete(Program.C_HelpFile);
                try
                {
                    Program.ShowHelp();
                    Assert.Fail();
                }
                catch(Exception ex)
                {
                    Assert.IsTrue(ex.Message != string.Empty);
                }
            }
            finally
            {
                File.Copy(c_CopyHelpFile, Program.C_HelpFile);
            }
        }

        [TestMethod]
        public void TestRunCalculation()
        {
            const string c_SourceFile = @"TestFiles\Test2.txt";
            const string c_ConsoleOutFile = @"RunCalculationOut.txt";
            TextWriter tmp = System.Console.Out;
            try
            {
                using (StreamWriter writer = new StreamWriter(c_ConsoleOutFile))
                {
                    System.Console.SetOut(writer);
                    Program.RunCalculation(c_SourceFile);
                }
                Assert.IsTrue(string.Format(File.ReadAllText(@"SampleFiles\RunCalculationOut.txt"), c_SourceFile, Program.ResultFile) ==
                    File.ReadAllText(c_ConsoleOutFile));
            }
            finally
            {
                System.Console.SetOut(tmp);
            }
        }

        [TestMethod]
        public void TestRunCalculationNotExistSourceFile()
        {
            const string c_SourceFile = @"TestFiles\not.exist";
            const string c_ConsoleOutFile = @"RunCalculationSFOut.txt";
            TextWriter tmp = System.Console.Out;
            try
            {
                using (StreamWriter writer = new StreamWriter(c_ConsoleOutFile))
                {
                    System.Console.SetOut(writer);
                    try
                    {
                        Program.RunCalculation(c_SourceFile);
                        Assert.Fail();
                    }
                    catch(Exception ex)
                    {
                        Assert.IsTrue(ex.Message == string.Format(EntropyCalculator.C_SourceFileNotExist, c_SourceFile));
                    }
                }
                Assert.IsTrue(File.ReadAllText(@"SampleFiles\RunCalculationSFOut.txt") == File.ReadAllText(c_ConsoleOutFile));
            }
            finally
            {
                System.Console.SetOut(tmp);
            }
        }

        [TestMethod]
        public void TestMainNoParameter()
        {
            const string c_ConsoleOutFile = @"MainNoParameterOut.txt";
            TextWriter tmp = System.Console.Out;
            try
            {
                using (StreamWriter writer = new StreamWriter(c_ConsoleOutFile))
                {
                    System.Console.SetOut(writer);
                    Program.Main(null);
                }
                Assert.IsTrue(string.Concat(File.ReadAllText(Program.C_HelpFile), Environment.NewLine) == File.ReadAllText(c_ConsoleOutFile));
            }
            finally
            {
                System.Console.SetOut(tmp);
            }
        }

        [TestMethod]
        public void TestMainBadParameter()
        {
            const string c_ConsoleOutFile = @"MainBadParameterOut.txt";
            const string c_SourceFile = "notExist.file";
            TextWriter tmp = System.Console.Out;
            try
            {
                using (StreamWriter writer = new StreamWriter(c_ConsoleOutFile))
                {
                    System.Console.SetOut(writer);
                    Program.Main(new string[] { c_SourceFile });
                }
                Assert.IsTrue(string.Format(File.ReadAllText(@"SampleFiles\MainBadParameterOut.txt"), c_SourceFile) == 
                    File.ReadAllText(c_ConsoleOutFile));
            }
            finally
            {
                System.Console.SetOut(tmp);
            }
        }

        [TestMethod]
        public void TestMain()
        {
            const string c_SourceFile = @"TestFiles\Test2.txt";
            const string c_ConsoleOutFile = @"MainOut.txt";
            TextWriter tmp = System.Console.Out;
            try
            {
                using (StreamWriter writer = new StreamWriter(c_ConsoleOutFile))
                {
                    System.Console.SetOut(writer);
                    Program.Main(new string[] { c_SourceFile });
                }
                Assert.IsTrue(string.Format(File.ReadAllText(@"SampleFiles\RunCalculationOut.txt"), c_SourceFile, Program.ResultFile) ==
                    File.ReadAllText(c_ConsoleOutFile));
            }
            finally
            {
                System.Console.SetOut(tmp);
            }
        }


        const string c_CopyHelpFile = "README_copy.md";

        private void CopyHelpFileTo(string pCopyHelpFile)
        {
            File.Delete(pCopyHelpFile);
            File.Copy(Program.C_HelpFile, pCopyHelpFile);
        }

        private void CheckHelpFileIsCorrect()
        {
            try
            {
                Program.CheckHelpFileIsCorrect();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message == string.Format(Program.C_HelpFileMD5Error, Program.C_HelpFile));
            }
        }
    }
}
