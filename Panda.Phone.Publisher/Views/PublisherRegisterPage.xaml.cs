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
using System.Device.Location;
using Panda.Phone.Publisher.PublisherServiceReference;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls.Maps;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Controls.Maps.Platform;
using Panda.Phone.Publisher.PublisherServiceReference;
using System.IO;
namespace Panda.Phone.Publisher.Views
{
    public partial class PublisherRegisterPage : PhoneApplicationPage
    {
        GeoCoordinateWatcher watcher;
        Image Pin_My;
        MapLayer layer;
        PhotoChooserTask photoChooserTask;
        PublisherServiceClient client;
        RegisterUser register = new PublisherServiceReference.RegisterUser();
        public PublisherRegisterPage()
        {
            InitializeComponent();
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            client= new PublisherServiceReference.PublisherServiceClient();
            register.Address = tb_Address.Text;
            register.City = "武汉";
            register.UserName = tb_userName.Text;
            register.Passwrod = tb_PassWord.Text;
            register.Latitude = (decimal)((GeoCoordinate)Pin_My.Tag).Latitude;
            register.Longitude = (decimal)((GeoCoordinate)Pin_My.Tag).Longitude;
            client.RegisterAsync(register);
            client.RegisterCompleted += new EventHandler<PublisherServiceReference.RegisterCompletedEventArgs>(client_RegisterCompleted);
        }

        void client_RegisterCompleted(object sender, PublisherServiceReference.RegisterCompletedEventArgs e)
        {
            MessageBox.Show(e.Result);
            client.CloseAsync();
            NavigationService.Navigate(new Uri("/Views/LoginPage.xaml", UriKind.Relative));
        }

        private void btn_Map_Click(object sender, RoutedEventArgs e)
        {
            sp_Map.Visibility = Visibility.Visible;
            sp_Content.Visibility = Visibility.Collapsed;
            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            watcher.Start();
            
            map.SetView(watcher.Position.Location, 14);
            map.Mode = new AerialMode();
            Pin_My = new Image();
            Pin_My = new Image() { Source = new BitmapImage(new Uri("/Image_Acquirer/man.png", UriKind.Relative)) };
            Pin_My.Width = 40;
            Pin_My.Height = 50;
            layer = new MapLayer();
            map.Children.Add(layer);
            layer.AddChild(Pin_My, watcher.Position.Location);
            Pin_My.Tag = watcher.Position.Location;
            map.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(map_Tap);

        }

        void map_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            layer.Children.Clear();
            layer.AddChild(Pin_My, map.ViewportPointToLocation(e.GetPosition(map)));
            Pin_My.Tag = map.ViewportPointToLocation(e.GetPosition(map));
           
        }

        private void btn_Avatar_Click(object sender, RoutedEventArgs e)
        {
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.PixelWidth = 72;
            photoChooserTask.PixelHeight = 72;
            photoChooserTask.ShowCamera = true;
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
            photoChooserTask.Show();
        }

       
        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
           
            if (e.TaskResult == TaskResult.OK)
            {
                BitmapImage bimg = new BitmapImage();
                bimg.SetSource(e.ChosenPhoto);
                image_Avatar.Source = bimg;
                register.ImageFileData = Helper.StreamToBytes(e.ChosenPhoto);
            }
        }

        private void btn_Map_Ok_Click(object sender, RoutedEventArgs e)
        {
            sp_Map.Visibility = Visibility.Collapsed;
            sp_Content.Visibility = Visibility.Visible;     
        }

       

        
    }
}