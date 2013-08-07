using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Panda.Service.Helper;

namespace Panda.Service.DataAccess
{
    public interface IUserInfoAccess
    {
        UserInfo GetUserInfo(string userId);
        bool AddUserInfo(string userId,string userName,int type);

        int AddPoint(string userId, int point);
        int AddDealCount(string userId, int dealCount = 1);
        int AddUploadCount(string userId, int uploadCount = 1);

        bool ChangeDescription(string userId, string description);
        bool ChangePhone(string userId, string phone);
        bool ChangeImage(string userId, string ImagePath);
        bool ChangeEmail(string userId, string email);
        bool ChangeUserName(string userId, string userName);

        bool ChagneBirthDate(string userId,int year,int month,int day);
        

        /// <summary>
        /// 调用时即可将LastCallTime时间改为当前时间
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool ChangeCallTime(string userId);

        bool ChangeAddress(string userId,string address);
        bool ChangeAddress(string userId,decimal longitude, decimal latitude);

        int GetAllRank(string userId,out int allCount);
        int GetCityRank(string userId,out int localCount);

        

    }
    public class UserInfoAccess
    {
       
        #region private function
        private Database_PandaEntities db = new Database_PandaEntities();
        /// <summary>
        /// 1-point,2-dealcount,3-uploadCount
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="count"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private int AddCount(string userId, int count, int type)
        {
            int currentCount = 0;
            try
            {
                var model = db.UserInfos.Where(c => c.UserId == userId).FirstOrDefault();
                if (model == null)
                    return currentCount;
                switch (type)
                {
                    case 1: currentCount = (model.Point += count); break;
                    case 2: currentCount = (model.DealCount += count); break;
                    default: currentCount = (model.UploadCount += count); break;
                }
                db.SaveChanges();
                return currentCount;
            }
            catch
            {
                return currentCount;
            }

        }
        /// <summary>
        /// 1-description,2-phone,3-image,4-email,5-username,6-address
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="content"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool ChangeInfo(string userId, string content, int type)
        {
           
            try
            {
                var model = db.UserInfos.Where(c => c.UserId == userId).FirstOrDefault();
                switch (type)
                {
                    case 1: model.Description = StringHelper.LimitLength(content, 500); break;
                    case 2: model.Phone = StringHelper.LimitLength(content, 15); break;
                    case 3:model.ImagePath=StringHelper.LimitLength(content,200);break;
                    case 4:model.Email=StringHelper.LimitLength(content,50);break;

                    case 5:model.UserName=StringHelper.LimitLength(content);break;
                    default: model.Address = StringHelper.LimitLength(content, 100); break;
                }
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
      

        #endregion

        public  UserInfo GetUserInfo(string userId)
        {
            return db.UserInfos.Where(c => c.UserId == userId).FirstOrDefault() ?? new UserInfo();
        }
        public bool AddUserInfo(string userId, string userName, int type)
        {
            try
            {
                int rank = db.UserInfos.Count();
                UserInfo model = new UserInfo()
                {
                    UserId = StringHelper.LimitLength(userId),
                    UserName = StringHelper.LimitLength(userName),
                    Point = 0,
                    DealCount = 0,
                    UploadCount = 0,
                    Email = string.Empty,
                    Description = string.Empty,
                    Type = (type == 1 ? 1 : 2),
                    Phone = string.Empty,
                    Country = 1,  //默认中国为1
                    Province = -1,
                    City = -1,
                    Town = -1,
                    Street = -1,
                    //-1 表示暂无意义
                    Address = string.Empty,
                    ImagePath = string.Empty,
                    Longitude = -1.0M,
                    Latitude = -1.0M,
                    HasCustome = false,
                    AllRank = rank,
                    LastAllRank = rank,
                    CityRank = -1,
                    LastCityRank = -1,
                    //-1表示没有参加排名,城市未知，不可判断
                    LastCallTime = DateTime.Now.AddMonths(-1)

                };
                db.AddToUserInfos(model);
                db.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }

        #region addCount
        public int AddPoint(string userId, int point)
        {
            return AddCount(userId, point, 1);
        }
        public int AddDealCount(string userId, int dealCount = 1)
        {
            return AddCount(userId, dealCount, 2);
        }
        public int AddUploadCount(string userId, int uploadCount = 1)
        {
            return AddCount(userId, uploadCount, 3);
        }
        #endregion

        #region changeInfo
        public bool ChangeDescription(string userId, string description)
        {
            return ChangeInfo(userId, description, 1);
        }
        public bool ChangePhone(string userId, string phone)
        {
            return ChangeInfo(userId, phone, 2);
        }
        public bool ChangeImage(string userId,string imagePath)
        {
            return ChangeInfo(userId,imagePath,3);
        }
        public bool ChangeEmail(string userId, string email)
        {
            return ChangeInfo(userId, email, 4);
        }
        public bool ChangeUserName(string userId, string userName)
        {
            return ChangeInfo(userId, userName, 5);
        }
        public bool ChangeAddress(string userId, string address)
        {
            return ChangeInfo(userId, address, 6);
        }

        #endregion

        public bool ChangeBirthDate(string userId, int year, int month, int day)
        {
            try
            {
                var model = db.UserInfos.Where(c => c.UserId == userId).FirstOrDefault();
                DateTime birthDate = new DateTime(year, month, day);
                //数据错误时自动抛出异常
                model.BirthDate = birthDate;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ChangeAddress(string userId,decimal longitude,decimal latitude)
        {
            try
            {
                var model = db.UserInfos.Where(c => c.UserId == userId).FirstOrDefault();

                model.Longitude = longitude;
                model.Latitude = latitude;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ChangeCallTime(string userId)
        {
            try
            {
                var model = db.UserInfos.Where(c => c.UserId == userId).FirstOrDefault();

                model.LastCallTime = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        
        public int  GetAllRank(string userId,out int allCount)
        {
            
           var currentUser = db.UserInfos.Where(c => c.UserId == userId).FirstOrDefault();
           allCount = db.UserInfos.Count();
           if (currentUser == null)
           {
               throw new Exception("该用户不存在");
           }
            int currentPoint = currentUser.Point;
            int currentRank = db.UserInfos.Where(c => c.Point > currentPoint).Count() + 1;

            try
            {

                currentUser.LastAllRank = currentUser.AllRank;
                currentUser.AllRank = currentRank;

                db.SaveChanges();

                return currentRank;
            }

            catch
            {
                return currentRank;
            }
          

        }
        public int  GetCityRank(string userId, out int localCount)
        {
            var currentUser = db.UserInfos.Where(c => c.UserId == userId).FirstOrDefault();
            if (currentUser == null)
            {
                throw new Exception("该用户不存在");
            }
            int currentPoint = currentUser.Point;
            int city = currentUser.City;
            localCount = db.UserInfos.Where(c => c.City == city).Count();
            int currentRank=db.UserInfos.Where(c => c.City == city && c.Point > currentPoint).Count() + 1;
            try
            {
                currentUser.LastCityRank = currentUser.CityRank;
                currentUser.CityRank = currentRank;
                db.SaveChanges();
                return currentRank;
            }
            catch 
            {
                return currentRank;
            }
           
        }

        
    }
}