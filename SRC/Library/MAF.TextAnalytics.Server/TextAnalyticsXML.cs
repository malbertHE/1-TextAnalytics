using System.Collections.Generic;

namespace MAF.TextAnalytics.Server
{
    public class TextAnalyticsXML
    {
        public List<XMLItem> ItemList = new List<XMLItem>();
		public List<UserItem> UserList = new List<UserItem>();
    }

	public class UserItem
	{
		public string LoginName;
		public string Name;
		public string Password;
	}

	public class XMLItem
    {
		public string UserID;
        public string File;
        public string FileMD5;
        public string ResultFile;
        public string ResultMD5;
    }
}