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
using Panda.Phone.Publisher.Controls;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using Microsoft.Phone.Shell;
namespace Panda.Phone.Publisher.Views
{
    public partial class PostPage : PhoneApplicationPage
    {
        
       // Dictionary<string, int> items_Post_Ok = new Dictionary<string, int>();   
        Post_FillMessage control_Post_FillMessage;
        ApplicationBar appBar1;
        ApplicationBar appBar2;
        ApplicationBarIconButton btnAppBarBack;
        ApplicationBarIconButton btnAppBarNext;
        ApplicationBarIconButton btnAppBarOK;
        public PostPage()
        {
            InitializeComponent();
            //MenuContent.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(MenuContent_ManipulationDelta);
            MenuContent.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(MenuContent_ManipulationCompleted);
            Storyboard4.Completed += new EventHandler(Storyboard4_Completed);
            Storyboard5.Completed += new EventHandler(Storyboard5_Completed);
            dealWithAppBar();
            this.Loaded += new RoutedEventHandler(PostPage_Loaded);
        }

        void PostPage_Loaded(object sender, RoutedEventArgs e)
        {
            control_Post_FillMessage = new Post_FillMessage(this);
            control_Post_FillMessage.Margin = new Thickness(0, 50, 0, 0);
        }

        void Storyboard5_Completed(object sender, EventArgs e)
        {
            this.ApplicationBar = appBar2;
        }

        void Storyboard4_Completed(object sender, EventArgs e)
        {
            this.ApplicationBar = appBar1;
        }

        #region[AppBar]
        void dealWithAppBar()
        {
            #region[AppBar1]
            appBar1 = new ApplicationBar();
            appBar1.Mode = ApplicationBarMode.Default;
            appBar1.Opacity = 0.5;
            appBar1.IsVisible = true;
            btnAppBarBack = new ApplicationBarIconButton();
            btnAppBarBack.IconUri = new Uri("/Image_Post/appbar.cancel.rest.png", UriKind.Relative);
            btnAppBarBack.Text = "cancel";
            appBar1.Buttons.Add(btnAppBarBack);
            btnAppBarNext = new ApplicationBarIconButton();
            btnAppBarNext.IconUri = new Uri("/Image_Post/appbar.check.rest.png", UriKind.Relative);
            btnAppBarNext.Text = "ok";
            appBar1.Buttons.Add(btnAppBarNext);
            btnAppBarBack.Click += new EventHandler(btnAppBarBack_Click);
            btnAppBarNext.Click += new EventHandler(btnAppBarNext_Click);
            #endregion
            #region[AppBar2]
            appBar2 = new ApplicationBar();
            appBar2.Mode = ApplicationBarMode.Default;
            appBar2.Opacity = 0.5;
            appBar2.IsVisible = true;
            btnAppBarOK = new ApplicationBarIconButton();
            btnAppBarOK.IconUri = new Uri("/Image_Post/appbar.check.rest.png", UriKind.Relative);
            btnAppBarOK.Text = "ok";
            appBar2.Buttons.Add(btnAppBarOK);
            btnAppBarOK.Click += new EventHandler(btnAppBarOK_Click);
            #endregion
            this.ApplicationBar = appBar2;
        }

        void btnAppBarOK_Click(object sender, EventArgs e)
        {
            if (control_Post_FillMessage.items_Post.Count != control_Post_FillMessage.Items_Image.Count)
            {
                MessageBox.Show("Please take the photos of resourse you post! ");
                return;
            }
            // Image image_front = (Image)sender;
            // image_front.Source = new BitmapImage(new Uri("/Image_Post/arrow_active.png", UriKind.Relative));
            Storyboard3.AutoReverse = true;
            Storyboard3.Begin();
            NavigationService.Navigate(new Uri("/Views/PostSuccessPage.xaml", UriKind.Relative));
           
        }

        void btnAppBarNext_Click(object sender, EventArgs e)
        {
            switch ((string)control_Post_FillMessage.lb_Category.Tag)
            {
                case "fabric": tile_Fabric.img_Allready.Opacity = 1; break;
                // ((PostPage)((Grid)this.Parent).Parent).tile_Fabric.img_Allready.Opacity = 1;break;
                case "e_waste": tile_Ewaste.img_Allready.Opacity = 1; break;
                // ((PostPage)((Grid)this.Parent).Parent).tile_Ewaste.img_Allready.Opacity = 1; break;
                case "paper": tile_Paper.img_Allready.Opacity = 1; break;
                // ((PostPage)((Grid)this.Parent).Parent).tile_Paper.img_Allready.Opacity = 1; break;
                case "glass": tile_Glass.img_Allready.Opacity = 1; break;
                //  ((PostPage)((Grid)this.Parent).Parent).tile_Glass.img_Allready.Opacity = 1; break;
                case "plastic": tile_Plastic.img_Allready.Opacity = 1; break;
                // ((PostPage)((Grid)this.Parent).Parent).tile_Plastic.img_Allready.Opacity = 1; break;
                case "metal": tile_Metal.img_Allready.Opacity = 1; break;
                // ((PostPage)((Grid)this.Parent).Parent).tile_Metal.img_Allready.Opacity = 1; break;
            };
            Storyboard5.Begin();
            ContentPanel.IsHitTestVisible = true;
            ContentPanel.Opacity = 1;
            TitlePanel.Opacity = 1;
        }

        void btnAppBarBack_Click(object sender, EventArgs e)
        {
            Storyboard5.Begin();
            ContentPanel.IsHitTestVisible = true;
            ContentPanel.Opacity = 1;
            TitlePanel.Opacity = 1;
        }
        #endregion

        #region[Menu_StoryBoard]
        void MenuContent_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            if (e.FinalVelocities.LinearVelocity.Y < -500)
            {
                Storyboard5.Begin();
                ContentPanel.IsHitTestVisible = true;
                TitlePanel.Opacity = 1;
                ContentPanel.Opacity = 1;
            }
        }
        void MenuContent_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            Grid grid = sender as Grid;
            CompositeTransform transform = grid.RenderTransform as CompositeTransform;
            if (transform.TranslateY + e.DeltaManipulation.Translation.Y <800)
                transform.TranslateY += e.DeltaManipulation.Translation.Y;
            if (transform.TranslateY < 400)
            {
                Storyboard5.Begin();
                ContentPanel.IsHitTestVisible = true;
                TitlePanel.Opacity = 1;
                ContentPanel.Opacity = 1;
            }
        }
        #endregion

        #region[Navigate]
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            Storyboard3.AutoReverse = true;
            Storyboard3.Duration = new Duration(new TimeSpan(0, 0, 0,0,600));
            Storyboard3.Begin();
            Storyboard3.SeekAlignedToLastTick(new TimeSpan(0, 0, 0, 0, 100));
         
            base.OnNavigatedTo(e);
        }
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            var target = e.Content as PostSuccessPage;
            if (target != null)
            {
                target.Items_Post = control_Post_FillMessage.items_Post;
                target.Items_Image = control_Post_FillMessage.Items_Image;
                target.items_Category = control_Post_FillMessage.items_Category;
                target.items_Describe = control_Post_FillMessage.items_Describe;
            }
            base.OnNavigatedFrom(e);
        }
        #endregion

        //void image_OK_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        //{
        //    TitlePanel.Opacity = 1;
        //    ContentPanel.Opacity = 1;
        //    ContentPanel.IsHitTestVisible = true;
        //    control_Post_FillMessage.Visibility = Visibility.Collapsed;
        //}
       
        private void dealWithContentMenu()
        {
            MenuContent.Children.Clear();
            MenuContent.Children.Add(control_Post_FillMessage);
            Storyboard4.Begin();
        }

        #region[Tile_Tab]
        private void tile_Ewaste_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TitlePanel.Opacity = 0.5;
            ContentPanel.Opacity = 0.5;
            ContentPanel.IsHitTestVisible = false;
          //  List<string> ewasteList = new List<string>() { "phone", "computer", "TV", "Map3/Mp4"};
            control_Post_FillMessage.lb_Category.ItemsSource = Constant.ewasteList;
            control_Post_FillMessage.lb_Category.Tag = "e_waste";
             dealWithContentMenu();
          //  Storyboard1.Begin();
           // ContentPanel.IsHitTestVisible = false;
           // control_Post_FillMessage.Visibility = Visibility.Visible;
          //  control_Post_FillMessage.sp_Photo.Visibility = Visibility.Collapsed;
            //control_Post_FillMessage.sp_Fill.Visibility = Visibility.Visible;
        }
  
        private void tile_Fabric_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TitlePanel.Opacity = 0.5;
            ContentPanel.Opacity = 0.5;
            ContentPanel.IsHitTestVisible = false;
           // List<string> fabricList = new List<string>() { "clothes", "shoes", "quilt", "curtains", "carpet" };
            control_Post_FillMessage.lb_Category.ItemsSource = Constant.fabricList;
            control_Post_FillMessage.lb_Category.Tag = "fabric";
            //Storyboard1.Begin();
            dealWithContentMenu();
           
           // control_Post_FillMessage.Visibility = Visibility.Visible;
          //  control_Post_FillMessage.sp_Photo.Visibility = Visibility.Collapsed;
           // control_Post_FillMessage.sp_Fill.Visibility = Visibility.Visible;
        }

        private void tile_Plastic_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TitlePanel.Opacity = 0.5;
            ContentPanel.Opacity = 0.5;
            ContentPanel.IsHitTestVisible = false;
           // List<string> plasticList = new List<string>() { "bottle " };
            control_Post_FillMessage.lb_Category.ItemsSource = Constant.plasticList;
            control_Post_FillMessage.lb_Category.Tag = "plastic";
            dealWithContentMenu();
           // Storyboard1.Begin();
           // control_Post_FillMessage.Visibility = Visibility.Visible;
        }

        private void tile_Paper_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TitlePanel.Opacity = 0.5;
            ContentPanel.Opacity = 0.5;
            ContentPanel.IsHitTestVisible = false;
          // List<string> paperList = new List<string>() { "bottle", "paper" };
            control_Post_FillMessage.lb_Category.ItemsSource = Constant.paperList;
            control_Post_FillMessage.lb_Category.Tag = "paper";
            dealWithContentMenu();
           
        }

        private void tile_Metal_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TitlePanel.Opacity = 0.5;
            ContentPanel.Opacity = 0.5;
            ContentPanel.IsHitTestVisible = false;
           // List<string> metalList = new List<string>() { "aluminium", "steel", "copper" };
            control_Post_FillMessage.lb_Category.ItemsSource = Constant.metalList;
            control_Post_FillMessage.lb_Category.Tag = "metal";
            dealWithContentMenu();
           
        }

        private void tile_Glass_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TitlePanel.Opacity = 0.5;
            ContentPanel.Opacity = 0.5;
            ContentPanel.IsHitTestVisible = false;
           // List<string> glassList = new List<string>() { "bottle" };
            control_Post_FillMessage.lb_Category.ItemsSource = Constant.glassList;
            control_Post_FillMessage.lb_Category.Tag = "glass";
            dealWithContentMenu();
           
        }
        #endregion

        private void PhoneApplicationPage_BackKeyPress(object sender, CancelEventArgs e)
        {
            if (ContentPanel.IsHitTestVisible == false)
            {
                Storyboard5.Begin();
                ContentPanel.IsHitTestVisible = true;
                e.Cancel = true;
                TitlePanel.Opacity = 1;
                ContentPanel.Opacity = 1;
            }
        }

        //private void btn_OK_Click(object sender, EventArgs e)
        //{
        //    if (control_Post_FillMessage.items_Post.Count != control_Post_FillMessage.Items_Image.Count)
        //    {
        //        MessageBox.Show("Please take the photos of resourse you post! ");
        //       // return;
        //    }
        //   // Image image_front = (Image)sender;
        //   // image_front.Source = new BitmapImage(new Uri("/Image_Post/arrow_active.png", UriKind.Relative));
        //    Storyboard3.AutoReverse = true;
        //    Storyboard3.Begin();
        //    NavigationService.Navigate(new Uri("/Views/PostSuccessPage.xaml", UriKind.Relative));
        //}

    }
   
}