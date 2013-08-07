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
using Panda.Phone.Publisher.DataBase;
using System.Linq;
using Panda.Phone.Publisher.Model;
using Panda.Phone.Publisher.PublisherServiceReference;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Panda.Phone.Publisher.Views;
namespace Panda.Phone.Publisher.ViewModel
{
    public class DataVM :INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region[Data]
        public int User_Hightest{ set; get; }
        public int User_Average{  set;  get; }
      
        public int User_My{ set; get; }

        public int City_Hightest{ set; get;}
        public int City_Average { set; get; }
        public int City_My { set; get; }
        private int user_Hightest_Hight;
        public int User_Hightest_Hight
        {
            set
            {
                if (value != user_Hightest_Hight)
                {
                    user_Hightest_Hight = value;
                    OnPropertyChanged("User_Hightest_Hight");
                }
            }
            get
            {
                return user_Hightest_Hight;
            }
        }
        private int user_Average_Hight;
        public int User_Average_Hight 
        {
            set
            {
                if (value != user_Average_Hight)
                {
                    user_Average_Hight = value;
                    OnPropertyChanged("User_Average_Hight");
                }
            }
            get
            {
                return user_Average_Hight;
            }
        }
        private int user_My_Hight;
        public int User_My_Hight 
        {
            set
            {
                if (value != user_My_Hight)
                {
                    user_My_Hight = value;
                    OnPropertyChanged("User_My_Hight");
                }
            }
            get
            {
                return user_My_Hight;
            }
        }
        private int city_Hightest_Hight;
        public int City_Hightest_Hight
        {
            set
            {
                if (value != city_Hightest_Hight)
                {
                    city_Hightest_Hight = value;
                    OnPropertyChanged("City_Hightest_Hight");
                }
            }
            get
            {
                return city_Hightest_Hight;
            }
        }
        private int city_Average_Hight;
        public int City_Average_Hight
        {
            set
            {
                if (value != city_Average_Hight)
                {
                    city_Average_Hight = value;
                    OnPropertyChanged("City_Average_Hight");
                }
            }
            get
            {
                return city_Average_Hight;
            }
        }
        private int city_My_Hight;
        public int City_My_Hight 
        {
            set
            {
                if (value != city_My_Hight)
                {
                    city_My_Hight = value;
                    OnPropertyChanged("City_My_Hight");
                }
            }
            get
            {
                return city_My_Hight;
            }
        }
        private string user_Rank;
        public string User_Rank 
        { 
             set
            {
                if (value != user_Rank)
                {
                    user_Rank = value;
                    OnPropertyChanged("User_Rank");
                }
            }
            get
            {
                return user_Rank;
            }
        }
        private string city_Rank;
        public string City_Rank 
        { 
             set
            {
                if (value != city_Rank)
                {
                    city_Rank = value;
                    OnPropertyChanged("City_Rank");
                }
            }
            get
            {
                return city_Rank;
            }
        }
      
        private Dictionary<int,string> dictionary_User;
        public Dictionary<int, string>  Dictionary_User 
        {
            set
            {
                if (value != dictionary_User)
                {
                    dictionary_User = value;
                    OnPropertyChanged("Dictionary_User");
                }
            }
            get
            {
                return dictionary_User;
            }
        }
        private Dictionary<int, string> dictionary_City;
        public  Dictionary<int, string> Dictionary_City 
        {
            set
            {
                if (value != dictionary_City)
                {
                    dictionary_City = value;
                    OnPropertyChanged("Dictionary_City");
                }
            }
            get
            {
                return dictionary_City;
            }
        }
        public ObservableCollection<int> List_User_Point = new ObservableCollection<int>();
        public ObservableCollection<int> List_City_Point = new ObservableCollection<int>();
        #endregion
        Database db;
        PublisherServiceClient client = new PublisherServiceClient();
        DataPage1 page;
        public DataVM(int id,DataPage1 _page)
        {
            page = _page;
            string city = (App.Current as App).Userinfo.City;
            db = new Database(Database.connectStr);
            Dictionary_User = new Dictionary<int, string>();
            Dictionary_City = new Dictionary<int, string>();
            client.GetMyPointsAsync(id);
            client.GethighestPointsAlluserAsync();
            client.GetAveragePointsAllUserAsync();
            client.GetAveragePointsInCityAsync(city);
            client.GetHighestPointsInCityAsync(city);
            client.GetMyRankAsync(id);
            client.GetMyCityRankAsync(id);
            client.GetMyRankCompleted+=new EventHandler<GetMyRankCompletedEventArgs>(client_GetMyRankCompleted);
            client.GetMyCityRankCompleted += new EventHandler<GetMyCityRankCompletedEventArgs>(client_GetMyCityRankCompleted);
            client.GetMyPointsCompleted += new EventHandler<GetMyPointsCompletedEventArgs>(client_GetMyPointsCompleted);
            client.GethighestPointsAlluserCompleted += new EventHandler<GethighestPointsAlluserCompletedEventArgs>(client_GethighestPointsAlluserCompleted);
            client.GetAveragePointsAllUserCompleted += new EventHandler<GetAveragePointsAllUserCompletedEventArgs>(client_GetAveragePointsAllUserCompleted);
            client.GetAveragePointsInCityCompleted += new EventHandler<GetAveragePointsInCityCompletedEventArgs>(client_GetAveragePointsInCityCompleted);
            client.GetHighestPointsInCityCompleted += new EventHandler<GetHighestPointsInCityCompletedEventArgs>(client_GetHighestPointsInCityCompleted);
            client.GetRankUserCompleted += new EventHandler<GetRankUserCompletedEventArgs>(client_GetRankUserCompleted);
            client.GetRankCityCompleted += new EventHandler<GetRankCityCompletedEventArgs>(client_GetRankCityCompleted);
            if (db.DataUsers.Count() == 0)
            {
                client.GetRankCityAsync();
                client.GetRankUserAsync();
            }
            else
            {
                //db.DataUsers.DeleteAllOnSubmit(db.DataUsers);
                //db.Citys.DeleteAllOnSubmit(db.Citys);
                //client.GetRankCityAsync();
                //client.GetRankUserAsync();
                dealWithRankList();
            }

           
           
        }
       
        #region[GetDataComplete]
        void client_GetMyCityRankCompleted(object sender, GetMyCityRankCompletedEventArgs e)
        {
            City_Rank = e.Result;
        }

        void client_GetMyRankCompleted(object sender, GetMyRankCompletedEventArgs e)
        {
            User_Rank = e.Result;
        }
        void dealWithRankList()
        {
            Dictionary<int,string> _dictionary_User=new Dictionary<int,string>();
            Dictionary<int, string> _dictionary_City=new Dictionary<int,string>();
            int i = 0;
            if (Dictionary_User.Count == 0)
            {
                foreach (DataUserTable userTable in db.DataUsers)
                {
                    i++;
                    _dictionary_User.Add(i, userTable.UserName);
                    List_User_Point.Add(userTable.Point);
                    if (i > 10) break;
                }
                Dictionary_User = _dictionary_User;
            }
            i = 0;
            if (Dictionary_City.Count == 0)
            {
                foreach (CityTable cityTable in db.Citys)
                {
                    i++;
                    _dictionary_City.Add(i, cityTable.Name);
                    List_City_Point.Add(cityTable.Point);
                    if (i > 10) break;
                }
                Dictionary_City = _dictionary_City;
            }
        }
        void client_GetRankCityCompleted(object sender, GetRankCityCompletedEventArgs e)
        {
            foreach (City city in e.Result)
            {
                CityTable cityTable = new CityTable()
                {
                    LastRank = city.LastRank,
                    Name = city.Name,
                    Point = city.Points
                };
                db.Citys.InsertOnSubmit(cityTable);
            }
            db.SubmitChanges();
            dealWithRankList();
        }

        void client_GetRankUserCompleted(object sender, GetRankUserCompletedEventArgs e)
        {
            foreach (DataUser dataUser in e.Result)
            {
                DataUserTable dataUserTable = new DataUserTable()
                {
                    City=dataUser.City,
                    Point=dataUser.Points,
                    UserName=dataUser.UserName
                };
                db.DataUsers.InsertOnSubmit(dataUserTable);
            }
            db.SubmitChanges();
            dealWithRankList();
        }

        void client_GetHighestPointsInCityCompleted(object sender, GetHighestPointsInCityCompletedEventArgs e)
        {
           // page.tree1[1] = User_Average_Hight / User_Hightest_Hight * 10;
            //tree1[2] = vm.User_My_Hight / vm.User_Hightest_Hight * 10;
            //tree1[4] = vm.City_Average_Hight / vm.City_Hightest_Hight * 10;
            //tree1[5] = vm.City_My_Hight / vm.City_Hightest_Hight * 10;
           City_Hightest_Hight= City_Hightest = e.Result;
        }

        void client_GetAveragePointsInCityCompleted(object sender, GetAveragePointsInCityCompletedEventArgs e)
        {
           City_Average_Hight= City_Average = e.Result;
        }

        void client_GetAveragePointsAllUserCompleted(object sender, GetAveragePointsAllUserCompletedEventArgs e)
        {
          User_Average_Hight= User_Average = e.Result;
        }

        void client_GethighestPointsAlluserCompleted(object sender, GethighestPointsAlluserCompletedEventArgs e)
        {
          User_Hightest_Hight=  User_Hightest = e.Result;
        }
      
        void client_GetMyPointsCompleted(object sender, GetMyPointsCompletedEventArgs e)
        {
           User_My_Hight=  User_My = e.Result;
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        private void OnPropertyChanging(string property)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(property));
            }
        }
       
    }
}
