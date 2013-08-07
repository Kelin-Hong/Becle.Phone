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
using System.Collections.ObjectModel;
using Panda.Phone.Models;
using Microsoft.Phone.Notification;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using Panda.Phone.ServiceReference;
using Panda.Phone.Views;
namespace Panda.Phone.ViewModels
{
    public class RecycleActivityViewModel:INotifyPropertyChanged
    {
      
        private string chanelStatue;
    
        public RecycleActivityViewModel()
        {
            //List<RecycleActivityModel> list = new List<RecycleActivityModel>() {
            //new RecycleActivityModel(){RecycleName="hkl",Address="hhhhhhhhhhhh",StartTime="2011.10.11", EndTime="2011.11.11"},
            // new RecycleActivityModel(){RecycleName="hsl",Address="hhhhhhhhhhhh",StartTime="2011.10.11", EndTime="2011.11.11"},
            //  new RecycleActivityModel(){RecycleName="ssl",Address="hhhhhhhhdddd",StartTime="2011.09.11", EndTime="2011.12.11"}
            //};
            //RecycleList = new ObservableCollection<RecycleActivityModel>(list);
           
           
        }
        //public ObservableCollection<RecycleActivityModel> RecycleList { set; get; }
         public ObservableCollection<RecycleActivity> RecycleList { set; get; }
          public string ChanelStatue 
        {
            set 
            { 
                chanelStatue = value;
                OnPropertyChanged("ChanelStatue");
            } 
            
            get 
            { 
                return chanelStatue; 
            } 
        }
          private void OnPropertyChanged(string property)
          {
              if (PropertyChanged != null)
              {
                  PropertyChanged(this, new PropertyChangedEventArgs(property));
              }
          }
       
      
        //private void UpdateStatus(string message)
        //{
        //    txtStatus.Text = message;
        //}
      
     

    
        public event PropertyChangedEventHandler  PropertyChanged;
   }
}
