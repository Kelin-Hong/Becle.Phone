using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Panda.Service.Helper;


namespace Panda.Service.DataAccess
{
    public interface IFriendRelationAccess
    {
        void AddFriend(string userId, string friendId);
        void AddFriend(string userId, List<string> friendIds);
        bool DeleteFriend(string userId, string friendId);
        bool ChangeFocus(string userId, string friendId, bool isFocus);
        List<FriendRelation> GetFriendList(string userId);
        FriendRelation GetFriend(string id);
        FriendRelation GetFriend(string userId, string friendId);

    }
    public class FriendRelationAccess
    {
      
        private Database_PandaEntities db = new Database_PandaEntities();
        public void AddFriend(string userId, string friendId)
        {
            try
            {
                FriendRelation addModel = new FriendRelation()
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    UserId = StringHelper.LimitLength(userId),
                    FriendId = StringHelper.LimitLength(friendId),
                    IsFocus = false
                };
                db.AddToFriendRelations(addModel);
                db.SaveChanges();
            }
            catch
            {
                throw new Exception("数据保存出错");
            }

        }
        public void AddFriend(string userId, List<string> friendIds)
        { 
            //以一百个为单位保存
            int count = 0;
            foreach (var friendId in friendIds)
            {
                try
                {
                    FriendRelation addModel = new FriendRelation()
                    {
                        Id = Guid.NewGuid(),
                        CreateTime = DateTime.Now,
                        UserId = StringHelper.LimitLength(userId),
                        FriendId = StringHelper.LimitLength(friendId),
                        IsFocus = false
                    };
                    db.AddToFriendRelations(addModel);
                    count++;
                    if (count > 100)
                    {
                        db.SaveChanges();
                        count = 0;
                    }
                }
                catch
                {
                    throw new Exception("保存出错");
                }
                finally
                {
                    db.SaveChanges();
                }
            }
        }
        public  bool DeleteFriend(string userId, string friendId)
        {
            try
            {
                var deleteModel = db.FriendRelations.Where(c => c.UserId == userId & c.FriendId == friendId).FirstOrDefault();
                if (deleteModel == null)
                    return true;
                db.FriendRelations.DeleteObject(deleteModel);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public  bool ChangeFocus(string userId, string friendId, bool isFocus)
        {
            try
            {
                var updateModel = db.FriendRelations.Where(c => c.UserId == userId & c.FriendId == friendId).FirstOrDefault();
                if (updateModel == null)
                    return false;
                updateModel.IsFocus = isFocus;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        List<FriendRelation> GetFriendList(string userId)
        {
            return db.FriendRelations.Where(c => c.UserId == userId).ToList();
        }
        FriendRelation GetFriend(string id)
        {
            var returnModel = db.FriendRelations.Where(c => c.Id == new Guid(id)).FirstOrDefault();
            return returnModel ?? new FriendRelation();
        }
        FriendRelation GetFriend(string userId,string friendId)
        {
            var returnModel = db.FriendRelations.Where(c => c.UserId==userId && c.FriendId==friendId).FirstOrDefault();
            return returnModel ?? new FriendRelation();
        }
        
    }
}