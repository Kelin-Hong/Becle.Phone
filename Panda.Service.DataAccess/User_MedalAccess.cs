using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Panda.Service.Helper;

namespace Panda.Service.DataAccess
{
    public interface IUser_MedalAccess
    {
        void AddUser_Medal(int type,string userId);
        User_Medal GetUser_Medal(string medalId);
        User_Medal GetUser_Medal(string userId, int type);
        int UpgradeUser_Medal(int type, string userId);
        List<User_Medal> GetUser_MedalList(string userId);
    }

    public class User_MedalAccess:IUser_MedalAccess
    {
        private Database_PandaEntities db = new Database_PandaEntities();
        public void AddUser_Medal(int type,string userId)
        {
            try
            {
                User_Medal addModel = new User_Medal()
                {
                    Id=Guid.NewGuid(),
                    UserId = userId,
                    Type = type,
                    IsShown = true,
                    CreateTime = DateTime.Now,
                    Rank = 1

                };
                db.AddToUser_Medals(addModel);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpgradeUser_Medal(int type, string userId)
        {
            try
            {
                User_Medal model = db.User_Medals.Where(c => c.Type == type && c.UserId == userId).FirstOrDefault();
                if (model != null)
                {
                    model.Rank++ ;
                    int returnInt = model.Rank;
                    model.IsShown = true;
                    model.CreateTime = DateTime.Now;
                    db.SaveChanges();
                    return returnInt;
                }
                else
                {
                    AddUser_Medal(type, userId);
                    return 1;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  User_Medal GetUser_Medal(string id)
        {
            var returnModel = db.User_Medals.Where(c => c.Id ==new Guid(id)).FirstOrDefault();
            return returnModel ?? new User_Medal();
        }
        public User_Medal GetUser_Medal(string userId, int type)
        {
            var returnModel = db.User_Medals.Where(c=>c.UserId==userId && c.Type==type).FirstOrDefault();
            return returnModel ?? new User_Medal();
        }
        public List<User_Medal> GetUser_MedalList(string userId)
        {
            return db.User_Medals.Where(c => c.UserId == userId).OrderBy(c => c.Rank).ToList();
        }
    }
}