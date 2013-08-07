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
    public class AchievementModel
    {
     
        public int  UserId { set; get; }
        public string Name { set; get; }
        public string Describe { set; get; }
        public string ImageUri { set; get; }
        public string Time { set; get; }
        public string  Category {set;get;}

    }
}
