

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Becle.Server.Service
{
    public class RegistrationService : IRegistrationService
    {
        #region[PushService]
        int count = 0;
        public void UnregisterUri(string uri, string id)
        {
            Uri channelUri = new Uri(uri, UriKind.Absolute);
            Unsubscribe(channelUri, Int32.Parse(id));
        }
        public void RegisterUri(string uri, string id)
        {
            Console.WriteLine("Uri Receive");
            Uri _uri = new Uri(uri);
            Subscribe(_uri, Int32.Parse(id));
        }
        public static event EventHandler<SubscriptionEventArgs> Subscribed;
        private static Dictionary<int, Uri> subscribers = new Dictionary<int, Uri>();
        private static object obj = new object();
        private void Subscribe(Uri channelUri, int id)
        {
            
            lock (obj)
            {
                if (!subscribers.Keys.Contains(id))
                {
                    subscribers.Add(id, channelUri);
                    Console.WriteLine(id + " Subcribe");
                }
            }
            OnSubscribed(channelUri, true);
        }
        public static void Unsubscribe(Uri channelUri, int id)
        {
            lock (obj)
            {
                subscribers.Remove(id);
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
        public static Dictionary<int, Uri> GetSubscribers()
        {
            return subscribers;
        }
        #endregion   
   
        public string GetMessage(string id)
        {
            Console.WriteLine("Ke"+id);
            if (id == "1")
            {
                count++;
            }
            if (id == "Hello")
            {
                return count+"";
            }
            if (id == "-1")
            {
                count--;
            }
            return "";
        }
    }
}