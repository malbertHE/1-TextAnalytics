using System.IO;
using System.ServiceModel;

namespace MAF.WCFTextAnalyticsService
{

    /// <summary>Szerver által nyújtött szöveg ellemző szogáltatások.</summary>
    [ServiceContract]
    public interface ITextAnalyticsService
    {
		#region Felhasználóhoz kötött szolgáltatások.

		/// <summary>Bejelentkezés szoltáltatása.</summary>
		/// <param name="pLoginName">Felhasználó neve.</param>
		/// <param name="pPassword">Felhasználó jelszava.</param>
		/// <returns>Felhasználó azonosító.</returns>
		[OperationContract]
		string SignIn(string pLoginName, string pPassword);

		/// <summary>Felhasználói kijelentkezés.</summary>
		/// <param name="pUserToken">Felhasználó azonosító token.</param>
		[OperationContract]
		void LoginOut(string pUserToken);

		/// <summary>Felhasználói regisztráció.</summary>
		[OperationContract]
        [FaultContract(typeof(FaultTextAnalytics))]
        void SignUp(string pLoginName, string pUserName, string pPassword);

		/// <summary>Felhasználó státusz lekérdezés. Igazat ad vissza, ha a felhasználó be van jelentkezve.</summary>
		/// <param name="pToken">Felhasználó token azonosítója.</param>
		/// <returns>Felhasználó státusza.</returns>
		[OperationContract]
		bool GetStatus(string pToken);

		#endregion


		#region Szöveg ellemzés szolgáltatásai.

		/// <summary>Leellenőrzi, hogy ezt a fájlt korábban feldolgozták-e már.</summary>
		/// <param name="pTextFileMD5">Fájl MD5 kódja.</param>
		/// <returns>Ha már feldolgozták, akkor igazat ad vissza, különben hamisat.</returns>
		[OperationContract]
        bool FileExist(string pTextFileMD5);

        /// <summary>Megadott md5 alapján megkeressük, hogy volt-e már feldolgozva az a fájl. Ha igen, akkor visszaadjuk
        /// a feldolgozás eredményét, ha nem, akkor <see cref="TextAnalyticsServiceException"/> hibát dob.</summary>
        /// <param name="pTextFileMD5">Eredeti feldolgozandó fájl md5-je.</param>
        /// <returns>Feldolgozás eredménye.</returns>
        [OperationContract]
		[FaultContract(typeof(FaultTextAnalytics))]
		FileDownloadReturnMessage GetResultData(FileDownloadMessage pTextFileMD5);

		/// <summary>Szöveges fájl feldolgozása.</summary>
		/// <param name="pTXTFile">Feldolgozandó szöveges fájl.</param>
		/// <param name="pUserToken">Feldolgozandó token azonosító.</param>
		/// <returns>Feldolgozás eredménye.</returns>
		[OperationContract]
		[FaultContract(typeof(FaultTextAnalytics))]
		FileDownloadReturnMessage RunCalculation(FileUploadMessage pTXTFile);

		#endregion
	}

	[MessageContract]
    public class FaultTextAnalytics
    {
        [MessageHeader(MustUnderstand = true)]
        public string ErrorText = string.Empty;
    }

    [MessageContract]
    public class FileDownloadReturnMessage
    {
        public FileDownloadReturnMessage(string filename, Stream stream)
        {
            DownloadedFilename = filename;
            FileByteStream = stream;
        }

        [MessageHeader(MustUnderstand = true)]
        public string DownloadedFilename = string.Empty;

        [MessageBodyMember(Order = 1)]
        public Stream FileByteStream = null;
    }

    [MessageContract]
    public class FileUploadMessage
    {
		[MessageHeader(MustUnderstand = true)]
		public string UserToken;

		[MessageHeader(MustUnderstand = true)]
        public string Filename;

        [MessageBodyMember(Order = 1)]
        public Stream FileByteStream;
    }

    [MessageContract]
    public class FileDownloadMessage
    {
        [MessageHeader(MustUnderstand = true)]
        public string MD5;
    }

}
