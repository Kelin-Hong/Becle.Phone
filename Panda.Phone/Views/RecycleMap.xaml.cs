﻿using System;
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
using Panda.Phone.ServiceReference;
namespace Panda.Phone.Views
{
    public partial class RecycleMap : PhoneApplicationPage
    {
        public RecycleMap()
        {
            InitializeComponent();
            if (NavigationContext.QueryString["name"] != null)
            {
                PhoneServiceClient client = new PhoneServiceClient();
                
            }
        }
        
    }
}