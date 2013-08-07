﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Panda.Phone.Publisher.Model
{
    public class ConfirmMessageModel
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Time { set; get; }
        public string Message { set; get; }
        public string ImageUri { set; get; }
    }
}
