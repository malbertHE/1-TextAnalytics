using System.Collections.Generic;

namespace MAF.TextAnalytics.DB.XML
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
        public string LoginName;
        public string SourceFile;
        public string SourceFileMD5;
        public string ResultFile;
        public string ResultMD5;
    }
}