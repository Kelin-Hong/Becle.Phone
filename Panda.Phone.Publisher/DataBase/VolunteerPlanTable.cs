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
    public class VolunteerPlanTable : INotifyPropertyChanging, INotifyPropertyChanged
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


        private int acquirerId;
        [Column]
        public int AcquirerId
        {
            get { return acquirerId; }
            set
            {
                if (acquirerId != value)
                {
                    OnPropertyChanging("AcquirerId");
                    acquirerId = value;
                    OnPropertyChanged("AcquirerId");
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
        private bool isFinish;
        [Column]
        public bool IsFinish
        {
            get { return isFinish; }
            set
            {
                if (isFinish != value)
                {
                    OnPropertyChanging("IsFinish");
                    isFinish = value;
                    OnPropertyChanged("IsFinish");
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
