using System;
using System.IO;
using System.Diagnostics;
using Microsoft.Build.Evaluation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAF.Entropy.Console.Test
{
    [TestClass]
    public class SystemTest
    {
        [AssemblyInitialize]
        public static void InitAssembly(TestContext context)
        {

            var collection = ProjectCollection.GlobalProjectCollection;
            var project = collection.LoadProject(solutions[0]);
            string configuration;
#if DEBUG
            configuration = "Debug";
#else
            configuration = "Release";
#endif
            project.SetProperty("Configuration", configuration);
            bool result = project.Build();
        }

        [TestInitialize]
        public void ClassInitialize()
        {
            File.Delete(c_CopyHelpFile);
            File.Copy(Program.C_HelpFile, c_CopyHelpFile);
        }

        [TestMethod]
        public void TestHelp()
        {
            Process p = new Process();
            SetProcess(p);
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            string error = p.StandardError.ReadToEnd();
            p.WaitForExit();
            Assert.IsTrue(p.ExitCode == (int)ExitCode.Ok);
            Assert.IsTrue(output == string.Concat(File.ReadAllText(Program.C_HelpFile), Environment.NewLine));
            Assert.IsTrue(error == string.Empty);
        }

        [TestMethod]
        public void TestHelpFileIsNotExist()
        {
            Process p = new Process();
            SetProcess(p);
            try
            {
                File.Delete(Program.C_HelpFile);
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                string error = p.StandardError.ReadToEnd();
                p.WaitForExit();
                Assert.IsTrue(p.ExitCode == (int)ExitCode.HelpFileMD5Error);
                Assert.IsTrue(error == string.Empty);
                Assert.IsTrue(output == string.Concat(string.Format(Program.C_HelpFileMD5Error, Program.C_HelpFile), Environment.NewLine,
                                                      Program.C_ErrorEndText, Environment.NewLine));
            }
            finally
            {
                File.Copy(c_CopyHelpFile, Program.C_HelpFile);
            }
        }

        [TestMethod]
        public void TestHelpFileIsBad()
        {
            Process p = new Process();
            SetProcess(p);
            try
            {
                using (StreamWriter sw = File.AppendText(Program.C_HelpFile))
                    sw.WriteLine("");
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                string error = p.StandardError.ReadToEnd();
                p.WaitForExit();
                Assert.IsTrue(p.ExitCode == (int)ExitCode.HelpFileMD5Error);
                Assert.IsTrue(error == string.Empty);
                Assert.IsTrue(output == string.Concat(string.Format(Program.C_HelpFileMD5Error, Program.C_HelpFile), Environment.NewLine,
                                                      Program.C_ErrorEndText, Environment.NewLine));
            }
            finally
            {
                File.Delete(Program.C_HelpFile);
                File.Copy(c_CopyHelpFile, Program.C_HelpFile);
            }
        }

        [TestMethod]
        public void TestRunCalculation()
        {
            const string c_SourceFile = @"TestFiles\Test2.txt";
            Process p = new Process();
            SetProcess(p);           
            p.StartInfo.Arguments = c_SourceFile;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            string error = p.StandardError.ReadToEnd();
            p.WaitForExit();
            Assert.IsTrue(p.ExitCode == (int)ExitCode.Ok);
            Assert.IsTrue(error == string.Empty);
        }


        static readonly string[] solutions = new string[] { @"..\..\..\MAF.Entropy.Console\MAF.Entropy.Console.csproj" };
        const string c_CopyHelpFile = "README_copy.md";

        private void SetProcess(Process p)
        {
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "Entropy.exe";
        }

    }
}
