using Microsoft.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CacheService.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.AutoDetect;
            var cacheService = new ServiceHost(typeof(CacheService));

            cacheService.Open();
            Console.WriteLine("Press ENTER to close");
            Console.ReadLine();
            cacheService.Close();
        }
    }
}
