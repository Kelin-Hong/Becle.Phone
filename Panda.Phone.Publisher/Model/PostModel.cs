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
using System.Windows.Media.Imaging;

namespace Panda.Phone.Publisher.Model
{
    public class PostModel
    {
        public string Name { set; get; }
        public string Time { set; get; }
        public string Describe { set; get; }
        public string PostAffect { set; get; }
        public int UserId { set; get; }
        public double Co2 { set; get; }
        public double  Forest { set; get; }
        public double Electric { set; get; }
    }
}
