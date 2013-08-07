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
using System.Windows.Media.Imaging;
using Panda.Phone.Publisher.ViewModel;
using Panda.Phone.Publisher.Model;
namespace Panda.Phone.Publisher.Views
{
    public partial class MessagePage : PhoneApplicationPage
    {
        MessageVM vm;
        public MessagePage()
        {
            InitializeComponent();  
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            this.DataContext=vm = new MessageVM();
            base.OnNavigatedTo(e);
        }
        private void StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StackPanel sp1 = sender as StackPanel;
            StackPanel sp = (sender as StackPanel).Parent as StackPanel;
            StackPanel tbk_message = sp.Children[1] as StackPanel;
            MessageModel model=tbk_message.DataContext as MessageModel;
            if (tbk_message.Visibility == Visibility.Collapsed)
            {
                tbk_message.Visibility = Visibility.Visible;
                Image image = (Image)sp1.Children[0];
                image.Source = new BitmapImage(new Uri("/Image_Recycle/Message/m_open.png", UriKind.Relative));
                vm.db.Messages.First(c => c.Id == model.Id).IsSee = true;
                vm.db.SubmitChanges();
            }
            else
            {
                tbk_message.Visibility = Visibility.Collapsed;
            }
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_NO_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StackPanel_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StackPanel sp1 = sender as StackPanel;
            StackPanel sp = (sender as StackPanel).Parent as StackPanel;
            StackPanel tbk_message = sp.Children[1] as StackPanel;
            ConfirmMessageModel model = tbk_message.DataContext as ConfirmMessageModel;
            if (tbk_message.Visibility == Visibility.Collapsed)
            {
                tbk_message.Visibility = Visibility.Visible;
                Image image = (Image)sp1.Children[0];
                image.Source = new BitmapImage(new Uri("/Image_Recycle/Message/m_open.png", UriKind.Relative));
                vm.db.ConfirmMessages.First(c => c.Id == model.Id).IsSee = true;
                vm.db.SubmitChanges();
            }
            else
            {
                tbk_message.Visibility = Visibility.Collapsed;
            }
        }

        
    }
}