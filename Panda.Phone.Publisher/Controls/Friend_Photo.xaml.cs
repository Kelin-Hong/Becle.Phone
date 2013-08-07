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
using System.Windows.Media.Imaging;
using Panda.Phone.Publisher.PublisherServiceReference;
using Microsoft.Phone.Controls;
namespace Panda.Phone.Publisher.Controls
{
    public partial class Friend_Photo : UserControl
    {
        PublisherServiceClient client=new PublisherServiceClient();
        
        public Friend_Photo(PublisherServiceClient _client)
        {
            InitializeComponent();
            hubTile= new HubTile();
            this.grid_Content.Children.Add(hubTile);
            hubTile.DisplayNotification = true;
            
           // hubTile.Background = new SolidColorBrush(new Color() {A=255, });
           // client = _client;
          //  client.GetImageByItemIdCompleted+=new EventHandler<GetImageByItemIdCompletedEventArgs>(client_GetImageByItemIdCompleted);
        }
        public string Name
        {
            set { this.hubTile.Title = value; }
        }
        //public string Time
        //{
        //    set { };
        //}
        public string Describe
        {
            //set { this.describe.Text = value; }
            set { this.hubTile.Notification = value; }
        }
        public int ItemId
        {
            set {
                client.GetImageByItemIdAsync(value);
                client.GetImageByItemIdCompleted += new EventHandler<GetImageByItemIdCompletedEventArgs>(client_GetImageByItemIdCompleted);
               }
        }

        void client_GetImageByItemIdCompleted(object sender, GetImageByItemIdCompletedEventArgs e)
        {
           // this.image_Waste.Source = Helper.BytesToBitMap(e.Result.Image);
            this.hubTile.Source = Helper.BytesToBitMap(e.Result.Image);
        }

        
    }
}
