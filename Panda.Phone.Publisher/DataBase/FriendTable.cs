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
using System.Data.Linq.Mapping;

namespace Panda.Phone.Publisher.DataBase
{
   [Table]
    public class FriendTable : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private int id;
        [Column(IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false, AutoSync = AutoSync.OnInsert, DbType = "INT NOT NULL Identity")]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                OnPropertyChanging("Id");
                id = value;
                OnPropertyChanged("Id");
            }
        }


        private int userid;
        [Column]
        public int UserId
        {
            get { return userid; }
            set
            {
                if (userid != value)
                {
                    OnPropertyChanging("UserId");
                    userid = value;
                    OnPropertyChanged("UserId");
                }
            }
        }

        private int friendId;
        [Column]
        public int FriendId
        {
            get { return friendId; }
            set
            {
                if (friendId != value)
                {
                    OnPropertyChanging("FriendId");
                    friendId = value;
                    OnPropertyChanged("FriendId");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        private void OnPropertyChanging(string property)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(property));
            }
        }
    }
}
