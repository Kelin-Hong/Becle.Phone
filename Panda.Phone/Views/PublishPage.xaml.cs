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
namespace Panda.Phone.Views
{
    public partial class PublishPage : PhoneApplicationPage
    {
         
        public PublishPage()
        {
            
            InitializeComponent();
            E_WasteList e_wasteList = (E_WasteList)this.e_Waste.Content;
            if (e_wasteList.phone.IsChecked==true)
            {

            }
        }
    }
}