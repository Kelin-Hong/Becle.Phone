using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Panda.Phone.DataModel
{
    #region[recycle]
    [DataContract]
    public class Adrress
    {
        [DataMember]
        public double Longitude;
        [DataMember]
        public double Latitude;
    }
    [DataContract]
    public class RecycleFriends
    {
        [DataMember]
        public string Name { set; get; }
        [DataMember]
        public string Image { set; get; }
        [DataMember]
        public string LatestActivity { set; get; }
        [DataMember]
        public List<RecycleActivitys> ActivityList { set; get; }
    }
    [DataContract]
    public class RecycleActivitys
    {
        [DataMember]
        public List<Adrress> LocationList { set; get; }
        [DataMember]
        public DateTime StartTime { set; get; }
        [DataMember]
        public DateTime EndTime { set; get; }
        [DataMember]
        public string Name { set; get; }
    }
#endregion
}
