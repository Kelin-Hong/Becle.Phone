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
    public class RewardTable : INotifyPropertyChanging, INotifyPropertyChanged
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
        private string avatarUri;
        [Column]
        public string AvatarUri
        {
            get { return avatarUri; }
            set
            {
                if (avatarUri != value)
                {
                    OnPropertyChanging("AvatarUri");
                    avatarUri = value;
                    OnPropertyChanged("AvatarUri");
                }
            }
        }

        private string phone;
        [Column]
        public string Phone
        {
            get { return phone; }
            set
            {
                if (phone != value)
                {
                    OnPropertyChanging("Phone");
                    phone = value;
                    OnPropertyChanged("Phone");
                }
            }
        }
        private string storeName;
        [Column]
        public string StoreName
        {
            get { return storeName; }
            set
            {
                if (storeName != value)
                {
                    OnPropertyChanging("StoreName");
                    storeName = value;
                    OnPropertyChanged("StoreName");
                }
            }
        }

        private string summary;
        [Column]
        public string Summary
        {
            get { return summary; }
            set
            {
                if (summary != value)
                {
                    OnPropertyChanging("Summary");
                    summary = value;
                    OnPropertyChanged("Summary");
                }
            }
        }
        private int points;
        [Column]
        public int Points
        {
            get { return points; }
            set
            {
                if (points != value)
                {
                    OnPropertyChanging("Points");
                    points = value;
                    OnPropertyChanged("Points");
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

        private string address;
        [Column]
        public string Address
        {
            get { return address; }
            set
            {
                if (address != value)
                {
                    OnPropertyChanging("Address");
                    address = value;
                    OnPropertyChanged("Address");
                }
            }
        }


        private decimal  latitude;
        [Column]
        public decimal  Latitude
        {
            get { return latitude; }
            set
            {
                if (latitude != value)
                {
                    OnPropertyChanging("Latitude");
                    latitude = value;
                    OnPropertyChanged("Latitude");
                }

            }

        }

        private string describe;
        [Column]
        public string Describe
        {
            get { return describe; }
            set
            {
                if (describe != value)
                {
                    OnPropertyChanging("Describe");
                    describe = value;
                    OnPropertyChanged("Describe");
                }

            }

        }
        private string endTime;
        [Column]
        public string EndTime
        {
            get { return endTime; }
            set
            {
                if (endTime != value)
                {
                    OnPropertyChanging("EndTime");
                    endTime = value;
                    OnPropertyChanged("EndTime");
                }

            }

        }
        private decimal  longitude;
        [Column]
        public  decimal  Longitude
        {
            get { return longitude; }
            set
            {
                if (longitude != value)
                {
                    OnPropertyChanging("Longitude");
                    longitude = value;
                    OnPropertyChanged("Longitude");
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
