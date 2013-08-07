using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Becle.Server.DataModel
{
    [DataContract]
    public class AchievementTrend
    {
        [DataMember]
        public string Name { set; get; }
        [DataMember]
        public string Achievement { set; get; }
        [DataMember]
        public string Time { set; get; }
        [DataMember]
        public string ImageUri { set; get; }
        [DataMember]
        public int UserId { set; get; }
    }
}
