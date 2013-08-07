
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Becle.Server.Service
{
    [ServiceContract]
    public interface IRegistrationService
    {
        #region[PushService]
        [OperationContract, WebGet]
        void RegisterUri(string uri, string id);

        [OperationContract, WebGet]
        void UnregisterUri(string uri, string id);

        [OperationContract, WebGet]
        string GetMessage(string id);
        #endregion
    }
}