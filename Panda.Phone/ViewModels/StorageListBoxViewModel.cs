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
using System.Collections.Generic;
using Panda.Phone.Models;
using Panda.Phone.Commands;
using System.Windows.Navigation;
using System.Collections.ObjectModel;
namespace Panda.Phone.ViewModels
{
   
    //public class StorageListBoxViewModel:IEnumerable<StorageItems>
    //{

    //    public SelectChangedCommand selectChangedCommand { set; get; }
    //    public StorageListBoxViewModel()
    //    {
    //        selectChangedCommand = new SelectChangedCommand(SelectChange, CanExcute);
    //    }
    //    private bool CanExcute(object o)
    //    {
    //        return true;
    //    }
    //    private void SelectChange(object o)
    //    {
    //        StorageItems item=(StorageItems)o;
    //        if (item.Name == "Publish")
    //        {
                
    //        }
    //    }
    //    public IEnumerator<StorageItems> GetEnumerator()
    //    {
    //       List<StorageItems>
    //           list=new List<StorageItems>()
    //           {
    //               new StorageItems(){Name="Publish",Discripe="Set Garbage"},
    //               new StorageItems(){Name="History",Discripe="History Information"},
    //               new StorageItems(){Name="Count",Discripe="Garbage Count"}
    //           };
    //       return list.GetEnumerator();
    //    }

    //    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    //    {
    //        return this.GetEnumerator();
    //    }
    //}
}
