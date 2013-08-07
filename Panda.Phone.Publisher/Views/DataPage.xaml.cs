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
using Panda.Phone.Publisher.ViewModel;
namespace Panda.Phone.Publisher.Views
{
    public partial class DataPage : PhoneApplicationPage
    {
        public DataPage()
        {
            InitializeComponent();
            
            //this.DataContext = new DataVM((App.Current as App).UserId,this);
          
            //DataVM vm=(DataVM) this.grid1.DataContext;
          
        }

        private void tb_City_Order_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            if ((Int32.Parse(tb.Text)) % 2 == 0)
            {
                ((Border)tb.Parent).Background =new SolidColorBrush(new Color() { R = 6, G = 128, B = 67, A = 255 });
            }
            else
            {
             ((Border)((StackPanel)((Border)tb.Parent).Parent).Children[1]).Background = new SolidColorBrush(new Color() { R = 6, G = 128, B = 67, A = 255 });
            }
            
        }

        

        private void tb_User_Order_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            if ((Int32.Parse(tb.Text)) % 2 == 0)
            {
                ((Border)tb.Parent).Background = new SolidColorBrush(new Color() { R = 6, G = 128, B = 67, A = 255 });
            }
            else
            {
                ((Border)((StackPanel)((Border)tb.Parent).Parent).Children[1]).Background = new SolidColorBrush(new Color() { R = 6, G = 128, B = 67, A = 255 });
            }
        }

   
    }
}