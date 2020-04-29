using System.Runtime.Serialization;
using System.ServiceModel;

namespace MAF.WCFUserService
{

    /// <summary>Szerver által nyújtott felhasználói szolgáltatások.</summary>
    [ServiceContract]
    public interface IUserService
    {
        /// <summary>Bejelentkezés szoltáltatása.</summary>
        /// <param name="pLoginName">Felhasználó neve.</param>
        /// <param name="pPassword">Felhasználó jelszava.</param>
        /// <returns>Felhasználó azonosító.</returns>
        [OperationContract]
        string LogIn(string pLoginName, string pPassword);

        /// <summary>Felhasználói kijelentkezés.</summary>
        [OperationContract]
        string LoginOut();

        /// <summary>Felhasználói regisztráció.</summary>
        [OperationContract]
        void Registration(string pLoginName, string pUserName, string pPassword);

        /// <summary>Felhasználó státusza.</summary>
        /// <returns>Ha sikeresen bejelentkezett, akkor igaz, különben hamis.</returns>
        [OperationContract]
        bool GetStatus(); 
    }

    /// <summary></summary>
    [DataContract]
    public class User
    {

        /// <summary>Felhasználó egyedi azonosító kódja.</summary>
        [DataMember]
        public int UserID { get; private set; }

        /// <summary>Felhasználó neve.</summary>
        [DataMember]
        public string UserName { get; private set; }


        public User(int pUserID, string pUserName)
        {
            UserID = pUserID;
            UserName = pUserName;
        }

    }
}