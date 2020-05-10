namespace MAF.TextAnalytics.DB
{
    /// <summary>Adatbázis interfész. Mindegyik típusú adatbázis kiszolgáló ezt az interfészt kell megvalósítsa, hogy a 
    /// <see cref="MAF.TextAnalytics.Server"/> ki tudja szolgálni.</summary>
    public interface ITextAnalyticsDB
    {
        /// <summary>Megmondja, hogy dolgozták-e már fel az adott fájlt.</summary>
        /// <param name="pSourceFileMD5">Az eredetileg feldolgozott fájl MD5-je.</param>
        /// <returns>Igaz, ha már dolgozták fel a fájlt.</returns>
        bool SourceFileExist(string pSourceFileMD5);

        /// <summary>Forrásfájl MD5 ellenörző összege alapján visszaad egy eredményfájl elérési útját, ill, ha nincs ilyen md5, 
        /// akkor üres stringet.</summary>
        /// <param name="pSourceFileMD5">Keresett fájl, aminek az md5-je ezzel egyezik.</param>
        /// <returns>A fájl elérési útja és neve vagy üres string.</returns>
        string GetResultDataFile(string pSourceFileMD5);

		/// <summary>Egy feldolgozott fájl információinak elmentése adatbázisba.</summary>
		/// <param name="pSourceFile">Eredeti fájl.</param>
		/// <param name="pResultFile">Eredmény fájl.</param>
		/// <param name="pLoginName">Felhasznál login neve.</param>
		void SaveCalculationInfo(string pSourceFile, string pResultFile, string pLoginName);

		/// <summary>Felhasználó regisztrálása.</summary>
		/// <param name="pLoginName">Felhasználó login neve.</param>
		/// <param name="pUserName">Felhasználó teljes neve.</param>
		/// <param name="pPassword">Felhasználó jelszava.</param>
		void SignUp(string pLoginName, string pUserName, string pPassword);

        /// <summary>Felhasználó beléptetése.</summary>
		/// <param name="pLoginName">Felhasználó login neve.</param>
		/// <param name="pPassword">Felhasználó jelszava.</param>
		/// <returns>Ha létezik ilyen felhasználó és a jelszó is az, akkor igazat ad vissza.</returns>
		bool SignIn(string pLoginName, string pPassword);

        /// <summary>Felhasználó ellenőrzése, hogy létezik-e az adott jelszóval.</summary>
        /// <param name="pLoginName">Felhasználó login neve.</param>
        /// <returns>Ha létezik ilyen felhasználó és a jelszó is az, akkor igazat ad vissza.</returns>
        bool UserExist(string pLoginName);
    }
}