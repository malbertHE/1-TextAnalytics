using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;

namespace MAF.TextAnalytics.Server.Test
{
    [TestClass]
    public class TextAnalyticsDBTest
    {
        static protected DataProvider db;

        const string c_NotExistFile = "Nem létező fájl!";
        const string c_NotExistUserLoginName = "Ez meg nem is felhasználó!";
        const string c_TextFile1 = @"TestFiles\TextFile1.txt";
        const string c_ResultTile1 = @"TestFiles\TextFile1.xml";

        protected const string c_TestUserLoginName = "malbert";
        protected const string c_TestUserName = "Megyesi Albert";
        protected const string c_TestUserPass = "123";

        [TestMethod]
        public void TestUserRegistrationAndCheckUserExist()
        {
            string userLoginName = GetTestUserLoginName();
            db.SignUP(userLoginName, c_TestUserName, c_TestUserPass);
            Assert.IsTrue(db.SignIn(userLoginName, c_TestUserPass));
        }

        [TestMethod]
        public void TestUserRegistrationFaliedUserExist()
        {
            string userLoginName = GetTestUserLoginName();
            db.SignUP(userLoginName, c_TestUserName, c_TestUserPass);
            try
            {
                db.SignUP(userLoginName, c_TestUserName, c_TestUserPass);
                Assert.Fail();
            }
            catch(DataProviderException ex)
            {
                Assert.IsTrue(ex.Message == DataProvider.C_UserExistError);
            }            
        }

        [TestMethod]
        public void TestFileNotExist()
        {
            Assert.IsFalse(db.SourceFileExist("ff4f45ca1b9a8609af6c4909d000ed43"));
        }

        [TestMethod]
        public void TestRunCalculation_NotExist()
        {
            try
            {
                db.RunCalculation(c_NotExistFile, c_NotExistUserLoginName);
                Assert.Fail();
            }
            catch(DataProviderException ex)
            {
                Assert.IsTrue(ex.Message == string.Format(DataProvider.C_FileNotExist, c_NotExistFile));
            }
        }

        [TestMethod]
        public void TestSaveCalculationInfo_NotExistUser()
        {
            try
            {
                db.RunCalculation(c_TextFile1, c_NotExistUserLoginName);
                Assert.Fail();
            }
            catch (DataProviderException ex)
            {
                Assert.IsTrue(ex.Message == string.Format(DataProvider.C_UserNotExistError, c_NotExistUserLoginName));
            }
        }

        [TestMethod]
        public void TestSaveCalculationInfo_NotExistFile()
        {
            string userLoginName = GetTestUserLoginName();
            db.SignUP(userLoginName, c_TestUserName, c_TestUserPass);
            try
            {
                db.RunCalculation(c_NotExistFile, userLoginName);
                Assert.Fail();
            }
            catch (DataProviderException ex)
            {
                Assert.IsTrue(ex.Message == string.Format(DataProvider.C_FileNotExist, c_NotExistFile));
            }
        }

        [TestMethod]
        public void TestRunCalculation()
        {
            string userLoginName = GetTestUserLoginName();
            db.SignUP(userLoginName, c_TestUserName, c_TestUserPass);
            db.RunCalculation(c_TextFile1, userLoginName);
            Assert.IsTrue(db.SourceFileExist("ff4f45ca1b9a8609af6c4909d000ed44"));
        }

        [TestMethod]
        public void TestGetResultDataFile()
        {
            Assert.IsTrue(db.GetResultFile("ff4f45ca1b9a8609af6c4909d000ed34") == string.Empty);
            string userLoginName = GetTestUserLoginName();
            db.SignUP(userLoginName, c_TestUserName, c_TestUserPass);
            db.RunCalculation(c_TextFile1, userLoginName);
            Assert.IsTrue(File.Exists(db.GetResultFile("ff4f45ca1b9a8609af6c4909d000ed44")));
        }

        [TestMethod]
        public void TestSignIn_UserNotExist()
        {
            Assert.IsFalse(db.SignIn(c_NotExistUserLoginName, c_TestUserPass));
        }

        string GetTestUserLoginName()
        {
            return $"{c_TestUserLoginName} {Stopwatch.GetTimestamp()}";
        }
    }
}
