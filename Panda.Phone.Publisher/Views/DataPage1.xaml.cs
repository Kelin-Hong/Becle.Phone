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
using System.Windows.Threading;
using System.Windows.Media.Imaging;
namespace Panda.Phone.Publisher.Views
{
    public partial class DataPage1 : PhoneApplicationPage
    {
        DataVM vm;
        int[] tree1 = new int[6] {10,5,2,10,4,2 };
        int[] tree1_now = new int[6]{0,0,0,0,0,0};
        DispatcherTimer timer1;
        DispatcherTimer timer2;
        DispatcherTimer timer3;
        DispatcherTimer timer4;
        DispatcherTimer timer5;
        DispatcherTimer timer6;
        public DataPage1()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(DataPage1_Loaded);
        }

        void DataPage1_Loaded(object sender, RoutedEventArgs e)
        {
            vm = new DataVM((App.Current as App).UserId,this);
            this.DataContext = vm;
        }
        private void tb_City_Order_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            (((Border)((StackPanel)((Border)tb.Parent).Parent).Children[2]).Child as TextBlock).Text = vm.List_City_Point[(Int32.Parse(tb.Text))-1] + "";
            if ((Int32.Parse(tb.Text)) % 2 == 0)
            {
                ((Border)tb.Parent).Background = new SolidColorBrush(new Color() { R = 6, G = 128, B = 67, A = 255 });
                ((Border)((StackPanel)((Border)tb.Parent).Parent).Children[2]).Background = new SolidColorBrush(new Color() { R = 6, G = 128, B = 67, A = 255 });
            }
            else
            {
                ((Border)((StackPanel)((Border)tb.Parent).Parent).Children[1]).Background = new SolidColorBrush(new Color() { R = 6, G = 128, B = 67, A = 255 });
            }

        }

        private void tb_User_Order_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            (((Border)((StackPanel)((Border)tb.Parent).Parent).Children[2]).Child as TextBlock).Text = vm.List_User_Point[(Int32.Parse(tb.Text))-1] + "";
            if ((Int32.Parse(tb.Text)) % 2 == 0)
            {
                ((Border)tb.Parent).Background = new SolidColorBrush(new Color() { R = 6, G = 128, B = 67, A = 255 });
                ((Border)((StackPanel)((Border)tb.Parent).Parent).Children[2]).Background = new SolidColorBrush(new Color() { R = 6, G = 128, B = 67, A = 255 });
            }
            else
            {
                ((Border)((StackPanel)((Border)tb.Parent).Parent).Children[1]).Background = new SolidColorBrush(new Color() { R = 6, G = 128, B = 67, A = 255 });
            }
        }

        private void image_User_Hightest_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromMilliseconds(200);
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();

        }

        void timer1_Tick(object sender, EventArgs e)
        {
            tree1_now[0]++;
          //  image_User_Hightest.Height = 200;
            image_User_Hightest.Source = new BitmapImage(new Uri("/Image_Data/tree/" + tree1_now[0] + ".png", UriKind.Relative));
            if (tree1_now[0] == tree1[0])
            {
                ((DispatcherTimer)sender).Stop();
            }
        }
      
        private void image_User_Average_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer2 = new DispatcherTimer();
            timer2.Interval = TimeSpan.FromMilliseconds(200);
            timer2.Tick += new EventHandler(timer2_Tick);
            timer2.Start();
            
        }

        void timer2_Tick(object sender, EventArgs e)
        {
            tree1_now[1]++;
           // image_User_Hightest.Height = 200;
          //  image_User_Average.Height = 30 * tree1_now[1];
            image_User_Average.Source = new BitmapImage(new Uri("/Image_Data/tree/" + tree1_now[1] + ".png", UriKind.Relative));
            if (tree1_now[1] == tree1[1])
            {
                ((DispatcherTimer)sender).Stop();
            }
        }

        private void image_User_My_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer3 = new DispatcherTimer();
            timer3.Interval = TimeSpan.FromMilliseconds(200);
            timer3.Tick += new EventHandler(timer3_Tick);
            timer3.Start();
            
        }

        void timer3_Tick(object sender, EventArgs e)
        {
            tree1_now[2]++;
            // image_User_Hightest.Height = 200;
           // image_User_My.Height = 30 * tree1_now[2];
            image_User_My.Source = new BitmapImage(new Uri("/Image_Data/tree/" + tree1_now[2] + ".png", UriKind.Relative));
            if (tree1_now[2] == tree1[2])
            {
                ((DispatcherTimer)sender).Stop();
            }
        }

        private void image_City_Hightest_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer4 = new DispatcherTimer();
            timer4.Interval = TimeSpan.FromMilliseconds(200);
            timer4.Tick += new EventHandler(timer4_Tick);
            timer4.Start();
           
        }

        void timer4_Tick(object sender, EventArgs e)
        {
            tree1_now[3]++;
            //image_User_Hightest.Height = 200;
          //  image_City_Hightest.Height = 30 * tree1_now[3];
            image_City_Hightest.Source = new BitmapImage(new Uri("/Image_Data/tree/" + tree1_now[3] + ".png", UriKind.Relative));
            if (tree1_now[3] == tree1[3])
            {
                ((DispatcherTimer)sender).Stop();
            }
        }

        private void image_City_Average_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer5 = new DispatcherTimer();
            timer5.Interval = TimeSpan.FromMilliseconds(200);
            timer5.Tick += new EventHandler(timer5_Tick);
            timer5.Start();

           
        }

        void timer5_Tick(object sender, EventArgs e)
        {
            tree1_now[4]++;
            //image_City_Average.Height = 200;
          //  image_City_Average.Height = 30 * tree1_now[4];
            image_City_Average.Source = new BitmapImage(new Uri("/Image_Data/tree/" + tree1_now[4] + ".png", UriKind.Relative));
            if (tree1_now[4] == tree1[4])
            {
                ((DispatcherTimer)sender).Stop();
            }
        }

        private void image_City_My_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer6 = new DispatcherTimer();
            timer6.Interval = TimeSpan.FromMilliseconds(200);
            timer6.Tick += new EventHandler(timer6_Tick);
            timer6.Start();   
        }

        void timer6_Tick(object sender, EventArgs e)
        {
            tree1_now[5]++;
           // image_City_My.Height = 200;
           // image_City_My.Height = 30 * tree1_now[5];
            image_City_My.Source = new BitmapImage(new Uri("/Image_Data/tree/" + tree1_now[5] + ".png", UriKind.Relative));
            if (tree1_now[5] == tree1[5])
            {
                ((DispatcherTimer)sender).Stop();
            }
        }

    
    }
}