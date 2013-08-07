using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Panda.Phone.Models;
using System.Collections.Generic;
namespace Panda.Phone.ViewModels
{
    public class FriendsListBoxViewModel:IEnumerable<FriendsItem>

    {

        public IEnumerator<FriendsItem> GetEnumerator()
        {
            List<FriendsItem> list = new List<FriendsItem>() { 
            new FriendsItem(){Name="FriendList"},
            new FriendsItem(){Name="Trends"},
            new FriendsItem(){Name="Map"}
            };
            return list.GetEnumerator();

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
