using System;

namespace MAF.TextAnalytics.DB
{
    public abstract class TextAnalyticsDB : Base
    {
        public const string C_GetResultDataFileError = "Hiba történt a megadott feldolgozott fájl megkeresése közben! " +
                                                       "Megadott fájl MD5 ellenörző összege: {0}! ";
        public const string C_SaveCalculationInfoError = "Hiba történt az entrópiaszámítás eredményének mentése közben! " +
                                                         "A mentés paraméterei: forrás fájl = {0}; eredmény fájl = {1}; " +
                                                         "felhasználó bejelentkezési neve = {2}.";
        public const string C_SignInError = "Hiba történt a következő felhasználó beléptetése közben: {0}!";
        public const string C_SignUpError = "Hiba történt a felhasználó regisztrálása közben! " +
                                            "A regisztráció paraméterei: bejelentkezési név: {0}; felhasználó név: {1}.";
        public const string C_SourceFileExistError = "Hiba történt a forrás fájl megkeresése közben! " +
                                                     "Megadott fájl MD5 ellenörző összege: {0}!";
        public const string C_UserExistError = "Hiba történt a következő felhasználó megkeresése közben: {0}!";

        /// <summary>Megmondja, hogy dolgozták-e már fel az adott fájlt.</summary>
        /// <param name="pSourceFileMD5">Az eredetileg feldolgozott fájl MD5-je.</param>
        /// <returns>Igaz, ha már dolgozták fel a fájlt.</returns>
        public override sealed bool SourceFileExist(string pSourceFileMD5)
        {
            try
            {
                return Child_SourceFileExist(pSourceFileMD5);
            }
            catch (Exception ex)
            {
                throw new TextAnalyticsDBException(string.Format(C_SourceFileExistError, pSourceFileMD5), ex);
            }
        }

        /// <summary>Forrásfájl MD5 ellenörző összege alapján visszaad egy eredményfájl elérési útját, ill, ha nincs ilyen md5, 
        /// akkor üres stringet.</summary>
        /// <param name="pSourceFileMD5">Keresett fájl, aminek az md5-je ezzel egyezik.</param>
        /// <returns>A fájl elérési útja és neve vagy üres string.</returns>
        public override sealed string GetResultDataFile(string pSourceFileMD5)
        {
            try
            {
                return Child_GetResultDataFile(pSourceFileMD5);
            }
            catch(Exception ex)
            {
                throw new TextAnalyticsDBException(string.Format(C_GetResultDataFileError, pSourceFileMD5), ex);
            }
        }

        /// <summary>Egy feldolgozott fájl információinak elmentése adatbázisba.</summary>
        /// <param name="pSourceFile">Eredeti fájl.</param>
        /// <param name="pResultFile">Eredmény fájl.</param>
        /// <param name="pLoginName">Felhasznál login neve.</param>
        public override sealed void SaveCalculationInfo(string pSourceFile, string pResultFile, string pLoginName)
        {
            try
            {
                Child_SaveCalculationInfo(pSourceFile, pResultFile, pLoginName);
            }
            catch(Exception ex)
            {
                throw new TextAnalyticsDBException(string.Format(C_SaveCalculationInfoError, pSourceFile, pResultFile, pLoginName), ex);
            }
        }

        /// <summary>Felhasználó regisztrálása.</summary>
        /// <param name="pLoginName">Felhasználó login neve.</param>
        /// <param name="pUserName">Felhasználó teljes neve.</param>
        /// <param name="pPassword">Felhasználó jelszava.</param>
        public override sealed void SignUp(string pLoginName, string pUserName, string pPassword)
        {
            try
            {
                Child_SignUp(pLoginName, pUserName, pPassword);
            }
            catch (Exception ex)
            {
                throw new TextAnalyticsDBException(string.Format(C_SignUpError, pLoginName, pUserName), ex);
            }
        }

        /// <summary>Felhasználó beléptetése.</summary>
		/// <param name="pLoginName">Felhasználó login neve.</param>
		/// <param name="pPassword">Felhasználó jelszava.</param>
		/// <returns>Ha létezik ilyen felhasználó és a jelszó is az, akkor igazat ad vissza.</returns>
        public override sealed bool SignIn(string pLoginName, string pPassword)
        {
            try
            {
                return Child_SignIn(pLoginName, pPassword);
            }
            catch(Exception ex)
            {
                throw new TextAnalyticsDBException(string.Format(C_SignInError, pLoginName), ex);
            }
        }

        /// <summary>Felhasználó ellenőrzése, hogy létezik-e az adott jelszóval.</summary>
        /// <param name="pLoginName">Felhasználó login neve.</param>
        /// <returns>Ha létezik ilyen felhasználó és a jelszó is az, akkor igazat ad vissza.</returns>
        public override sealed bool UserExist(string pLoginName)
        {
            try
            {
                return Child_UserExist(pLoginName);
            }
            catch (Exception ex)
            {
                throw new TextAnalyticsDBException(string.Format(C_UserExistError, pLoginName), ex);
            }
        }


        #region Védett terület!
        protected abstract bool Child_SourceFileExist(string pSourceFileMD5);

        protected abstract string Child_GetResultDataFile(string pSourceFileMD5);

        protected abstract void Child_SaveCalculationInfo(string pFile, string pResultFile, string pUserLoginName);

        protected abstract bool Child_SignIn(string pLoginName, string pPassword);

        protected abstract void Child_SignUp(string pLoginName, string pUserName, string pPassword);

        protected abstract bool Child_UserExist(string pLoginName);

        #endregion
    }

    [Serializable]
    internal class TextAnalyticsDBException : Exception
    {
        public TextAnalyticsDBException(string message) : base(message)
        {
        }

        public TextAnalyticsDBException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
