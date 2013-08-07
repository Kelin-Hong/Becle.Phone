using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Becle.Server.DataModel
{
    [DataContract]
   public  class UserImage
    {
        [DataMember]
        public int UserId { set; get; }
        [DataMember]
        public byte[] Avatar { set; get; }
    }
}
