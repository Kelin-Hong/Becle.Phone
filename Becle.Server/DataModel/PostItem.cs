using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Becle.Server.DataModel
{
     [DataContract]
    public class PostItem
    {
        [DataMember]
        public string Name { set; get; }
        [DataMember]
        public int Amount { set; get; }
        [DataMember]
        public string PostAffect { set; get; }
        [DataMember]
        public int UserId { set; get; }
        [DataMember]
        public byte[] PostImage { set; get; }
        [DataMember]
        public string Describe { set; get; }
        [DataMember]
        public int CategoryId { set; get; }
        [DataMember]
        public int GetPoints { set; get; }
      
    }
}
