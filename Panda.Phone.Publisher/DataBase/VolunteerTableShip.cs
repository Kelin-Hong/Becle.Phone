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
    public class VolunteerPlanShipTable : INotifyPropertyChanging, INotifyPropertyChanged
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

        private bool isCheck;
        [Column]
        public bool IsCheck
        {
            get { return isCheck; }
            set
            {
                if (isCheck != value)
                {
                    OnPropertyChanging("IsCheck");
                    isCheck = value;
                    OnPropertyChanged("IsCheck");
                }
            }
        }
        private int planId;
        [Column]
        public int PlanId
        {
            get { return planId; }
            set
            {
                if (planId != value)
                {
                    OnPropertyChanging("PlanId");
                    planId = value;
                    OnPropertyChanged("PlanId");
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
