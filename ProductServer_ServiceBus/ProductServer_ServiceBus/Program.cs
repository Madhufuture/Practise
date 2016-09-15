using Microsoft.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ProductServer_ServiceBus
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine(ServiceBusEnvironment.DefaultIdentityHostName);
            //Console.WriteLine(ServiceBusEnvironment.CreateAccessControlUri("madhunstest"));
            //Console.WriteLine(ServiceBusEnvironment.SystemConnectivity.Mode);

            //Console.ReadLine();

            //var sh = new ServiceHost(typeof(ProductService));      

            //sh.Open();

            //Console.WriteLine("Press Enter key to close");
            //Console.ReadLine();

            //sh.Close();


            ServiceHost sh = new ServiceHost(typeof(ProductService));

            //sh.AddServiceEndpoint(
            //   typeof(IProducts), new NetTcpBinding(),
            //   "net.tcp://localhost:9358/products");

            sh.AddServiceEndpoint(
               typeof(IProducts), new NetTcpRelayBinding(),
               ServiceBusEnvironment.CreateServiceUri("sb", "madhunstest", "products"))
                .Behaviors.Add(new TransportClientEndpointBehavior
                {
                    TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider("RootManageSharedAccessKey", "JhgoHy5xXBxFbaPTBMO8qTakFzNYtfGHqfaYPSK81Tc=")
                });

            sh.Open();

            Console.WriteLine("Press ENTER to close");
            Console.ReadLine();

            sh.Close();


        }
    }

    class ProductService : IProducts
    {

        ProductData[] products = new[]
        {
            new ProductData {ID=1,Name="Rock",Quantity="1" },
            new ProductData {ID=2,Name="Product 1",Quantity="2" },
            new ProductData {ID=3,Name="Product 2",Quantity="3" }
        };

        public IList<ProductData> getProducts()
        {
            Console.WriteLine("Get Product called");
            return products;
        }
    }

}
