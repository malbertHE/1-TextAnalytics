﻿namespace MAF.TextAnalytics.Server
{
    /// <summary>Adatbázis interfész. Mindegyik típusú adatbázis kiszolgáló ezt az interfészt kell megvalósítsa, hogy a 
    /// <see cref="MAF.TextAnalytics.Server"/> ki tudja szolgálni.</summary>
    public interface ITextAnalyticsDB
    {
        /// <summary>Megmondja, hogy dolgozták-e már fel az adott fájlt.</summary>
        /// <param name="pSourceFileMD5">Az eredetileg feldolgozott fájl MD5-je.</param>
        /// <returns>Igaz, ha már dolgozták fel a fájlt.</returns>
        bool FileExist(string pSourceFileMD5);

        /// <summary>MD5 alapján visszaad egy eredményfájl elérési útját, ill, ha nincs ilyen md5, akkor üres stringet.</summary>
        /// <param name="pSourceFileMD5">Keresett fájl, aminek az md5-je ezzel egyezik.</param>
        /// <returns>A fájl elérési útja és neve vagy üres string.</returns>
        string GetResultDataFile(string pSourceFileMD5);

		/// <summary>Egy feldolgozott fájl információinak elmentése adatbázisba.</summary>
		/// <param name="pFile">Eredeti fájl.</param>
		/// <param name="pResultFile">Eredmény fájl.</param>
		/// <param name="pUserLoginName">Felhasznál login neve.</param>
		void SaveCalculationInfo(string pFile, string pResultFile, string pUserLoginName);

		/// <summary>Felhasználó regisztrálása.</summary>
		/// <param name="pLoginName">Felhasználó login neve.</param>
		/// <param name="pUserName">Felhasználó teljes neve.</param>
		/// <param name="pPassword">Felhasználó jelszava.</param>
		void SignUp(string pLoginName, string pUserName, string pPassword);

		/// <summary>Felhasználó ellenőrzése, hogy létezik-e az adott jelszóval.</summary>
		/// <param name="pLoginName">Felhasználó login neve.</param>
		/// <param name="pPassword">Felhasználó jelszava.</param>
		/// <returns>Ha létezik ilyen felhasználó és a jelszó is az, akkor igazat ad vissza.</returns>
		bool UserExist(string pLoginName, string pPassword);
	}
}