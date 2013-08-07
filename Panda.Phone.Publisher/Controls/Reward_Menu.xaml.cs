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
using Panda.Phone.Publisher.ViewModel;
using Microsoft.Phone.Controls;
using Panda.Phone.Publisher.Model;
using System.Windows.Media.Imaging;
using System.Device.Location;
using Panda.Phone.Publisher.Views;
namespace Panda.Phone.Publisher.Controls
{
    public partial class Reward_Menu : UserControl
    {
        RewardVM vm;
        RecyclePage page;
        public Reward_Menu(RecyclePage _page)
        {
            InitializeComponent();
            page = _page;
           // vm = new RewardVM(1);
            this.DataContext = page.rewardVm;
        }
        private void StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StackPanel sp = ((StackPanel)sender).Parent as StackPanel;
            if (sp.Children[1].Visibility == Visibility.Collapsed)
            {
                sp.Children[1].Visibility = Visibility.Visible;
            }
            else
            {
                sp.Children[1].Visibility = Visibility.Collapsed;
            }
        }

        private void btn_Close_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Reward_Menu_Detail_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (e.OriginalSource is Image)
            {
                page.Storyboard2.Begin();
                page.rb_Reward.IsChecked = true;
                Image image = e.OriginalSource as Image;
                //RewardModel model = image.DataContext as RewardModel;
                //Image pin = new Image() { Source = new BitmapImage(new Uri("/Image_Recycle/bubble.png", UriKind.Relative)) };
                //pin.Width = 40;
                //pin.Height = 40;
                //pin.Tag = model.Id;
                //pin.DataContext = model;
                //page.layer.AddChild(pin, new GeoCoordinate(model.Latitude, model.Longitude));
                RewardModel reward = image.DataContext as RewardModel;
                    Reward_Menu_1 menu = new Reward_Menu_1();
                    menu.tbk_UserName.Text = reward.StoreName;
                    menu.tbk_Phone.Text = reward.Phone;
                    menu.tbk_Address.Text = reward.Address;
                    menu.tbk_Describe.Text = reward.Describe;
                    menu.Width = 350;
                    menu.Height = 250;
                    page.layer.AddChild(menu, new GeoCoordinate(reward.Latitude, reward.Longitude));
                    page.map.SetView(new GeoCoordinate(reward.Latitude, reward.Longitude), 16);
               
            }
        }

       

       

       

       
    }
}
