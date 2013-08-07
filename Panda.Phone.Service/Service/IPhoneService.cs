using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
namespace Panda.Phone.Service.Service
{   
    public class MyData
    {
    }
    public class Customer
    {
    }
    [ServiceContract]
    public interface IPhoneService
    {
        [OperationContract,WebGet]
        void getRecycleActivityList(string _uri);
        [OperationContract]
        [WebInvoke(Method = "PUT")]
        void PutData(MyData data);
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/customer/{id}")]
        void PutCustomers(string id, Customer customer);
       

          
    
       
    }
}
