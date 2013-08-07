using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Becle.Server.DataModel
{
    [DataContract]
    public class UserInfo
    {
        [DataMember]
        public string UserName { set; get; }
        [DataMember]
        public decimal Longitude { set; get; }
        [DataMember]
        public decimal Latitude { set; get; }
        [DataMember]
        public string Address { set; get; }
        [DataMember]
        public string City { set; get; }
        [DataMember]
        public int LastRank { set; get; }
    }

}
