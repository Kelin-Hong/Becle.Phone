using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using Panda.Phone.ServiceReference;
using System.Collections.ObjectModel;
namespace Panda.Phone.ViewModels
{
     public class RecycleFriendViewModel:INotifyPropertyChanged
    {
        private string channelStatus;
        public string ChannelStatus
        {
            set
            {
                channelStatus = value;
                OnPropertyChanged("ChannelStatus");
            }
            get
            {
                return channelStatus;
            }
        }
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public ObservableCollection<RecycleFriends> RecyeleFriendList { set; get; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
