using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAF.TextAnalytics.Server.Test
{
    [TestClass]
    public class DataProviderTest : TextAnalyticsDBTest
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            db = new DataProvider(DataProvider.DataBaseType.NA);
        }
    }
}
