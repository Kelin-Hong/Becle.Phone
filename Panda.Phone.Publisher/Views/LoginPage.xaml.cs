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
using Panda.Phone.Publisher.PublisherServiceReference;
using System.Windows.Media.Imaging;
using System.Device.Location;
using Microsoft.Phone.Controls.Maps;
using Microsoft.Phone.Tasks;
using System.IO.IsolatedStorage;
using System.IO;
using Microsoft.Phone.Shell;
namespace Panda.Phone.Publisher.Views
{
    public partial class LoginPage : PhoneApplicationPage
    {
        bool[] b = {true,false,false };
        GeoCoordinateWatcher watcher;
        Image Pin_My;
        MapLayer layer;
        PhotoChooserTask photoChooserTask;
        PublisherServiceClient client = new PublisherServiceReference.PublisherServiceClient();
        RegisterUser register = new PublisherServiceReference.RegisterUser();
        string UserName, Password;
        public LoginPage()
        {
            InitializeComponent();
            client.RegisterCompleted += new EventHandler<RegisterCompletedEventArgs>(client_RegisterCompleted);
            client.LoginCompleted += new EventHandler<LoginCompletedEventArgs>(client_LoginCompleted);
            getPassword();
            ShellTile tile = ShellTile.ActiveTiles.First();
            StandardTileData NewTileData = new StandardTileData
            {
                
                BackgroundImage = new Uri("tile.jpg", UriKind.Relative),
                BackTitle = "",
                BackBackgroundImage = new Uri("tile3.jpg", UriKind.Relative),
                BackContent = "",
            };
            tile.Update(NewTileData);
        }

        void client_LoginCompleted(object sender, LoginCompletedEventArgs e)
        {
            LoginBack login= e.Result;
            if (login.Message == "Error") MessageBox.Show("UserName or PassWord Error");
            else
            {
                if (login.UserType == 0)
                {
                    (App.Current as App).UserId = login.UserId;
                    client.GetUserInfoCompleted += new EventHandler<GetUserInfoCompletedEventArgs>(client_GetUserInfoCompleted);
                    NavigationService.Navigate(new Uri("/Views/MainPage.xaml", UriKind.Relative));
                    client.GetUserInfoAsync((App.Current as App).UserId);
                }
                else
                    if(login.UserType==1)
                {
                    (App.Current as App).AcquirerId = login.UserId;
                  
                    NavigationService.Navigate(new Uri("/Views/AcquirerPage.xaml", UriKind.Relative));
                }
            }
          
        }

        void client_GetUserInfoCompleted(object sender, GetUserInfoCompletedEventArgs e)
        {
            (App.Current as App).Userinfo = e.Result;
            client.CloseAsync();
        }

        private void img_resident_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            sp_StoreName.Visibility = Visibility.Collapsed;
            sp_Phone.Visibility = Visibility.Collapsed;
         //   Image image = (Image)sender;
            if (!b[0])
            {
                b[0] = true;
                img_resident.Source = new BitmapImage(new Uri("/Image_Login/select.png", UriKind.Relative));
                if (b[2])
                {
                    b[2] = false;
                   // img_Merchant.Source = new BitmapImage(new Uri("/Image_Login/unselect.png", UriKind.Relative));
                }
                if (b[1])
                {
                    b[1] = false;
                    img_Recycler.Source = new BitmapImage(new Uri("/Image_Login/unselect.png", UriKind.Relative));
                }
            }
       
        }

        private void img_Recycler_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            sp_StoreName.Visibility = Visibility.Visible;
            sp_Phone.Visibility = Visibility.Visible;
            
            if (!b[1])
            {
                b[1] = true;
                img_Recycler.Source = new BitmapImage(new Uri("/Image_Login/select.png", UriKind.Relative));
                if (b[2])
                {
                    b[2] = false;
                   // img_Merchant.Source = new BitmapImage(new Uri("/Image_Login/unselect.png", UriKind.Relative));
                }
                if (b[0])
                {
                    b[0] = false;
                    img_resident.Source = new BitmapImage(new Uri("/Image_Login/unselect.png", UriKind.Relative));
                }
            }
        }

        private void img_Merchant_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            sp_StoreName.Visibility = Visibility.Visible;
            sp_Phone.Visibility = Visibility.Visible;
           // Image image = (Image)sender;
            if (!b[2])
            {
                b[2] = true;
                //img_Merchant.Source = new BitmapImage(new Uri("/Image_Login/select.png", UriKind.Relative));
                if (b[0])
                {
                    b[0] = false;
                   img_resident.Source = new BitmapImage(new Uri("/Image_Login/unselect.png", UriKind.Relative));
                }
                if (b[1])
                {
                    b[1] = false;
                    img_Recycler.Source = new BitmapImage(new Uri("/Image_Login/unselect.png", UriKind.Relative));
                }
            }
        }

        private void btn_RegisterOK_Click(object sender, RoutedEventArgs e)
        {
            panorama.TabIndex = 0;
            register.Address = tb_Address_Register.Text;
            register.UserName = tb_UserName_Register.Text;
            register.Passwrod = tb_Possword_Register.Password;
           
            register.Phone = tb_Phone_Register.Text;
            register.StoreBinName = tb_BinName_Register.Text;
          
            if (register.UserName == "")
            {
                MessageBox.Show("The user name cannot be empty");
                return;
            }
            if (register.Passwrod =="")
            {
                MessageBox.Show("The password cannot be empty");
                return;
            }
            if (tb_RePossword_Register.Password == "")
            {
                MessageBox.Show("The re-password cannot be empty");
                return;
            }
            if (register.Passwrod !=tb_RePossword_Register.Password)
            {
                MessageBox.Show("The password is not the same!");
                return;
            }
            if (Pin_My == null)
            {
                MessageBox.Show("Please tab the little image of map to set your accurate location!");
                return;
            }
            if (b[1] && register.StoreBinName == "")
            {
                MessageBox.Show("The Bin Name cannot be empty");
                return;
            }
            if (b[1] && register.Phone == "")
            {
                MessageBox.Show("The Phone Number cannot be empty");
                return;
            }
            if (register.ImageFileData == null)
            {
                MessageBox.Show("Please Tab the Image of avatar to Choose a Image as you avatar");
                return;
            }
            register.Latitude = (decimal)((GeoCoordinate)Pin_My.Tag).Latitude;
            register.Longitude = (decimal)((GeoCoordinate)Pin_My.Tag).Longitude;
            if(b[0])  register.UserType = 0;
            if (b[1]) register.UserType = 1;
            if (b[2]) register.UserType = 2;
            if (b[2])
            {
                MessageBox.Show("Currently does not support this type of user");
                return;
            }
            if (register.Address == "" || !register.Address.Contains("省") || !register.Address.Contains("市"))
            {
                MessageBox.Show("Address Must be of such a format \"xxx省xxx市xxxx....\"!");
                return;
            }
            client.RegisterAsync(register);
           
        }

        void client_RegisterCompleted(object sender, RegisterCompletedEventArgs e)
        {
            MessageBox.Show(e.Result);
        }

        private void img_Avatar_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.PixelWidth = 72;
            photoChooserTask.PixelHeight = 72;
            photoChooserTask.ShowCamera = true;
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
            photoChooserTask.Show();
        }

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                BitmapImage bimg = new BitmapImage();
                bimg.SetSource(e.ChosenPhoto);
                img_Avatar.Source = bimg;
                register.ImageFileData = Helper.StreamToBytes(e.ChosenPhoto);
            }
        }

        private void img_Map_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            sp_Map.Visibility = Visibility.Visible;
            panorama.Visibility = Visibility.Collapsed;
            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            watcher.Start();
            map.SetView(watcher.Position.Location, 17);
            map.Mode = new AerialMode();
            Pin_My = new Image();
            Pin_My = new Image() { Source = new BitmapImage(new Uri("/Image_Acquirer/man.png", UriKind.Relative)) };
            Pin_My.Width = 40;
            Pin_My.Height = 50;
            layer = new MapLayer();
            map.Children.Add(layer);
            layer.AddChild(Pin_My, watcher.Position.Location);
            Pin_My.Tag = watcher.Position.Location;
            map.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(map_Tap);
        }

        void map_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            layer.Children.Clear();
            layer.AddChild(Pin_My, map.ViewportPointToLocation(e.GetPosition(map)));
            Pin_My.Tag = map.ViewportPointToLocation(e.GetPosition(map));
        }
        void getPassword()
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isf.FileExists("password.dat"))
                {
                    using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream("password.dat", FileMode.Open, isf))
                    {
                        using (StreamReader sr = new StreamReader(isfs))
                        {
                            UserName = tb_UserName.Text = sr.ReadLine();
                            Password = tb_Possword.Password = sr.ReadLine();
                            sr.Close();
                        }
                    }
                }
            }
        }
        void savePassword(string name,string password)
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isf.FileExists("password.dat"))
                {
                    isf.DeleteFile("password.dat");
                }
                using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream("password.dat", FileMode.Create, isf))
                {
                    using (StreamWriter sw = new StreamWriter(isfs))
                    {
                        sw.WriteLine(name);
                        sw.WriteLine(password);
                        sw.Close();
                    }
                }
            }
        }
        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            if (tb_UserName.Text == "")
            {
                MessageBox.Show("The user name cannot be empty");
                return;
            }
            if (tb_Possword.Password == "")
            {
                MessageBox.Show("The password cannot be empty");
                return;
            }
            if (tb_UserName.Text != "" && tb_Possword.Password != "")
            {
                if(tb_UserName.Text!=UserName)
                savePassword(tb_UserName.Text, tb_Possword.Password);
                Login login = new Login()
                {
                    UserName = tb_UserName.Text,
                    Passwrod = tb_Possword.Password
                };
                client.LoginAsync(login);
               
            }
        }

        private void img_MapOk_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            sp_Map.Visibility = Visibility.Collapsed;
            panorama.Visibility = Visibility.Visible;
        }

       
        private void renrenLogin_Click(object sender, RoutedEventArgs e)
        {
            if (App.api.IsAccessTokenValid())
            {
                NavigationService.Navigate(new Uri("/Views/MainPage.xaml", UriKind.Relative));
            }
            else
            {   
                App.api.Login(this, (e1, e2) =>
                {
                    if (e2.Error == null)
                    {
                        NavigationService.Navigate(new Uri("/Views/MainPage.xaml", UriKind.Relative));
                    }
                    else
                    {
                        MessageBox.Show(e2.Error.Message);
                    }
                });
            }
        }
    }
}