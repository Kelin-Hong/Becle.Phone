using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Becle.Server.DataModel
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public int FromId { set; get; }
        [DataMember]
        public int ToId { set; get; }
        [DataMember]
        public bool IsSee { set; get; }
        [DataMember]
        public string Time { set; get; }
        [DataMember]
        public string MessageContent { set; get; }
    }  
}
