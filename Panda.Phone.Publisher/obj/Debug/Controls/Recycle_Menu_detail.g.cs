﻿#pragma checksum "C:\Users\hkl\Documents\Visual Studio 2010\Projects\WindowsPhone_Application\Panda.Phone\Panda.Phone.Publisher\Controls\Recycle_Menu_detail.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8F4289143A625778698055C47C175142"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.261
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Panda.Phone.Publisher.Controls {
    
    
    public partial class Recycle_Menu_detail : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.TextBlock tbk_UserName;
        
        internal System.Windows.Controls.TextBlock tbk_Phone;
        
        internal System.Windows.Controls.TextBlock tbk_Address;
        
        internal System.Windows.Controls.Image btn_SeeInMap;
        
        internal System.Windows.Controls.Image btn_SendMessage;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Panda.Phone.Publisher;component/Controls/Recycle_Menu_detail.xaml", System.UriKind.Relative));
            this.tbk_UserName = ((System.Windows.Controls.TextBlock)(this.FindName("tbk_UserName")));
            this.tbk_Phone = ((System.Windows.Controls.TextBlock)(this.FindName("tbk_Phone")));
            this.tbk_Address = ((System.Windows.Controls.TextBlock)(this.FindName("tbk_Address")));
            this.btn_SeeInMap = ((System.Windows.Controls.Image)(this.FindName("btn_SeeInMap")));
            this.btn_SendMessage = ((System.Windows.Controls.Image)(this.FindName("btn_SendMessage")));
        }
    }
}
