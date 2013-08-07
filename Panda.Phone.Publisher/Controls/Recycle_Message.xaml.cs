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
using Panda.Phone.Publisher.Views;
using System.Windows.Media.Imaging;
using Panda.Phone.Publisher.Model;
namespace Panda.Phone.Publisher.Controls
{
    public partial class Recycle_Message : UserControl
    {
        private RecyclePage page;
        public Recycle_Message(RecyclePage _page)
        {
            InitializeComponent();
            page = _page;
            this.DataContext = page.recycleVm;
        }

        private void StackPanel_Tap(object sender, GestureEventArgs e)
        {
            StackPanel sp1 = sender as StackPanel;
            StackPanel sp = (sender as StackPanel).Parent as StackPanel;
            StackPanel tbk_message = sp.Children[1] as StackPanel;
            if (tbk_message.Visibility == Visibility.Collapsed)
            {
                tbk_message.Visibility = Visibility.Visible;
                Image image = (Image)sp1.Children[0];
                image.Source = new BitmapImage(new Uri("/Image_Recycle/Message/m_open.png", UriKind.Relative));
            }
            else
            {
                tbk_message.Visibility = Visibility.Collapsed;
            }
        }

        private void Image_Tap(object sender, GestureEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
