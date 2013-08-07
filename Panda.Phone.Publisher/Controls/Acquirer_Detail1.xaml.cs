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
using Panda.Phone.Publisher.Model;
using Panda.Phone.Publisher.PublisherServiceReference;
using System.Windows.Media.Imaging;
namespace Panda.Phone.Publisher.Controls
{
    public partial class Acquirer_Detail1 : UserControl
    {
        PublisherServiceClient client = new PublisherServiceClient();
        Dictionary<int, Image> dic_image = new Dictionary<int, Image>();
        Dictionary<int, WriteableBitmap> wb_Dic = new Dictionary<int, WriteableBitmap>();
        public Acquirer_Detail1()
        {
            InitializeComponent();
            client.GetImageByItemIdCompleted += new EventHandler<GetImageByItemIdCompletedEventArgs>(client_GetImageByItemIdCompleted);
        }

        void client_GetImageByItemIdCompleted(object sender, GetImageByItemIdCompletedEventArgs e)
        {
            wb_Dic.Add(e.Result.ItemId,Helper.BytesToBitMap(e.Result.Image));
            dic_image[e.Result.ItemId].Source =Helper.BytesToBitMap(e.Result.Image);
        }

        private void image_detail_close_Tap(object sender, GestureEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            Image image = (Image)sender;
            ItemModel item = (ItemModel)image.DataContext;
            if (!dic_image.Keys.Contains(item.Id))
            {
                dic_image.Add(item.Id, image);
                client.GetImageByItemIdAsync(item.Id);
            }
            else
            {
                image.Source = wb_Dic[item.Id];
            }
        }
    }
}
