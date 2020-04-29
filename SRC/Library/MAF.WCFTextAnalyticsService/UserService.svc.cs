using System;

namespace MAF.WCFUserService
{
    public class UserService : IUserService
    {
        public bool GetStatus()
        {
            return true;
        }

        public void Registration(string pLoginName, string pUserName, string pPassword)
        {
            throw new NotImplementedException();
        }

        public string LogIn(string pLoginName, string pPassword)
        {
            throw new NotImplementedException();
        }

        public string LoginOut()
        {
            throw new NotImplementedException();
        }
    }
}
