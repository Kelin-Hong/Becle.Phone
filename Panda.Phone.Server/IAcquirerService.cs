using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Panda.Phone.DataModel;

namespace Panda.Phone.Server
{
    [ServiceContract]
    public interface IAcquirerService
    {
        [OperationContract]
        List<Users>  GetChoicedPoint(List<Adrress> list);
        [OperationContract]
        List<Users> getAllUser();
        [OperationContract]
        List<Items> getItemsByUserId(int Id);
        [OperationContract]
        List<Items> getItemsByCategoryId(int Id);

    }
}
