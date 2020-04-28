using System.IO;
using MAF.Collection;

namespace MAF.TextAnalytics.Server
{
    /// <summary>Ha nincs adatbázis, akkor egy xml fájlba tároljuk a feldolgozások információit.</summary>
    internal class TextAnalyticsFilesInfo : ITextAnalyticsDB
    {
        /// <summary>Konstruktor, ami betölti az eddig feldolgozott fájlok információit.</summary>
        public TextAnalyticsFilesInfo()
        {
            if (File.Exists(c_dbFile))
                ta = (TextAnalyticsXML)XMLObject.XMLToObject(c_dbFile, typeof(TextAnalyticsXML));
            else
                ta = new TextAnalyticsXML();
        }

        /// <summary>Megmondja, hogy dolgozták-e már fel az adott fájlt.</summary>
        /// <param name="pSourceFileMD5">Az eredetileg feldolgozott fájl MD5-je.</param>
        /// <returns>Igaz, ha már dolgozták fel a fájlt.</returns>
        public bool SourceFileExist(string pSourceFileMD5)
        {
            return ta.ItemList.Exists(x => x.FileMD5 == pSourceFileMD5);
        }

        /// <summary>MD5 alapján visszaad egy eredményfájl elérési útját, ill, ha nincs ilyen md5, akkor üres stringet.</summary>
        /// <param name="pMD5">Keresett fájl, aminek az md5-je ezzel egyezik.</param>
        /// <returns>A fájl elérési útja és neve vagy üres string.</returns>
        public string GetResultDataFile(string pMD5)
        {
            XMLItem item = ta.ItemList.Find(x => x.FileMD5 == pMD5);
            return item != null ? item.ResultFile : string.Empty;
        }

		/// <summary>Egy feldolgozott fájl információinak elmentése adatbázisba.</summary>
		/// <param name="pFile">Eredeti fájl.</param>
		/// <param name="pResultFile">Eredmény fájl.</param>
		/// <param name="pUserID">Felhasznál azonsoító.</param>
		public void SaveCalculationInfo(string pFile, string pResultFile, string pUserID)
        {
			XMLItem item = new XMLItem()
            {
				UserID = pUserID,
				File = pFile,
                ResultFile = pResultFile,
                FileMD5 = Cryptography.FileMD5Calculator(pFile),
                ResultMD5 = Cryptography.FileMD5Calculator(pResultFile)
            };
			XMLItem xmlI = ta.ItemList.Find(x => x.FileMD5 == item.FileMD5);
			if (xmlI != null)
			{
				xmlI.UserID = item.UserID;
				xmlI.File = item.File;
				xmlI.ResultFile = item.ResultFile;
				xmlI.ResultMD5 = item.ResultMD5;
			}
			else
				ta.ItemList.Add(item);
		}

		/// <summary>Felhasználó regisztrálása.</summary>
		/// <param name="pLoginName">Felhasználó login neve.</param>
		/// <param name="pUserName">Felhasználó teljes neve.</param>
		/// <param name="pPassword">Felhasználó jelszava.</param>
		public void SignUp(string pLoginName, string pUserName, string pPassword)
		{
			UserItem ui = new UserItem() { LoginName = pLoginName, Name = pUserName, Password = pPassword };
			ta.UserList.Add(ui);
		}

		/// <summary>Felhasználó beléptetése.</summary>
		/// <param name="pLoginName">Felhasználó login neve.</param>
		/// <param name="pPassword">Felhasználó jelszava.</param>
		/// <returns>Ha létezik ilyen felhasználó és a jelszó is az, akkor igazat ad vissza.</returns>
		public bool SignIn(string pLoginName, string pPassword)
		{
			return ta.UserList.Exists(x => x.LoginName == pLoginName && x.Password == pPassword);
		}

        /// <summary>Felhasználó ellenőrzése, hogy létezik-e.</summary>
        /// <param name="pLoginName">Felhasználó login neve.</param>
        /// <returns>Ha létezik ilyen felhasználó, akkor igazat ad vissza.</returns>
        public bool UserExist(string pLoginName)
        {
            return ta.UserList.Exists(x => x.LoginName == pLoginName);
        }

        const string c_dbFile = "dbAnalytics.xml";
        readonly TextAnalyticsXML ta;

		~TextAnalyticsFilesInfo()
		{
			XMLObject.ObjectToXML(c_dbFile, ta);
		}
	}
}