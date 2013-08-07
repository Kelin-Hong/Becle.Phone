using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Becle.Server.DataModel
{
    [DataContract]
    public class LoginBack
    {
        [DataMember]
        public string Message { set; get; }
        [DataMember]
        public int  UserType { set; get; }
        [DataMember]
        public int UserId { set; get; }
    }
}
