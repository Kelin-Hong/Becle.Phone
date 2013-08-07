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
    public class CityTable:INotifyPropertyChanging,INotifyPropertyChanged
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

        private int lastRank;
        [Column]
        public int LastRank
        {
            get
            { return lastRank; }
            set
            {
                if (lastRank != value)
                {
                    OnPropertyChanging("LastRank");
                    lastRank = value;
                    OnPropertyChanged("LastRank");
                }
            }
        }
        private int point;
         [Column]
         public int Point
         {
             get
             {return  point;}
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
