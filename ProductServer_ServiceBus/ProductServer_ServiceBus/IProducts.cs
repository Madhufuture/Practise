using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ProductServer_ServiceBus
{
    [ServiceContract]
    interface IProducts
    {
        [OperationContract]
        IList<ProductData> getProducts();
    }

    interface IProductChannel:IProducts,IClientChannel
    {

    }

    [DataContract]
    class ProductData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Quantity { get; set; }
    }
}
