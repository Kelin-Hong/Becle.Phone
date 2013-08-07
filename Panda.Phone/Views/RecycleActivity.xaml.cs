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
using System.Xml;
using System.Xml.Linq;
using Panda.Phone.ViewModels;
using Panda.Phone.Models;
using System.Collections.ObjectModel;
using Panda.Phone.ServiceReference;
namespace Panda.Phone.Views
{
   
    public partial class RecycleActivity : PhoneApplicationPage
    {

        //private HttpNotificationChannel httpChannel;
        //private const string channelName = "WeatherUpdatesChannel";
        //private const string fileName = "PushNotificationsSettings.dat";
        //private const int pushConnectTimeout = 30;
        RecycleActivityViewModel vm; 
  
        public RecycleActivity()
        {
            vm = new RecycleActivityViewModel();
            InitializeComponent();
            PhoneServiceClient client = new PhoneServiceClient();
            client.getRecycleActivityListAsync("");
            client.getRecycleActivityListCompleted += new EventHandler<getRecycleActivityListCompletedEventArgs>(client_getRecycleActivityListCompleted);
            //this.DataContext = vm;
          //  if (!tryFindChanel())
              //  DoConnect();
          
        }

        void client_getRecycleActivityListCompleted(object sender, getRecycleActivityListCompletedEventArgs e)
        {

           // vm.RecycleList = e.Result;
            vm.ChanelStatue = "Success";
            Dispatcher.BeginInvoke(() => this.DataContext = vm);
        }

        private void activityList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           List<RecycleActivitys> list = (List<RecycleActivitys>)e.AddedItems;
           RecycleActivitys item = list[0];
           NavigationService.Navigate(new Uri(string.Format("/Views/RecycleMap.xaml?id={0}", item.Name)));
        }

        //private void DoConnect()
        //{
        //   try
        //    {
        //    httpChannel = HttpNotificationChannel.Find(channelName);
        //    if (httpChannel != null)
        //    {

        //        httpChannel.Close();
        //        // Dispatcher.BeginInvoke(() => { vm.ChanelStatue = "Channel retrieved";  });
        //        //Dispatcher.BeginInvoke(() => txtStatus.Text = "Channel retrieved");
        //        //SubScribeEvent();
        //        //sendtoWCFService();


        //    }
            
        //        httpChannel = new HttpNotificationChannel(channelName, "PhoneService");
        //        SubScribeEvent();
        //        httpChannel.Open();
        //        Collection<Uri> uris = new Collection<Uri>();
        //        uris.Add(new Uri("http://jquery.andreaseberhard.de/pngFix/pngtest.png"));

        //        httpChannel.BindToShellTile(uris);
            
        //   }
        //    catch (Exception ex)
        //    {
        //        Dispatcher.BeginInvoke(() => txtStatus.Text="Channel error: " + ex.Message);
        //    }
        //}
        //private bool tryFindChanel()
        //{
        //    bool bRes = false;
        //        using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
        //        {
        //            if (iso.FileExists(fileName))
        //            {
        //                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileName, FileMode.Open, iso))
        //                {
        //                    using (StreamReader reader = new StreamReader(stream))
        //                    {
        //                        string uri = reader.ReadLine();

        //                        httpChannel = HttpNotificationChannel.Find(channelName);
        //                        if (httpChannel != null)
        //                        {
        //                            //httpChannel.Close();
        //                            if (httpChannel.ChannelUri.ToString() == uri)
        //                             {
        //                                //Dispatcher.BeginInvoke(() => UpdateStatus("Channel retrieved"));
        //                               //  Dispatcher.BeginInvoke(() =>vm.ChanelStatue = "Channel retrieved");
        //                                Dispatcher.BeginInvoke(() => txtStatus.Text = "Channel retrieved");
        //                                SubScribeEvent();
        //                                sendtoWCFService();
        //                                bRes = true;
        //                            }
        //                            reader.Close();
        //                        }
        //                    }

        //                }
        //            }



        //        }
        //        return bRes;
        //}

        //private void ParseRAWPayload(Stream e)
        //{
        //    XDocument document;

        //    using (var reader = new StreamReader(e))
        //    {
        //        string payload = reader.ReadToEnd().Replace('\0',
        //          ' ');
        //        document = XDocument.Parse(payload);
        //    }
        //    List<RecycleActivityModel> list = new List<RecycleActivityModel>();
        //    for (int i = 0; i < 3; i++)
        //    {
        //        RecycleActivityModel model = new RecycleActivityModel();
        //        model.RecycleName = (from c in document.Descendants("ActivityService")
        //                             select c.Elements("RecycleActivity").ElementAtOrDefault(i).Element("RecycleMan").Value).FirstOrDefault();
        //        model.Address = (from c in document.Descendants("ActivityService")
        //                         select c.Elements("RecycleActivity").ElementAtOrDefault(i).Element("Address").Value).FirstOrDefault();
        //        model.StartTime = (from c in document.Descendants("ActivityService")
        //                           select c.Elements("RecycleActivity").ElementAtOrDefault(i).Element("StartTime").Value).FirstOrDefault();
        //        model.EndTime = (from c in document.Descendants("ActivityService")
        //                         select c.Elements("RecycleActivity").ElementAtOrDefault(i).Element("EndTime").Value).FirstOrDefault();
        //        list.Add(model);

        //    }
        //    // vm.RecycleList = new ObservableCollection<RecycleActivityModel>(list);
        //    // Dispatcher.BeginInvoke(() => this.DataContext = vm);

        //    //location = (from c in document.Descendants("WeatherUpdate")
        //    //            select c.Element("Location").Value).FirstOrDefault();
        //    //Trace("Got location: " + location);

        //    //temperature = (from c in document.Descendants("WeatherUpdate")
        //    //               select c.Element("Temperature").Value).FirstOrDefault();
        //    //Trace("Got temperature: " + temperature);

        //    //weather = (from c in document.Descendants("WeatherUpdate")
        //    //           select c.Element("WeatherType").Value).FirstOrDefault();
        //}

        //private void SubScribeEvent()
        //{
        //    httpChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(httpChannel_ChannelUriUpdated);
        //    httpChannel.HttpNotificationReceived += new EventHandler<HttpNotificationEventArgs>(httpChannel_HttpNotificationReceived);
        //    httpChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(httpChannel_ErrorOccurred);
        //    httpChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(httpChannel_ShellToastNotificationReceived);
        //}

        //void httpChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //void httpChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //void httpChannel_HttpNotificationReceived(object sender, HttpNotificationEventArgs e)
        //{
        //    ParseRAWPayload(e.Notification.Body);
            
        //}

        //void httpChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        //{

        //    sendtoWCFService();
        //    saveChannelUri();
        //    //  Dispatcher.BeginInvoke(() => UpdateStatus("Channel created successfully"));
        //    // Dispatcher.BeginInvoke(() =>vm.ChanelStatue = "Channel created successfully");
        //    Dispatcher.BeginInvoke(() => txtStatus.Text = "Channel created successfully");
        //}

        //private void saveChannelUri()
        //{
        //    using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
        //    {
        //        using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileName, FileMode.Create, iso))
        //        {
        //            using (StreamWriter writer = new StreamWriter(stream))
        //            {
        //                writer.WriteLine(httpChannel.ChannelUri.ToString());
        //                writer.Close();
        //            }
        //        }

        //    }
        //}

        //private void sendtoWCFService()
        //{
        //    string baseUri = "http://localhost:8080/PhoneService/getRecycleActivityList?_uri={0}";
        //    string theUri = String.Format(baseUri, httpChannel.ChannelUri.ToString());
        //    WebClient client = new WebClient();
        //    client.DownloadStringCompleted += (s, e) =>
        //    {
        //        if (null == e.Error)
        //            //Dispatcher.BeginInvoke(() => UpdateStatus("Registration succeeded"));
        //            //  Dispatcher.BeginInvoke(() =>vm.ChanelStatue = "Registration succeeded");
        //            Dispatcher.BeginInvoke(() => txtStatus.Text = "Registration succeeded");
        //        else
        //            // Dispatcher.BeginInvoke(() => UpdateStatus("Registration failed: " + e.Error.Message));
        //            // Dispatcher.BeginInvoke(() =>vm.ChanelStatue = "Registration failed: " + e.Error.Message);
        //            Dispatcher.BeginInvoke(() => txtStatus.Text = "Registration failed: " + e.Error.Message);
        //    };
        //    client.DownloadStringAsync(new Uri(theUri));
        //    // Uri uri=new Uri("http://localhost:8080/PhoneService/PutData");
        //   // client.OpenWriteAsync(uri,null,null);
        //   /// Uri uri = new Uri("http://localhost:8080/PhoneService/customer/5");
        //     //client.OpenWriteAsync(uri,null,null);
        //    // client.OpenWriteCompleted += new OpenWriteCompletedEventHandler(client_OpenWriteCompleted);
        //   // client.UploadStringAsync(uri,"ddd");
        //    //client.UploadStringCompleted += new UploadStringCompletedEventHandler(client_UploadStringCompleted);
        //}


       

      
    }
}