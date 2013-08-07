using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Panda.Phone.DataModel;
namespace Panda.Phone.Server
{
    [ServiceContract]
    public interface IPushNotificationService
    {
        [OperationContract]
        void Register(string uri);
       
    }
   
    [ServiceContract]
    public interface IPhoneService:IPushNotificationService
    {
        [OperationContract]
        List<RecycleActivitys> getRecycleActivityList(string uri);
        [OperationContract]
        List<RecycleFriends> getRecycleFriendList();
        [OperationContract]
        List<Adrress> getAddressByName(string name);
    }
}
