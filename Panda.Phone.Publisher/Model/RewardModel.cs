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

namespace Panda.Phone.Publisher.Model
{
    public class RewardModel
    {
        public string AvatarUri { set; get; }
        public string StoreName { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Describe { set; get; }
        public int Id { set; get; }
        public int Points { set; get; }
        public string Summary { set; get; }
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
    }
}
