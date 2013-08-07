using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Becle.Server.DataModel
{
    
    [DataContract]
    public class Login
    {
        [DataMember]
        public string UserName { set; get; }
        [DataMember]
        public string Passwrod { set; get; }
    }
}
