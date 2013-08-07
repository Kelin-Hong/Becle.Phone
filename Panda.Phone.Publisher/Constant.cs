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
using System.Collections.ObjectModel;
namespace Panda.Phone.Publisher
{
    public class Constant
    {
        public static ObservableCollection<string> ewasteList = new ObservableCollection<string>() { "phone", "computer", "TV", "Map3/Mp4" };
        public static ObservableCollection<string> fabricList = new ObservableCollection<string>() { "clothes", "shoe", "quilt", "curtains", "carpet" };
        public static ObservableCollection<string> plasticList = new ObservableCollection<string>() { "plastic_bottle","plastic" };
        public static ObservableCollection<string> paperList = new ObservableCollection<string>() { "carton", "paper" };
        public static ObservableCollection<string> metalList = new ObservableCollection<string>() { "aluminium", "steel", "copper","battery","aluminum can" };
        public static ObservableCollection<string> glassList = new ObservableCollection<string>() { "glass bottle","glass" };
        public static Dictionary<string, string> getDic_Danwei()
        {
            Dictionary<string, string> Dic_Danwei = new Dictionary<string, string>();
            Dic_Danwei.Add("plastic", "Kg");
            Dic_Danwei.Add("carton", "Kg");
            Dic_Danwei.Add("paper", "Kg");
            Dic_Danwei.Add("aluminium","Kg");
            Dic_Danwei.Add("steel","Kg"); 
            Dic_Danwei.Add("copper","Kg");
            Dic_Danwei.Add("battery", "Kg");
            Dic_Danwei.Add("glass", "Kg");
            return Dic_Danwei;
        }
       // public static Dictionary<string, Affect> Dictionary_Affect = new Dictionary<string, Affect>();
       
        public static Dictionary<string, Affect> getDictionary_Affect()
        {
            Dictionary<string, Affect> Dictionary_Affect = new Dictionary<string, Affect>();
            Dictionary_Affect.Add("glass", new Affect() { electricity = 0.4, carbon_emissions = 0.38 });
            Dictionary_Affect.Add("aluminum", new Affect() { gasoline = 6.30, carbon_emissions = 13.86 });
            Dictionary_Affect.Add("steel", new Affect() { gasoline = 0.62, carbon_emissions = 1.36 });
            Dictionary_Affect.Add("copper", new Affect() { gasoline = 0.60, carbon_emissions = 1.32 });
            Dictionary_Affect.Add("plastic", new Affect() { gasoline = 0.25, electricity = 5.0, carbon_emissions = 4.8 });
            Dictionary_Affect.Add("paper", new Affect() { electricity = 4.08, water = 36.32, carbon_emissions = 3.92 });
            Dictionary_Affect.Add("carton", new Affect() { electricity = 5.58, water = 30.00, carbon_emissions = 4.86 });
            Dictionary_Affect.Add("battery", new Affect() { water = 12, soil = 1, carbon_emissions = 0.38 });
            Dictionary_Affect.Add("plastic_bottle", new Affect() { electricity = 0.36, water = 3, carbon_emissions = 0.35 });
            Dictionary_Affect.Add("glass bottle", new Affect() { electricity = 0.24, carbon_emissions = 0.23 });
            Dictionary_Affect.Add("aluminium can", new Affect() { gasoline = 1.89, electricity = 0.4, carbon_emissions = 0.38 });
            Dictionary_Affect.Add("aluminium", new Affect() { gasoline = 1.89, electricity = 0.4, carbon_emissions = 0.38 });
            Dictionary_Affect.Add("other", new Affect() { gasoline = 1.89, electricity = 0.4, carbon_emissions = 0.38 });
            return Dictionary_Affect;
        }
       
    }
   public class Affect
    {
       public  double water;
       public   double forest;
       public double electricity;
       public double carbon_emissions;
       public double soil;
       public double gasoline;
    }
}
