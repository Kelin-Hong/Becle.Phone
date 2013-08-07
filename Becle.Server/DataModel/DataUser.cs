using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Becle.Server.DataModel
{
    [DataContract]
    public class DataUser
    {
        [DataMember]
        public string UserName { set; get; }
        [DataMember]
        public int Points { set; get; }
        [DataMember]
        public string City { set; get; }
    }
}
