using System;
using System.ServiceModel;
using MAF.WCFTextAnalyticsService;

namespace MAF.TextAnalzticsServiceHost
{
    class Program
    {
        static ServiceHost host;
        const string c_ExitText = "Kilépés enterrel";
        const string c_StartHostText = "Host elindítva {0}";
        const string c_ServerStartError = "A szerver indítása a következő hiba miatt nem lehetséges {0}";

        static void Main()
        {
            StartHost();
            WaitToEnd();
            EndHost();
        }

        private static void StartHost()
        {
            try
            {
                host = new ServiceHost(typeof(TextAnalyticsService));
                host.Open();
                Console.WriteLine(c_StartHostText, DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(c_ServerStartError,  ex.Message);
                host = null;
            }
        }

        private static void WaitToEnd()
        {
            Console.WriteLine(c_ExitText);
            Console.ReadLine();
        }

        private static void EndHost()
        {
            if (host != null && host.State == CommunicationState.Opened)
                host.Close();
        }

    }
}
