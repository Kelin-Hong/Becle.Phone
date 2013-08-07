using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
namespace Becle.Server.DataModel
{
    [DataContract]
    public class Acquirer
    {
        [DataMember]
        public string UserName { set; get; }
        [DataMember]
        public string BinName { set; get; }
        [DataMember]
        public decimal  Longitude { set; get; }
        [DataMember]
        public decimal  Latitude { set; get; }
        [DataMember]
        public string City { set; get; }
        [DataMember]
        public int AcquirerId { set; get; }
        [DataMember]
        public string Address { set; get; }
        [DataMember]
        public string Phone { set; get; }
    }  
}
