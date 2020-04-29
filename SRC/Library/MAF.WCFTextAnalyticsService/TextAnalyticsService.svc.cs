using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using MAF.Collection;
using MAF.TextAnalytics.Server;
using MAF.WCFUserService;

namespace MAF.WCFTextAnalyticsService
{
    /// <summary>Elérhető szöveg ellemző szolgáltatások megvalósítása.</summary>
    public class TextAnalyticsService : ITextAnalyticsService
    {
        /// <summary>Konstruktor. Létrehozza az eredeti feldolgozandó fájlok tárolásához a mappát, ha az még nem volt létrehozva.</summary>
        public TextAnalyticsService()
        {
            if (!Directory.Exists(c_TextFilePath))
                Directory.CreateDirectory(c_TextFilePath);
        }

		#region Felhasználóhoz kötött szolgáltatások.

		/// <summary>Felhasználó státusz lekérdezés. Igazat ad vissza, ha a felhasználó be van jelentkezve.</summary>
		/// <param name="pToken">Felhasználó token azonosítója.</param>
		/// <returns>Felhasználó státusza.</returns>
		public bool GetStatus(string pToken)
		{
			return allLoginedUsers.Exists(x => x.Token == pToken);
		}

		/// <summary>Új felhasználó beregisztrálása.</summary>
		/// <param name="pLoginName">Felhasználó login neve.</param>
		/// <param name="pUserName">Felhasználó teljes neve.</param>
		/// <param name="pPassword">Felhasználó jelszava.</param>
		public void SignUp(string pLoginName, string pUserName, string pPassword)
		{
            try
            {
                if (pLoginName == string.Empty)
                    throw new TextAnalyticsServiceException("A felhasználónevet, jelszót és azonosítót kötelező megadni!");

                string p = Cryptography.CalculateSHA512Hash(pPassword);
                r.SignUp(pLoginName, pUserName, p);
            }
            catch (Exception ex)
            {
                FaultTextAnalytics frc = new FaultTextAnalytics() { ErrorText = ex.Message };
                throw new FaultException<FaultTextAnalytics>(frc);
            }
        }

        /// <summary>Felhasználó bejelentkezése.</summary>
        /// <param name="pLoginName">Felhasználó login neve.</param>
        /// <param name="pPassword">Felhasználó jelszava.</param>
        /// <returns>Token a felhasználó azonosítására a későbbiekben.</returns>
        public string SignIn(string pLoginName, string pPassword)
		{
			try
			{
				string p = Cryptography.CalculateSHA512Hash(pPassword);

				if (!r.SignIn(pLoginName, p))
					return string.Empty;

				string token = Guid.NewGuid().ToString();
				UserLoginedInfo uli = new UserLoginedInfo(pLoginName, token);
				allLoginedUsers.Add(uli);
				return token;
			}
			catch
			{
				return string.Empty;
			}
		}

		/// <summary>Felhasználói kijelentkezés.</summary>
		/// <param name="pUserToken">Felhasználó azonosító token.</param>
		public void LoginOut(string pUserToken)
		{
			UserLoginedInfo uli = allLoginedUsers.Find(x => x.Token == pUserToken);
			if (uli == null)
				return;

			allLoginedUsers.Remove(uli);
		}

		#endregion


		#region Szöveg ellemzés szolgáltatásai.

		/// <summary>Leellenőrzi, hogy ezt a fájlt korábban feldolgozták-e már.</summary>
		/// <param name="pTextFileMD5">Fájl MD5 kódja.</param>
		/// <returns>Ha már feldolgozták, akkor igazat ad vissza, különben hamisat.</returns>
		public bool FileExist(string pTextFileMD5)
        {
            return r.SourceFileExist(pTextFileMD5);
        }

        /// <summary>Megadott md5 alapján megkeressük, hogy volt-e már feldolgozva az a fájl. Ha igen, akkor visszaadjuk
        /// a feldolgozás eredményét, ha nem, akkor <see cref="TextAnalyticsServiceException"/> hibát dob.</summary>
        /// <param name="pTextFileMD5">Eredeti feldolgozandó fájl md5-je.</param>
        /// <returns>Feldolgozás eredménye.</returns>
        public FileDownloadReturnMessage GetResultData(FileDownloadMessage pTextFileMD5)
        {
            try
            {
                string resultFile = r.GetResultFile(pTextFileMD5.MD5);
                if (resultFile == string.Empty)
                    throw new TextAnalyticsServiceException("Nincs ilyen feldolgozott fájl!");
                return StreamToFileDownloadReturnMessage(resultFile);
            }
            catch (Exception ex)
            {
                FaultTextAnalytics frc = new FaultTextAnalytics() { ErrorText = ex.Message };
                throw new FaultException<FaultTextAnalytics>(frc);
            }
        }

		/// <summary>Szöveges fájl feldolgozása.</summary>
		/// <param name="pTXTFile">Feldolgozandó szöveges fájl.</param>
		/// <param name="pUserToken">Feldolgozandó token azonosító.</param>
		/// <returns>Feldolgozás eredménye.</returns>
		public FileDownloadReturnMessage RunCalculation(FileUploadMessage pTXTFile)
        {
            try
            {
                if (!GetStatus(pTXTFile.UserToken))
                    throw new TextAnalyticsServiceException("Csak bejelentkezett felhasználó veheti igénybe a szolgáltatást!");

				//Meghatározzuk, hogy a szerveren hova mentsük el a fájlt.
				string file = Path.Combine(c_TextFilePath, pTXTFile.Filename);

                //Feltöltjük a szerverre a fájlt.
                StreamFunc.StreamToFile(pTXTFile.FileByteStream, file);
				
				//Elvégezzük a fájl feldolgozását és az eredmény fájlt visszaküldjük.
				string resultFile = r.RunCalculation(file, allLoginedUsers.Find(x => x.Token == pTXTFile.UserToken).LoginName);
                return StreamToFileDownloadReturnMessage(resultFile);
            }
            catch (Exception ex)
            {
                FaultTextAnalytics frc = new FaultTextAnalytics() { ErrorText = ex.Message };
                throw new FaultException<FaultTextAnalytics>(frc);
            }
        }

		#endregion


		static DataProvider r = new DataProvider();
		static List<UserLoginedInfo> allLoginedUsers = new List<UserLoginedInfo>();

		const string c_TextFilePath = "TextFiles";

		FileDownloadReturnMessage StreamToFileDownloadReturnMessage(string resultFile)
		{
			FileDownloadReturnMessage fdrm;
			FileStream stream = null;
			try
			{
				stream = File.Open(resultFile, FileMode.Open);
				MemoryStream memoryStream = new MemoryStream();
				stream.CopyTo(memoryStream);
				memoryStream.Position = 0;
				fdrm = new FileDownloadReturnMessage(Path.GetFileName(resultFile), memoryStream);
			}
			finally
			{
				stream.Close();
			}

			return fdrm;
		}
	}

	[Serializable]
    public class TextAnalyticsServiceException : Exception
    {
        public TextAnalyticsServiceException(string message) : base(message)
        {
        }
    }
}
