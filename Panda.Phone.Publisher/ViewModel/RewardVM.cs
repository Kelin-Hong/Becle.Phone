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
using Panda.Phone.Publisher.PublisherServiceReference;
namespace Panda.Phone.Publisher.ViewModel
{
    public class RewardVM
    {
        public string Title { set; get; }
        public List<RewardModel> List_RewardModel { set; get; }
        Database db;
        public delegate void CallBack();
        public RewardVM (int id,CallBack callback)
	    {
            db = new Database(Database.connectStr);
            List_RewardModel = new List<RewardModel>();
           // UserTable user = db.Users.Single(c => c.Id == id);
            Title = "Rewrad List";
            //List<RewardTable> list_Acquirer = db.Rewards.Where(c => Math.Abs(c.Latitude - user.Latitude) < 1 && Math.Abs(c.Longitude - user.Longitude) < 1).ToList<RewardTable>();
            if (db.Rewards.Count() == 0)
            {
                PublisherServiceClient client = new PublisherServiceClient();
                client.GetRewardAsync(id);
                client.GetRewardCompleted += new EventHandler<GetRewardCompletedEventArgs>(client_getRewardCompleted);
            }
            else
            {
                getList_RewardModel();
            }
             
       
	    }
        void getList_RewardModel()
        {
            List<RewardTable> list_RewardModel = db.Rewards.ToList();
            foreach (RewardTable reward in list_RewardModel)
            {
                RewardModel model = new RewardModel()
                {
                    StoreName = reward.StoreName,
                    Describe = reward.Describe,
                    Address = reward.Address,
                    Phone = reward.Phone,
                    Points=reward.Points,
                    Summary=reward.Summary,
                    Latitude = (double)reward.Latitude,
                    Longitude = (double)reward.Longitude,
                    AvatarUri = reward.AvatarUri,
                    Id = reward.Id
                };
                List_RewardModel.Add(model);
            }
        }
        void client_getRewardCompleted(object sender, GetRewardCompletedEventArgs e)
        {
            foreach (Panda.Phone.Publisher.PublisherServiceReference.Reward item in e.Result)
            {
                RewardTable reward = new RewardTable()
                {
                    Address=item.Address,
                    City=item.City,
                    Describe=item.Describe,
                    Latitude=item.Latitude,
                    Longitude = item.Longitude,
                    Phone=item.Phone,
                    StoreName=item.StoreName    
                };
                db.Rewards.InsertOnSubmit(reward);
            }
            db.SubmitChanges();
            getList_RewardModel(); 
        }
        
    }
}
