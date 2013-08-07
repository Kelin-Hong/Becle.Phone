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
    public class AchievementTrendTable: INotifyPropertyChanging, INotifyPropertyChanged
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
        private string imageUri;
        [Column]
        public string ImageUri
        {
            get { return imageUri; }
            set
            {
                if (imageUri != value)
                {
                    OnPropertyChanging("ImageUri");
                    imageUri = value;
                    OnPropertyChanged("ImageUri");
                }
            }
        }
        private int userId;
        [Column]
        public int UserId
        {
            get { return userId; }
            set
            {
                if (userId != value)
                {
                    OnPropertyChanging("UserId");
                    userId = value;
                    OnPropertyChanged("UserId");
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

        private string name;
        [Column]
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    OnPropertyChanging("Name");
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string category;
        [Column]
        public string Category
        {
            get { return category; }
            set
            {
                if (category != value)
                {
                    OnPropertyChanging("Category");
                    category = value;
                    OnPropertyChanged("Category");
                }
            }
        }
        private string achievement;
        [Column]
        public string Achievement
        {
            get { return achievement; }
            set
            {
                if (achievement != value)
                {
                    OnPropertyChanging("Achievement");
                    achievement = value;
                    OnPropertyChanged("Achievement");
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
