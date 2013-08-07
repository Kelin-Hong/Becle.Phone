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
using Panda.Phone.ServiceReference;
using Panda.Phone.ViewModels;
namespace Panda.Phone.Views
{
    public partial class RecycleFriend : PhoneApplicationPage
    {
        
        RecycleFriendViewModel vm;
        public RecycleFriend()
        {
            vm = new RecycleFriendViewModel();
            InitializeComponent();
            //vm = new RecycleFriendViewModel();
            PhoneServiceClient client = new PhoneServiceClient();
           
            client.getRecycleFriendListAsync();
            client.getRecycleFriendListCompleted += new EventHandler<getRecycleFriendListCompletedEventArgs>(client_getRecycleFriendListCompleted);
        }

        void client_getRecycleFriendListCompleted(object sender, getRecycleFriendListCompletedEventArgs e)
        {
            vm.ChannelStatus = "Success";
            vm.RecyeleFriendList = e.Result;
            Dispatcher.BeginInvoke(()=> this.DataContext = vm);
            Dispatcher.BeginInvoke(() => this.txtStatus.Text = "Success");

        }
    }
}