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
    public class CategoryTable : INotifyPropertyChanged, INotifyPropertyChanging
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

        private int num;
        [Column]
        public int Num
        {
            get { return num; }
            set
            {
                if (num != value)
                {
                    OnPropertyChanging("Num");
                    num = value;
                    OnPropertyChanged("Num");
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
