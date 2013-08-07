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
using Panda.Phone.Publisher.Model;
using Panda.Phone.Publisher.Controls;
using Panda.Phone.Publisher.PublisherServiceReference;
using System.Windows.Media.Imaging;

namespace Panda.Phone.Publisher.Views
{
    public partial class FriendPage : PhoneApplicationPage
    {
        FriendVM vm;
        PublisherServiceClient client = new PublisherServiceClient();
        Dictionary<int, List<Image>> image_Dic = new Dictionary<int, List<Image>>();
        Dictionary<int, WriteableBitmap> wb_Dic = new Dictionary<int, WriteableBitmap>();
        public FriendPage()
        {
            client.GetImageByUserIdCompleted += new EventHandler<GetImageByUserIdCompletedEventArgs>(client_GetImageByUserIdCompleted);
            int userId = (App.Current as App).UserId;
            InitializeComponent();
            vm = new FriendVM(userId, 
                delegate {list_PostResourse.ItemsSource = vm.list_PostModel; },
                delegate { list_Achievement.ItemsSource = vm.list_AchievementModel; },
                delegate {
                          foreach (UnloadModel photo in vm.list_UnloadModel)
                         {
                          Friend_Photo control = new Friend_Photo(client) { ItemId = photo.ItemId, Describe=photo.Describe, Name = photo.Name+" "};
                          control.Margin = new Thickness(5);
                          wp_Photo.Children.Add(control);
                         }
            });
            callback(); 
        }
        void callback()
        {
            list_PostResourse.ItemsSource = vm.list_PostModel;
            list_Achievement.ItemsSource = vm.list_AchievementModel;
            foreach (UnloadModel photo in vm.list_UnloadModel)
            {
                Friend_Photo control = new Friend_Photo(client) { Describe = photo.Describe, ItemId = photo.ItemId, Name = photo.Name + " " };
                control.Margin = new Thickness(5);
                //HubTile hubTile = new HubTile()
                //{
                //    //Source="/Images/Dessert.jpg",
                //    Tag=photo.ItemId,
                //    Title=photo.Name,
                //    Notification=photo.Describe,
                //    DisplayNotification=true,
                //    GroupTag="Food"
                //};


                wp_Photo.Children.Add(control);
            }
        }

        
        private void img_Avatar_Loaded(object sender, RoutedEventArgs e)
        {
            Image image = (Image)sender;
            PostModel post = (PostModel)image.DataContext;
            if (!image_Dic.Keys.Contains(post.UserId))
            {
                List<Image> list = new List<Image>();
                list.Add(image);
                image_Dic.Add(post.UserId, list);
                client.GetImageByUserIdAsync(post.UserId);
               
            }
            else
            {
                if (wb_Dic.Keys.Contains(post.UserId))
                {
                    image.Source = wb_Dic[post.UserId];
                }
                else
                {
                    List<Image> list = image_Dic[post.UserId];
                   list.Add(image);
                }
            }
          
           
        }

        void client_GetImageByUserIdCompleted(object sender, GetImageByUserIdCompletedEventArgs e)
        {
            if(!wb_Dic.Keys.Contains(e.Result.UserId))
            wb_Dic.Add(e.Result.UserId, Helper.BytesToBitMap(e.Result.Avatar));
            foreach (Image image in image_Dic[e.Result.UserId])
            {
                image.Source = wb_Dic[e.Result.UserId];
            }
           
        }

        private void UserAvatar_Loaded(object sender, RoutedEventArgs e)
        {
            Image image = (Image)sender;
            int userId = ((AchievementModel)image.DataContext).UserId;
            if (!image_Dic.Keys.Contains(userId))
            {
                List<Image> list = new List<Image>();
                list.Add(image);
                image_Dic.Add(userId, list);
                client.GetImageByUserIdAsync(userId);
               // client.GetImageByUserIdCompleted += new EventHandler<GetImageByUserIdCompletedEventArgs>(client_GetImageByUserIdCompleted);
            }
            else
            {
                if (wb_Dic.Keys.Contains(userId))
                {
                    image.Source = wb_Dic[userId];
                }
                else
                {
                    List<Image> list = image_Dic[userId];
                    list.Add(image);
                }
            }
          
        }

       
    }
}