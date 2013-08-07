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
    public class ItemModel
    {
        
        private string avatarUri;
        public WriteableBitmap AvatarUri
        {
            get;
            set;
        }
        private string name;
       
        public string Name
        {
            get;
            set;
        }

        private string userName;
        public string UserName
        {
            get;
            set;

        }
        private double latitude;

        public double Latitude
        {
            get;
            set;


        }
        private double longitude;

        public double Longitude
        {
            get;
            set;


        }

        public string SubmitTime { set; get; }
        private int categoryid;
        
        public int CategoryId
        {
            set;
            get;
        }
        private int id;

        public int Id
        {
            set;
            get;
        }
        public int UserId
        {
            set;
            get;
        }
        private int num;

        public int Num
        {
            get;
            set;
        }
    }
}
