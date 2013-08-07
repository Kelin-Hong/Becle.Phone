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
using System.Windows.Threading;
using System.Windows.Media.Imaging;
namespace Panda.Phone.Publisher.Controls
{
    public partial class Tile2 : UserControl
    {
        DispatcherTimer timer;
        Random r = new Random();
        public Tile2()
        {
            InitializeComponent();
            //this.LayoutRoot.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri("/Image_Home/card3.jpg", UriKind.Relative)) };
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            
           // Storyboard storyboard=new Storyboard();
           // DoubleAnimationUsingKeyFrames keyFrame = new DoubleAnimationUsingKeyFrames();
            
           // Storyboard.SetTargetName(storyboard, " ");
           //// Storyboard.SetTargetProperty(storyboard,);
           // Storyboard.SetTargetProperty(storyboard, new PropertyPath(PlaneProjection.RotationXProperty));
           // storyboard.Children.Add(keyFrame);
           // EasingDoubleKeyFrame keyFrame1 = new EasingDoubleKeyFrame();
           // keyFrame1.KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0));
           // keyFrame1.Value = 90;
           // EasingDoubleKeyFrame keyFrame2 = new EasingDoubleKeyFrame();

            
           
             
            //  Storyboard.TargetPropertyProperty=(UIElement.Projection).(PlaneProjection.RotationX);
            
        }

        void Storyboard1_Completed(object sender, EventArgs e)
        {
            int i = r.Next(2);
            Storyboard1.Stop();
            
            //Storyboard.SetTargetName(animation1, "image" + (i + 1));
            //Storyboard.SetTargetName(animation2, "rectangle" + (i + 1));
            //Storyboard1.Begin();
        }

        void timer_Tick(object sender, EventArgs e)
        {

             // image10.Clip = new RectangleGeometry() { Rect = new Rect(0, 0, image10.Width / 2, image10.Height / 2) };
              int i = r.Next(12);
              int j = r.Next(5);
              switch (i)
              {
                  case 1: 
                  image1.Source = new BitmapImage(new Uri("/Image_Home/Avatar/" + j + ".jpg", UriKind.Relative));    Storyboard1.Begin(); break;
                  case 2: image2.Source = new BitmapImage(new Uri("/Image_Home/Avatar/" + j + ".jpg", UriKind.Relative));  Storyboard2.Begin(); break;
                  case 3: image3.Source = new BitmapImage(new Uri("/Image_Home/Avatar/" + j + ".jpg", UriKind.Relative)); Storyboard3.Begin(); break;
                  case 4: image4.Source = new BitmapImage(new Uri("/Image_Home/Avatar/" + j + ".jpg", UriKind.Relative)); Storyboard4.Begin(); break;
                  case 5: image5.Source = new BitmapImage(new Uri("/Image_Home/Avatar/" + j + ".jpg", UriKind.Relative)); Storyboard5.Begin(); break;
                  case 6: image6.Source = new BitmapImage(new Uri("/Image_Home/Avatar/" + j + ".jpg", UriKind.Relative)); Storyboard6.Begin(); break;
                  case 7: image7.Source = new BitmapImage(new Uri("/Image_Home/Avatar/" + j + ".jpg", UriKind.Relative)); Storyboard7.Begin(); break;
                  case 8: image8.Source = new BitmapImage(new Uri("/Image_Home/Avatar/" + j + ".jpg", UriKind.Relative)); Storyboard8.Begin(); break;
                  case 9: image9.Source = new BitmapImage(new Uri("/Image_Home/Avatar/" + j + ".jpg", UriKind.Relative)); Storyboard9.Begin(); break;
                  case 10: image10.Source = new BitmapImage(new Uri("/Image_Home/Avatar/" + j + ".jpg", UriKind.Relative)); image10.Visibility = Visibility.Collapsed; break;
                  case 11: image10.Visibility = Visibility.Visible; break;
                  //case 10:
                  //    {
                  //        //timer.Stop();
                  //        //int jj = r.Next(4);
                           
                  //       // image10.Source = new BitmapImage(new Uri("/Image_Home/Avatar/" + j + ".jpg", UriKind.Relative)); image10.Clip = new RectangleGeometry() { Rect = new Rect(0, 0, 60, 60) }; Storyboard1.Begin(); break;
                  //        //image1.Source = new BitmapImage(new Uri("/Image_Home/Avatar/" + jj + ".jpg", UriKind.Relative)); image1.Height = 60; image1.Width = 60; image1.Clip = new RectangleGeometry() { Rect = new Rect(0, 0, 30, 30) };  image1.Width = 60; image1.Height = 60; Storyboard1.Begin();
                  //        //image2.Source = new BitmapImage(new Uri("/Image_Home/Avatar/" + jj + ".jpg", UriKind.Relative)); image2.Height = 60; image2.Width = 60; image2.Clip = new RectangleGeometry() { Rect = new Rect(30, 0, 30, 30) }; image2.Width = 120; image2.Height = 120; Storyboard2.Begin();
                  //        //image5.Source = new BitmapImage(new Uri("/Image_Home/Avatar/" + jj + ".jpg", UriKind.Relative)); image5.Height = 120; image5.Width = 120; image5.Clip = new RectangleGeometry() { Rect = new Rect(0, 60, 60, 60) }; Storyboard5.Begin();
                  //        //image4.Source = new BitmapImage(new Uri("/Image_Home/Avatar/" + jj + ".jpg", UriKind.Relative)); image4.Height = 120; image4.Width = 120; image4.Clip = new RectangleGeometry() { Rect = new Rect(60,60, 60, 60) }; Storyboard4.Begin();

                  //    }break;
              };
          
            
           
           
          // if (i == 1)
              // Storyboard1.Begin();
         //  else
              // Storyboard2.Begin();
           
           
        }
    }
}
