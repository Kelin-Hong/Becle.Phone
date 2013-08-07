using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Panda.Phone.Publisher.ViewModel;
namespace Panda.Phone.Publisher.Views
{
    public partial class RewardPage : PhoneApplicationPage
    {
        RewardVM vm;
        int userId;
        public RewardPage()
        {   
            InitializeComponent();
            userId=(App.Current as App).UserId;
            vm=new RewardVM(userId,null);
            lb_Reward.ItemsSource = vm.List_RewardModel;
        }

        private void lb_Reward_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_Exchange_Click(object sender, RoutedEventArgs e)
        {
          MessageBoxResult result=  MessageBox.Show("Are you sure to exchange?", "Check", MessageBoxButton.OKCancel);
          if (result == MessageBoxResult.OK)
          {
              string s="";
              Random r = new Random();
              for(int i=0;i<12;i++)
              {
                  s = s + r.Next(9);
              }
              MessageBox.Show("you key:" + s);
          }
        }
    }
}