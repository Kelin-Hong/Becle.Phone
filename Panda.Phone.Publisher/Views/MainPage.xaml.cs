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
using Microsoft.Phone.Notification;
using System.IO.IsolatedStorage;
using System.IO;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Windows.Threading;
namespace Panda.Phone.Publisher
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        private HttpNotificationChannel httpChannel;
        const string channelName = "WeatherUpdatesChannel";
        const string fileName = "PushNotificationsSettings.dat";
        const int pushConnectTimeout = 30;
        private DispatcherTimer timer;
        public MainPage()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            if (!TryFindChannel())
                DoConnect();
        }

        #region[Push Notification Service]
        void timer_Tick(object sender, EventArgs e)
        {
            if (txtStatus.Visibility == Visibility.Visible)
            {
                txtStatus.Visibility = Visibility.Collapsed;
            }
            else
            {
                timer.Stop();
            }
        }
        #region Tracing and Status Updates
        private void UpdateStatus(string message)
        {
           // txtStatus.Text = message;
        }

        private void Trace(string message)
        {
         #if DEBUG
            Debug.WriteLine(message);
         #endif
        }
        #endregion

        #region Misc logic
        private void DoConnect()
        {
            try
            {
                //First, try to pick up existing channel
                httpChannel = HttpNotificationChannel.Find(channelName);

                if (null != httpChannel)
                {
                    Trace("Channel Exists - no need to create a new one");
                    SubscribeToChannelEvents();

                    Trace("Register the URI with 3rd party web service");
                    SubscribeToService();

                    Trace("Subscribe to the channel to Tile and Toast notifications");
                    SubscribeToNotifications();

                    Dispatcher.BeginInvoke(() => UpdateStatus("Channel recovered"));
                }
                else
                {
                    Trace("Trying to create a new channel...");
                    //Create the channel
                    httpChannel = new HttpNotificationChannel(channelName, "HOLWeatherService");
                    Trace("New Push Notification channel created successfully");

                    SubscribeToChannelEvents();

                    Trace("Trying to open the channel");
                    httpChannel.Open();
                    Dispatcher.BeginInvoke(() => UpdateStatus("Channel open requested"));
                }
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(() => UpdateStatus("Channel error: " + ex.Message));
            }
        }

        //private void ParseRAWPayload(Stream e, out string weather, out string location, out string temperature)
        //{
        //    XDocument document;

        //    using (var reader = new StreamReader(e))
        //    {
        //        string payload = reader.ReadToEnd().Replace('\0',
        //          ' ');
        //        document = XDocument.Parse(payload);
        //    }

        //    location = (from c in document.Descendants("WeatherUpdate")
        //                select c.Element("Location").Value).FirstOrDefault();
        //    Trace("Got location: " + location);

        //    temperature = (from c in document.Descendants("WeatherUpdate")
        //                   select c.Element("Temperature").Value).FirstOrDefault();
        //    Trace("Got temperature: " + temperature);

        //    weather = (from c in document.Descendants("WeatherUpdate")
        //               select c.Element("WeatherType").Value).FirstOrDefault();
        //}
        #endregion

        #region Subscriptions
        private void SubscribeToChannelEvents()
        {
            //Register to UriUpdated event - occurs when channel successfully opens
            httpChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(httpChannel_ChannelUriUpdated);

            //Subscribed to Raw Notification
            httpChannel.HttpNotificationReceived += new EventHandler<HttpNotificationEventArgs>(httpChannel_HttpNotificationReceived);

            //general error handling for push channel
            httpChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(httpChannel_ExceptionOccurred);

            //subscrive to toast notification when running app    
            httpChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(httpChannel_ShellToastNotificationReceived);
        }

        private void SubscribeToService()
        {
            //Hardcode for solution - need to be updated in case the REST WCF service address change
            string baseUri = "http://192.168.90.1:8080/RegirstatorService/RegisterUri?uri={0}&id={1}";
            string theUri = String.Format(baseUri, httpChannel.ChannelUri.ToString(),(App.Current as App).UserId+"");
            WebClient client = new WebClient();
            client.DownloadStringCompleted += (s, e) =>
            {
                if (null == e.Error)
                    Dispatcher.BeginInvoke(() => UpdateStatus("Registration succeeded"));
                else
                    Dispatcher.BeginInvoke(() => UpdateStatus("Registration failed: " + e.Error.Message));
               
            };
            client.DownloadStringAsync(new Uri(theUri));
        }
        private void SubscribeToNotifications()
        {
            //////////////////////////////////////////
            // Bind to Toast Notification 
            //////////////////////////////////////////
            try
            {
                if (httpChannel.IsShellToastBound == true)
                {
                    Trace("Already bounded (register) to to Toast notification");
                }
                else
                {
                    Trace("Registering to Toast Notifications");
                    httpChannel.BindToShellToast();
                }
            }
            catch (Exception ex)
            {
                // handle error here
            }

            //////////////////////////////////////////
            // Bind to Tile Notification 
            //////////////////////////////////////////
            try
            {
                if (httpChannel.IsShellTileBound == true)
                {
                    Trace("Already bounded (register) to Tile Notifications");
                }
                else
                {
                    Trace("Registering to Tile Notifications");

                    // you can register the phone application to receive tile images from remote servers [this is optional]
                    Collection<Uri> uris = new Collection<Uri>();
                    uris.Add(new Uri("http://jquery.andreaseberhard.de/pngFix/pngtest.png"));

                    httpChannel.BindToShellTile(uris);
                }
            }
            catch (Exception ex)
            {
                //handle error here
            }
        }

        #endregion

        #region Channel event handlers
        void httpChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            Trace("Channel opened. Got Uri:\n" + httpChannel.ChannelUri.ToString());
            Dispatcher.BeginInvoke(() => SaveChannelInfo());

            Trace("Subscribing to channel events");
            SubscribeToService();
            SubscribeToNotifications();

            Dispatcher.BeginInvoke(() => UpdateStatus("Channel created successfully"));
        }

        void httpChannel_ExceptionOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            Dispatcher.BeginInvoke(() => UpdateStatus(e.ErrorType + " occurred: " + e.Message));
        }

        void httpChannel_HttpNotificationReceived(object sender, HttpNotificationEventArgs e)
        {

            //Trace("===============================================");
            //Trace("RAW notification arrived:");

            //string weather, location, temperature;
            //ParseRAWPayload(e.Notification.Body, out weather, out location, out temperature);

            //Dispatcher.BeginInvoke(() => this.textBlockListTitle.Text = location);
            //Dispatcher.BeginInvoke(() => this.txtTemperature.Text = temperature);
            //Dispatcher.BeginInvoke(() => this.imgWeatherConditions.Source = new BitmapImage(new Uri(@"Images/" + weather + ".png", UriKind.Relative)));
            //Trace(string.Format("Got weather: {0} with {1}F at location {2}", weather, temperature, location));

            //Trace("===============================================");
        }
        void httpChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            Trace("===============================================");
            Trace("Toast/Tile notification arrived:");
            foreach (var key in e.Collection.Keys)
            {
                string msg = e.Collection[key];
                Trace(msg);
                Dispatcher.BeginInvoke(() => UpdateStatus("Toast/Tile message: " + msg));
            }

            Trace("===============================================");
        }
        #endregion

        #region Loading/Saving Channel Info
        private bool TryFindChannel()
        {
            bool bRes = false;

            Trace("Getting IsolatedStorage for current Application");
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                Trace("Checking channel data");
                if (isf.FileExists(fileName))
                {
                    Trace("Channel data exists! Loading...");
                    using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream(fileName, FileMode.Open, isf))
                    {
                        using (StreamReader sr = new StreamReader(isfs))
                        {
                            string uri = sr.ReadLine();
                            Trace("Finding channel");
                            httpChannel = HttpNotificationChannel.Find(channelName);

                            if (null != httpChannel)
                            {
                                if (httpChannel.ChannelUri.ToString() == uri)
                                {
                                    Dispatcher.BeginInvoke(() => UpdateStatus("Channel retrieved"));
                                    SubscribeToChannelEvents();
                                    SubscribeToService();
                                    bRes = true;
                                }

                                sr.Close();
                            }
                        }
                    }
                }
                else
                    Trace("Channel data not found");
            }

            return bRes;
        }

        private void SaveChannelInfo()
        {
            Trace("Getting IsolatedStorage for current Application");
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                Trace("Creating data file");
                using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream(fileName, FileMode.Create, isf))
                {
                    using (StreamWriter sw = new StreamWriter(isfs))
                    {
                        Trace("Saving channel data...");
                        sw.WriteLine(httpChannel.ChannelUri.ToString());
                        sw.Close();
                        Trace("Saving done");
                    }
                }
            }
        }
        #endregion
        #endregion
   
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {


            //Storyboard1.AutoReverse = true;
            //Storyboard1.BeginTime=new TimeSpan(0, 0, 0,0, 200);
            //Storyboard1.Duration = new Duration(new TimeSpan(0, 0, 0, 1000));
            //Storyboard1.FillBehavior = FillBehavior.Stop;
            //Storyboard1.Begin();
           // Storyboard1.AutoReverse = true;
            //  Storyboard1.Duration = new Duration(new TimeSpan(0,0,0,1));

            //Storyboard1.Begin();
            //Storyboard1.SkipToFill();
           Storyboard3.Begin();
           Storyboard3.BeginTime = new TimeSpan(0, 0, 0, 0,600);
           while (NavigationService.BackStack.Any())
           {
               NavigationService.RemoveBackEntry();
           }
           
           base.OnNavigatedTo(e);

        }

        #region[Tile_Tab]
        private void Tile1_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PostPage.xaml", UriKind.Relative));
            //Storyboard1.AutoReverse = true;
            //  Storyboard1.Duration = new Duration(new TimeSpan(0,0,0,1));

            Storyboard1.Begin();
            //Storyboard1.SkipToFill();

        }
        private void tile5_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //Storyboard4.AutoReverse = true; 
            Storyboard4.Begin();
            NavigationService.Navigate(new Uri("/Views/DataPage1.xaml", UriKind.Relative));
        }
        private void tile2_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/FriendPage.xaml", UriKind.Relative));
        }
        private void tile3_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/RecyclePage.xaml?type=recycle", UriKind.Relative));
        }
        private void tile4_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //Storyboard2.AutoReverse = true;
           // Storyboard2.Duration = new Duration(new TimeSpan(0, 0, 0, 1));
            Storyboard2.Begin();
            NavigationService.Navigate(new Uri("/Views/RewardPage.xaml", UriKind.Relative));
        }

        private void tile7_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Storyboard5.Begin();
            NavigationService.Navigate(new Uri("/Views/MessagePage.xaml", UriKind.Relative));
        }

        private void tile8_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Storyboard6.Begin();
            NavigationService.Navigate(new Uri("/Views/VolunteerPage.xaml", UriKind.Relative));
        }
        #endregion

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            while (NavigationService.BackStack.Any())
            {
                NavigationService.RemoveBackEntry();
            }
            MessageBoxResult result= MessageBox.Show("Are you sure to quit!", "Exit", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }


    }
}