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
    public class RecyclePlanModel
    {
        public string Time { set; get; }
        public bool IsFinish { set; get; }
        public string UserName { set; get; }
        public bool IsCheck { set; get; }
        public string Address { set; get; }
        public int UserId { set; get; }
        public int ShipId { set; get; }
    }
}
