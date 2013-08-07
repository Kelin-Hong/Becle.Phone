using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Becle.Server.DataModel;
namespace Becle.Server.Service
{
    [ServiceContract]
    public interface IAcquirerService
    {
        [OperationContract]
        string Register(Acquirer acquirer);
        [OperationContract]
        string Login(Login login);
        [OperationContract]
        List<Resident> GetResidents(int acquirerId);
        
        
    }
}
