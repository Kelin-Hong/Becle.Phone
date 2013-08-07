using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Panda.Phone.Server;
namespace Panda.Phone.ConcoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(PhoneService));
            host.Open();
            ServiceHost host1 = new ServiceHost(typeof(AcquirerService));
            host1.Open();
            Console.WriteLine("Service Begin");
            // PhoneService.Subscribed += new EventHandler<PhoneService.SubscriptionEventArgs>(PhoneService_Subscribed);
           // new DealWith().subscribe();
            Console.ReadLine();
        }
    }
}
