using System;
using System.Data.SQLite;
using MAF.Collection;
using MAF.TextAnalytics.Server;

namespace MAF.DB.SQLite
{
    /// <summary>SQLite adatbázis kiszolgáló.</summary>
    public class TextAnalyticsDB : ITextAnalyticsDB
    {
        public const string C_DBName = "TextAnalyticsService.sqlite";
        public const string C_UserIDNotExist = "Nem létezik a következő felhasználó azonosító: {0}!";

        /// <summary>Konstruktor szükség esetén adatbázis fájl és táblák létrehozásával. A konstruktor az adatbázist megnyitja.</summary>
        public TextAnalyticsDB()
        {
            m_dbConnection = new SQLiteConnection($"Data Source={C_DBName};Version=3;");
            m_dbConnection.Open();
            CheckAndSetDB();
        }

        /// <summary>Megmondja, hogy dolgozták-e már fel az adott fájlt.</summary>
        /// <param name="pSourceFileMD5">Az eredetileg feldolgozott fájl MD5-je.</param>
        /// <returns>Igaz, ha már dolgozták fel a fájlt.</returns>
        public bool FileExist(string pMD5)
        {
            string sql = $"SELECT 1 FROM {c_TextAnalyticsTableName} WHERE {c_SourceFileMD5} = '{pMD5}'";
            SQLiteCommand c = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = c.ExecuteReader();
            return reader.HasRows;
        }

        /// <summary>MD5 alapján visszaad egy eredményfájl elérési útját, ill, ha nincs ilyen md5, akkor üres stringet.</summary>
        /// <param name="pSourceFileMD5">Keresett fájl, aminek az md5-je ezzel egyezik.</param>
        /// <returns>A fájl elérési útja és neve vagy üres string.</returns>
        public string GetResultDataFile(string pSourceFileMD5)
        {
            string sql = $"SELECT {c_ResultFile} FROM {c_TextAnalyticsTableName} WHERE {c_SourceFileMD5} = '{pSourceFileMD5}'";
            SQLiteCommand c = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = c.ExecuteReader();
            if (!reader.HasRows)
                return string.Empty;
            reader.Read();
            return reader[c_ResultFile].ToString();
        }

        /// <summary>Egy feldolgozott fájl információinak elmentése adatbázisba.</summary>
        /// <param name="pFile">Eredeti fájl.</param>
        /// <param name="pResultFile">Eredmény fájl.</param>
        /// <param name="pUserLoginName">Felhasznál login neve.</param>
        public void SaveCalculationInfo(string pFile, string pResultFile, string pUserLoginName)
        {
            string userID = GetUserID(pUserLoginName);
            SaveCalculation(pFile, pResultFile, userID);
        }

        /// <summary>Felhasználó regisztrálása.</summary>
        /// <param name="pLoginName">Felhasználó login neve.</param>
        /// <param name="pUserName">Felhasználó teljes neve.</param>
        /// <param name="pPassword">Felhasználó jelszava.</param>
        public void SignUp(string pLoginName, string pUserName, string pPassword)
        {
            if (UserExist(pLoginName, pUserName))
                throw new Exception("Már létezik ilyen felhasználó azonosító!");

            string sql = $"insert into {c_UserTableName} ({c_UserLoginName}, {c_UserName}, {c_UserPassword}) values ('{pLoginName}', '{pUserName}', '{pPassword}')";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        /// <summary>Felhasználó ellenőrzése, hogy létezik-e az adott jelszóval.</summary>
        /// <param name="pLoginName">Felhasználó login neve.</param>
        /// <param name="pPassword">Felhasználó jelszava.</param>
        /// <returns>Ha létezik ilyen felhasználó és a jelszó is az, akkor igazat ad vissza.</returns>
        public bool UserExist(string pLoginName, string pPassword)
        {
            string sql = $"SELECT 1 FROM {c_UserTableName} WHERE {c_UserLoginName} = '{pLoginName}' AND {c_UserPassword} = '{pPassword}'";
            SQLiteCommand c = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = c.ExecuteReader();
            return reader.HasRows;
        }


        const string c_UserTableName = "Users";
        const string c_UserID = "u_ID";
        const string c_UserLoginName = "u_LoginName";
        const string c_UserName = "u_Name";
        const string c_UserPassword = "u_Password";
        const string c_UserInsertDate = "u_InsertDate";

        const string c_TextAnalyticsTableName = "TextAnalytics";
        const string c_TextAnalyticsID = "ta_ID";
        const string c_SourceFileMD5 = "ta_SourceFileMD5";
        const string c_SourceFile = "ta_SourceFile";
        const string c_ResultMD5 = "ta_ResultMD5";
        const string c_ResultFile = "ta_ResultFile";

        const string c_InfoTableName = "TextAnalyticsInfo";
        const string c_InfoID = "tai_ID";
        const string c_InfoUserID = "tai_u_ID";
        const string c_InfoTextAnalyticsID = "tai_ta_ID";
        const string c_InfoInsertDate = "tai_InsertDate";

        SQLiteConnection m_dbConnection;

        void CheckAndSetDB()
        {
            if (!CheckTableExists(c_UserTableName))
                CreateUserTable();

            if (!CheckTableExists(c_TextAnalyticsTableName))
                CreateTextAnalyticsTable();

            if (!CheckTableExists(c_InfoTableName))
                CreateInfoTable();
        }

        void CreateTextAnalyticsTable()
        {
            string sql = $"CREATE TABLE {c_TextAnalyticsTableName} ({c_TextAnalyticsID} INTEGER PRIMARY KEY AUTOINCREMENT, {c_SourceFileMD5} VARCHAR(32) NOT NULL UNIQUE, " +
                $"{c_SourceFile} VARCHAR(255) NOT NULL, {c_ResultMD5} VARCHAR(32) NOT NULL, {c_ResultFile} VARCHAR(255) NOT NULL, {c_UserInsertDate} DATETIME DEFAULT now)";
            SQLiteCommand c = new SQLiteCommand(sql, m_dbConnection);
            c.ExecuteNonQuery();
        }

        void CreateUserTable()
        {
            string sql = $"CREATE TABLE {c_UserTableName}({c_UserID} INTEGER PRIMARY KEY AUTOINCREMENT, {c_UserLoginName} VARCHAR(20) NOT NULL UNIQUE, " +
                         $"{c_UserName} VARCHAR(255) NOT NULL, {c_UserPassword} VARCHAR(512) NOT NULL, {c_UserInsertDate} DATETIME DEFAULT now)";
            SQLiteCommand c = new SQLiteCommand(sql, m_dbConnection);
            c.ExecuteNonQuery();
        }

        void CreateInfoTable()
        {
            string sql = $"CREATE TABLE {c_InfoTableName}({c_InfoID} INTEGER PRIMARY KEY AUTOINCREMENT, {c_InfoUserID} INTEGER NOT NULL, " +
                         $"{c_InfoTextAnalyticsID} INTEGER NOT NULL, {c_InfoInsertDate} DATETIME DEFAULT now, " +
                         $"FOREIGN KEY({c_InfoUserID}) REFERENCES {c_UserTableName}({c_UserID}), " +
                         $"FOREIGN KEY({c_InfoTextAnalyticsID}) REFERENCES {c_TextAnalyticsTableName}({c_TextAnalyticsID}))";
            SQLiteCommand c = new SQLiteCommand(sql, m_dbConnection);
            c.ExecuteNonQuery();
        }

        bool CheckTableExists(string pTablename)
        {
            string sql = $"SELECT name FROM sqlite_master WHERE type = 'table' AND name = '{pTablename}'";
            SQLiteCommand c = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = c.ExecuteReader();
            return reader.HasRows;
        }

        string InsertOrUpdateTextAnalytics(string pFile, string pResultFile)
        {
            string sourceMD5 = Cryptography.FileMD5Calculator(pFile);
            string resultMD5 = Cryptography.FileMD5Calculator(pResultFile);
            if (FileExist(sourceMD5))
                UpdateTextAnalytics(pFile, pResultFile, sourceMD5, resultMD5);
            else
                InsertTextAnalytics(pFile, pResultFile, sourceMD5, resultMD5);
            return sourceMD5;
        }

        void UpdateTextAnalytics(string pSourceFile, string pResultFile, string pSourceMD5, string pResultMD5)
        {
            string sql = $"update {c_TextAnalyticsTableName} set {c_SourceFile} = '{pSourceFile}', {c_ResultFile} = '{pResultFile}', {c_ResultMD5} = '{pResultMD5}'" +
                         $" where {c_SourceFileMD5}='{pSourceMD5}'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        void InsertTextAnalytics(string pSourceFile, string pResultFile, string pSourceMD5, string pResultMD5)
        {
            string sql = $"insert into {c_TextAnalyticsTableName} ({c_SourceFileMD5}, {c_SourceFile}, {c_ResultMD5}, {c_ResultFile}) " +
                         $"values ('{pSourceMD5}', '{pSourceFile}', '{pResultMD5}', '{pResultFile}')";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        void InsertInfo(string pSourceFileMD5, string pUserID)
        {
            string sql = $"insert into {c_InfoTableName} ({c_InfoUserID}, {c_InfoTextAnalyticsID}) " +
                         $"select '{pUserID}', {c_TextAnalyticsID} from {c_TextAnalyticsTableName} where {c_SourceFileMD5}='{pSourceFileMD5}'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        void BeginTran()
        {
            string sql = "BEGIN TRANSACTION";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        void Commit()
        {
            string sql = "COMMIT";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        void Rollback()
        {
            string sql = "ROLLBACK";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        void SaveCalculation(string pFile, string pResultFile, string pUserID)
        {
            try
            {
                BeginTran();
                string sourceFileMD5 = InsertOrUpdateTextAnalytics(pFile, pResultFile);
                InsertInfo(sourceFileMD5, pUserID);
                Commit();
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        string GetUserID(string pUserLoginName)
        {
            string sql = $"SELECT {c_UserID} FROM {c_UserTableName} WHERE {c_UserLoginName} = '{pUserLoginName}'";
            SQLiteCommand c = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = c.ExecuteReader();            
            if (!reader.HasRows)
                throw new TextAnalyticsDBException(string.Format(C_UserIDNotExist, pUserLoginName));
            reader.Read();
            return reader[c_UserID].ToString();
        }

    }

    [Serializable]
    public class TextAnalyticsDBException : Exception
    {
        public TextAnalyticsDBException(string message) : base(message)
        {
        }

        public TextAnalyticsDBException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
