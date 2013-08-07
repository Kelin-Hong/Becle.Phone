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
using Panda.Phone.Publisher.PublisherServiceReference;
namespace Panda.Phone.Publisher.Controls
{
    public partial class Recycle_Menu_SendMessage : UserControl
    {
        PublisherServiceClient client = new PublisherServiceClient();
        public Recycle_Menu_SendMessage()
        {
            InitializeComponent();
        }

        private void btn_Close_Tap(object sender, GestureEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btn_Send_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Message.Text != "")
            {
                client.SendMessageToAcquirerAsync(new Message()
                {
                    FromId=(App.Current as App).UserId,
                    MessageContent=tb_Message.Text,
                    ToId=(int)this.Tag
                });
                this.Visibility = Visibility.Collapsed;
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
