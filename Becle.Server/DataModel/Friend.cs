using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Becle.Server.DataModel
{
    [DataContract]
    public class Friend
    {
        [DataMember]
        public int FriendId { set; get; }
        [DataMember]
        public int UserId { set; get; }
       

    }
}
