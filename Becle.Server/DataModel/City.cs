using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Becle.Server.DataModel
{
    [DataContract]
    public class City
    {
        [DataMember]
        public string Name { set; get; }
        [DataMember]
        public int Points { set; get; }
        [DataMember]
        public int LastRank { set; get; }
        

    }
}
