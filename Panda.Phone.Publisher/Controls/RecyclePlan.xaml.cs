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
using Panda.Phone.Publisher.DataBase;
using Panda.Phone.Publisher.ViewModel;
using System.Collections.ObjectModel;
using Panda.Phone.Publisher.Model;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using Panda.Phone.Acquirer;
using Panda.Phone.Publisher.PublisherServiceReference;
namespace Panda.Phone.Publisher.Controls
{
    public partial class RecyclePlan : UserControl
    {
        Page page;
        RecyclePlanVM vm;
        public RecyclePlan(Page _page)
        {
            InitializeComponent();
            page = _page;
            vm= new RecyclePlanVM();
            this.lb_Unfinished.ItemsSource = vm.Dic_Unfinish;
            this.lb_History.ItemsSource = vm.Dic_History;
            //this.lb_Unfinished.ItemsSource = vm.ListPlan;
           // this.lb_Unfinished.DataContext = vm;
        }
        private void btn_SeeInMap_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //MessageBox.Show("Tab");
            if (e.OriginalSource is Image)
            {
                MessageBox.Show("Image");
            }
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            RecyclePlanModel model = ((Image)sender).DataContext as RecyclePlanModel;  
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            RecyclePlanModel model = ((Image)sender).DataContext as RecyclePlanModel;
            if (model.IsCheck == false)
            {
                ((Image)sender).Source = new BitmapImage(new Uri("/Image_Volunteer/X.png", UriKind.Relative));
            }
            else
            {
                ((Image)sender).Source = new BitmapImage(new Uri("/Image_Volunteer/finished.png", UriKind.Relative));
            }

        }

        private void btn_Route_Click(object sender, RoutedEventArgs e)
        {  
           MenuItem menuItem = (MenuItem)sender;
           ((AcquirerPage)page).list_User_Choosed=vm.getChooseUser((((KeyValuePair<string, ObservableCollection<RecyclePlanModel>>)menuItem.DataContext)).Value);
           ((AcquirerPage)page).GenerateRoute();
        }

        private void btn_Notice_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            foreach(UserModel model in vm.getChooseUser((((KeyValuePair<string, ObservableCollection<RecyclePlanModel>>)menuItem.DataContext)).Value))
            {
                MessageToUser message1 = new MessageToUser()
                {
                    FromId = ((AcquirerPage)page).acquirerId,
                    ToId = model.Id,
                    Time = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(),
                    Type = 1,
                };
                message1.MessageContent = ((AcquirerPage)page).vm.acquirerInfo.BinName + " Recycler ";
                message1.MessageContent += "has arrived at your community! now you can take you resourse to recycler!";
                ((AcquirerPage)page).client.SendMessageToUserAsync(message1);
            }
           
        }

        private void sp_UserCheck_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
          MessageBoxResult  result=  MessageBox.Show("Do you want to check the user's resourse have been recycled!","Check",MessageBoxButton.OKCancel);
          if (result == MessageBoxResult.OK)
          {
              StackPanel sp = (StackPanel)sender;
              (sp.Children[0] as Image).Source = new BitmapImage(new Uri("/Image_Volunteer/finished.png", UriKind.Relative));
              RecyclePlanModel model = ((StackPanel)sender).DataContext as RecyclePlanModel;
              MessageToUser message1 = new MessageToUser()
              {
                  FromId = ((AcquirerPage)page).acquirerId,
                  ToId = model.UserId,
                  Time = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(),
                  Type = 2,
              };
              vm.db.RecyclePlanShips.First(c => c.Id == model.ShipId).IsCheck = true;
              message1.MessageContent = ((AcquirerPage)page).vm.acquirerInfo.BinName + " recycle";
              message1.MessageContent += " resourse in your area,Do you want to confirm that these resourses have been recycled?";
              ((AcquirerPage)page).client.SendMessageToUserAsync(message1);
          }
          else
          {
              RecyclePlanModel model = ((StackPanel)sender).DataContext as RecyclePlanModel;
              vm.db.RecyclePlanShips.First(c => c.Id == model.ShipId).IsCheck = false;
              StackPanel sp = (StackPanel)sender;
              (sp.Children[0] as Image).Source = new BitmapImage(new Uri("/Image_Volunteer/X.png", UriKind.Relative));
          }

        }

      

        
    }
}
