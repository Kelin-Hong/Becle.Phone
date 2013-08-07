using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Becle.Server.DataModel
{

    [DataContract]
    public class Reward
    {
        [DataMember]
        public string StoreName { set; get; }
        [DataMember]
        public string Describe { set; get; }
        [DataMember]
        public decimal Longitude { set; get; }
        [DataMember]
        public decimal Latitude { set; get; }
        [DataMember]
        public string City { set; get; }
        [DataMember]
        public byte[] AvatarUri { set; get; }
        [DataMember]
        public string Address { set; get; }
        [DataMember]
        public string Phone { set; get; }
    }  
}
