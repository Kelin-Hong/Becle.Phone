using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panda.Phone.Service.Service
{
    class PhoneService:IPhoneService
    {
       
        public void getRecycleActivityList(string _uri)
        {
            Console.WriteLine("Uri Receive");
            Uri uri = new Uri(_uri);
            Subscribe(uri);
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




        public void PutData(MyData data)
        {
            Console.WriteLine("OK");
        }





        public void PutCustomers(string id, Customer customer)
        {
            Console.WriteLine("OK");
        }
    }
    
}
