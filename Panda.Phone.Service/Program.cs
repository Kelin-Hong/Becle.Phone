using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhone.PushNotificationManager;
using Panda.Phone.Service.Service;
using System.IO;
using System.Xml;
using System.ServiceModel;
using System.Threading;
namespace Panda.Phone.Service
{
    public class ActivityModel
    {
        public string Address{set;get;}
        public string RecycleMan{set;get;}
        public string StratTime{set;get;}
        public string EndTime{set;get;}

    }
    public class DealWith
    {
        private  NotificationSenderUtility send = new NotificationSenderUtility();
        public void subscribe()
        {
            PhoneService.Subscribed += new EventHandler<PhoneService.SubscriptionEventArgs>(PhoneService_Subscribed);
        }
        private void PhoneService_Subscribed(object sender, PhoneService.SubscriptionEventArgs e)
        {
            List<Uri> subscribers = new List<Uri>();
            subscribers.Add(e.ChannelUri);
            List<ActivityModel> list = new List<ActivityModel>() {
            new ActivityModel(){RecycleMan="hkl",Address="hhhhhhhhhhhh",StratTime="2011.10.11", EndTime="2011.11.11"},
            new ActivityModel(){RecycleMan="hsl",Address="hhhhhhhhhhhh",StratTime="2011.10.11", EndTime="2011.11.11"},
            new ActivityModel(){RecycleMan="ssl",Address="hhhhhhhhdddd",StratTime="2011.09.11", EndTime="2011.12.11"}
            };
            ThreadPool.QueueUserWorkItem((aa) => send.SendRawNotification(subscribers, prepareRAWPayload(list), null));
            if(true)
            ThreadPool.QueueUserWorkItem((a)=>send.SendTileNotification(subscribers,"PushNotificationsToken", "/Images/" + "Person" + ".png", 11,"RecycleActivity",null));
        }
        // private static byte[] prepareTilePayload(string tokenId, string backgroundImageUri, int count, string title)
        //{
        //    MemoryStream stream = new MemoryStream();
        //    XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, Encoding = Encoding.UTF8 };
        //    XmlWriter writer = XmlWriter.Create(stream, settings);
        //    writer.WriteStartDocument();
        //    writer.WriteStartElement("wp", "Notification", "WPNotification");
        //    writer.WriteStartElement("wp", "Tile", "WPNotification");
        //    writer.WriteStartElement("wp", "BackgroundImage", "WPNotification");
        //    writer.WriteValue(backgroundImageUri);
        //    writer.WriteEndElement();
        //    writer.WriteStartElement("wp", "Count", "WPNotification");
        //    writer.WriteValue(count.ToString());
        //    writer.WriteEndElement();
        //    writer.WriteStartElement("wp", "Title", "WPNotification");
        //    writer.WriteValue(title);
        //    writer.WriteEndElement();
        //    writer.WriteEndElement();
        //    writer.Close();

        //    byte[] payload = stream.ToArray();
        //    return payload;
        //}
        private static byte[] prepareRAWPayload(List<ActivityModel> list)
        {
            MemoryStream stream = new MemoryStream();

            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, Encoding = Encoding.UTF8 };
            XmlWriter writer = XmlTextWriter.Create(stream, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("ActivityService");
            foreach (ActivityModel a in list)
            {
                writer.WriteStartElement("RecycleActivity");

                writer.WriteStartElement("RecycleMan");
                writer.WriteValue(a.RecycleMan);
                writer.WriteEndElement();

                writer.WriteStartElement("Address");
                writer.WriteValue(a.Address);
                writer.WriteEndElement();

                writer.WriteStartElement("StartTime");
                writer.WriteValue(a.StratTime);
                writer.WriteEndElement();

                writer.WriteStartElement("EndTime");
                writer.WriteValue(a.EndTime);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
            byte[] payload = stream.ToArray();
            return payload;
        }
    }
    class Program
    {
       
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(PhoneService));
            host.Open();
            Console.WriteLine("Service Begin");
           // PhoneService.Subscribed += new EventHandler<PhoneService.SubscriptionEventArgs>(PhoneService_Subscribed);
            new DealWith().subscribe();
            Console.ReadLine();
        }
       
    }
}
