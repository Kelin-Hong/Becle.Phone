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
namespace Panda.Phone.Models
{
    public class RecycleActivityModel:INotifyPropertyChanged
    {
        private string recycleName;
        private string address;
        private string atartTime ;
        private string endTime ;


        public string RecycleName 
        {
            set{
                recycleName = value;
                OnPropertyChanged("RecycleName");
               }
            get 
               {
                return recycleName; 
               }
        }
        public string Address { set; get; }
        public string StartTime { set; get; }
        public string EndTime { set; get; }
      

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
