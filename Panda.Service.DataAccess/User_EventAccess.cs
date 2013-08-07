using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Panda.Service.Helper;

namespace Panda.Service.DataAccess
{

    public interface IUser_EventAccess
    {
        void AddUser_Event(string userId, string content);
        bool ChangeIsSeen(string id, bool isSeen);

        User_Event GetUser_Event(string id);
        List<User_Event> GetUser_EventList(string userId);
        int AddGoodCount(string id,int goodCount);
        
        
    }
    public class User_EventAccess:IUser_EventAccess
    {
        private Database_PandaEntities db = new Database_PandaEntities();
        public void AddUser_Event(string userId, string content)
        {
            try
            {
                User_Event addModel = new User_Event()
                {
                    Id = Guid.NewGuid(),
                    Content = StringHelper.LimitLength(content, 500),
                    CreateTime = DateTime.Now,
                    GoodCount = 0,
                    IsSeen = true,
                    UserId = StringHelper.LimitLength(userId)
                };
                db.AddToUser_Events(addModel);
                db.SaveChanges();
            }
            catch
            { 
                throw new Exception("添加错误");
            }
        }
        public bool ChangeIsSeen(string id, bool isSeen)
        {
            try
            {
                var updateModel = db.User_Events.Where(c => c.Id == new Guid(id)).FirstOrDefault();
                if (updateModel == null)
                    return false;
                updateModel.IsSeen = isSeen;
                db.SaveChanges();
                return true;

            }
            catch { return false; }
        }
        /// <summary>
        /// 目前认为只能加，不打算减
        /// </summary>
        /// <param name="id"></param>
        /// <param name="goodCount"></param>
        /// <returns></returns>
        public int AddGoodCount(string id, int goodCount = 1)
        {
            int returnCount = 0;
            try
            {
                var updateModel = db.User_Events.Where(c => c.Id == new Guid(id)).FirstOrDefault();
                if (updateModel == null)
                    return returnCount;
                 returnCount = ++updateModel.GoodCount;
                db.SaveChanges();
                return returnCount;

            }
            catch
            {
                return returnCount;
            }
        }

        public User_Event GetUser_Event(string id)
        {
            var returnModel = db.User_Events.Where(c => c.Id == new Guid(id)).FirstOrDefault();
            return returnModel ?? new User_Event();
        }
        public List<User_Event> GetUser_EventList(string userId)
        {
            return db.User_Events.Where(c => c.UserId == userId).OrderByDescending(c=>c.CreateTime).ToList();
        }
    }
}