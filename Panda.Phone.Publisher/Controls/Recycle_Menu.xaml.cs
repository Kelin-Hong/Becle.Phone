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
    public partial class Recycle_Menu : UserControl
    {
        RecycleVM vm;
        RecyclePage page;
        public Recycle_Menu(RecyclePage _page)
        {
           
            InitializeComponent();
            page = _page;

            this.DataContext = page.recycleVm;
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

        private void Recycle_Menu_detail_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (e.OriginalSource is Image)
            {
                page.Storyboard2.Begin();
                Image image = e.OriginalSource as Image;
                AcquirerModel acquirer = image.DataContext as AcquirerModel;
                if (image.Name == "btn_SeeInMap")
                {
                    //AcquirerModel model = image.DataContext as AcquirerModel;
                    //Image pin = new Image() { Source = new BitmapImage(new Uri("/Image_Recycle/bubble.png", UriKind.Relative)) };
                    //pin.Width = 40;
                    //pin.Height = 40;
                    //pin.Tag = model.Id;
                    //pin.DataContext = model;
                    //page.layer.AddChild(pin, new GeoCoordinate(model.Latitude, model.Longitude));
                    page.rb_Recycle.IsChecked = true;
                   
                    Recycle_Menu_1 menu = new Recycle_Menu_1();
                    menu.tbl_BinName.Text = acquirer.BinName;
                    menu.tbk_UserName.Text = acquirer.AcquirerName;
                    menu.tbk_Phone.Text = acquirer.Phone;
                    menu.tbk_Address.Text = acquirer.Address;
                    menu.Tag = acquirer.Id;
                    menu.Width = 300;
                    menu.Height = 250;
                    page.layer.AddChild(menu, new GeoCoordinate(acquirer.Latitude, acquirer.Longitude));
                    page.map.SetView(new GeoCoordinate(acquirer.Latitude, acquirer.Longitude), 16);
                    this.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Recycle_Menu_SendMessage control_SendMessage = new Recycle_Menu_SendMessage();
                    control_SendMessage.Tag = acquirer.Id;
                    control_SendMessage.tbk_Name.Text = acquirer.AcquirerName;
                    control_SendMessage.Width = 350;
                    control_SendMessage.Height = 330;
                    control_SendMessage.Visibility = Visibility.Visible;
                    page.LayoutRoot.Children.Add(control_SendMessage);
                    this.Visibility = Visibility.Collapsed;
                }
            }
        }
           
    }
}
