using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Panda.Phone.DataModel
{
    [DataContract]
    public class Items
    {
        [DataMember]
         public int Id { set; get; }
        [DataMember]
        public int UserId { set; get; }

        [DataMember]
        public string Name { set; get; }
        [DataMember]
        public int CategoryId { set; get; }
        [DataMember]
        public int Num { set; get; }
    }
    [DataContract]
    public class Category
    {
        [DataMember]
        public int Id { set; get; }
        [DataMember]
        public string Name { set; get; }
    }
    [DataContract]
    public class E_Waste
    {
        [DataMember]
        public int Id { set; get; }
        [DataMember]
        public int Phone { set; get; }
        [DataMember]
        public int Iv { set; get; }
        [DataMember]
        public int computer{set;get;}
        [DataMember]
        public int Mp3mp4 { set; get; }
        [DataMember]
        public string Other{set;get;}
        [DataMember]
        public int OtherNum{set;get;}
        [DataMember]
        public int Num { set; get; }
    }
    [DataContract]
    public class Metal
    {
        [DataMember]
        public int Id { set; get; }
        [DataMember]
        public int Copper { set; get; }
        [DataMember]
        public int Icon { set; get; }
        [DataMember]
        public int Aluminum { set; get; }
        [DataMember]
        public string Other { set; get; }
        [DataMember]
        public int OtherNum { set; get; }
        [DataMember]
        public int Num { set; get; }
    }

}
