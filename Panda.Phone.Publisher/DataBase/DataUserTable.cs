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
    public class DataUserTable : INotifyPropertyChanging, INotifyPropertyChanged
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
        private string name;
        [Column]
        public string UserName
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    OnPropertyChanging("UserName");
                    name = value;
                    OnPropertyChanged("UserName");
                }
            }
        }

        private string city;
        [Column]
        public string City
        {
            get { return city; }
            set
            {
                if (city != value)
                {
                    OnPropertyChanging("City");
                    city = value;
                    OnPropertyChanged("City");
                }
            }
        }

        private int point;
        [Column]
        public int Point
        {
            get
            { return point; }
            set
            {
                if (point != value)
                {
                    OnPropertyChanging("Point");
                    point = value;
                    OnPropertyChanged("Point");
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
