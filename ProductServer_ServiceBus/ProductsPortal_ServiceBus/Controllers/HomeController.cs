using Microsoft.ServiceBus;
using ProductServer_ServiceBus;
using ProductsPortal_ServiceBus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;

namespace ProductsPortal_ServiceBus.Controllers
{
    public class HomeController : Controller
    {

        static ChannelFactory<IProductChannel> channelFactory;

        static HomeController()
        {
            channelFactory = new ChannelFactory<IProductChannel>(new NetTcpRelayBinding(),
                                                                    "sb://madhunstest.servicebus.windows.net/products");

            channelFactory.Endpoint.EndpointBehaviors.Add(new TransportClientEndpointBehavior
            {
                TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider("RootManageSharedAccessKey", "JhgoHy5xXBxFbaPTBMO8qTakFzNYtfGHqfaYPSK81Tc=")
            });
        }
        public ActionResult Index(string identifier, string productName)
        {

            using (IProductChannel channel = channelFactory.CreateChannel())
            {
                return View(from prod in channel.getProducts()
                            select
                            new Products { ID = prod.ID.ToString(), Name = prod.Name, Quantity = prod.Quantity }

                                 );
            }


            //var products = new List<Products>
            //{
            //    new Products {ID=identifier,Name=productName }
            //};
            //return View(products);
        }

    }
}