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
using System.Device.Location;
using Panda.Phone.Acquirer.GeocodeServiceServiceReference;
using Microsoft.Phone.Controls.Maps;
using Microsoft.Phone.Controls.Maps.Platform;
using Panda.Phone.Acquirer.AcquirerServiceReference;
using System.Collections.ObjectModel;
using System.Threading;
using System.Diagnostics;
namespace Panda.Phone.Acquirer
{
    public partial class MainPage : PhoneApplicationPage
    {
        Color color = Colors.Blue;
        SolidColorBrush brush;
        GeocodeResult[] Georesults;
        List<Location> list;
        MapLayer layer;
        List<Image> pushpinList;
        GeoCoordinate center;
        double zoomLevel;
        AcquirerServiceClient client;
        bool normalView=true;
        bool select_rIsHit = false;
        List<User> userList;
        MapPolyline line = new MapPolyline();
        MapPolygon polygon = new MapPolygon();
        List<User> choicedUserList = new List<User>();
        ListBox Lb = new ListBox();
        bool isbreak = false;
        bool firstTime = true;
        bool list2IsPin = true;
        bool pivotIsPin = true;
        bool list1IsPin = true;
        bool[] b = new bool[6];
        List<User> listFriend = new List<User>();
        public MainPage()
        {
            InitializeComponent();
          //  Console.WriteLine("dsjglsjgs;gj;s");
           this.nagetive.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri("/Images/up.jpg",UriKind.Relative)) };
            brush = new SolidColorBrush(color);
            layer = new MapLayer();
            pushpinList = new List<Image>();
            map.Children.Add(layer);
            layer.Children.Add(line);
            layer.Children.Add(polygon);
           
            client = new AcquirerServiceClient();
            client.getAllUserAsync();
            client.getAllUserCompleted += new EventHandler<getAllUserCompletedEventArgs>(client_getAllUserCompleted);
            map.DoubleTap += new EventHandler<System.Windows.Input.GestureEventArgs>(map_DoubleTap);
        }

        void map_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            line.Opacity = 0;
            polygon.Opacity = 0;
        }

        void client_getAllUserCompleted(object sender, getAllUserCompletedEventArgs e)
        {
            //MapLayer layer = new MapLayer();
            //map.Children.Add(layer);
            foreach(User item in e.Result)
            {
                
                //Pushpin pin = new Pushpin();
                //pin.Location = new GeoCoordinate(item.Latitude,item.Longitude);
                //pin.Content = item.UserName;
                //pin.Tag = item.Id;
                //pin.DataContext = item;
                //layer.AddChild(pin, pin.Location);
                //pin.MouseLeftButtonDown += new MouseButtonEventHandler(pin_MouseLeftButtonDown);
                Image pin = new Image(){Source=new BitmapImage(new Uri("Images/pin.png", UriKind.Relative))};
                pin.Width = 35;
                pin.Height = 35;
                pin.Tag = item.Id;
                pin.DataContext = item;
                
                layer.AddChild(pin, new GeoCoordinate(item.Latitude, item.Longitude));
                pin.MouseLeftButtonDown += new MouseButtonEventHandler(pin_MouseLeftButtonDown);
                
                pin.DoubleTap += new EventHandler<System.Windows.Input.GestureEventArgs>(pin_DoubleTap);
                pushpinList.Add(pin);
            }
            map.SetView(new GeoCoordinate(e.Result[0].Latitude, e.Result[0].Longitude), 10);
            userList = e.Result.ToList<User>();
        }

        void pin_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Image pin=(Image)sender;
            User user=(User)pin.DataContext;
            //dd.Children.Remove(pp);
            //layer.AddChild(pp,new GeoCoordinate(user.Latitude,user.Longitude));
            //LbUserDeal.Visibility = Visibility.Visible;
            //LbUserDeal.VerticalAlignment = VerticalAlignment.Top;
            //LbUserDeal.HorizontalAlignment = HorizontalAlignment.Left;
            //LbUserDeal.Margin = new Thickness(e.GetPosition(dd).X, e.GetPosition(dd).Y, 0, 0);
            userName.Content = user.UserName;
            list2Panel.Visibility = Visibility.Visible;
            // list2Panel.VerticalAlignment = VerticalAlignment.Top;
            list2Panel.HorizontalAlignment = HorizontalAlignment.Left;
            // list2Panel.Margin = new Thickness(e.GetPosition(dd).X, e.GetPosition(dd).Y, 0, 0);
            LbUserDeal.DataContext = user;
            list2tRec.MouseLeftButtonDown += new MouseButtonEventHandler(list2tRec_MouseLeftButtonDown);
            if (!choicedUserList.Contains(user))
            {
                choicedUserList.Add(user);
                pin.Source = new BitmapImage(new Uri("Images/choicepin.png", UriKind.Relative));
            }
            else
            {
                choicedUserList.Remove(user);
                pin.Source = new BitmapImage(new Uri("Images/pin.png", UriKind.Relative));
            }
          //  list2Panel.MouseLeftButtonDown += new MouseButtonEventHandler(list2Panel_MouseLeftButtonDown);
            //dd.MouseLeftButtonUp += new MouseButtonEventHandler(ddList2tRec_MouseLeftButtonUp);
            //list2tRec.MouseLeftButtonUp += new MouseButtonEventHandler(list2tRec_MouseLeftButtonUp);
        }

      

        void list2tRec_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            list2IsPin = false;
            
        }
        
        

        void pin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          //  Storyboard4.Begin();
          //  User user=((Image) sender).DataContext as User;
          ////   MessageBox.Show(user.UserName,user.items.E_waste.Num+"",MessageBoxButton.OKCancel);
          // client.getItemsByUserIdAsync(user.Id);
          // client.getItemsByUserIdCompleted += new EventHandler<getItemsByUserIdCompletedEventArgs>(client_getItemsByUserIdCompleted);
            Image pin = (Image)sender;
            User user=(User)((Image)sender).DataContext;

          
            dd.MouseMove += new MouseEventHandler(dd_MouseMove);

            pivotPanel.Visibility = Visibility.Visible;
            pivotPanel.VerticalAlignment = VerticalAlignment.Top;
            pivotPanel.HorizontalAlignment = HorizontalAlignment.Left;
            //pivotPanel.Margin = new Thickness(((ListBox)sender).Margin.Left + 150, ((ListBox)sender).Margin.Top, ((ListBox)sender).Margin.Right, ((ListBox)sender).Margin.Bottom);
            pivotRec.MouseLeftButtonDown += new MouseButtonEventHandler(pivotRec_MouseLeftButtonDown);
            // pivotRec.MouseLeftButtonUp += new MouseButtonEventHandler(pivotRec_MouseLeftButtonUp);
            //dd.MouseLeftButtonUp += new MouseButtonEventHandler(dd_MouseLeftButtonUp);
            //pp.MouseLeftButtonUp+=new MouseButtonEventHandler(pp_MouseLeftButtonUp);
            client.getItemsByUserIdAsync(user.Id);
            client.getItemsByUserIdCompleted += new EventHandler<getItemsByUserIdCompletedEventArgs>(client_getItemsByUserIdCompleted);

            
        }

        void client_getItemsByUserIdCompleted(object sender, getItemsByUserIdCompletedEventArgs e)
        {
           // this.LbItems.DataContext = e.Result[0];
          
            this.pp.Visibility = Visibility.Visible;
          //  List<Items> list = e.Result.ToList<Items>();
            //LbEwaste.DataContext = e.Result.Where(c => c.CategoryId == 0);
            LbEwaste.ItemsSource = e.Result.Where(c => c.CategoryId == 0);
            LbMetal.ItemsSource = e.Result.Where(c => c.CategoryId == 1);
            LbFabric.ItemsSource = e.Result.Where(c => c.CategoryId == 2);
            LbPlastic.ItemsSource = e.Result.Where(c => c.CategoryId == 3);
            LbPaper.ItemsSource = e.Result.Where(c => c.CategoryId == 4);
            LbGlass.ItemsSource = e.Result.Where(c => c.CategoryId == 5);
        }
        
        void client_GeocodeCompleted(object sender, GeocodeCompletedEventArgs e)
        {
            int id = Convert.ToInt32(e.UserState);
            Georesults[id] = e.Result.Results[0];
            GeocodeResponse response = e.Result;
            double latitude = response.Results[0].Locations[0].Latitude;
            double longitude = response.Results[0].Locations[0].Longitude;
            map.SetView(new GeoCoordinate(latitude, longitude), 14);

            //if (Georesults.All(m => m != null))
            //    CalculateRoute(Georesults);
        }

        private void Geocode(string name, int id)
        {
            GeocodeServiceClient client = new GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
            GeocodeRequest request = new GeocodeRequest();
            request.Credentials = new Credentials();
            request.Credentials.ApplicationId = "AjlmpILfXZMz8HCz-aqkBfnZZt6UXFnVqXVIP7glejUCYrliehYU6xtEBZVshDZM";
            request.Query = name;
            client.GeocodeCompleted += new EventHandler<GeocodeCompletedEventArgs>(client_GeocodeCompleted);
            client.GeocodeAsync(request, id);
        }

        void map_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btnSelect_area.Source = new BitmapImage(new Uri("Images/select_area.png", UriKind.Relative));
             //layer.Children.Remove(line);
             // layer.Children.Clear();
             // MapPolygon line = new MapPolygon();
            polygon.Locations = new LocationCollection();
            polygon.Opacity = 0.6;
            polygon.StrokeThickness = 4;
            polygon.Stroke = brush;
            foreach (Location l in list)
            {
                polygon.Locations.Add(l);
            }
            //
            
            map.MouseMove -= new MouseEventHandler(map_MouseMove);
            map.MouseLeftButtonDown -= new MouseButtonEventHandler(map_MouseLeftButtonDown);
            map.MouseLeftButtonUp -= new MouseButtonEventHandler(map_MouseLeftButtonUp);
            ObservableCollection<Adrress> AddressList=new ObservableCollection<Adrress>();
            foreach (Location l in list)
            {
               
                AddressList.Add(new Adrress() {Latitude=l.Latitude,Longitude=l.Longitude });
            }
           
            foreach (User item in userList)
            {
                bool alarge = true;
                bool alow = true;
                bool llarge = true;
                bool llow = true;
                foreach (Location a in list)
                {
                    if (item.Longitude < a.Longitude) alarge = false;
                    if (item.Longitude > a.Longitude) alow = false;
                    if (item.Latitude < a.Latitude) llarge = false;
                    if (item.Latitude > a.Latitude) llow = false;
                }
                if (!(alarge | alow | llarge | llow))
                {
                    choicedUserList.Add(item);
                }
            }
            foreach (User item in choicedUserList)
            {
                Image pin = pushpinList.First(c => (int)c.Tag == item.Id);
                
                pin.Source = new BitmapImage(new Uri("Images/e-waste.png", UriKind.Relative));

            }
           
            list.Clear();
        }

        void client_GetChoicedPointCompleted(object sender, GetChoicedPointCompletedEventArgs e)
        {
            //MapLayer layer = new MapLayer();
            //map.Children.Add(layer);
            foreach (User item in e.Result)
            {
                Image pin = pushpinList.First(c => (int)c.Tag == item.Id);
                //Pushpin pin = new Pushpin();
                //pin.Location = new GeoCoordinate(item.Latitude,item.Longitude);
               //  pin.Content = "this be choiced";
               // pin.Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                pin.Source = new BitmapImage(new Uri("Images/choicepin.png", UriKind.Relative));

                //layer.AddChild(pin, pin.Location);
            }
        }
       
        void map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            list = new List<Location>();
            center = map.Center;
            zoomLevel = map.ZoomLevel;
        }
       
        void map_MouseMove(object sender, MouseEventArgs e)
        {
            // List<Location> list = new List<Location>();
            map.SetView(center, zoomLevel);
            list.Add(map.ViewportPointToLocation(e.GetPosition(map)));
            //MapPolyline line = new MapPolyline();
            line.Locations = new LocationCollection();
            line.Opacity = 0.6;
            line.StrokeThickness = 4;
            line.Stroke = brush;
            
            foreach (Location l in list)
            {
                line.Locations.Add(l);
            }
           
        }
      
       
        public ObservableCollection<Adrress> AddressList { get; set; }

        private void btnSearch_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Georesults = new GeocodeResult[2];
            Geocode(txtSearch.Text,0);
        }

        private void btnSelect_area_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnSelect_area.Source = new BitmapImage(new Uri("Images/select_area1.png", UriKind.Relative));
            
            map.MouseMove += new MouseEventHandler(map_MouseMove);
            map.MouseLeftButtonDown += new MouseButtonEventHandler(map_MouseLeftButtonDown);
            map.MouseLeftButtonUp += new MouseButtonEventHandler(map_MouseLeftButtonUp);
        }

        private void btnSelect_r_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            select_rIsHit = !select_rIsHit;
            if (select_rIsHit)
            {
                line.Opacity = 0;
                polygon.Opacity = 0;
                this.list1Panel.Visibility = Visibility.Visible;
                this.btnSelect_r.Source = new BitmapImage(new Uri("Images/select_r1.png", UriKind.Relative));
                list1tRec.MouseLeftButtonDown += new MouseButtonEventHandler(list1tRec_MouseLeftButtonDown);
                dd.MouseMove += new MouseEventHandler(dd_MouseMove);
                firstTime = true;
            }
            else
            {
                this.list1Panel.Visibility = Visibility.Collapsed;
                this.btnSelect_r.Source = new BitmapImage(new Uri("Images/select_r.png", UriKind.Relative));
            }
            
        }

        void list1tRec_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            list1IsPin = false;
           
        }

        private void btnMessage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnViewChange_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            normalView = !normalView;
            if (!normalView)
            {
                map.Mode = new AerialMode();
                btnViewChange.Source = new BitmapImage(new Uri("Images/nor.jpg", UriKind.Relative));
            }
            else
            {
                map.Mode = new RoadMode();
                btnViewChange.Source = new BitmapImage(new Uri("Images/sat.jpg", UriKind.Relative));
            }
               
                
                
        }

        private void Lbcategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             isbreak = true;
            client.getItemsByCategoryIdAsync(this.Lbcategory.SelectedIndex);
            client.getItemsByCategoryIdCompleted += new EventHandler<getItemsByCategoryIdCompletedEventArgs>(client_getItemsByCategoryIdCompleted);
            //this.Lbcategory.Visibility = Visibility.Collapsed;
       
            //this.Lbcategory1.Margin = new Thickness(Lbcategory.Margin.Left + 100, Lbcategory.Margin.Top + Lbcategory.SelectedIndex * 35+20, 0, 0);
            //this.Lbcategory1.Visibility = Visibility.Visible;
            

        }

        void client_getItemsByCategoryIdCompleted(object sender, getItemsByCategoryIdCompletedEventArgs e)
        {
            ObservableCollection<Items> list = e.Result;
          //  MapLayer layer = new MapLayer();
           
           // map.Children.Add(layer);
            int[] a = new int[100];
           
            foreach(Items item in list)
            {
                a[item.UserId] += item.Num;
            }
            for (int i = 0; i < userList.Count; i++)
            {
                if (a[i] > 0)
                {
                    
                     Thread thread = new Thread(new ParameterizedThreadStart(threadDo));

                     thread.Start(i);
                     
                }
            }

        }

         void threadDo(object i)
        {
            isbreak = false;
            long time0 = DateTime.Now.Ticks;
            User user = userList.First(c => c.Id == (int)i);
            bool cansee = true;
             while(true)
               {
                   if (isbreak)
                   {
                       Dispatcher.BeginInvoke(new Action(delegate()
                       {
                           Image pin = pushpinList.First(c => (int)c.Tag == user.Id);
                           pin.Opacity = 1.0;
                           Debug.WriteLine(pin.Tag + "-end");
                       }));
                       break;
                   }
                 TimeSpan span0 = new TimeSpan(DateTime.Now.Ticks - time0);
                    if (span0.Seconds%2==0&&!cansee)
                   {
                       cansee =! cansee;
                       Dispatcher.BeginInvoke(new Action(delegate()
                           {
                               Image pin = pushpinList.First(c => (int)c.Tag == user.Id);
                               pin.Opacity = 1.0;
                           }));
                   }
                   if (span0.Seconds%2!=0&&cansee) 
                   {
                       cansee = !cansee;
                       Dispatcher.BeginInvoke(new Action(delegate() 
                           { 
                               Image pin = pushpinList.First(c => (int)c.Tag == user.Id); 
                               pin.Opacity = 0;
                               Debug.WriteLine(pin.Tag+"-0");
                           }));
                      
                       //time = DateTime.Now.Ticks;
                      
                   }
                   if (span0.Seconds>20)
                   {
                       Dispatcher.BeginInvoke(new Action(delegate() 
                           { 
                               Image pin = pushpinList.First(c => (int)c.Tag == user.Id); 
                               pin.Opacity = 1.0;
                               Debug.WriteLine(pin.Tag + "-end");
                           }));
                       break;
                   } 
                }
            
        }
      

        

      
       

  
        void pivotRec_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            pivotIsPin = false;
            
        }

        void dd_MouseMove(object sender, MouseEventArgs e)
        {
              if (!pivotIsPin) 
              pivotPanel.Margin = new Thickness(e.GetPosition(dd).X, e.GetPosition(dd).Y, 0, 0);
              if(!list1IsPin)
              list1Panel.Margin = new Thickness(e.GetPosition(dd).X, e.GetPosition(dd).Y, 0, 0);
              if(!list2IsPin)
              list2Panel.Margin = new Thickness(e.GetPosition(dd).X, e.GetPosition(dd).Y, 0, 0);
              
        }

        private void pivotPanel_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
          {
              pivotPanel.Visibility = Visibility.Collapsed;
          }

        private void list2Panel_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            list2Panel.Visibility = Visibility.Collapsed;
           
        }

     

        private void LbUserDeal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User user = ((ListBox)sender).DataContext as User;

            if (LbUserDeal.SelectedIndex == 2)
            {
                pivotPanel.Visibility = Visibility.Visible;
                pivotPanel.VerticalAlignment = VerticalAlignment.Top;
                pivotPanel.HorizontalAlignment = HorizontalAlignment.Left;
                pivotRec.MouseLeftButtonDown += new MouseButtonEventHandler(pivotRec_MouseLeftButtonDown);
                dd.MouseMove += new MouseEventHandler(dd_MouseMove);
                // pivotRec.MouseLeftButtonUp += new MouseButtonEventHandler(pivotRec_MouseLeftButtonUp);
                //dd.MouseLeftButtonUp += new MouseButtonEventHandler(dd_MouseLeftButtonUp);
                //pp.MouseLeftButtonUp+=new MouseButtonEventHandler(pp_MouseLeftButtonUp);
                client.getItemsByUserIdAsync(user.Id);
                client.getItemsByUserIdCompleted += new EventHandler<getItemsByUserIdCompletedEventArgs>(client_getItemsByUserIdCompleted);
            }
            if (LbUserDeal.SelectedIndex == 1)
            {
                tblSendMessage.Text = "Send Message To " + user.UserName;
                SendMessagePanel.Visibility = Visibility.Visible;

            }
            if (LbUserDeal.SelectedIndex == 3)
            {
                MessageBox.Show("add" + user.UserName + "to Friends List success!");
                listFriend.Add(user);
            }

        }

        private void btnPinTo_Click(object sender, EventArgs e)
        {
            list2IsPin = true;
            pivotIsPin = true;
            list1IsPin = true;
        }

        private void list1Panel_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            list1Panel.Visibility = Visibility.Collapsed;
            this.btnSelect_r.Source = new BitmapImage(new Uri("Images/select_r.png", UriKind.Relative));
            select_rIsHit = !select_rIsHit;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            sendContent.Text = "hello! I will in" +  "  " + time.Value;
            timePanel.Visibility = Visibility.Visible;
            date.ValueChanged += new EventHandler<DateTimeValueChangedEventArgs>(date_ValueChanged);
            time.ValueChanged += new EventHandler<DateTimeValueChangedEventArgs>(time_ValueChanged);
        }

        void time_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            sendContent.Text = "hello! I will in" + "  " + time.Value;
        }

        void date_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            sendContent.Text = "hello! I will in" + "  " + time.Value;
            
        }

        private void sendOk_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            timePanel.Visibility = Visibility.Collapsed;
        }

        private void sendOk_Click(object sender, RoutedEventArgs e)
        {
            timePanel.Visibility = Visibility.Collapsed;
        }

        private void sendCancel_Click(object sender, RoutedEventArgs e)
        {
            timePanel.Visibility = Visibility.Collapsed;
        }

        private void tbsendMessageOk_Click(object sender, RoutedEventArgs e)
        {
            SendMessagePanel.Visibility = Visibility.Collapsed;
        }

        private void tbsendMessageCancel_Click(object sender, RoutedEventArgs e)
        {
            SendMessagePanel.Visibility = Visibility.Collapsed;
        }

        private void btnFriend_Click(object sender, EventArgs e)
        {
            List<FriendModel> list = new List<FriendModel>();
            foreach (User user in listFriend)
            {
                FriendModel item = new FriendModel();
                item.UserName = user.UserName;

            }
        }

           
    }
    class FriendModel
    {
        public string UserName { set; get; }
        
    }
}