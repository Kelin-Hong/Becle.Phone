using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhone.PushNotificationManager;
using Panda.Phone.DataModel;
namespace Panda.Phone.Server
{
    public class PhoneService:IPhoneService
    {
        #region[Register]
        public void Register(string uri)
        {
            Console.WriteLine("Uri Receive");
            Uri _uri = new Uri(uri);
            Subscribe(_uri);
        }
        public static event EventHandler<SubscriptionEventArgs> Subscribed;
        private static List<Uri> subscribers = new List<Uri>();
        private static object obj = new object();
        private void Subscribe(Uri channelUri)
        {
            Console.WriteLine("Subcribe");
            lock (obj)
            {
                if (!subscribers.Exists((u) => u == channelUri))
                {
                    subscribers.Add(channelUri);
                }
            }
            OnSubscribed(channelUri, true);
        }
        public static void Unsubscribe(Uri channelUri)
        {
            lock (obj)
            {
                subscribers.Remove(channelUri);
            }
            OnSubscribed(channelUri, false);
        }
        private static void OnSubscribed(Uri channelUri, bool isActive)
        {
            EventHandler<SubscriptionEventArgs> handler = Subscribed;
            if (handler != null)
            {
                handler(null,
                  new SubscriptionEventArgs(channelUri, isActive));
            }
        }
        public class SubscriptionEventArgs : EventArgs
        {
            public SubscriptionEventArgs(Uri channelUri, bool isActive)
            {
                this.ChannelUri = channelUri;
                this.IsActive = isActive;
            }

            public Uri ChannelUri { get; private set; }
            public bool IsActive { get; private set; }
        }



        public static List<Uri> GetSubscribers()
        {
            return subscribers;
        }
        #endregion
        List<RecycleActivitys> IPhoneService.getRecycleActivityList(string uri)
        {
            List<RecycleActivitys> list = new List<RecycleActivitys>() {
            new RecycleActivitys(){LocationList=null,StartTime=new DateTime(2011,10,11),EndTime=new DateTime(2011,11,11),Name="hkl"},
            new RecycleActivitys(){LocationList=null,StartTime=new DateTime(2011,10,11),EndTime=new DateTime(2011,11,11),Name="hkl"},
            new RecycleActivitys(){LocationList=null,StartTime=new DateTime(2011,10,11),EndTime=new DateTime(2011,11,11),Name="hkl"},
            new RecycleActivitys(){LocationList=null,StartTime=new DateTime(2011,10,11),EndTime=new DateTime(2011,11,11),Name="hkl"}
           
            };
            return list;
        }



       


        public List<RecycleFriends> getRecycleFriendList()
        {
            List<RecycleActivitys> activityList = new List<RecycleActivitys>() {
            new RecycleActivitys(){LocationList=null,StartTime=new DateTime(2011,10,11),EndTime=new DateTime(2011,11,11),Name="hkl"},
            new RecycleActivitys(){LocationList=null,StartTime=new DateTime(2011,10,11),EndTime=new DateTime(2011,11,11),Name="hkl"},
            new RecycleActivitys(){LocationList=null,StartTime=new DateTime(2011,10,11),EndTime=new DateTime(2011,11,11),Name="hkl"},
             };
            List<RecycleFriends> list = new List<RecycleFriends>() 
            { 
               new RecycleFriends(){Name="hkb",Image="",LatestActivity="beijing shi qu",ActivityList=activityList},
               new RecycleFriends(){Name="hwp",Image="",LatestActivity="changjiang shi qu",ActivityList=activityList}
               
            };
            return list;
        }


        public List<Adrress> getAddressByName(string name)
        {
            List<Adrress> list = new List<Adrress> 
            { new Adrress(){Longitude=10,Latitude=15},
                new Adrress(){Longitude=11,Latitude=14},
                new Adrress(){Longitude=12,Latitude=13},
                new Adrress(){Longitude=13,Latitude=12},
                new Adrress(){Longitude=14,Latitude=11},
                new Adrress(){Longitude=15,Latitude=10},
            
            };
            return list;
        }
    }
}
