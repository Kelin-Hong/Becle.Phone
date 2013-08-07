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

namespace Panda.Phone.Publisher.Controls
{
    public partial class Recycle_Menu_detail : UserControl
    {

        public DependencyProperty UserNameProperty = DependencyProperty.Register("UserName", typeof(string), typeof(Recycle_Menu_detail),
         new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
         {
             var control = e1 as Recycle_Menu_detail;
             if (control != null && e2.NewValue != null)
             {
                 control.tbk_UserName.Text = (string)e2.NewValue;
             }

         })));

        public DependencyProperty PhoneProperty = DependencyProperty.Register("Phone", typeof(string), typeof(Recycle_Menu_detail),
         new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
         {
             var control = e1 as Recycle_Menu_detail;
             if (control != null && e2.NewValue != null)
             {
                 control.tbk_Phone.Text = (string)e2.NewValue;
             }

         })));

        public DependencyProperty AddressProperty = DependencyProperty.Register("Address", typeof(string), typeof(Recycle_Menu_detail),
         new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
         {
             var control = e1 as Recycle_Menu_detail;
             if (control != null && e2.NewValue != null)
             {
                 control.tbk_Address.Text = (string)e2.NewValue;
             }

         })));
        public string UserName
        {
            get
            {

                return base.GetValue(UserNameProperty) as string;
            }
            set
            {
                base.SetValue(UserNameProperty, value);
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

       
        public Recycle_Menu_detail()
        {
            InitializeComponent();
        }
    }
}
