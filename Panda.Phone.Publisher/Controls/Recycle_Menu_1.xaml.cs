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
using Panda.Phone.Publisher.PublisherServiceReference;
using System.Windows.Media.Imaging;
namespace Panda.Phone.Publisher.Controls
{
    public partial class Recycle_Menu_1 : UserControl
    {
        PublisherServiceClient client = new PublisherServiceClient();
        Dictionary<int, WriteableBitmap> wb_Dic = new Dictionary<int, WriteableBitmap>();
        Dictionary<int, Image> dic_image = new Dictionary<int, Image>();
        public Recycle_Menu_1()
        {
            InitializeComponent();
            client.GetImageByAcquirerIdCompleted += new EventHandler<GetImageByAcquirerIdCompletedEventArgs>(client_GetImageByAcquirerIdCompleted);
        }

        void client_GetImageByAcquirerIdCompleted(object sender, GetImageByAcquirerIdCompletedEventArgs e)
        {
            wb_Dic.Add(e.Result.AcquirerId, Helper.BytesToBitMap(e.Result.Image));
            dic_image[e.Result.AcquirerId].Source = wb_Dic[e.Result.AcquirerId];
        }

        private void btn_Close_Tap(object sender, GestureEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void img_Avatar_Loaded(object sender, RoutedEventArgs e)
        {
            int id = (int)(this.Tag);
            if (!wb_Dic.Keys.Contains(id))
            {
                dic_image.Add(id, (Image)sender);
                client.GetImageByAcquirerIdAsync(id);
            }
            else
            {
                dic_image[id].Source = wb_Dic[id];
            }
        }
    }
}
