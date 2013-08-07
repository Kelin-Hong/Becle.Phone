using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Panda.Service.Helper;

namespace Panda.Service.DataAccess
{
    public interface IUser_ItemAccess
    {
        void AddUser_Item(string userId, string itemName, int typeId, int trashid, decimal itemAmount);
        bool UpdateUser_Item(string userId, decimal itemAmount,string itemName);
        bool UpdateUser_Item(string userId, decimal itemAmount, int trashId);
        User_Item GetUser_Item(string userId, string itemName);
        User_Item GetUser_Item(string userId, int trashId);
        List<User_Item> GetUser_ItemList(string userId);
    }
    public class User_ItemAccess:IUser_ItemAccess
    {
        private Database_PandaEntities db = new Database_PandaEntities();

        public void AddUser_Item(string userId, string itemName, int typeId, int trashId, decimal itemAmount)
        {
            try
            {
                User_Item addModel = new User_Item()
                {
                    Id = Guid.NewGuid(),
                    UserId = StringHelper.LimitLength(userId),
                    ItemName = StringHelper.LimitLength(itemName),
                    TypeId = typeId,
                    TrashId = trashId,
                    ItemAmount = itemAmount,
                    ModifyTime = DateTime.Now
                };
                db.AddToUser_Items(addModel);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool  UpdateUser_Item(string userId, decimal itemAmount,string itemName )
        {
            try
            {
                User_Item updateModel = db.User_Items.Where(c => c.UserId == userId && c.ItemName == itemName).FirstOrDefault();
                if (updateModel != null)
                {
                    updateModel.ItemAmount += itemAmount;
                    updateModel.ModifyTime = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch 
            {
                return false;
            }
        }
        public bool UpdateUser_Item(string userId, decimal itemAmount, int trashId)
        {
            try
            {
                User_Item updateModel = db.User_Items.Where(c => c.UserId == userId  && c.TrashId==trashId).FirstOrDefault();
                if (updateModel != null)
                {
                    updateModel.ItemAmount += itemAmount;
                    updateModel.ModifyTime = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch 
            {
                return false;
            }
        }
        public User_Item GetUser_Item(string userId, string itemName)
        {
            User_Item returnModel = db.User_Items.Where(c => c.UserId == userId && c.ItemName == itemName).FirstOrDefault();
            return returnModel ?? new User_Item();
        }

        public User_Item GetUser_Item(string userId, int trashId)
        {
            User_Item returnModel = db.User_Items.Where(c => c.UserId == userId && c.TrashId==trashId).FirstOrDefault();
            return returnModel ?? new User_Item();
        }
        public List<User_Item> GetUser_ItemList(string userId)
        {
            return db.User_Items.Where(c => c.UserId == userId).OrderByDescending(c=>c.ItemAmount).ToList();
        }

    }
}