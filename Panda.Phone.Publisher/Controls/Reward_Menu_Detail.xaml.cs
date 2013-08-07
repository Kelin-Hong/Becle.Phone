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

using System.Device.Location;
namespace Panda.Phone.Publisher.Controls
{
    public partial class Reward_Menu_Detail : UserControl
    {
        //public delegate void Callback(RewardModel model);
        //public event Callback SeeMap_Click
        //{
        //    add{}
        //    remove { }
        //}

        public DependencyProperty ActivityProperty = DependencyProperty.Register("Activity", typeof(string), typeof(Reward_Menu_Detail),
         new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
         {
             var control = e1 as Reward_Menu_Detail;
             if (control != null && e2.NewValue != null)
             {
                 control.tbk_Activity.Text = (string)e2.NewValue;
             }

         })));

        public DependencyProperty PhoneProperty = DependencyProperty.Register("Phone", typeof(string), typeof(Reward_Menu_Detail),
         new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
         {
             var control = e1 as Reward_Menu_Detail;
             if (control != null && e2.NewValue != null)
             {
                 control.tbk_Phone.Text = (string)e2.NewValue;
             }

         })));

        public DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(string), typeof(Reward_Menu_Detail),
        new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
        {
            var control = e1 as Reward_Menu_Detail;
            if (control != null && e2.NewValue != null)
            {
              
               // control.tbk_Time.Text=(string)e2.NewValue;
            }

        })));
        public DependencyProperty AddressProperty = DependencyProperty.Register("Address", typeof(string), typeof(Reward_Menu_Detail),
         new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
         {
             var control = e1 as Reward_Menu_Detail;
             if (control != null && e2.NewValue != null)
             {
                 control.tbk_Address.Text = (string)e2.NewValue;
             }

         })));
       // public RoutedEvent SeeMapEvent=
        //public RoutedEvent SeeMap_ClickProperty =.Register("SeeMap_Click", typeof(Callback), typeof(Reward_Menu_Detail),
        // new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
        // {
        //     var control = e1 as Reward_Menu_Detail;
        //     if (control != null && e2.NewValue != null)
        //     {
        //         control.SeeMap_Click += (Callback)e2.NewValue;
        //     }

        // })));

        public string Activity
        {
            get
            {

                return base.GetValue(ActivityProperty) as string;
            }
            set
            {
                base.SetValue(ActivityProperty, value);
            }
        }

        public string Time
        {
            get
            {

                return base.GetValue(TimeProperty) as string;
            }
            set
            {
                base.SetValue(TimeProperty, value);
            }
        }

        public string Phone
        {
            get
            {

                return base.GetValue(PhoneProperty) as string;
            }
            set
            {
                base.SetValue(PhoneProperty, value);
            }
        }

        public string Address
        {
            get
            {

                return base.GetValue(AddressProperty) as string;
            }
            set
            {
                base.SetValue(AddressProperty, value);
            }
        }


        
        //{
        //    add
        //    {
        //        base.SetValue(SeeMap_ClickProperty, value);
        //    }
        //    remove
        //    {

        //    }
        //}

      
        public Reward_Menu_Detail()
        {
            InitializeComponent();
            
        }

        private void btn_SeeInMap_Tap(object sender, GestureEventArgs e)
        {
            
          //  SeeMap_Click(model);

          
            //Image pin = new Image() { Source = new BitmapImage(new Uri("/Image_Recycle/bubble.png", UriKind.Relative)) };
            //pin.Width = 40;
            //pin.Height = 40;
            //pin.Tag = acquirer.Id;
            //pin.DataContext = acquirer;
            
            //layer.AddChild(pin, new GeoCoordinate(acquirer.Latitude, acquirer.Longitude));
            //list_Pin.Add(pin);
        }
    }
}
