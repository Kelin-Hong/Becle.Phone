using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Becle.Server.DataModel
{
    [DataContract]
    public class AcquirerImage
    {
        [DataMember]
        public int AcquirerId { set; get; }
        [DataMember]
        public byte[] Image { set; get; }
    }
}
