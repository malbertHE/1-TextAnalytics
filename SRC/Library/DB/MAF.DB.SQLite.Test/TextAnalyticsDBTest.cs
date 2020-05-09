using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace MAF.DB.SQLite.Test
{
    [TestClass]
    public class TextAnalyticsDBTest
    {
        const string c_NotExistFile = "Nem létező fájl!";
        const string c_NotExistUserLoginName = "Ez meg nem is felhasználó!";
        const string c_TextFile1 = @"TestFiles\TextFile1.txt";
        const string c_ResultTile1 = @"TestFiles\TextFile1.xml";

        const string c_TestUserLoginName = "malbert";
        const string c_TestUserName = "Megyesi Albert";
        const string c_TestUserPass = "123";

        private static TextAnalyticsDB db;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            if (File.Exists(TextAnalyticsDB.C_DBName))
                File.Delete(TextAnalyticsDB.C_DBName);
            db = new TextAnalyticsDB();
            Assert.IsTrue(File.Exists(TextAnalyticsDB.C_DBName));

            db.SignUp(c_TestUserLoginName, c_TestUserName, c_TestUserPass);
        }


        [TestMethod]
        public void TestFileExistFalse()
        {
            Assert.IsFalse(db.SourceFileExist("ff4f45ca1b9a8609af6c4909d000ed43"));
        }

        [TestMethod]
        public void TestSaveCalculationInfo_NotExist()
        {
            try
            {
                db.SaveCalculationInfo(c_NotExistFile, c_NotExistFile, c_NotExistUserLoginName);
                Assert.Fail();
            }
            catch(Exception ex)
            {
                Assert.IsTrue(ex is InvalidOperationException);
                Assert.IsTrue(ex.Message == "No current row");
            }
        }

        [TestMethod]
        public void TestSaveCalculationInfo_ExistUserID()
        {
            try
            {
                db.SaveCalculationInfo(c_TextFile1, c_NotExistFile, c_TestUserLoginName);
                Assert.Fail();
            }
            catch (FileNotFoundException ex)
            {
                Assert.IsTrue(ex.FileName == Path.Combine(Environment.CurrentDirectory, c_NotExistFile));
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestSaveCalculationInfo_NotExistResourceFileAndUserID()
        {
            try
            {
                db.SaveCalculationInfo(c_TextFile1, c_NotExistFile, c_NotExistUserLoginName);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is InvalidOperationException);
                Assert.IsTrue(ex.Message == "No current row");
            }
        }

        [TestMethod]
        public void TestSaveCalculationInfo_NotExistResourceFile()
        {
            try
            {
                db.SaveCalculationInfo(c_TextFile1, c_NotExistFile, c_TestUserLoginName);
                Assert.Fail();
            }
            catch (FileNotFoundException ex)
            {
                Assert.IsTrue(ex.FileName == Path.Combine(Environment.CurrentDirectory, c_NotExistFile));
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestSaveCalculationInfo_NotExistUserID()
        {
            try
            {
                db.SaveCalculationInfo(c_TextFile1, c_ResultTile1, c_NotExistUserLoginName);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is InvalidOperationException);
                Assert.IsTrue(ex.Message == "No current row");
            }
        }

        [TestMethod]
        public void TestSaveCalculationInfoAndFileExist()
        {
            db.SaveCalculationInfo(c_TextFile1, c_ResultTile1, c_TestUserLoginName);
            Assert.IsTrue(db.SourceFileExist("ff4f45ca1b9a8609af6c4909d000ed44"));
        }

        [TestMethod]
        public void TestGetResultDataFile()
        {
            Assert.IsTrue(db.GetResultDataFile("ff4f45ca1b9a8609af6c4909d000ed34") == string.Empty);
            db.SaveCalculationInfo(c_TextFile1, c_ResultTile1, c_TestUserLoginName);
            Assert.IsTrue(db.GetResultDataFile("ff4f45ca1b9a8609af6c4909d000ed44") == c_ResultTile1);
        }
    }
}
