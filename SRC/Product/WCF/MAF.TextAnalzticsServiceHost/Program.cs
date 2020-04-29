using System;
using System.ServiceModel;
using MAF.WCFTextAnalyticsService;

namespace MAF.TextAnalzticsServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = null;
            try
            {
                host = new ServiceHost(typeof(TextAnalyticsService));
                host.Open();
                Console.WriteLine($"Host elindítva {DateTime.Now.ToString()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A szerver indítása a következő hiba miatt nem lehetséges: {ex.Message}");
                host = null;
            }
            Console.WriteLine("Kilépés enterrel");
            Console.ReadLine();
            if (host != null && host.State == CommunicationState.Opened)
                host.Close();
        }
    }
}
