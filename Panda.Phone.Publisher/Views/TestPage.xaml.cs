using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Panda.Phone.Publisher.PublisherServiceReference;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Phone;
namespace Panda.Phone.Publisher.Views
{
    public partial class TestPage : PhoneApplicationPage
    {
        public TestPage()
        {
            InitializeComponent();
            PublisherServiceClient client = new PublisherServiceClient();
           // client.getImageByUserIdAsync(2);
            //client.getImageByUserIdCompleted += new EventHandler<getImageByUserIdCompletedEventArgs>(client_getImageByUserIdCompleted);
        }

        //void client_getImageByUserIdCompleted(object sender, getImageByUserIdCompletedEventArgs e)
        //{
        //    //byte[] data = Convert.FromBase64String(e.Result);    //这三行把base64转换成图片
        //    //System.IO.Stream memStream = new System.IO.MemoryStream(data, 0, data.Length);
        //    //System.Windows.Media.Imaging.WriteableBitmap bmp = Microsoft.Phone.PictureDecoder.DecodeJpeg(memStream);

        //    //System.Windows.Media.ImageBrush ib = new System.Windows.Media.ImageBrush();
            
        //    //ib.ImageSource = bmp;
        //    ////ContentPanel.Background = ib;


        //    image.Source = Helper.BytesToBitMap(e.Result);
        //    MessageBox.Show("OK");

        //}
    }
}