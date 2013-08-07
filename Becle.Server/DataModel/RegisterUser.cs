using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
namespace Becle.Server.DataModel
{
    [DataContract]
    public class RegisterUser
    {
        [DataMember]
        public string UserName { set; get; }
        [DataMember]
        public decimal Longitude { set; get; }
        [DataMember]
        public decimal Latitude { set; get; }
        [DataMember]
        public string Passwrod { set; get; }
        [DataMember]
        public string Address { set; get; }
        [DataMember]
        public byte[] ImageFileData { set; get; }
        [DataMember]
        public string City { set; get; }
        [DataMember]
        public string StoreBinName { set; get; }
        [DataMember]
        public string Phone { set; get; }
        [DataMember]
        public int UserType { set; get; }
    }
}
