using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Becle.Server.DataModel;
using System.IO;
using Becle.Server.Database;
//using WindowsPhone.PushNotificationManager;
namespace Becle.Server.Service
{
   public class PublisherService:IPublisherService
    {
       BecleEntities db = new BecleEntities();
       private NotificationSenderUtility notifier = new NotificationSenderUtility();
       #region{Register&Login]
       string getCity(string s)
       {
         return s.Substring(s.IndexOf('省')+1,s.IndexOf('市')-s.IndexOf('省')-1);
       }
       public string  Register(RegisterUser user)
        {
            Console.WriteLine(user.UserName + "Register");
            if (user.UserType == 0)
            {
                UserTable userTable = new UserTable()
                {
                    UserName = user.UserName,
                    Address = user.Address,
                    City = getCity(user.Address),
                    Latitude = user.Latitude,
                    Longitude = user.Longitude,
                    Password = user.Passwrod,
                    Point = 10,
                    Avatar = user.ImageFileData,
                    LastRank = 200,
                    CityLastRank=100,
                    NowRank=200,
                    NowCityRank=100,
                    UserRank=100,
                    CityRank=100
                };
                db.UserTables.AddObject(userTable);
                db.SaveChanges();
                UserTable userTable1 = db.UserTables.SingleOrDefault(c => c.UserName == userTable.UserName && c.Password == userTable.Password);
                foreach (UserTable item in db.UserTables.Where(c=>c.City==userTable1.City))
                {
                    FriendTable Friend = new FriendTable()
                    {
                        UserId = userTable1.Id,
                        FriendId=item.Id
                    };
                    if (Friend.UserId != Friend.FriendId)
                    {
                        db.FriendTables.AddObject(Friend);
                      //  db.SaveChanges();
                    }
                    FriendTable Friend1 = new FriendTable()
                    {
                        UserId = item.Id,
                        FriendId = userTable1.Id
                    };
                    if (Friend1.UserId != Friend1.FriendId)
                    {
                        db.FriendTables.AddObject(Friend1);
                      //  db.SaveChanges();
                    }
                }
            }
            if (user.UserType == 1)
            {
                AcquirerTable acquirer = new AcquirerTable()
                {
                    UserName = user.UserName,
                    Address = user.Address,
                    City = getCity(user.Address),
                    Latitude = user.Latitude,
                    Longitude = user. Longitude,
                    PassWord = user.Passwrod,
                    Phone=user.Phone,
                    BinName=user.StoreBinName,
                    Avatar = user.ImageFileData,
                };
                db.AcquirerTables.AddObject(acquirer);
            }
            if (user.UserType == 2)
            {
                MerChantTable merchant = new MerChantTable()
                {
                    UserName = user.UserName,
                    Address = user.Address,
                    Latitude = user.Latitude,
                    Longitude = user.Longitude,
                    PassWord = user.Passwrod,
                    Phone = user.Phone,
                    StoreName = user.StoreBinName,
                    Avatar = user.ImageFileData,
                };
                db.MerChantTables.AddObject(merchant);
            }
            if (!db.CityTables.Select(c => c.Name).Contains(getCity(user.Address)))
                {
                    CityTable city = new CityTable()
                    {
                        Name = user.City,
                        LastRank = 100,
                        Point = 10
                    };
                    db.CityTables.AddObject(city);
                }
               
                db.SaveChanges();

            return "Register Success";
        }
       public LoginBack Login(Login login)
        {
            Console.WriteLine(login.UserName + "Login");                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
            LoginBack loginBack = new LoginBack();
            UserTable user = db.UserTables.SingleOrDefault(c => c.UserName == login.UserName&&c.Password==login.Passwrod);
            AcquirerTable acquirer = db.AcquirerTables.SingleOrDefault(c => c.UserName == login.UserName && c.PassWord == login.Passwrod);
          //  MerChantTable merchant = db.MerChantTables.SingleOrDefault(c => c.UserName == login.UserName && c.PassWord == login.Passwrod);
           if (user != null)
            {
                loginBack.Message = "OK";
                loginBack.UserId = user.Id;
                loginBack.UserType = 0;
                Console.WriteLine(login.UserName + "Login Success");
                return loginBack;
            }
            if (acquirer != null)
            {
                loginBack.Message = "OK";
                loginBack.UserId = acquirer.Id;
                loginBack.UserType = 1;
                Console.WriteLine(login.UserName + "Login Success");
                return loginBack;
            }
            //if (merchant != null)
            //{
            //    loginBack.Message = "OK";
            //    loginBack.UserId = merchant.Id;
            //    loginBack.UserType = 2;
            //    return loginBack;
            //}

            loginBack.Message = "Error";
            loginBack.UserId = -1;
            loginBack.UserType = -1;
            return loginBack;
        }
        public UserInfo GetUserInfo(int userId)
        {
            UserTable user = db.UserTables.SingleOrDefault(c => c.Id == userId);
            UserInfo userInfo = new UserInfo()
            {
                UserName=user.UserName,
                City=user.City,
                Address=user.Address,
                Longitude=user.Longitude,
                Latitude=user.Latitude,
                LastRank=(int)user.LastRank,
            };
            return userInfo;
        }
       #endregion

       #region[PostResourse]
        public string Post(PostItem postItem)
        {
           UserTable user= db.UserTables.SingleOrDefault(c=>c.Id==postItem.UserId);
           Console.WriteLine(user.UserName + "Post" + postItem.Name);
           user.Point += postItem.GetPoints;
            ItemTable item = new ItemTable()
            {
                Amount=postItem.Amount,
                CategoryId=postItem.CategoryId,
                Describe=postItem.Describe,
                Name=postItem.Name,
                UserId=postItem.UserId,      
                Image=postItem.PostImage,
                Time=DateTime.Now.ToShortDateString()
            };
            db.ItemTables.AddObject(item);
            db.SaveChanges();
            Console.WriteLine(user.UserName + "Post" + postItem.Name + "Save To DataBase Success");
            PostAffectTable postAffect = new PostAffectTable()
            {
                UserId=postItem.UserId,
                Time=DateTime.Now.ToShortDateString(),
                PostAffect=postItem.PostAffect,
                Describe="Posts "+postItem.Amount+"  "+postItem.Name,
             };
            db.PostAffectTables.AddObject(postAffect);
            PhotoTrendTable photoTrendTable = new PhotoTrendTable()
            {
                Describe=" posts "+ postItem.Name,
                ItemId=item.Id,
                Time = DateTime.Now.ToShortDateString(),
                UserId=postItem.UserId
            };
            db.PhotoTrendTables.AddObject(photoTrendTable);
            db.SaveChanges();
            int i = 0;
            foreach (UserTable userTable in db.UserTables.ToList().OrderByDescending(c => c.Point))
            {
                i++;
                if (userTable.Id == postItem.UserId)
                {
                    user.NowRank = i;
                    break;
                }
            }
            i = 0;
            foreach (UserTable userTable in db.UserTables.Where(c=>c.City==user.City).ToList().OrderByDescending(c => c.Point))
            {
                i++;
                if (userTable.Id == postItem.UserId)
                {
                    user.NowCityRank = i;
                    break;
                }
            }
            dealWithAchievement(user);
            user.UserRank = user.NowRank - user.LastRank;
            user.CityRank = user.NowCityRank - user.CityLastRank;
            user.LastRank = user.NowRank;
            user.CityLastRank = user.NowCityRank;
            db.SaveChanges();
            return postItem.Name+"Unload OK";
        }
        #endregion
        
       #region[FriendTrend]
        public List<PostTrend> GetPostTrend(int userId)
        {
            List<PostTrend> list_PostTrend = new List<PostTrend>();
            List<PostAffectTable> list_postAffectTable = new List<PostAffectTable>();
            List<int> list_FriendId = db.FriendTables.Where(c => c.UserId == userId).Select(c => c.FriendId).ToList<int>();
            list_FriendId.Add(userId);
            foreach(int friendId in list_FriendId)
            {
               list_postAffectTable.AddRange(db.PostAffectTables.Where(c => c.UserId == friendId));
            }
            foreach (PostAffectTable item in list_postAffectTable)
            {
                UserTable user=db.UserTables.SingleOrDefault(c=>c.Id==item.UserId);
                PostTrend postTrend = new PostTrend()
                {
                    PostAffect = item.PostAffect,
                    Describe=item.Describe,
                    Time=item.Time,
                    UserId=user.Id,
                    Name=user.UserName
                };
                list_PostTrend.Add(postTrend);

            }

            return list_PostTrend;
        }

        public List<PhotoTrend> GetPhotoTrend(int userId)
        {
            List<PhotoTrend> list_PhotoTrend = new List<PhotoTrend>();
            List<PhotoTrendTable> list_photoTrendTable = new List<PhotoTrendTable>();
            List<int> list_FriendId = db.FriendTables.Where(c => c.UserId == userId).Select(c => c.FriendId).ToList<int>();
            list_FriendId.Add(userId);
            foreach (int friendId in list_FriendId)
            {
                list_photoTrendTable.AddRange(db.PhotoTrendTables.Where(c => c.UserId == friendId));
            }
            foreach (PhotoTrendTable item in list_photoTrendTable)
            {
               UserTable user=db.UserTables.SingleOrDefault(c=>c.Id==item.UserId);
                PhotoTrend photoTrend = new PhotoTrend()
                {
                    Describe = item.Describe,
                    Time = item.Time,
                    Name=user.UserName,
                    ItemId=item.ItemId,
                    UserId=item.UserId
                };
                list_PhotoTrend.Add(photoTrend);
            }
            return list_PhotoTrend;
        }

       private void dealWithAchievement(UserTable user)
       {
          // List<int> list_FriendId = db.FriendTables.Where(c => c.UserId == userId).Select(c => c.FriendId).ToList<int>();
          
          // list_FriendId.Add(userId);
           //foreach (int j in list_FriendId) Console.WriteLine(j);
           int[] Num=new int[10];
           //foreach (int friendId in list_FriendId)
          // {
            //   {
               // //   if (user.Id == friendId)
                 //  {
                       if (user.NowRank<=8 && user.LastRank > 8)
                       {
                           AchievementTrendTable achievementTable = new AchievementTrendTable()
                           {
                               Achievement="rise to NO."+user.NowRank,
                               Time=DateTime.Now.ToShortDateString(),
                               UserId=user.Id
                           };
                       
                           db.AchievementTrendTables.AddObject(achievementTable);
                           
                       }
                       if (user.NowRank == 1 && user.LastRank != 1)
                       {
                           foreach (ItemTable item in db.ItemTables.Where(c=>c.UserId==user.Id))
                           {
                               Num[item.CategoryId]+=item.Amount;
                           }
                           int max=0;
                           int categotyId=0;
                           for(int j=1;j<=6;j++)
                           {
                               if(Num[j]>max)
                               {
                                   max=Num[j];
                                   categotyId=j;
                               }
                           }
                           string categoryName=db.CategoryTables.SingleOrDefault(c=>c.Id==categotyId).Name;
                           AchievementTrendTable achievementTable = new AchievementTrendTable()
                           {
                               Achievement = categoryName,
                               Time = DateTime.Now.ToShortDateString(),
                               UserId = user.Id
                           };
                           db.AchievementTrendTables.AddObject(achievementTable);
                       }
                     
                       
           db.SaveChanges();
       }
        public List<AchievementTrend> GetAchievementTrend(int userId)
        {
           
            List<AchievementTrend> list_AchievementTrend = new List<AchievementTrend>();
            List<AchievementTrendTable> list_AchievementTrendTable = new List<AchievementTrendTable>();
            List<int> list_FriendId = db.FriendTables.Where(c => c.UserId == userId).Select(c => c.FriendId).ToList<int>();
            list_FriendId.Add(userId);
            foreach (int friendId in list_FriendId)
            {
                list_AchievementTrendTable.AddRange(db.AchievementTrendTables.Where(c => c.UserId == friendId));
            }
            foreach (AchievementTrendTable item in list_AchievementTrendTable)
            {
                 UserTable user=db.UserTables.SingleOrDefault(c=>c.Id==item.UserId);
                AchievementTrend achievementTrend = new AchievementTrend()
                {
                   Achievement=item.Achievement,
                   Time=item.Time,
                   Name=user.UserName,
                   UserId=user.Id
                };
                //if(achievementTrend.Achievement.Contains("title"))
                //{
                //    achievementTrend.ImageUri = "/Image_Friends/crown.png";
                //}
                //else
                //{
                //    achievementTrend.ImageUri = "/Image_Friends/crown.png";
                //};
                list_AchievementTrend.Add(achievementTrend);
            }
            return list_AchievementTrend;
        }
        #endregion

       #region[Recycle&Reward]
        public string SendMessageToAcquirer(Message message)
        {
            MessageTable table = new MessageTable()
            {
                FromId=message.FromId,
                IsSee=false,
                ToId=message.ToId,
                Time=DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString(),
                Message=message.MessageContent,
            };
            db.MessageTables.AddObject(table);
            db.SaveChanges();
            return "OK";
        }
        public List<Reward> GetReward(int userId)
        {
            List<Reward> list_Reward = new List<Reward>();
            UserTable user=db.UserTables.SingleOrDefault(c=>c.Id==userId);
            foreach (RewardTable item in db.RewardTables)
            {
                if (item.City == user.City)
                {
                    Reward reward = new Reward()
                    {
                        Address = item.Address,
                        Describe = item.Describe,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        Phone = item.Phone,
                        StoreName = item.StoreName
                    };
                    list_Reward.Add(reward);
                }
            }
            return list_Reward;
        }
        public List<Acquirer> GetAcquirer(int userId)
        {
            List<Acquirer> list_Acquirer = new List<Acquirer>();
            UserTable user = db.UserTables.SingleOrDefault(c => c.Id == userId);
            foreach (AcquirerTable item in db.AcquirerTables)
            {
                if (item.City == user.City)
                {
                    Acquirer acquirer = new Acquirer()
                    {
                        Address = item.Address,
                        BinName=item.BinName,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        Phone = item.Phone,
                        UserName= item.UserName,
                        AcquirerId=item.Id
                    };
                    list_Acquirer.Add(acquirer);
                }
            }
            return list_Acquirer;
        }
        public List<MessageToUser> GetMessge(int userId)
        {
            List<MessageToUser> list = new List<MessageToUser>();
            foreach (MessageToUserTable item in db.MessageToUserTables.Where(c => c.ToId == userId && c.IsSee == false))
            {
                if (item.Type == 1)
                {
                    string name="";
                    AcquirerTable acquirer=db.AcquirerTables.FirstOrDefault(c => c.Id == item.FromId);
                    UserTable user = db.UserTables.FirstOrDefault(c => c.Id == item.FromId);
                    if (acquirer != null)
                    {
                        name= acquirer.UserName;
                    }
                    if (user != null)
                    {
                        name = user.UserName;
                    }
                    MessageToUser message = new MessageToUser()
                    {
                        Name=name,
                        FromId = item.FromId,
                        MessageContent = item.MessageContent,
                        Type = item.Type
                    };
                    list.Add(message);
                    item.IsSee = true;
                }
                else
                {
                        MessageToUser message = new MessageToUser()
                        {
                            MessageContent = item.MessageContent,
                            Type = item.Type
                        };
                        item.IsSee = true;
                        list.Add(message);
                }
            }
            db.SaveChanges();
            return list;
        }
        #endregion

       #region[GetImage]
        public UserImage GetImageByUserId(int userId)
        {
            UserTable user= db.UserTables.Single(c => c.Id == userId);
            return
                new UserImage()
            {
                UserId=user.Id,
                Avatar=user.Avatar
            };
        }
        public AcquirerImage GetImageByAcquirerId(int acquirerId)
        {
            AcquirerTable acquirer = db.AcquirerTables.Single(c => c.Id == acquirerId);
            return
                new AcquirerImage()
                {
                   AcquirerId=acquirerId,
                   Image=acquirer.Avatar
                };
        }
        public ItemImage GetImageByItemId(int itemId)
        {
            ItemTable item = db.ItemTables.Single(c => c.Id == itemId);
            return new ItemImage()
            {
                Image=item.Image,
                ItemId=item.Id
            };
        }
        #endregion

       #region[Data]
        public string GetMyRank(int userId)
        {
            UserTable user=db.UserTables.SingleOrDefault(c=>c.Id==userId);
            int i =(int)user.UserRank;
            string User_Rank="";
            if (i <= 0) User_Rank =user.NowRank+ " ↓";
            if (i > 0) User_Rank = user.NowRank + " ↑";
            //foreach (UserTable user in db.UserTables.ToList().OrderByDescending(c => c.Point))
            //{
            //    i++;
            //    if (user.Id == userId)
            //    {
            //        if (i > user.LastRank) User_Rank = "↓" + (i - user.LastRank);
            //        if (i <= user.LastRank) User_Rank = "↑" + (user.LastRank - i);
            //        user.LastRank = i;
            //        break;
            //    }  
            //}
            //db.SaveChanges();

            return User_Rank;
           
        }

        public string GetMyCityRank(int userId)
        {
            UserTable user = db.UserTables.SingleOrDefault(c => c.Id == userId);
            int i = (int)user.CityRank;
            string City_Rank = "";
            if (i <= 0) City_Rank = user.NowCityRank+" ↓";
            if (i > 0) City_Rank = user.NowCityRank + " ↑";
           

            //foreach (UserTable item in db.UserTables.ToList().Where(c=>c.City==city).OrderByDescending(c => c.Point))
            //{
            //    i++;
            //    if (item.Id == userId) 
            //    {
            //        if (i > item.LastRank) City_Rank = "↓" + (i - item.LastRank);
            //        if (i <= item.LastRank) City_Rank = "↑" + (item.LastRank - i);
            //        item.LastRank = i;
            //        break;
            //    }  
            //}
            //db.SaveChanges();
            return City_Rank;
        }
        public int GetMyPoints(int userId)
        {
            return db.UserTables.Single(c => c.Id == userId).Point;
        }

        public int GetAveragePointsAllUser()
        {
            return (int)Math.Round(db.UserTables.Average(c => c.Point), 0);
        }

        public int GethighestPointsAlluser()
        {
            return db.UserTables.Max(c => c.Point);
        }

        public int GetHighestPointsInCity(string city)
        {
            return db.UserTables.Where(c => c.City == city).Max(c => c.Point);
        }

        public int GetAveragePointsInCity(string city)
        {
            return  (int)Math.Round(db.UserTables.Where(c => c.City == city).Average(c => c.Point),0);
        }

        public List<City> GetRankCity()
        {
           List<City> list_City=new List<City>();
           foreach (CityTable item in db.CityTables.ToList().OrderByDescending(c => c.Point).Take(10))
           {
                City city=new City()
                {
                    LastRank=(int)item.LastRank,
                    Name=item.Name,
                    Points=item.Point
                };
                list_City.Add(city);
           }
            return list_City;
        }
        public List<DataUser> GetRankUser()
        {
            List<DataUser> list_DataUser = new List<DataUser>();
            foreach (UserTable item in db.UserTables.ToList().OrderByDescending(c => c.Point).Take(10))
            {
                DataUser datauser = new DataUser()
                {
                    City=item.City,
                    Points=item.Point,
                    UserName=item.UserName
                };
                list_DataUser.Add(datauser);
            }
            return list_DataUser;
        }
        #endregion

        #region[AcquirerService]
        public List<Resident> GetResidents(int acquirerId)
        {
            List<Resident> list = new List<Resident>();
           AcquirerTable acquirer=db.AcquirerTables.SingleOrDefault(c=>c.Id==acquirerId);
           foreach (UserTable user in db.UserTables.Where(c => c.City == acquirer.City))
           {
               Resident resident = new Resident()
               {
                   Address=user.Address,
                   Latitude=user.Latitude,
                   Longitude=user.Longitude,
                   UserName=user.UserName,
                   UserId=user.Id
               };
               list.Add(resident);
           }
           return list;
        }
        public string SendMessageToUser(MessageToUser message)
        {
            List<Uri> list = new List<Uri>();
            
            if (message.Type == 1)
            {
                Console.WriteLine("type:  1"+message.FromId+"--"+message.ToId);
                MessageToUserTable table = new MessageToUserTable()
                {
                    FromId = message.FromId,
                    ToId = message.ToId,
                    Type = 1,
                    MessageContent = message.MessageContent,
                    Time = message.Time,
                    IsSee = false,
                };
                db.MessageToUserTables.AddObject(table);
                db.SaveChanges();
                if (RegistrationService.GetSubscribers().Keys.Contains(message.ToId))
                {
                    list.Add(RegistrationService.GetSubscribers()[message.ToId]);
                    
                        notifier.SendTileNotification(list, "", "", 1, "", null);
                        string UserName = db.AcquirerTables.SingleOrDefault(c => c.Id == message.FromId).UserName;
                        Console.WriteLine("From " + UserName + " To " + message.ToId+"   MessageContent:"+message.MessageContent);
                        notifier.SendToastNotification(list, UserName, message.MessageContent, null);
                   
                }
            }
            if (message.Type == 2)
            {
                MessageToUserTable table2 = new MessageToUserTable()
                {
                    FromId = message.FromId,
                    ToId = message.ToId,
                    Type = 2,
                    MessageContent = message.MessageContent,
                    Time = message.Time,
                    IsSee = false,
                };
                db.MessageToUserTables.AddObject(table2);
                db.SaveChanges();
                Console.WriteLine("type:  2");
                if (RegistrationService.GetSubscribers().Keys.Contains(message.ToId))
                {
                    list.Add(RegistrationService.GetSubscribers()[message.ToId]);
                    notifier.SendTileNotification(list, "", "", 1, "", null);
                    Console.WriteLine("From System Message to "+ +  message.ToId+ " MessageContent:" + message.MessageContent);
                    notifier.SendToastNotification(list, "System Message", message.MessageContent, null);
                }
            }
            return "OK";
        }
        public List<Item> GetItems(int acquirerId)
        {
           List<Item> list = new List<Item>();
           AcquirerTable acquirer=db.AcquirerTables.SingleOrDefault(c=>c.Id==acquirerId);
           foreach (UserTable user in db.UserTables.Where(c => c.City == acquirer.City))
           {
               foreach (ItemTable item in db.ItemTables.Where(c => c.UserId == user.Id))
               {
                   Item item1 = new Item()
                   {
                       CategoryId=item.CategoryId,
                       UserId=item.UserId,
                       Name=item.Name,
                       Num=item.Amount,
                       Time=item.Time,
                       ItemId=item.Id
                   };
                   list.Add(item1);
               }
           }
           return list;
        }
        public List<Message> GetAciquirer_Messages(int acquirerId)
        {
            List<Message> list_Message=new List<Message>();
            List<MessageTable> list = db.MessageTables.Where(c => c.ToId == acquirerId&&c.IsSee==false).ToList();
            foreach (MessageTable item in list)
            {
                Message message = new Message()
                {
                    FromId=item.FromId,
                    MessageContent=item.Message,
                    Time=item.Time
                };
                item.IsSee = true;
                list_Message.Add(message);
            }
            db.SaveChanges();
            return list_Message;
        }
        public Acquirer GetAcquirerInfo(int acquirerId)
        {
          AcquirerTable acquirerTable=  db.AcquirerTables.SingleOrDefault(c => c.Id == acquirerId);
          Acquirer acquirer = new Acquirer()
          {
              Address=acquirerTable.Address,
              BinName=acquirerTable.BinName,
              City=acquirerTable.City,
              Latitude=acquirerTable.Latitude,
              Longitude=acquirerTable.Longitude,
              UserName=acquirerTable.UserName,
              Phone=acquirerTable.Phone    
          };
          return acquirer;
        }
        #endregion

        #region[VolunteerService]

        public List<Resident> GetResidents_V(int acquirerId)
        {
            List<Resident> list = new List<Resident>();
            UserTable acquirer = db.UserTables.SingleOrDefault(c => c.Id == acquirerId);
            foreach (UserTable user in db.UserTables.Where(c => c.City == acquirer.City))
            {
                Resident resident = new Resident()
                {
                    Address = user.Address,
                    Latitude = user.Latitude,
                    Longitude = user.Longitude,
                    UserName = user.UserName,
                    UserId = user.Id
                };
                list.Add(resident);
            }
            return list;
        }
        public string SendMessageToUser_V(MessageToUser message)
        {
            List<Uri> list = new List<Uri>();
            if (message.Type == 1)
            {
                MessageToUserTable table = new MessageToUserTable()
                {
                    FromId = message.FromId,
                    ToId = message.ToId,
                    Type = 1,
                    MessageContent = message.MessageContent,
                    Time = message.Time,
                    IsSee = false,
                };
                db.MessageToUserTables.AddObject(table);
                db.SaveChanges();
                if (RegistrationService.GetSubscribers().Keys.Contains(message.ToId))
                {
                    list.Add(RegistrationService.GetSubscribers()[message.ToId]);
                    {
                        notifier.SendTileNotification(list, "", "", 1, "", null);
                        string UserName = db.UserTables.SingleOrDefault(c => c.Id == message.FromId).UserName;
                        notifier.SendToastNotification(list, UserName, message.MessageContent, null);
                    }
                }
            }
            if (message.Type == 2)
            {
                MessageToUserTable table2 = new MessageToUserTable()
                {
                    FromId = message.FromId,
                    ToId = message.ToId,
                    Type = 2,
                    MessageContent = message.MessageContent,
                    Time = message.Time,
                    IsSee = false,
                };
                db.MessageToUserTables.AddObject(table2);
                db.SaveChanges();
                if (RegistrationService.GetSubscribers().Keys.Contains(message.ToId))
                {
                    list.Add(RegistrationService.GetSubscribers()[message.ToId]);
                    notifier.SendTileNotification(list, "", "", 1, "", null);
                    notifier.SendToastNotification(list, "System Message", message.MessageContent, null);
                }
            }
            //if(message.Type==3)
            //{
            //    MessageToUserTable table3 = new MessageToUserTable()
            //    {
            //        FromId = message.FromId,
            //        ToId = message.ToId,
            //        Type = 3,
            //        MessageContent = message.MessageContent,
            //        Time = message.Time,
            //        IsSee = false,
            //    };
            //    db.MessageToUserTables.AddObject(table3);
            //    db.SaveChanges();
            //}
            return "OK";
        }
        public List<Item> GetItems_V(int acquirerId)
        {
            List<Item> list = new List<Item>();
            UserTable acquirer = db.UserTables.SingleOrDefault(c => c.Id == acquirerId);
            foreach (UserTable user in db.UserTables.Where(c => c.City == acquirer.City))
            {
                foreach (ItemTable item in db.ItemTables.Where(c => c.UserId == user.Id))
                {
                    Item item1 = new Item()
                    {
                        CategoryId = item.CategoryId,
                        UserId = item.UserId,
                        Name = item.Name,
                        Num = item.Amount,
                        Time = item.Time,
                        ItemId = item.Id
                    };
                    list.Add(item1);
                }
            }
            return list;
        }
        #endregion
        
    }
}
