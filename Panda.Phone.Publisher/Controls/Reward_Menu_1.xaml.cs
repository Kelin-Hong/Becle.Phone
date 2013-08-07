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
    public partial class Reward_Menu_1 : UserControl
    {
        public Reward_Menu_1()
        {
            InitializeComponent();
        }

        private void btn_Close_Tap(object sender, GestureEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
