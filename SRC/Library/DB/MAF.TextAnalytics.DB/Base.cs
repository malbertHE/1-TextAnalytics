namespace MAF.TextAnalytics.DB
{
    public abstract class Base : ITextAnalyticsDB
    {
        public virtual string GetResultDataFile(string pSourceFileMD5)
        {
            return string.Empty;
        }

        public virtual void SaveCalculationInfo(string pSourceFile, string pResultFile, string pLoginName)
        {
        }

        public virtual bool SignIn(string pLoginName, string pPassword)
        {
            return false;
        }

        public virtual void SignUp(string pLoginName, string pUserName, string pPassword)
        {
        }

        public virtual bool SourceFileExist(string pSourceFileMD5)
        {
            return false;
        }

        public virtual bool UserExist(string pLoginName)
        {
            return false;
        }
    }
}
