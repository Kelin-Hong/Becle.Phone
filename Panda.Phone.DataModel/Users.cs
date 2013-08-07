using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
namespace Panda.Phone.DataModel
{
    [DataContract]
    public class Users
    {
        [DataMember]
        public string UserName { set; get; }
        [DataMember]
        public double Longitude { set; get; }
        [DataMember]
        public double Latitude { set; get; }
        [DataMember]
        public string Passwrod { set; get; }
        [DataMember]
        public string Address { set; get; }
        [DataMember]
        public string Points { set; get; }
        [DataMember]
        public Stream FileData { set; get; }
       
    }
}
