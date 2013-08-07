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
using System.Collections.Generic;
using System.Linq;
using Panda.Phone.Publisher.Model;
using Panda.Phone.Publisher.PublisherServiceReference;
using System.Collections.ObjectModel;
namespace Panda.Phone.Publisher.ViewModel
{
    public class FriendVM
    {
        Database db;
        public ObservableCollection<PostModel> list_PostModel { set; get; }
        public ObservableCollection<UnloadModel> list_UnloadModel { set; get; }
        public ObservableCollection<AchievementModel> list_AchievementModel { set; get; }
        List<int> list_FriendId;
        DateTime nowTime = DateTime.Now;
        PublisherServiceClient client;
        public  delegate void Callback();
        Callback callback1;
        Callback callback2;
        Callback callback3;
        public FriendVM(int id, Callback _callback1, Callback _callback2, Callback _callback3)
        {
            callback1 = _callback1;
            callback2 = _callback2;
            callback3 = _callback3;
            db = new Database(Database.connectStr);
            list_PostModel = new ObservableCollection<PostModel>();
            list_UnloadModel = new ObservableCollection<UnloadModel>();
            list_AchievementModel = new ObservableCollection<AchievementModel>();
            list_FriendId=db.Friends.Where(c=>c.UserId==id).Select(c=>c.FriendId).ToList<int>();
            list_FriendId.Add(id);
            if (db.PostTrends.Count() != 0)
            {
                dealWithAchievement();
                dealWithPhotoTrend();
                dealWithPostTrend();
            }
            else
            {
                getTrendFromServer();
            }
         
           
        }
        private void getTrendFromServer()
        {
            client = new PublisherServiceClient();
            client.GetPostTrendAsync((App.Current as App).UserId);
            client.GetPostTrendCompleted += new EventHandler<GetPostTrendCompletedEventArgs>(client_GetPostTrendCompleted);
            client.GetPhotoTrendAsync((App.Current as App).UserId);
            client.GetPhotoTrendCompleted += new EventHandler<GetPhotoTrendCompletedEventArgs>(client_GetPhotoTrendCompleted);
            client.GetAchievementTrendAsync((App.Current as App).UserId);
            client.GetAchievementTrendCompleted += new EventHandler<GetAchievementTrendCompletedEventArgs>(client_GetAchievementTrendCompleted);
        }

        void client_GetAchievementTrendCompleted(object sender, GetAchievementTrendCompletedEventArgs e)
        {
            foreach (AchievementTrend item in e.Result)
            {
                AchievementTrendTable achievementTrendTable = new AchievementTrendTable()
                {
                    Achievement=item.Achievement,
                    Name=item.Name,
                    Time=item.Time,
                    UserId=item.UserId
                };
                if(item.Achievement.Contains("NO"))
                {
                    string index =item.Achievement.Substring(item.Achievement.IndexOf("NO.")+3,1);
                    achievementTrendTable.ImageUri = "/Image_Friends/top/"+index+".png";
                    achievementTrendTable.Category = "";
                }
                else
                {
                    achievementTrendTable.Achievement = "get a title of ";
                    achievementTrendTable.ImageUri = "/Image_Friends/crown/"+item.Achievement+".png";
                    achievementTrendTable.Category = item.Achievement + " baron";
                };
                db.AchievementTrends.InsertOnSubmit(achievementTrendTable);
            }
            db.SubmitChanges();
            dealWithAchievement();
            callback2();
        }
        void client_GetPhotoTrendCompleted(object sender, GetPhotoTrendCompletedEventArgs e)
        {
            foreach (PhotoTrend item in e.Result)
            {
                PhotoTrendTable photoTrendTable = new PhotoTrendTable()
                {
                    Describe = item.Describe,
                    Time=item.Time,
                    ItemId=item.ItemId,
                    UserId=item.UserId,
                    Name=item.Name
                };
                photoTrendTable.Describe = photoTrendTable.Describe.Insert(photoTrendTable.Describe.IndexOf(' '), "\r\n");
                db.PhotoTrends.InsertOnSubmit(photoTrendTable);
            }
            db.SubmitChanges();
            dealWithPhotoTrend();
            callback3();
        }
        void client_GetPostTrendCompleted(object sender, GetPostTrendCompletedEventArgs e)
        {
            foreach (PostTrend item in e.Result)
            {
                PostTrendTable postTrendTable = new PostTrendTable()
                {
                    Describe=item.Describe,
                    PostAffect=item.PostAffect,
                    Time=item.Time,
                    UserId=item.UserId,
                    Name=item.Name
                };
                db.PostTrends.InsertOnSubmit(postTrendTable);
            }
            db.SubmitChanges();
            dealWithPostTrend();
            callback1();
        }
        
        private void dealWithPostTrend()
        {
            List<PostTrendTable> list_PostTrends = new List<PostTrendTable>();
            foreach (int i in list_FriendId)
            {
                // TimeSpan span=24*nowTime.Subtract(DateTime.Parse(c.Time)).Days+nowTime.Subtract(DateTime.Parse(c.Time)).Hours;
                foreach (PostTrendTable trends in db.PostTrends)
                {
                    int days = nowTime.Subtract(DateTime.Parse(trends.Time)).Duration().Days;
                    int hour = nowTime.Subtract(DateTime.Parse(trends.Time)).Duration().Hours;
                    DateTime d = DateTime.Parse(trends.Time);
                    int hours = 24 * nowTime.Subtract(DateTime.Parse(trends.Time)).Duration().Days + nowTime.Subtract(DateTime.Parse(trends.Time)).Duration().Hours;
                    //if (trends.UserId == i && hours <= 200)
                        list_PostTrends.Add(trends);
                }
            }
            foreach (PostTrendTable trend in db.PostTrends)
            {
              //  UserTable user = db.Users.First(c => c.Id == trend.UserId);
                PostModel postModel = new PostModel()
                {
                    Describe = trend.Describe,
                    Name = trend.Name,
                    PostAffect = trend.PostAffect,
                    Time = trend.Time,
                    UserId=trend.UserId,
                };
                //string s = trend.Describe.Substring(0, trend.Describe.LastIndexOf(' ') - 1);
                //string name = trend.Describe.Substring(trend.Describe.LastIndexOf(' ') + 1);
                //if (Constant.getDic_Danwei().Keys.Contains(name))
                //{
                //    name = Constant.getDic_Danwei()[name] + " " + name;
                //}
                //postModel.Describe = s + " " + name;
               getData(postModel, trend);

               
            }
            
        }

        private void  getData(PostModel postModel,PostTrendTable trend)
        {
             string name = trend.Describe.Substring(trend.Describe.LastIndexOf(' ')+1);
             string Num = trend.Describe.Substring(6);
             Num = Num.Substring(0, Num.IndexOf(' '));
             if (Constant.getDic_Danwei().Keys.Contains(name))
             {
                 postModel.Describe = "Post " + Num + " " + Constant.getDic_Danwei()[name] + " " + name;
             }
             else
             {
                 if (Int32.Parse(Num) > 1) postModel.Describe += "s";
             }
             
                Affect  affect;
                if (!Constant.getDictionary_Affect().ContainsKey(name))
                {
                    affect = Constant.getDictionary_Affect()["other"];
                }
                else
                {
                    affect = Constant.getDictionary_Affect()[name];
                }
                if (affect.carbon_emissions != 0)
                {
                    postModel.Co2 = affect.carbon_emissions * Int32.Parse(Num);
                }
                if (affect.electricity != 0)
                {
                    postModel.Electric = affect.electricity *Int32.Parse(Num);
                }
                if (affect.gasoline != 0)
                {
                    postModel.Forest = affect.gasoline * Int32.Parse(Num); 
                }
               
                list_PostModel.Add(postModel);
        }
        private void dealWithPhotoTrend()
        {
            
            List<PhotoTrendTable> list_PhotoTrends = new List<PhotoTrendTable>();
            foreach (int i in list_FriendId)
            {
                // TimeSpan span=24*nowTime.Subtract(DateTime.Parse(c.Time)).Days+nowTime.Subtract(DateTime.Parse(c.Time)).Hours;
                foreach (PhotoTrendTable trends in db.PhotoTrends)
                {
                    int days = nowTime.Subtract(DateTime.Parse(trends.Time)).Duration().Days;
                    int hour = nowTime.Subtract(DateTime.Parse(trends.Time)).Duration().Hours;
                    DateTime d = DateTime.Parse(trends.Time);
                    int hours = 24 * nowTime.Subtract(DateTime.Parse(trends.Time)).Duration().Days + nowTime.Subtract(DateTime.Parse(trends.Time)).Duration().Hours;
                  //  if (trends.UserId == i && hours <= 200)
                        list_PhotoTrends.Add(trends);
                }
            }

            foreach (PhotoTrendTable trend in db.PhotoTrends)
            {
                //UserTable user = db.Users.First(c => c.Id == trend.UserId);
                UnloadModel unloadModel = new UnloadModel()
                {
                    ItemId=trend.ItemId,
                    Name = trend.Name,
                    Time = trend.Time,
                    Describe =trend.Describe
                };
                list_UnloadModel.Add(unloadModel);
            }
            
        }
        private void dealWithAchievement()
        {
            List<AchievementTrendTable> list_AchievementTrends = new List<AchievementTrendTable>();
            foreach (int i in list_FriendId)
            {
                // TimeSpan span=24*nowTime.Subtract(DateTime.Parse(c.Time)).Days+nowTime.Subtract(DateTime.Parse(c.Time)).Hours;
                foreach (AchievementTrendTable trends in db.AchievementTrends)
                {
                    int days = nowTime.Subtract(DateTime.Parse(trends.Time)).Duration().Days;
                    int hour = nowTime.Subtract(DateTime.Parse(trends.Time)).Duration().Hours;
                    DateTime d = DateTime.Parse(trends.Time);
                    int hours = 24 * nowTime.Subtract(DateTime.Parse(trends.Time)).Duration().Days + nowTime.Subtract(DateTime.Parse(trends.Time)).Duration().Hours;
                  //  if (trends.UserId == i && hours <= 200)
                        list_AchievementTrends.Add(trends);
                }
            }

            foreach (AchievementTrendTable trend in db.AchievementTrends)
            {
               // UserTable user = db.Users.First(c => c.Id == trend.UserId);
                AchievementModel achievement = new AchievementModel()
                {
                    UserId=trend.UserId,
                    Name = trend.Name,
                    ImageUri=trend.ImageUri,
                    Describe=trend.Achievement,
                    Time=trend.Time,
                    Category=trend.Category
                };
                list_AchievementModel.Add(achievement);
            }
           
        }
    }
}
