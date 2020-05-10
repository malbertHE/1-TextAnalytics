using System;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Reflection;
using MAF.Collection;

namespace MAF.TextAnalytics.DB.MySQL
{
    /// <summary>MySQL adatbázis kiszolgáló.</summary>
	public class TextAnalyticsDB : DB.TextAnalyticsDB
	{
        /// <summary>Konstruktor szükség esetén adatbázis fájl és táblák létrehozásával. A konstruktor az adatbázist megnyitja.</summary>
        public TextAnalyticsDB()
		{
			var appConfig = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
			string connectionString = appConfig.AppSettings.Settings["ConnectionString"].Value;

			m_dbConnection = new MySqlConnection(connectionString);
			m_dbConnection.Open();

			CheckAndSetDB();
		}


        #region Védett terület!

        /// <summary>Megmondja, hogy dolgozták-e már fel az adott fájlt.</summary>
        /// <param name="pSourceFileMD5">Az eredetileg feldolgozott fájl MD5-je.</param>
        /// <returns>Igaz, ha már dolgozták fel a fájlt.</returns>
        protected override bool Child_SourceFileExist(string pSourceFileMD5)
		{
			string sql = $"SELECT 1 FROM {c_infoTableName} WHERE {c_taiMD5} = @p1";
			MySqlCommand c = new MySqlCommand(sql, m_dbConnection);
			c.Parameters.AddWithValue("@p1", pSourceFileMD5);
			MySqlDataReader reader = c.ExecuteReader();
			try
			{
				return reader.HasRows;
			}
			finally
			{
				reader.Close();
			}
		}

        /// <summary>Forrásfájl MD5 ellenörző összege alapján visszaad egy eredményfájl elérési útját, ill, ha nincs ilyen md5, 
        /// akkor üres stringet.</summary>
        /// <param name="pSourceFileMD5">Keresett fájl, aminek az md5-je ezzel egyezik.</param>
        /// <returns>A fájl elérési útja és neve vagy üres string.</returns>
		protected override string Child_GetResultDataFile(string pSourceFileMD5)
		{
			string sql = $"SELECT {c_taiResultFile} FROM {c_infoTableName} WHERE {c_taiMD5} = @p1";
			MySqlCommand c = new MySqlCommand(sql, m_dbConnection);
			c.Parameters.AddWithValue("@p1", pSourceFileMD5);
			MySqlDataReader reader = c.ExecuteReader();
			try
			{
				if (!reader.HasRows)
					return string.Empty;
				reader.Read();
				return reader[c_taiResultFile].ToString();
			}
			finally
			{
				reader.Close();
			}
		}

        /// <summary>Egy feldolgozott fájl információinak elmentése adatbázisba.</summary>
        /// <param name="pSourceFile">Eredeti fájl.</param>
        /// <param name="pResultFile">Eredmény fájl.</param>
        /// <param name="pLoginName">Felhasznál login neve.</param>
        protected override void Child_SaveCalculationInfo(string pSourceFile, string pResultFile, string pLoginName)
		{
			string md5 = Cryptography.FileMD5Calculator(pSourceFile);
			string md5Result = Cryptography.FileMD5Calculator(pResultFile);
			if (SourceFileExist(md5))
			{
				string sql = $"update {c_infoTableName} set {c_taiUserLoginName} = @p1, {c_taiFile} = @p2, " +
					$"{c_taiResultFile} = @p3, {c_taiResultMD5} = @p4";
				MySqlCommand c = new MySqlCommand(sql, m_dbConnection);
				c.Parameters.AddWithValue("@p1", pLoginName);
				c.Parameters.AddWithValue("@p2", pSourceFile);
				c.Parameters.AddWithValue("@p3", pResultFile);
				c.Parameters.AddWithValue("@p4", md5Result);
				c.ExecuteNonQuery();
			}
			else
			{
				string sql = $"insert into {c_infoTableName} values (@p1, @p2, @p3, @p4, @p5)";
				MySqlCommand c = new MySqlCommand(sql, m_dbConnection);
				c.Parameters.AddWithValue("@p1", md5);
				c.Parameters.AddWithValue("@p2", pLoginName);
				c.Parameters.AddWithValue("@p3", pSourceFile);
				c.Parameters.AddWithValue("@p4", md5Result);
				c.Parameters.AddWithValue("@p5", pResultFile);
				c.ExecuteNonQuery();
			}
		}

        /// <summary>Felhasználó regisztrálása.</summary>
        /// <param name="pLoginName">Felhasználó login neve.</param>
        /// <param name="pUserName">Felhasználó teljes neve.</param>
        /// <param name="pPassword">Felhasználó jelszava.</param>
        protected override void Child_SignUp(string pLoginName, string pUserName, string pPassword)
		{
			if (UserExist(pLoginName))
				throw new Exception("Már létezik ilyen felhasználó azonosító!");

			string sql = $"insert into {c_userTableName} values (@p1, @p2, @p3)";
			MySqlCommand c = new MySqlCommand(sql, m_dbConnection);
			c.Parameters.AddWithValue("@p1", pLoginName);
			c.Parameters.AddWithValue("@p2", pUserName);
			c.Parameters.AddWithValue("@p3", pPassword);
			c.ExecuteNonQuery();
		}

        /// <summary>Felhasználó beléptetése.</summary>
		/// <param name="pLoginName">Felhasználó login neve.</param>
		/// <param name="pPassword">Felhasználó jelszava.</param>
		/// <returns>Ha létezik ilyen felhasználó és a jelszó is az, akkor igazat ad vissza.</returns>
        protected override bool Child_SignIn(string pLoginName, string pPassword)
        {
            string sql = $"SELECT 1 FROM {c_userTableName} WHERE {c_tauLoginName} = @p1 AND {c_tauPassword} = @p2;";
            MySqlCommand c = new MySqlCommand(sql, m_dbConnection);
            c.Parameters.AddWithValue("@p1", pLoginName);
            c.Parameters.AddWithValue("@p2", pPassword);
            MySqlDataReader reader = c.ExecuteReader();
            try
            {
                return reader.HasRows;
            }
            finally
            {
                reader.Close();
            }
        }

        /// <summary>Felhasználó ellenőrzése, hogy létezik-e az adott jelszóval.</summary>
        /// <param name="pLoginName">Felhasználó login neve.</param>
        /// <returns>Ha létezik ilyen felhasználó és a jelszó is az, akkor igazat ad vissza.</returns>
        protected override bool Child_UserExist(string pLoginName)
		{
			string sql = $"SELECT 1 FROM {c_userTableName} WHERE {c_tauLoginName} = @p1";
			MySqlCommand c = new MySqlCommand(sql, m_dbConnection);
			c.Parameters.AddWithValue("@p1", pLoginName);
			MySqlDataReader reader = c.ExecuteReader();
			try
			{
				return reader.HasRows;
			}
			finally
			{
				reader.Close();
			}
		}

        #endregion


        #region Privát terület!

        const string c_userTableName = "TextAnalyticsUsers";
		const string c_tauLoginName = "tauLoginName";
		const string c_tauName = "tauName";
		const string c_tauPassword = "tauPassword";
		const string c_infoTableName = "TextAnalyticsInfo";
		const string c_taiMD5 = "taiMD5";
		const string c_taiFile = "taiFile";
		const string c_taiUserLoginName = "taiUserLoginName";
		const string c_taiResultFile = "taiResultFile";
		const string c_taiResultMD5 = "taiResultMD5";

		MySqlConnection m_dbConnection;

		void CheckAndSetDB()
		{
			if (!CheckTableExists(c_userTableName))
				CreateUserTable();

			if (!CheckTableExists(c_infoTableName))
				CreateInfoTable();
		}

		void CreateInfoTable()
		{
			string sql = $"CREATE TABLE {c_infoTableName} ({c_taiMD5} VARCHAR(32) PRIMARY KEY, {c_taiUserLoginName} VARCHAR(20) NOT NULL, " +
				$"{c_taiFile} VARCHAR(255) NOT NULL, {c_taiResultMD5} VARCHAR(32) NOT NULL, {c_taiResultFile} VARCHAR(255) NOT NULL) ENGINE=InnoDB;";
			MySqlCommand c = new MySqlCommand(sql, m_dbConnection);
			c.ExecuteNonQuery();
		}

		void CreateUserTable()
		{
			
			string sql = $"CREATE TABLE {c_userTableName} ({c_tauLoginName} VARCHAR(20) PRIMARY KEY, {c_tauName} VARCHAR(255) NOT NULL," +
				$"{c_tauPassword} VARCHAR(512) NOT NULL) ENGINE=InnoDB;";
			MySqlCommand c = new MySqlCommand(sql, m_dbConnection);
			c.ExecuteNonQuery();
		}

		bool CheckTableExists(string pTablename)
		{
			string sql = $"SHOW TABLES LIKE '{pTablename}'";
			MySqlCommand c = new MySqlCommand(sql, m_dbConnection);
			MySqlDataReader reader = c.ExecuteReader();
			try
			{
				return reader.HasRows;
			}
			finally
			{
				reader.Close();
			}
		}

        #endregion
    }
}
