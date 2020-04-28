using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using MAF.Entropy;

namespace MAF.TextAnalytics.Server
{
    /// <summary>Az <see cref="EntropyCalculator"/> szerver módú futtatásához készült.
    /// Ha ezen keresztül megy a feldolgozás, akkor nyilvántartja, hogy milyen fájlok lettek már feldolgozva.</summary>
    public class DataProvider
    {
		/// <summary>Adatbázís típusok.</summary>
        public enum DataBaseType
		{
			/// <summary>XML fál. Alapértelmezett. Csak teszteléshez ajánlott.</summary>
			NA,

			/// <summary>Egyszerű, telepítést nem igénylő SQL alapú adatbázis, arra az esetre, ha nincs elérhető SQL szerver.</summary>
			SQLite,

			/// <summary>MySQL szerver elérés esetére.</summary>
			MySQL,

			/// <summary>Oracle szerver elérés esetére.</summary>
			Oracle
		}

        public const string C_UserExistError = "Már létezik ilyen felhasználó!";
        public const string C_UserNotExistError = "A következő felhasználó nem létezik: {0}!";
        public const string C_FileNotExist = "Nem létezik a következő fájl: {0}";

        /// <summary>A konstruktor inicializálja a megfelelő adatelérési objektumot.</summary>
        /// <param name="pResultPath">A feldolgozási eredmények könyvtára.</param>
        public DataProvider(string pResultPath = EntropyCalculator.C_DefaultResultDir)
        {
            resultPath = pResultPath;
            var appConfig = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
			string sDBType = appConfig.AppSettings.Settings["DBType"].Value;
            DataBaseType dbt = Enum.TryParse(sDBType, out DataBaseType dbType) ? dbType : DataBaseType.NA;
            SetDB(dbt);
        }

        /// <summary>Ez a konstruktor tesztekhez készült, ettő</summary>
        /// <param name="pDataBaseType">Adatbázis típusa.</param>
        public DataProvider(DataBaseType pDataBaseType)
        {
            SetDB(pDataBaseType);
        }

        /// <summary>Felhasználó regisztrálása.</summary>
        /// <param name="pLoginName">Felhasználó login neve.</param>
        /// <param name="pUserName">Felhasználó teljes neve.</param>
        /// <param name="pPassword">Felhasználó jelszava.</param>
        public void SignUP(string pLoginName, string pUserName, string pPassword)
		{
            if (db.UserExist(pLoginName))
                throw new DataProviderException(C_UserExistError);
            lock (db) { db.SignUp(pLoginName, pUserName, pPassword); }
		}

        /// <summary>Felhasználó bejelentkezése.</summary>
        /// <param name="pLoginName">Felhasználó login neve.</param>
        /// <param name="pPassword">Felhasználó jelszava.</param>
        /// <returns>Ha létezik ilyen felhasználó és a jelszó is az, akkor igazat ad vissza.</returns>
        public bool SignIn(string pLoginName, string pPassword)
		{
			bool exist;
			lock (db) { exist = db.SignIn(pLoginName, pPassword); }
			return exist;
		}

		/// <summary>Megmondja, hogy dolgozták-e már fel az adott fájlt.</summary>
		/// <param name="pSourceFileMD5">Az eredetileg feldolgozott fájl MD5-je.</param>
		/// <returns>Igaz, ha már dolgozták fel a fájlt.</returns>
		public bool SourceFileExist(string pSourceFileMD5)
        {
            bool b;
            lock (db) { b = db.SourceFileExist(pSourceFileMD5); }
            return b;
        }

        /// <summary>MD5 alapján visszaad egy eredményfájl elérési útját, ill, ha nincs ilyen md5, akkor üres stringet.</summary>
        /// <param name="pResultMD5">Keresett fájl, aminek az md5-je ezzel egyezik.</param>
        /// <returns>A fájl elérési útja és neve vagy üres string.</returns>
        public string GetResultFile(string pResultMD5)
        {
            string file;
            lock (db) { file = db.GetResultDataFile(pResultMD5); }
            return file;
        }

		/// <summary>Szöveges fájl feldolgozása.</summary>
		/// <param name="pSourceFile">Feldolgozandó szöveges fájl.</param>
		/// <param name="pUserLoginName">Felhasznál azonsoító.</param>
		/// <returns>Eredmény fájl.</returns>
		public string RunCalculation(string pSourceFile, string pUserLoginName)
        {
            CheckConditions(pSourceFile, pUserLoginName);
            string resultFile = RunCalculation(pSourceFile);
            lock (db) { db.SaveCalculationInfo(pSourceFile, resultFile, pUserLoginName); };
            return resultFile;
        }

        string resultPath = EntropyCalculator.C_DefaultResultDir;
        ITextAnalyticsDB db = null;

        string RunCalculation(string pSourceFile)
        {
            EntropyCalculator ec = new EntropyCalculator() { ResultDir = resultPath };
            ec.RunCalculation(pSourceFile);
            ec.WaitForAll();
            return ec.ResultFile;
        }

        void CheckConditions(string pSourceFile, string pUserLoginName)
        {
            if (!File.Exists(pSourceFile))
                throw new DataProviderException(string.Format(C_FileNotExist, pSourceFile));
            bool userExist;
            lock (db) { userExist = db.UserExist(pUserLoginName); }
            if (!userExist)
                throw new DataProviderException(string.Format(C_UserNotExistError, pUserLoginName));
        }

        void SetDB(DataBaseType dbt)
        {
            switch (dbt)
            {
                case DataBaseType.SQLite:
                    db = new DB.SQLite.TextAnalyticsDB();
                    break;
                case DataBaseType.MySQL:
                    //db = new TextAnalyticsMySQL.TextAnalyticsDB();
                    break;
                default:
                    db = new TextAnalyticsFilesInfo();
                    break;
            }
        }
    }

    [Serializable]
    public class DataProviderException : Exception
    {
        public DataProviderException(string message) : base(message)
        {
        }

        public DataProviderException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
