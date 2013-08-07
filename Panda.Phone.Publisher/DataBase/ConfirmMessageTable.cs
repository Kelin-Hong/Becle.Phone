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
using System.Data.Linq.Mapping;
using System.ComponentModel;

namespace Panda.Phone.Publisher.DataBase
{
    [Table]
    public class ConfirmMessageTable : INotifyPropertyChanging, INotifyPropertyChanged
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


        private int fromid;
        [Column]
        public int FromId
        {
            get { return fromid; }
            set
            {
                if (fromid != value)
                {
                    OnPropertyChanging("FromId");
                    fromid = value;
                    OnPropertyChanged("FromId");
                }
            }
        }

        private int toId;
        [Column]
        public int ToId
        {
            get { return toId; }
            set
            {
                if (toId != value)
                {
                    OnPropertyChanging(" ToId");
                    toId = value;
                    OnPropertyChanged(" ToId");
                }
            }
        }

        private string time;
        [Column]
        public string Time
        {
            get { return time; }
            set
            {
                if (time != value)
                {
                    OnPropertyChanging("Time");
                    time = value;
                    OnPropertyChanged("Time");
                }
            }
        }
        private string title;
        [Column]
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    OnPropertyChanging("Title");
                    title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        private string message;
        [Column]
        public string Message
        {
            get { return message; }
            set
            {
                if (message != value)
                {
                    OnPropertyChanging("Message");
                    message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        private bool isSee;
        [Column]
        public bool IsSee
        {
            get { return isSee; }
            set
            {
                if (isSee != value)
                {
                    OnPropertyChanging("isSee");
                    isSee = value;
                    OnPropertyChanged("isSee");
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
