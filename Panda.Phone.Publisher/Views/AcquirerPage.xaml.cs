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
using Microsoft.Phone.Controls.Maps;
using Microsoft.Phone.Controls.Maps.Platform;
using System.Collections.ObjectModel;
using System.Threading;
using System.Diagnostics;
using Panda.Phone.Publisher.LocationServiceReference;
using Panda.Phone.Publisher.ViewModel;
using Panda.Phone.Publisher.Model;
using Panda.Phone.Publisher.RouteServiceReference;
using System.Windows.Threading;
using Panda.Phone.Publisher.PublisherServiceReference;
using Panda.Phone.Publisher;
using Panda.Phone.Publisher.Controls;
using Panda.Phone.Publisher.DataBase;
namespace Panda.Phone.Acquirer
{
    public partial class AcquirerPage : PhoneApplicationPage
    {
        bool isbreak = false;
        GeocodeResult[] Georesults;
        bool normalView = true;
      //  bool select_rIsHit = false;
        MapLayer layer = new MapLayer();
        MapPolyline line;
        MapPolygon polygon;
        SolidColorBrush brush = new SolidColorBrush(new Color() { R = 6, G = 128, B = 67, A = 255 });
        List<UserModel> list_User_AllChoosed = new List<UserModel>();
        public   List<UserModel> list_User_Choosed = new List<UserModel>();
        List<ItemModel> list_Item_Choosed = new List<ItemModel>();
        List<Location> list_Dot = new List<Location>();
        List<Image> list_Pin = new List<Image>();
        internal AcquirerVM vm;
        Image pin_Acquirer;
        internal PublisherServiceClient client = new PublisherServiceClient();
        internal int acquirerId;
        Acquirer_Message acquirer_Message;
        Acquirer_Detail1 acquirer_Detail = new Acquirer_Detail1();
        RecyclePlan recyclePlan;
       // Acquirer_Menu control_Menu = new Acquirer_Menu();
        public AcquirerPage()
        {
            InitializeComponent();
            acquirerId = (App.Current as App).AcquirerId;
          
            vm = new AcquirerVM(callback1,callback2,callback3);
            this.nagetive.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri("/Image_Acquirer/up.jpg", UriKind.Relative)) };
            map.Children.Add(layer);
            
            btnSelect_area.IsHitTestVisible = false;
            control_Category.image_OK.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(image_Ok_Tap);
            control_Category.image_Cancel.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(image_Cancel_Tap);
            MenuContent.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(MenuContent_ManipulationDelta);
            MenuContent.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(MenuContent_ManipulationCompleted);
            control_Menu.menu_Yes.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(menu_Yes_Tap);
            control_Menu.menu_no.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(menu_no_Tap);
            Storyboard6.Completed += new EventHandler(Storyboard6_Completed);
        }

        void Storyboard6_Completed(object sender, EventArgs e)
        {
            dd.IsHitTestVisible = true;
        }

        void image_Cancel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Storyboard1.AutoReverse = true;
            Storyboard1.Begin();
            Storyboard1.SeekAlignedToLastTick(new TimeSpan(0, 0, 0, 0, 200));
            // Storyboard1.SkipToFill();
            control_Category.Visibility = Visibility.Collapsed;
        }
        void mapClear()
        {
            layer.Children.Clear();
            pin_Acquirer = new Image() { Source = new BitmapImage(new Uri("/Image_Acquirer/man.png", UriKind.Relative)) };
            pin_Acquirer.Width = 40;
            pin_Acquirer.Height = 50;
            layer.AddChild(pin_Acquirer, new GeoCoordinate((double)vm.acquirerInfo.Latitude, (double)vm.acquirerInfo.Longitude));
            list_Pin.Add(pin_Acquirer);
            map.SetView(new GeoCoordinate((double)vm.acquirerInfo.Latitude, (double)vm.acquirerInfo.Longitude), 16);          
        }
        void callback1()
        {
            pin_Acquirer = new Image() { Source = new BitmapImage(new Uri("/Image_Acquirer/man.png", UriKind.Relative)) };
            pin_Acquirer.Width = 40;
            pin_Acquirer.Height = 50;
            layer.AddChild(pin_Acquirer, new GeoCoordinate((double)vm.acquirerInfo.Latitude, (double)vm.acquirerInfo.Longitude));
            list_Pin.Add(pin_Acquirer);
            map.SetView(new GeoCoordinate((double)vm.acquirerInfo.Latitude, (double)vm.acquirerInfo.Longitude), 16);          
        }

        void MenuContent_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            if (e.FinalVelocities.LinearVelocity.Y < -500)
            {
                Storyboard6.Begin();
                
            }
        }

        void MenuContent_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            Grid grid = sender as Grid;
            CompositeTransform transform = grid.RenderTransform as CompositeTransform;
            if (e.DeltaManipulation.Translation.Y < 0)
                transform.TranslateY += e.DeltaManipulation.Translation.Y;
            if (transform.TranslateY < 400)
            {
                Storyboard6.Begin();
            }
        }
        
        void callback2()
        {
           btnSelect_r.IsHitTestVisible =true;
          
           
        }
        void callback3()
        {
            acquirer_Message = new Acquirer_Message();
            acquirer_Message.DataContext = vm;
           // AcquirerVM vm = acquirer_Message.DataContext as AcquirerVM;
            if (vm.List_Message.Where(c=>c.IsSee==false).Count() != 0)
            {
                Thread thread = new Thread(new ThreadStart(threadDo));
                thread.Start();
            }
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            while (NavigationService.BackStack.Any())
            {
                NavigationService.RemoveBackEntry();
            }
            base.OnNavigatedTo(e);
        }

        #region[search]
        private void btnSearch_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Georesults = new GeocodeResult[2];
            Geocode(txtSearch.Text, 0);
        }
        void client_GeocodeCompleted(object sender, GeocodeCompletedEventArgs e)
        {
            int id = Convert.ToInt32(e.UserState);
            Georesults[id] = e.Result.Results[0];
            GeocodeResponse response = e.Result;
            double latitude = response.Results[0].Locations[0].Latitude;
            double longitude = response.Results[0].Locations[0].Longitude;
            map.SetView(new GeoCoordinate(latitude, longitude), 14);
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
        #endregion

        #region[drawregion]
        void map_MapPan(object sender, MapDragEventArgs e)
        {
            e.Handled = true;
            Point p2 = e.ViewportPoint;
            list_Dot.Add(map.ViewportPointToLocation(e.ViewportPoint));
            line.Locations.Add(map.ViewportPointToLocation(e.ViewportPoint));
        }
        private void btnSelect_area_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (list_Pin.Count == 0)
            {
                MessageBox.Show("Please select a category first!");
                return;
            }
            btnSelect_area.Source = new BitmapImage(new Uri("/Image_Acquirer/select_area.png", UriKind.Relative));
            map.MapPan+=new EventHandler<MapDragEventArgs>(map_MapPan);
            map.MouseLeftButtonDown += new MouseButtonEventHandler(map_MouseLeftButtonDown);
            map.MouseLeftButtonUp += new MouseButtonEventHandler(map_MouseLeftButtonUp);
        }
        void map_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btnSelect_area.Source = new BitmapImage(new Uri("/Image_Acquirer/select_area1.png", UriKind.Relative));
            list_Item_Choosed.Clear();
            list_User_Choosed.Clear();
            foreach (Location l in list_Dot)
            {
                polygon.Locations.Add(l);
            }
            map.MapPan-=new EventHandler<MapDragEventArgs>(map_MapPan);
            map.MouseLeftButtonDown -= new MouseButtonEventHandler(map_MouseLeftButtonDown);
            map.MouseLeftButtonUp -= new MouseButtonEventHandler(map_MouseLeftButtonUp);
            foreach (UserModel item in vm.List_User)
            {
                bool alarge = true;
                bool alow = true;
                bool llarge = true;
                bool llow = true;
                foreach (Location a in list_Dot)
                {
                    if (item.Longitude < a.Longitude) alarge = false;
                    if (item.Longitude > a.Longitude) alow = false;
                    if (item.Latitude < a.Latitude) llarge = false;
                    if (item.Latitude > a.Latitude) llow = false;
                }
                if (!(alarge | alow | llarge | llow))
                {
                    list_User_Choosed.Add(item);
                }
            }
             foreach (UserModel item in list_User_Choosed)
            {
                Image pin = list_Pin.First(c => (int)c.Tag == item.Id);
                pin.Source = new BitmapImage(new Uri("/Image_Acquirer/Category/"+(item.Category+1)+(item.Category+1)+".png", UriKind.Relative));
            }

             foreach (ItemModel item in vm.List_Item)
             {
                 bool alarge = true;
                 bool alow = true;
                 bool llarge = true;
                 bool llow = true;
                 foreach (Location a in list_Dot)
                 {
                     if (item.Longitude < a.Longitude) alarge = false;
                     if (item.Longitude > a.Longitude) alow = false;
                     if (item.Latitude < a.Latitude) llarge = false;
                     if (item.Latitude > a.Latitude) llow = false;
                 }
                 if (!(alarge | alow | llarge | llow))
                 {
                     list_Item_Choosed.Add(item);
                 }
             }
             drawRegionComplete();
        }
 
        void map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            list_Dot.Clear();
            line.Locations.Clear();
            polygon.Locations.Clear();
            //if(!layer.Children.Contains(polygon))
            //layer.Children.Add(polygon);
            //if(!layer.Children.Contains(line))
            //layer.Children.Remove(line);
        }
        #endregion

        #region[MenuDeal]
        void drawRegionComplete()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            for (int j = 0; j < AcquirerVM.categoryCount; j++)
            {
                int count = 0;
                if (AcquirerVM.category[j])
                {
                    //int count=(from c in list_Item_Choosed 
                    //         where c.CategoryId==j+1
                    //         select c.Num).Count();
                    var list = list_Item_Choosed.Where(c => c.CategoryId == j + 1);
                    foreach (var item in list)
                    {
                        count += item.Num;
                    }
                    
                    //dictionary.Add(AcquirerVM.categoryStr[j] + "  ", list_Item_Choosed.Where(c => c.CategoryId == j + 1).);
                    dictionary.Add(AcquirerVM.categoryStr[j] + "  ", count);
                }

            }
            Storyboard3.AutoReverse = false;
            Storyboard3.Begin();
           
            control_Menu.lb_Category_Count.ItemsSource = dictionary;
            control_Menu.Height = 500;
            control_Menu.Width = 374;
            control_Menu.Visibility = Visibility.Visible;
            //MenuContent.Children.Clear();
            //MenuContent.Children.Add(control_Menu);
            //dd.IsHitTestVisible = false;
          
        }

        void menu_no_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            mapClear();
            control_Menu.Visibility = Visibility.Collapsed;
            layer.Children.Remove(polygon);
            layer.Children.Remove(line);
        }

        void menu_Yes_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
           // Storyboard3.AutoReverse = true;
           // Storyboard3.Begin();
          //  Storyboard3.SeekAlignedToLastTick(new TimeSpan(0, 0, 0, 0, 200));
            Storyboard4.Begin();
            layer.Children.Remove(polygon);
            layer.Children.Remove(line);
            control_Menu.Visibility = Visibility.Collapsed;
            control_TiemMenu.Visibility = Visibility.Visible;
            control_TiemMenu.btn_Send.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(btn_Send_Tap);
           
        }
        void btn_Send_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
           //   btnRoute.IsEnabled = true;
            mapClear();
            string time=(string)control_TiemMenu.time.SelectedItem;
            time = control_TiemMenu.date.ValueString + "  " + time;
            int planId= vm.saveRecyclePlan(acquirerId, time);
            foreach (UserModel user in list_User_Choosed)
            {
                vm.saveRecycleShip(planId, user.Id);
                MessageToUser message = new MessageToUser()
                {
                    FromId=acquirerId,
                    ToId=user.Id,
                    Time=DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString(),
                    Type=1,    
                };
                MessageToUser message1 = new MessageToUser()
                {
                    FromId = acquirerId,
                    ToId = user.Id,
                    Time = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(),
                    Type = 2,
                };
                List<int> list = new List<int>();
                foreach (ItemModel item in list_Item_Choosed.Where(c => c.UserId == user.Id))
                {
                    if(!list.Contains(item.CategoryId-1))
                        list.Add(item.CategoryId-1);
                }
               // "Hust recycle bin recycle paper resourse in your area and your paper cups,paper box are in the confines .Do you want to confirm that these resourses have been recycled?
                message.MessageContent=vm.acquirerInfo.BinName+" will come to recycle ";
                message1.MessageContent = vm.acquirerInfo.BinName + "recycle";
                for(int i=0;i<list.Count-1;i++)
                {
                    message.MessageContent += AcquirerVM.categoryStr[list[i]] + " & ";
                   // message1.MessageContent+=AcquirerVM.categoryStr[i]+ " & ";
                }
                message.MessageContent += AcquirerVM.categoryStr[list[list.Count-1]] + " ";
                message1.MessageContent += AcquirerVM.categoryStr[list[list.Count-1]] + " ";
                message.MessageContent +="in your area in " +control_TiemMenu.date.ValueString + " " + control_TiemMenu.time.SelectedItem;
                message1.MessageContent +="resourse in your area,Do you want to confirm that these resourses have been recycled?";
                client.SendMessageToUserAsync(message);
               // client.SendMessageToUserAsync(message1);
            }
        }
        #endregion

        #region[MapView]
        private void btnViewChange_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            normalView = !normalView;
            if (!normalView)
            {
                map.Mode = new AerialMode();
                btnViewChange.Source = new BitmapImage(new Uri("/Image_Acquirer/nor.jpg", UriKind.Relative));
            }
            else
            {
                map.Mode = new RoadMode();
                btnViewChange.Source = new BitmapImage(new Uri("/Image_Acquirer/sat.jpg", UriKind.Relative));
            }
        }
       
        #endregion     

        #region[selectCategory]
        private void btnSelect_r_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Storyboard1.AutoReverse = false;
            Storyboard1.Begin();
            control_Category.Visibility = Visibility.Visible;
            
        }

        void newLineAndpolygon()
        {
            line= new MapPolyline();
            polygon = new MapPolygon();
            line.Locations = new LocationCollection();
            line.Opacity = 0.6;
            line.StrokeThickness = 4;
            line.Stroke = brush;
            polygon.Opacity = 0.6;
            polygon.StrokeThickness = 4;
            polygon.Stroke = brush;
            polygon.Locations = new LocationCollection();
            layer.Children.Add(line);
            layer.Children.Add(polygon);
        }

        
        void putImageInMap()
        {
            layer.Children.Clear();
            layer.Children.Add(pin_Acquirer);
            list_Pin.Clear();
            newLineAndpolygon();
            foreach (UserModel user in vm.List_User)
            {
                Image pin = new Image() { Source = new BitmapImage(new Uri("/Image_Acquirer/Category/" + (user.Category+1) + ".png", UriKind.Relative)) };
                pin.Width = 40;
                pin.Height = 40;
                pin.Tag = user.Id;
                pin.DataContext = user;
                layer.AddChild(pin, new GeoCoordinate(user.Latitude, user.Longitude));
                list_Pin.Add(pin);
                pin.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(pin_Tap);
            }
          if(vm.List_User.Count!=0)
           map.SetView(new GeoCoordinate(vm.List_User[0].Latitude, vm.List_User[0].Longitude), 13);
          btnSelect_area.IsHitTestVisible = true;   
        }

        void pin_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //Storyboard2.Begin();
            Storyboard5.Begin();
            Image pin = (Image)sender;
            List<ItemModel> list = vm.List_Item.Where(c => c.UserId == (int)pin.Tag).ToList();
            acquirer_Detail.lb_Detail.ItemsSource = list;
            MenuContent.Children.Clear();
            MenuContent.Children.Add(acquirer_Detail);
            dd.IsHitTestVisible = false;
            //control_Detail.lb_Detail.ItemsSource = list;
           // control_Detail.Visibility = Visibility.Visible;

        }
        void image_Ok_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            putImageInMap();
            Storyboard1.AutoReverse = true;
            Storyboard1.Begin();
            Storyboard1.SeekAlignedToLastTick(new TimeSpan(0, 0, 0, 0, 200));
           // Storyboard1.SkipToFill();
            control_Category.Visibility = Visibility.Collapsed;
        }
        #endregion
       
        #region[Gurid Route]


        internal void GenerateRoute()
        {
            if (list_User_Choosed.Count == 0)
            {
                MessageBox.Show("you must select a region first!");
                return;
            }
             layer.Children.Clear();
             layer.Children.Add(pin_Acquirer);
             list_Pin.Clear();
             foreach (UserModel user in list_User_Choosed)
            {
                Image pin = new Image() { Source = new BitmapImage(new Uri("/Image_Acquirer/Category/" + (user.Category+1) + ".png", UriKind.Relative)) };
                pin.Width = 40;
                pin.Height = 40;
                pin.Tag = user.Id;
                pin.DataContext = user;
                layer.AddChild(pin, new GeoCoordinate(user.Latitude, user.Longitude));
                list_Pin.Add(pin);
                pin.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(pin_Tap);
            }
            CalculateRoute();
            layer.Children.Remove(polygon);
            layer.Children.Remove(line);
        }
        private void CalculateRoute()
        {
            RouteServiceClient routeService = new RouteServiceClient("BasicHttpBinding_IRouteService");
            routeService.CalculateRouteCompleted += new EventHandler<CalculateRouteCompletedEventArgs>(routeService_CalculateRouteCompleted);
            RouteRequest request = new RouteRequest();
            request.Credentials = new Credentials();
            request.Credentials.ApplicationId = ((ApplicationIdCredentialsProvider)map.CredentialsProvider).ApplicationId;
            request.Options = new RouteOptions();
            request.Options.Optimization = RouteOptimization.MinimizeTime;
            request.Options.RoutePathType = RoutePathType.Points;
            request.Waypoints = new System.Collections.ObjectModel.ObservableCollection<Waypoint>();
            Waypoint wayPointAcquirer = new Waypoint();

            wayPointAcquirer.Location = new Location();
            wayPointAcquirer.Location.Latitude = 40.738965;
            wayPointAcquirer.Location.Longitude = -73.996047;
            request.Waypoints.Add(wayPointAcquirer);
            foreach (UserModel user in list_User_Choosed)
            {
                Waypoint wayPoint = new Waypoint();
                wayPoint.Description = user.Address;
                wayPoint.Location = new Location();
                wayPoint.Location.Latitude = user.Latitude;
                wayPoint.Location.Longitude = user.Longitude;
                request.Waypoints.Add(wayPoint);
            }
          
            //foreach (GeocodeResult result in results)
            //{
            //    Waypoint wayPoint = new Waypoint();
            //    wayPoint.Description = result.DisplayName;
                
            //    wayPoint.Location = new Location();
            //    wayPoint.Location.Latitude = result.Locations[0].Latitude;
            //    wayPoint.Location.Longitude = result.Locations[0].Longitude;
            //    request.Waypoints.Add(wayPoint);
            //}
            routeService.CalculateRouteAsync(request);

        }

        void routeService_CalculateRouteCompleted(object sender, CalculateRouteCompletedEventArgs e)
        {
            RouteResponse response = e.Result;
            if ((response.ResponseSummary.StatusCode == Panda.Phone.Publisher.RouteServiceReference.ResponseStatusCode.Success) && (response.Result.Legs.Count != 0))
            {
                Color color = Colors.Blue;
               // SolidColorBrush brush = new SolidColorBrush(color);
                SolidColorBrush brush = new SolidColorBrush(new Color() { R = 6, G = 128, B = 67,A=255 });
                MapPolyline line = new MapPolyline();
                line.Locations = new LocationCollection();
                line.Opacity = 0.6;
                line.StrokeThickness = 10;
                line.Stroke = brush;
                //DispatcherTimer timer = new DispatcherTimer();
                //timer.Interval = TimeSpan.FromMilliseconds(200);
                //timer.Tick += new EventHandler(timer_Tick);
                //timer.Start();
                MapLayer layer = new MapLayer();
                layer.Children.Add(line);
                map.Children.Add(layer);
                foreach (Location l in response.Result.RoutePath.Points)
                {
                    line.Locations.Add(l);
                }
               
                //foreach (GeocodeResult r in Georesults)
                //{
                //    Ellipse ellipse = new Ellipse();
                //    ellipse.Height = 10;
                //    ellipse.Width = 10;
                //    ellipse.Fill = new SolidColorBrush(Colors.Red);
                //    Location location = new Location()
                //    {
                //        Latitude = r.Locations[0].Latitude,
                //        Longitude = r.Locations[0].Longitude
                //    };
                //    layer.AddChild(ellipse, location);
                //}

                foreach (UserModel user in list_User_Choosed)
                {
                    Ellipse ellipse = new Ellipse();
                    ellipse.Height = 10;
                    ellipse.Width = 10;
                    ellipse.Fill = new SolidColorBrush(new Color() { A = 0 });
                    Location location = new Location()
                    {
                        Latitude = user.Latitude,
                        Longitude = user.Longitude
                    };
                    layer.AddChild(ellipse, location);
                }
                //GeoCoordinate p = new GeoCoordinate(Georesults[1].Locations[0].Latitude, Georesults[1].Locations[0].Longitude);
               // var routeModel = new RouteModel(e.Result.Result.RoutePath.Points);
              //  LocationRect rect = LocationRect.CreateLocationRect(routeModel.Locations);

                // Set the map view using the rectangle which bounds the rendered route.
                // map.SetView(p, 8);
            }

        }

        void timer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion


        void threadDo()
        {
            isbreak = false;
            long time0 = DateTime.Now.Ticks;
            bool cansee = true;
            while (true)
            {
                if (isbreak)
                {
                    Dispatcher.BeginInvoke(new Action(delegate()
                    {
                        btnMessage.Source = new BitmapImage(new Uri("/Image_Acquirer/message1.png", UriKind.Relative));

                    }));
                    break;
                }
                TimeSpan span0 = new TimeSpan(DateTime.Now.Ticks - time0);
                if (span0.Seconds % 2 == 0 && !cansee)
                {
                    cansee = !cansee;
                    Dispatcher.BeginInvoke(new Action(delegate()
                    {
                        btnMessage.Source = new BitmapImage(new Uri("/Image_Acquirer/message1.png", UriKind.Relative));
                    }));
                }
                if (span0.Seconds % 2 != 0 && cansee)
                {
                    cansee = !cansee;
                    Dispatcher.BeginInvoke(new Action(delegate()
                    {
                       btnMessage.Source =new BitmapImage(new Uri("/Image_Recycle/Message/m_ope.png", UriKind.Relative));
                    }));



                }
                if (span0.Seconds > 20)
                {
                    Dispatcher.BeginInvoke(new Action(delegate()
                    {
                        btnMessage.Source = new BitmapImage(new Uri("/Image_Acquirer/message1.png", UriKind.Relative));
                    }));
                    break;
                }
            }

        }

        private void btnMessage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Storyboard5.Begin();
            isbreak = true;
            MenuContent.Children.Clear();
            MenuContent.Children.Add(acquirer_Message);
            dd.IsHitTestVisible = false;
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dd.IsHitTestVisible==false)
            {
                Storyboard6.Begin();
                dd.IsHitTestVisible = true;
                e.Cancel = true;
            }
            else
            {
            MessageBoxResult result = MessageBox.Show("Are you sure to quit!", "Exit", MessageBoxButton.OKCancel);
            if (result != MessageBoxResult.OK)
            {
                 e.Cancel = true;
            }
          
            }
            //if (control_Category.Visibility == Visibility.Visible)
            //{
            //    Storyboard1.AutoReverse = true;
            //    Storyboard1.Begin();
            //    Storyboard1.SeekAlignedToLastTick(new TimeSpan(0, 0, 0, 0, 200));
            //    // Storyboard1.SkipToFill();
            //    control_Category.Visibility = Visibility.Collapsed;
            //    e.Cancel = true;
            //}
          //  if (control_Detail.Visibility == Visibility.Visible)
           // {
            //    control_Detail.Visibility =Visibility.Collapsed;
            //    e.Cancel = true;
          //  }
            {
                vm.client.CloseAsync();
            }
        }

        private void btn_Plan_Click(object sender, EventArgs e)
        {
            recyclePlan = new RecyclePlan(this);
            Storyboard5.Begin();
            MenuContent.Children.Clear();
            MenuContent.Children.Add(recyclePlan);
            dd.IsHitTestVisible = false;
        }

       
       
    }
    
}