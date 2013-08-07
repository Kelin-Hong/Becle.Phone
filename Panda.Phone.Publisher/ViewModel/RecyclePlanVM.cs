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
using System.Collections.ObjectModel;
using Panda.Phone.Publisher.Model;
using System.Linq;
using System.Collections.Generic;
namespace Panda.Phone.Publisher.ViewModel
{

    public class RecyclePlanVM
    {
       internal Database db;
       public  ObservableCollection<RecyclePlanModel> ListUser_Un {set;get;}
       public  ObservableCollection<string> ListPlan_Un{set;get;}
       public Dictionary<string, ObservableCollection<RecyclePlanModel>> Dic_Unfinish { set; get; }
       public Dictionary<string, ObservableCollection<RecyclePlanModel>> Dic_History { set; get; }
          
        public RecyclePlanVM()
        {
            db = new Database(Database.connectStr);
            Dic_Unfinish = new Dictionary<string, ObservableCollection<RecyclePlanModel>>();
            ListPlan_Un  = new ObservableCollection<string>();
            ListUser_Un = new ObservableCollection<RecyclePlanModel>();
            foreach (RecyclePlanTable plan in db.RecyclePlans.Where(c=>c.IsFinish==false))
            {
                ListUser_Un.Clear();
                foreach (RecyclePlanShipTable ship in db.RecyclePlanShips.Where(c => c.PlanId == plan.Id))
                {
                    RecyclePlanModel model = new RecyclePlanModel()
                    {
                        UserName = db.Users.Single(c => c.Id == ship.UserId).UserName,
                        Address=db.Users.Single(c=>c.Id==ship.UserId).Address,
                        IsFinish = plan.IsFinish,
                        IsCheck = ship.IsCheck,
                        UserId=ship.UserId,
                        ShipId=ship.Id,
                    };
                    ListUser_Un.Add(model);
                }
                if (!Dic_Unfinish.Keys.Contains(plan.Time))
                    Dic_Unfinish.Add(plan.Time, ListUser_Un);
                else
                {
                    Dic_Unfinish.Add(plan.Time + " ", ListUser_Un);
                }
            }

            foreach (RecyclePlanTable plan in db.RecyclePlans.Where(c => c.IsFinish == true))
            {
                ListUser_Un.Clear();
                foreach (RecyclePlanShipTable ship in db.RecyclePlanShips.Where(c => c.PlanId == plan.Id))
                {
                    RecyclePlanModel model = new RecyclePlanModel()
                    {
                        UserName = db.Users.Single(c => c.Id == ship.UserId).UserName,
                        Address = db.Users.Single(c => c.Id == ship.UserId).Address,
                        IsFinish = plan.IsFinish,
                        IsCheck = ship.IsCheck,
                        UserId = ship.UserId
                    };
                    ListUser_Un.Add(model);
                }
                if (!Dic_History.Keys.Contains(plan.Time))
                    Dic_History.Add(plan.Time, ListUser_Un);
                else
                {
                    Dic_History.Add(plan.Time + " ", ListUser_Un);
                }
            }
        }

       
       public List<UserModel> getChooseUser(ObservableCollection<RecyclePlanModel> listPlanModel)
        {
          List<UserModel> list = new List<UserModel>(); ;
          foreach(RecyclePlanModel moel in listPlanModel)
             {
              UserTable user = db.Users.First(c => c.Id == moel.UserId);
                  UserModel userModel = new UserModel()
                       {
                           AvatarUri = user.AvatarUri,
                           UserName = user.UserName,
                           Latitude=(double)user.Latitude,
                           Longitude = (double)user.Longitude,
                           //Category = i,
                           Id=user.Id,
                           Address=user.Address
                       };
                       list.Add(userModel);
             }
          return list;
        }


       
    }
}
