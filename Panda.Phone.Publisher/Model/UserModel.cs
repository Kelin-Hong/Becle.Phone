using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using Panda.Phone.Publisher.LocationServiceReference;
namespace Panda.Phone.Publisher.Model
{
    public class UserModel
    {
       
        public int Id
        {
            get;
            set;
          
        }
       
        public string AvatarUri
        {
            get;
            set;
        }
        
        public string UserName
        {
            get;
            set;

        }
        public string Address
        {
            get;
            set;

        }

        public int Category
        {
            set;
            get;
        }
       
        public double Latitude
        {
            get;
            set;
        }

        public double Longitude
        {
            get;
            set;
        }
        public GeocodeResult Geocoderesult
        {
            get;
            set;
        }
    }
}
