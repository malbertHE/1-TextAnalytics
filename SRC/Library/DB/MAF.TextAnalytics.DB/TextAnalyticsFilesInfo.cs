using System.IO;
using MAF.Collection;

namespace MAF.TextAnalytics.DB.XML
{
    /// <summary>Ha nincs adatbázis, akkor egy xml fájlba tároljuk a feldolgozások információit.</summary>
    public class TextAnalyticsFilesInfo : TextAnalyticsDB
    {
        /// <summary>Konstruktor, ami betölti az eddig feldolgozott fájlok információit.</summary>
        public TextAnalyticsFilesInfo()
        {
            if (File.Exists(c_dbFile))
                ta = (TextAnalyticsXML)XMLObject.XMLToObject(c_dbFile, typeof(TextAnalyticsXML));
            else
                ta = new TextAnalyticsXML();
        }


        #region Védett terület!

        /// <summary>Megmondja, hogy dolgozták-e már fel az adott fájlt.</summary>
        /// <param name="pSourceFileMD5">Az eredetileg feldolgozott fájl MD5-je.</param>
        /// <returns>Igaz, ha már dolgozták fel a fájlt.</returns>
        protected override bool Child_SourceFileExist(string pSourceFileMD5)
        {
            return ta.ItemList.Exists(x => x.SourceFileMD5 == pSourceFileMD5);
        }

        /// <summary>Forrásfájl MD5 ellenörző összege alapján visszaad egy eredményfájl elérési útját, ill, ha nincs ilyen md5, 
        /// akkor üres stringet.</summary>
        /// <param name="pSourceFileMD5">Keresett fájl, aminek az md5-je ezzel egyezik.</param>
        /// <returns>A fájl elérési útja és neve vagy üres string.</returns>
        protected override string Child_GetResultDataFile(string pSourceFileMD5)
        {
            XMLItem item = ta.ItemList.Find(x => x.SourceFileMD5 == pSourceFileMD5);
            return item != null ? item.ResultFile : string.Empty;
        }

        /// <summary>Egy feldolgozott fájl információinak elmentése adatbázisba.</summary>
        /// <param name="pSourceFile">Eredeti fájl.</param>
        /// <param name="pResultFile">Eredmény fájl.</param>
        /// <param name="pLoginName">Felhasznál login neve.</param>
        protected override void Child_SaveCalculationInfo(string pSourceFile, string pResultFile, string pLoginName)
        {
            XMLItem item = new XMLItem()
            {
                LoginName = pLoginName,
                SourceFile = pSourceFile,
                ResultFile = pResultFile,
                SourceFileMD5 = Cryptography.FileMD5Calculator(pSourceFile),
                ResultMD5 = Cryptography.FileMD5Calculator(pResultFile)
            };
            XMLItem xmlI = ta.ItemList.Find(x => x.SourceFileMD5 == item.SourceFileMD5);
            if (xmlI != null)
            {
                xmlI.LoginName = item.LoginName;
                xmlI.SourceFile = item.SourceFile;
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
        protected override void Child_SignUp(string pLoginName, string pUserName, string pPassword)
        {
            UserItem ui = new UserItem() { LoginName = pLoginName, Name = pUserName, Password = pPassword };
            ta.UserList.Add(ui);
        }

        /// <summary>Felhasználó beléptetése.</summary>
        /// <param name="pLoginName">Felhasználó login neve.</param>
        /// <param name="pPassword">Felhasználó jelszava.</param>
        /// <returns>Ha létezik ilyen felhasználó és a jelszó is az, akkor igazat ad vissza.</returns>
        protected override bool Child_SignIn(string pLoginName, string pPassword)
        {
            return ta.UserList.Exists(x => x.LoginName == pLoginName && x.Password == pPassword);
        }

        /// <summary>Felhasználó ellenőrzése, hogy létezik-e az adott jelszóval.</summary>
        /// <param name="pLoginName">Felhasználó login neve.</param>
        /// <returns>Ha létezik ilyen felhasználó és a jelszó is az, akkor igazat ad vissza.</returns>
        protected override bool Child_UserExist(string pLoginName)
        {
            return ta.UserList.Exists(x => x.LoginName == pLoginName);
        }

        #endregion


        #region Privát terület!

        const string c_dbFile = "dbAnalytics.xml";
        readonly TextAnalyticsXML ta;

        ~TextAnalyticsFilesInfo()
        {
            XMLObject.ObjectToXML(c_dbFile, ta);
        }

        #endregion
    }
}