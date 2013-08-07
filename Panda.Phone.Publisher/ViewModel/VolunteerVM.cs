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
using Panda.Phone.Publisher.DataBase;
using Panda.Phone.Publisher.Model;
using System.Collections.Generic;
using System.Linq;
using Panda.Phone.Publisher.LocationServiceReference;
using Microsoft.Phone.Controls.Maps;
using Panda.Phone.Publisher.PublisherServiceReference;
using System.Collections.ObjectModel;
namespace Panda.Phone.Publisher.ViewModel
{
    public class VolunteerVM
    {
        public static int categoryCount = 6;
        public static bool[] category = new bool[10];
        public static string[] categoryStr = new string[] { "E_Waste", "Fabric", "Glass", "Metal", "Paper", "Plastic" };
        Database db;
        internal PublisherServiceClient client = new PublisherServiceClient();
        public ObservableCollection<AcquirerMessageModel> List_Message { set; get; }
        public List<UserModel> List_User
        {
            get
            {
                return getUsersByCategoryFromDatabase();
            }
        }
        public List<ItemModel> List_Item
        {
            get
            {
                return getItemsByCategoryFromDatabase();
            }

        }

        internal UserInfo acquirerInfo;
        public delegate void CallBack();
        CallBack callback1, callback2, callback3;
        int acquirerId;

        public VolunteerVM(CallBack _callback1, CallBack _callback2, CallBack _callback3)
        {
            db = new Database(Database.connectStr);
            callback1 = _callback1;
            callback2 = _callback2;
            callback3 = _callback3;
            acquirerId = (App.Current as App).UserId;
            acquirerInfo = (App.Current as App).Userinfo;
            //client.GetResidentsAsync(acquirerId);
          //  client.GetAcquirerInfoAsync(acquirerId);

           // client.GetAcquirerInfoCompleted += new EventHandler<GetAcquirerInfoCompletedEventArgs>(client_GetAcquirerInfoCompleted);
            client.GetResidents_VAsync(acquirerId);
            client.GetItems_VCompleted += new EventHandler<GetItems_VCompletedEventArgs>(client_GetItems_VCompleted);
            client.GetResidents_VCompleted += new EventHandler<GetResidents_VCompletedEventArgs>(client_GetResidents_VCompleted);
        }

        void client_GetResidents_VCompleted(object sender, GetResidents_VCompletedEventArgs e)
        {
            foreach (Resident resident in e.Result)
            {
                UserTable userTable = new UserTable()
                {
                    Address = resident.Address,
                    City = resident.City,
                    Latitude = resident.Latitude,
                    Longitude = resident.Longitude,
                    UserName = resident.UserName,
                    Id = resident.UserId
                };
                db.Users.InsertOnSubmit(userTable);
            }
            db.SubmitChanges();
            client.GetItems_VAsync(acquirerId);
           
        }

        void client_GetItems_VCompleted(object sender, GetItems_VCompletedEventArgs e)
        {
            foreach (Item item in e.Result)
            {
                ItemTable itemTable = new ItemTable()
                {
                    CategoryId = item.CategoryId,
                    Name = item.Name,
                    Num = item.Num,
                    Time = item.Time,
                    UserId = item.UserId,
                    Id = item.ItemId
                };
                db.Items.InsertOnSubmit(itemTable);
            }
            db.SubmitChanges();
            callback2();
        }

        internal int saveRecyclePlan(int acquirerId, string time)
        {
            VolunteerPlanTable plan = new VolunteerPlanTable()
            {
                AcquirerId = acquirerId,
                IsFinish = false,
                Time = time
            };
            db.VolunteerPlans.InsertOnSubmit(plan);
            db.SubmitChanges();
            return db.VolunteerPlans.ToList().ElementAt(db.VolunteerPlans.ToList().Count - 1).Id;
        }
        internal void saveRecycleShip(int planId, int userId)
        {
            VolunteerPlanShipTable recycleShip = new VolunteerPlanShipTable()
            {
                PlanId = planId,
                UserId = userId
            };
            db.VolunteerPlanShips.InsertOnSubmit(recycleShip);
            db.SubmitChanges();
        }
      
        void getItemsByUserAndCategory(int UserId)
        {
            List<ItemModel> list = List_Item.Where(c => c.UserId == UserId).ToList();
        }

        List<UserModel> getUsersByCategoryFromDatabase()
        {
            List<UserModel> list_user = new List<UserModel>();
            for (int i = 0; i < categoryCount; i++)
            {
                if (category[i])
                {
                    List<ItemTable> list = db.Items.Where(c => c.CategoryId == i + 1).ToList<ItemTable>();
                    foreach (ItemTable item in list)
                    {
                        UserTable user = db.Users.First(c => c.Id == item.UserId);
                        UserModel userModel = new UserModel()
                        {
                            AvatarUri = user.AvatarUri,
                            UserName = user.UserName,
                            Latitude = (double)user.Latitude,
                            Longitude = (double)user.Longitude,
                            Category = i,
                            Id = user.Id,
                            Address = user.Address
                        };
                        // Geocode(userModel.Address,user.Id);
                        if (!list_user.Any(c => c.Id == user.Id))
                            list_user.Add(userModel);
                    }
                }
            }
            return list_user;
        }

        List<ItemModel> getItemsByCategoryFromDatabase()
        {
            List<ItemModel> list_item = new List<ItemModel>();
            for (int i = 0; i < categoryCount; i++)
            {
                if (category[i])
                {
                    List<ItemTable> list = db.Items.Where(c => c.CategoryId == i + 1).ToList<ItemTable>();
                    foreach (ItemTable item in list)
                    {
                        UserTable user = db.Users.First(c => c.Id == item.UserId);
                        ItemModel itemModel = new ItemModel()
                        {
                            //  AvatarUri = item.AvatarUri,
                            Name = item.Name,
                            Num = item.Num,
                            UserName = user.UserName,
                            Latitude = (double)user.Latitude,
                            Longitude = (double)user.Longitude,
                            CategoryId = item.CategoryId,
                            Id = item.Id,
                            UserId = user.Id,
                            SubmitTime = item.Time
                        };
                        list_item.Add(itemModel);
                    }
                }
            }
            return list_item;
        }

        // GeocodeResult[] Georesults;
        void client_GeocodeCompleted(object sender, GeocodeCompletedEventArgs e)
        {
            int id = Convert.ToInt32(e.UserState);

            // Georesults[id] = e.Result.Results[0];
            GeocodeResponse response = e.Result;
            //double latitude = response.Results[0].Locations[0].Latitude;
            //double longitude = response.Results[0].Locations[0].Longitude;
            //map.SetView(new GeoCoordinate(latitude, longitude), 14);
        }
        private void Geocode(string name, int id)
        {
            GeocodeServiceClient client = new GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
            GeocodeRequest request = new GeocodeRequest();
            request.Credentials = new Credentials();
            request.Credentials.ApplicationId = "AjlmpILfXZMz8HCz-aqkBfnZZt6UXFnVqXVIP7glejUCYrliehYU6xtEBZVshDZM";
            request.Query = name;
            client.GeocodeCompleted += new EventHandler<GeocodeCompletedEventArgs>(client_GeocodeCompleted);
            client.GeocodeAsync(request);

        }
    }
}
