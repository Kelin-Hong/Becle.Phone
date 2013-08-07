using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Becle.Server.Service;
namespace Becle.Server.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(PublisherService));
            host.Open();
            ServiceHost host1 = new ServiceHost(typeof(RegistrationService));
            host1.Open();
            Console.WriteLine("Service Begin");
            // PhoneService.Subscribed += new EventHandler<PhoneService.SubscriptionEventArgs>(PhoneService_Subscribed);
            // new DealWith().subscribe();
            Console.ReadLine();
        }
    }
}
