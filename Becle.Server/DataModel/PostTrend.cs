using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Becle.Server.DataModel
{
    [DataContract]
    public class PostTrend
    {
        [DataMember]
        public int UserId{ set; get; }
        [DataMember]
        public string Describe { set; get; }
        [DataMember]
        public string Time { set; get; }
        [DataMember]
        public string PostAffect { set; get; }
        [DataMember]
        public string Name { set; get; }

    }
}
