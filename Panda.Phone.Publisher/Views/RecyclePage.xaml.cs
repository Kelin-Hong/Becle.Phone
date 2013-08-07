using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Panda.Phone.Publisher.ViewModel;
using Microsoft.Phone.Controls.Maps;
using Panda.Phone.Publisher.Model;
using System.Windows.Media.Imaging;
using System.Device.Location;
using Panda.Phone.Publisher.Controls;
using System.Threading;
using Microsoft.Phone.Shell;
using Panda.Phone.Publisher.PublisherServiceReference;
namespace Panda.Phone.Publisher.Views
{
    public partial class RecyclePage : PhoneApplicationPage
    {
        bool isbreak=false;
        bool normalView = true;
        internal MapLayer layer = new MapLayer();
        internal RecycleVM recycleVm;
        internal  RewardVM rewardVm;
        List<Image> list_Pin = new List<Image>();
        Recycle_Menu control_RecycleMenu;
        Reward_Menu control_RewardMenu;
        Recycle_Message control_Recycle_Message;
        Image pin_Me;
        ApplicationBarIconButton btn_Message;
        UserInfo userInfo;
        public RecyclePage()
        {
            InitializeComponent();
            int userId = (App.Current as App).UserId;
            userInfo = (App.Current as App).Userinfo;
            rewardVm = new RewardVM(userId,callback2);
            recycleVm = new RecycleVM(userId, callback1,callback3);
            if (recycleVm.List_Acquirers.Count != 0)
                callback1();
            if (rewardVm.List_RewardModel.Count != 0)
                callback2();
            map.Children.Add(layer);
            dealWithAppBar();
            MenuContent.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(MenuContent_ManipulationDelta);
            MenuContent.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(MenuContent_ManipulationCompleted);
        }

        #region[CallBack]
        void callback2()
        {
            if((bool)rb_Reward.IsChecked)
            foreach (RewardModel reward in rewardVm.List_RewardModel)
            {
                Image pin = new Image() { Source = new BitmapImage(new Uri("/Image_Recycle/gift.png", UriKind.Relative)) };
                pin.Width = 40;
                pin.Height = 40;
                pin.Tag = reward.Id;
                pin.DataContext = reward;
                layer.AddChild(pin, new GeoCoordinate(reward.Latitude, reward.Longitude));
                pin.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(pin_Tap);
                list_Pin.Add(pin);
            }
            control_RewardMenu = new Reward_Menu(this);
           
        }

        void callback1()
        {
            if ((bool)rb_Recycle.IsChecked)
            {
                foreach (AcquirerModel acquirer in recycleVm.List_Acquirers)
                {
                    Image pin = new Image() { Source = new BitmapImage(new Uri("/Image_Recycle/bubble.png", UriKind.Relative)) };
                    pin.Width = 40;
                    pin.Height = 40;
                    pin.Tag = acquirer.Id;
                    pin.DataContext = acquirer;
                    layer.AddChild(pin, new GeoCoordinate(acquirer.Latitude, acquirer.Longitude));
                    list_Pin.Add(pin);
                    pin.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(pin_Tap);
                }
            }
            control_RecycleMenu = new Recycle_Menu(this);
          
        }

        void callback3()
        {
            control_Recycle_Message = new Recycle_Message(this);
            RecycleVM vm= control_Recycle_Message.DataContext as RecycleVM;
            if (vm.List_Messages.Count != 0 || vm.List_ConfirmMessages.Count != 0)
            {
                Thread thread = new Thread(new ThreadStart(threadDo));
                thread.Start();
            }

        }
       #endregion

        private void btnViewChange_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            normalView = !normalView;
            if (!normalView)
            {
                map.Mode = new AerialMode();
                btnViewChange.Source = new BitmapImage(new Uri("/Image_Acquirer/nor.jpg", UriKind.Relative));
            }
            else
            {
                map.Mode = new RoadMode();
                btnViewChange.Source = new BitmapImage(new Uri("/Image_Acquirer/sat.jpg", UriKind.Relative));
            }
        }
        void  dealWithAppBar()
        {
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Mode = ApplicationBarMode.Default;
            ApplicationBar.Opacity = 0.5;
            ApplicationBar.IsVisible = true;
            ApplicationBar.ForegroundColor = new Color() { A = 255, B = 255, G = 255, R = 255 };
            ApplicationBar.BackgroundColor = new Color() { A = 255, B = 0, G = 0, R = 0 };
            btn_Message = new ApplicationBarIconButton();
            btn_Message.IconUri = new Uri("/Image_Recycle/Message/m_closed.png", UriKind.Relative);
            btn_Message.Text = "Message";
            ApplicationBar.Buttons.Add(btn_Message);
            btn_Message.Click+=new EventHandler(btn_Message_Click);

            ApplicationBarIconButton btn_History = new ApplicationBarIconButton();
            btn_History.IconUri = new Uri("/Image_Recycle/history.png", UriKind.Relative);
            btn_History.Text = "History";
            ApplicationBar.Buttons.Add(btn_History);

           
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string type = NavigationContext.QueryString["type"];
            if (type == "recycle") rb_Recycle.IsChecked = true;
            if (type == "reward") rb_Reward.IsChecked = true;
            
            base.OnNavigatedTo(e);
        }
         
        private void btn_RecyleList_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if(this.control_RecycleMenu!=null)
            this.control_RecycleMenu.Visibility = Visibility.Visible;
            MenuContent.Children.Clear();
            MenuContent.Children.Add(control_RecycleMenu);
            Storyboard1.Begin();
            grid_Content.IsHitTestVisible = false;
        }

        private void btn_RewardList_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if(this.control_RewardMenu!=null)
            this.control_RewardMenu.Visibility = Visibility.Visible;
            MenuContent.Children.Clear();
            MenuContent.Children.Add(control_RewardMenu);
            Storyboard1.Begin();
            grid_Content.IsHitTestVisible = false;
           
        }

        void MenuContent_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            if (e.FinalVelocities.LinearVelocity.Y < -500)
            {
                Storyboard2.Begin();
                grid_Content.IsHitTestVisible = true;
            }
        }
 
        void MenuContent_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            Grid grid = sender as Grid;
            CompositeTransform transform = grid.RenderTransform as CompositeTransform;
            if(e.DeltaManipulation.Translation.Y<0)
            transform.TranslateY += e.DeltaManipulation.Translation.Y;
            if (transform.TranslateY < 400)
            {
                Storyboard2.Begin();
                grid_Content.IsHitTestVisible = true;
            }
        }
            
        private void rb_Recycle_Checked(object sender, RoutedEventArgs e)
        {
            layer.Children.Clear();
            list_Pin.Clear();
            pin_Me = new Image() { Source = new BitmapImage(new Uri("/Image_Acquirer/man.png", UriKind.Relative)) };
            pin_Me.Width = 40;
            pin_Me.Height = 50;
           
            layer.AddChild(pin_Me, new GeoCoordinate((double)userInfo.Latitude,(double)userInfo.Longitude));
            list_Pin.Add(pin_Me);
            map.SetView(new GeoCoordinate((double)userInfo.Latitude, (double)userInfo.Longitude), 14);
            foreach (AcquirerModel acquirer in recycleVm.List_Acquirers)
            {
                Image pin = new Image() { Source = new BitmapImage(new Uri("/Image_Recycle/bubble.png", UriKind.Relative)) };
                pin.Width = 40;
                pin.Height = 40;
                pin.Tag = acquirer.Id;
                pin.DataContext = acquirer;
                layer.AddChild(pin, new GeoCoordinate(acquirer.Latitude, acquirer.Longitude));
                list_Pin.Add(pin);
                pin.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(pin_Tap);
            }
        }

        private void rb_Reward_Checked(object sender, RoutedEventArgs e)
        {
            layer.Children.Clear();
            list_Pin.Clear();
            pin_Me = new Image() { Source = new BitmapImage(new Uri("/Image_Acquirer/man.png", UriKind.Relative)) };
            pin_Me.Width = 40;
            pin_Me.Height = 50;
            layer.AddChild(pin_Me, new GeoCoordinate((double)userInfo.Latitude, (double)userInfo.Longitude));
            list_Pin.Add(pin_Me);
            map.SetView(new GeoCoordinate((double)userInfo.Latitude, (double)userInfo.Longitude), 14);
            foreach (RewardModel reward in rewardVm.List_RewardModel)
            {
                Image pin = new Image() { Source = new BitmapImage(new Uri("/Image_Recycle/gift.png", UriKind.Relative)) };
                pin.Width = 40;
                pin.Height = 40;
                pin.Tag = reward.Id;
                pin.DataContext = reward;
                layer.AddChild(pin, new GeoCoordinate(reward.Latitude, reward.Longitude));
                pin.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(pin_Tap);
                list_Pin.Add(pin);
            }
        }

        void pin_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Image pin = sender as Image;
            if (pin.DataContext is AcquirerModel)
            {
                AcquirerModel acquirer = pin.DataContext as AcquirerModel;
                Recycle_Menu_1 menu = new Recycle_Menu_1();
                menu.tbl_BinName.Text = acquirer.BinName;
                menu.tbk_UserName.Text = acquirer.AcquirerName;
                menu.tbk_Phone.Text = acquirer.Phone;
                menu.tbk_Address.Text = acquirer.Address;
                menu.Tag = acquirer.Id;
                menu.Width = 300;
                menu.Height = 250;
                layer.AddChild(menu, new GeoCoordinate(acquirer.Latitude, acquirer.Longitude));
            }
            else
            {
                RewardModel reward = pin.DataContext as RewardModel;
                Reward_Menu_1 menu = new Reward_Menu_1();
                menu.tbk_UserName.Text = reward.StoreName;
                menu.tbk_Phone.Text = reward.Phone;
                menu.tbk_Address.Text = reward.Address;
                menu.tbk_Describe.Text = reward.Describe;
                menu.image_Item.Source = new BitmapImage(new Uri(reward.AvatarUri, UriKind.Relative));
                menu.Width = 350;
                menu.Height = 250;
                layer.AddChild(menu, new GeoCoordinate(reward.Latitude, reward.Longitude));
            }
        }

        private void btn_Message_Click(object sender, EventArgs e)
        {
            isbreak = true;
            if (control_Recycle_Message != null)
            {
                MenuContent.Children.Clear();
                MenuContent.Children.Add(control_Recycle_Message);
                Storyboard1.Begin();
                grid_Content.IsHitTestVisible = false;
            }
            else
            {
                control_Recycle_Message = new Recycle_Message(this);
                MenuContent.Children.Clear();
                MenuContent.Children.Add(control_Recycle_Message);
                Storyboard1.Begin();
                grid_Content.IsHitTestVisible = false;
            }
        }

        void threadDo()
        {
            isbreak = false;
            long time0 = DateTime.Now.Ticks;
            bool cansee = true;
            while (true)
            {
                if (isbreak)
                {
                    Dispatcher.BeginInvoke(new Action(delegate()
                    {
                        btn_Message.IconUri = new Uri("/Image_Recycle/Message/m_closed.png",UriKind.Relative); 
                    }));
                    break;
                }
                TimeSpan span0 = new TimeSpan(DateTime.Now.Ticks - time0);
                if (span0.Seconds % 2 == 0 && !cansee)
                {
                    cansee = !cansee;
                    Dispatcher.BeginInvoke(new Action(delegate()
                    {
                        btn_Message.IconUri = new Uri("/Image_Recycle/Message/m_closed.png", UriKind.Relative);
                    }));
                }
                if (span0.Seconds % 2 != 0 && cansee)
                {
                    cansee = !cansee;
                    Dispatcher.BeginInvoke(new Action(delegate()
                    {
                        btn_Message.IconUri = new Uri("/Image_Recycle/Message/m_ope.png", UriKind.Relative);
                    }));

                  

                }
                if (span0.Seconds > 20)
                {
                    Dispatcher.BeginInvoke(new Action(delegate()
                    {
                        btn_Message.IconUri = new Uri("/Image_Recycle/Message/m_closed.png", UriKind.Relative);
                    }));
                    break;
                }
            }

        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (grid_Content.IsHitTestVisible == false)
            {
                Storyboard2.Begin();
                grid_Content.IsHitTestVisible = true;
                e.Cancel = true;
            }
        }
    }
}