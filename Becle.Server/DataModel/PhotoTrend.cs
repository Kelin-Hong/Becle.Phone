using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Becle.Server.DataModel
{
    [DataContract]
    public class PhotoTrend
    {
        [DataMember]
        public int UserId { set; get; }
        [DataMember]
        public string Describe { set; get; }
        [DataMember]
        public string Time { set; get; }
        [DataMember]
        public int ItemId { set; get; }
        [DataMember]
        public string Name { set; get; }
    }
}
