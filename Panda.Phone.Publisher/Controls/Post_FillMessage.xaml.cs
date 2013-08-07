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
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using Panda.Phone.Publisher.Views;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.IO;
namespace Panda.Phone.Publisher.Controls
{
    public partial class Post_FillMessage : UserControl
    {
        internal Dictionary<string, int> items_Post = new Dictionary<string, int>();
        internal Dictionary<string,Stream> Items_Image = new Dictionary<string, Stream>();
        internal Dictionary<string, string> items_Describe = new Dictionary<string, string>();
        internal Dictionary<string, int> items_Category = new Dictionary<string, int>();
        PhotoChooserTask photoChooserTask;
        internal ObservableCollection<string> list_Choosed = new ObservableCollection<string>();
        BitmapImage bitmap;
        Image image;
        PostPage page;
        public Post_FillMessage(PostPage _page)
        {
            InitializeComponent();
            //photoTask = new CameraCaptureTask();
            //photoTask.Completed+=new EventHandler<PhotoResult>(photoTask_Completed);
          //  Storyboard1.Begin();
            lb_Category_Photo.ItemsSource = list_Choosed;
            page = _page;
        }

        int getCategory(string name)
        {
            switch(name)
            {
                case "e_waste": return 1; break;
                case "fabric": return 2; break;
                case "glass": return 3; break;
                case "metal": return 4; break;
                case "paper": return 5; break;
                case "plastic": return 6; break;
            };
            return 0;
        }

        void photoTask_Completed(object sender, PhotoResult e)
        {
                if (e.TaskResult == TaskResult.OK)
                {
                    bitmap = new BitmapImage();
                    bitmap.SetSource(e.ChosenPhoto);
                    image.Source = bitmap;
                    Items_Image.Add((string)image.Tag, e.ChosenPhoto);
                }
        }

        private void sp2_Tap(object sender, GestureEventArgs e)
        {
            
         //   Storyboard1.Begin();
           // sp_Photo.Visibility = Visibility.Visible;
        //    image_sp2.Source = new BitmapImage(new Uri("/Image_Post/Post_Fill/triangle1.png", UriKind.Relative));
           // image_sp1.Source = new BitmapImage(new Uri("/Image_Post/Post_Fill/triangle2.png", UriKind.Relative));
          //  Storyboard1.Completed += new EventHandler(Storyboard1_Completed);
            
        }

        void Storyboard1_Completed(object sender, EventArgs e)
        {
         //   sp_Fill.Visibility = Visibility.Collapsed;
           // sp_Photo.Visibility = Visibility.Visible;
        }

        private void btn_TakePhoto_Tap(object sender, GestureEventArgs e)
        {
            try
            {
                photoChooserTask = new PhotoChooserTask();
                photoChooserTask.PixelWidth = 120;
                photoChooserTask.PixelHeight = 100;
                photoChooserTask.ShowCamera = true;
                photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
                photoChooserTask.Show();
                //photoTask.Show();
                //photoTask.PixelWidth = 72;
                //photoTask.PixelHeight = 72;
                image = sender as Image;
                image.Source = new BitmapImage(new Uri("/Image_Post/Post_Fill/shot_active.png", UriKind.Relative));
                image.Tag  = image.DataContext as string;
              //  Items_Image.Add(name, image.);
                
            }
            catch (System.InvalidOperationException )
            {
                
            }
        }

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                bitmap = new BitmapImage();
                bitmap.SetSource(e.ChosenPhoto);
                image.Source = bitmap;
                Items_Image.Add((string)image.Tag, e.ChosenPhoto);
            }
        }

        private void sp1_Tap(object sender, GestureEventArgs e)
        {
            Storyboard2.Begin();
           // image_sp1.Source = new BitmapImage(new Uri("/Image_Post/Post_Fill/triangle1.png", UriKind.Relative));
           // image_sp2.Source = new BitmapImage(new Uri("/Image_Post/Post_Fill/triangle2.png", UriKind.Relative));
            Storyboard2.Completed += new EventHandler(Storyboard2_Completed);
        }

        void Storyboard2_Completed(object sender, EventArgs e)
        {
          //  sp_Photo.Visibility = Visibility.Collapsed;
          //  sp_Fill.Visibility = Visibility.Visible;
        }

        private void image_Cancel_Tap(object sender, GestureEventArgs e)
        {
           // ((PostPage)((Grid)this.Parent).Parent).Storyboard2.Begin();
            //((Grid)this.Parent).Children[1].IsHitTestVisible = true;
            // this.Visibility = Visibility.Collapsed;
            page.Storyboard5.Begin();
            page.ContentPanel.IsHitTestVisible = true;
        }

        private void image_OK_Tap(object sender, GestureEventArgs e)
        {
           //((Grid)this.Parent).Children[1].Opacity = 1;
           //((Grid)this.Parent).Children[0].Opacity = 1;
            switch((string)lb_Category.Tag)
            {
                case "fabric": page.tile_Fabric.img_Allready.Opacity = 1; break;
             // ((PostPage)((Grid)this.Parent).Parent).tile_Fabric.img_Allready.Opacity = 1;break;
                case "e_waste": page.tile_Ewaste.img_Allready.Opacity = 1; break;
             // ((PostPage)((Grid)this.Parent).Parent).tile_Ewaste.img_Allready.Opacity = 1; break;
                case "paper": page.tile_Paper.img_Allready.Opacity = 1; break;
             // ((PostPage)((Grid)this.Parent).Parent).tile_Paper.img_Allready.Opacity = 1; break;
                case "glass": page.tile_Glass.img_Allready.Opacity = 1; break;
            //  ((PostPage)((Grid)this.Parent).Parent).tile_Glass.img_Allready.Opacity = 1; break;
                case "plastic": page.tile_Plastic.img_Allready.Opacity = 1; break;
             // ((PostPage)((Grid)this.Parent).Parent).tile_Plastic.img_Allready.Opacity = 1; break;
                case "metal": page.tile_Metal.img_Allready.Opacity = 1; break;
             // ((PostPage)((Grid)this.Parent).Parent).tile_Metal.img_Allready.Opacity = 1; break;
            };
            page.Storyboard5.Begin();
            page.ContentPanel.IsHitTestVisible = true;
            page.ContentPanel.Opacity = 1;
            page.TitlePanel.Opacity = 1;
            //this.Visibility = Visibility.Collapsed;
           
        }

        //private void tb_Num_TextInput(object sender, TextCompositionEventArgs e)
        //{
            
        //    TextBox tb = (TextBox)sender;
            
        //    string name = tb.DataContext as string;
        //    if (tb.Text != "")
        //    {
        //        if (!items_Post.Keys.Contains(name))
        //        {
        //            items_Post.Add(name, Int32.Parse(tb.Text));
        //        }
        //        else
        //        {
        //            items_Post.Remove(name);
        //            items_Post.Add(name, Int32.Parse(tb.Text));
        //        }
        //    }
        //}

        private void tb_Num_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string name = tb.DataContext as string;
            if (tb.Text != "")
            {
                if(!list_Choosed.Contains(name))
                list_Choosed.Add(name);

                if (!items_Post.Keys.Contains(name))
                {
                    items_Post.Add(name, Int32.Parse(tb.Text));
                }
                else
                {
                    items_Post.Remove(name);
                    items_Post.Add(name, Int32.Parse(tb.Text));
                }


                if (!items_Category.Keys.Contains(name))
                {
                    items_Category.Add(name,getCategory((string)lb_Category.Tag));
                }
                else
                {
                    items_Category.Remove(name);
                    items_Category.Add(name,getCategory((string)lb_Category.Tag));
                }
            }
            else
            {
                if(list_Choosed.Contains(name))
                    list_Choosed.Remove(name);
            }
        }

        private void tb_Describe_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            string name = tb.DataContext as string;
            if (tb.Text != "")
            {

                if (!items_Describe.ContainsKey(name))
                {
                    items_Describe.Add(name, tb.Text);
                }
                else
                {
                    items_Describe.Remove(name);
                    items_Describe.Add(name, tb.Text);
                }
            }
        }

        private void tb_Other_Name_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tb_Other_Name.Text != "")
            {
                list_Choosed.Add(tb_Other_Name.Text);
                switch ((string)lb_Category.Tag)
                {
                    case "fabric":
                        Constant.fabricList.Add(tb_Other_Name.Text); break;
                    case "e_waste":
                        Constant.ewasteList.Add(tb_Other_Name.Text); break;
                    case "paper":
                        Constant.paperList.Add(tb_Other_Name.Text); break;
                    case "glass":
                        Constant.glassList.Add(tb_Other_Name.Text); break;
                    case "plastic":
                        Constant.plasticList.Add(tb_Other_Name.Text); break;
                    case "metal":
                        Constant.metalList.Add(tb_Other_Name.Text); break;
                };
                tb_Other_Name.Text = "";
                //tb_Other_Num.Text = "";
                //  cb_Other.IsChecked = false;
            }
        }

        private void tbl_Danwei_Loaded(object sender, RoutedEventArgs e)
        {
           TextBlock tbl= (TextBlock)sender;
            if(Constant.getDic_Danwei().Keys.Contains(tbl.DataContext as string))
           tbl.Text = Constant.getDic_Danwei()[tbl.DataContext as string];
        }      
    }
}
