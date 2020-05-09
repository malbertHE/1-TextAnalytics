namespace MAF.WCFUserService
{
	internal class UserLoginedInfo
	{
		public string LoginName { get; private set; }

		public string Token { get; private set; }

		public UserLoginedInfo(string pLoginName, string pToken)
		{
			LoginName = pLoginName;
			Token = pToken;
		}
	}
}