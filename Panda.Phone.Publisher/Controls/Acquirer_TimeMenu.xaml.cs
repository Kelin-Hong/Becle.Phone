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
    public partial class Acquirer_TimeMenu : UserControl
    {
        public Acquirer_TimeMenu()
        {
            InitializeComponent();
        }

        private void btn_Send_Tap(object sender, GestureEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
