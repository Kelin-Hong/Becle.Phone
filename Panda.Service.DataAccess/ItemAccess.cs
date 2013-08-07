using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Panda.Service.Helper;

namespace Panda.Service.DataAccess
{
    public interface IItemAccess
    {
        
        void AddItem(int typeId,int trashId,decimal amount,string imagePath,string description,string name,string userId,string address,decimal longitude,decimal latitude);
        bool DeleteItem(string id);
        
        bool ChangeDeal(string id);
        bool ChangeImagePath(string id, string imagePath);
        bool ChangeDescription(string id, string description);
        List<Item> GetAllItemList(string userId);
        List<Item> GetUndealItemList(string userId);
        List<Item> GetDealItemList(string userId);
        List<Item> GetLocalItemList(string address);
        List<Item> GetLocalItemList(decimal longitude, decimal latitude);
        Item GetItem(string id);

    }
    public class ItemAccess : IItemAccess
    {
        private Database_PandaEntities db = new Database_PandaEntities();

        /// <summary>
        /// 只能将true改为false,不能将false改为true
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ChangeDeal(string id)
        {
            try
            {
                var model = db.Items.Where(c => c.ItemId == new Guid(id)).FirstOrDefault();
                if (model == null)
                    return false;
                model.IsDeal = false;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ChangeImagePath(string id, string imagePath)
        {
            try
            {
                var model = db.Items.Where(c => c.ItemId == new Guid(id)).FirstOrDefault();
                if (model == null)
                    return false;
                model.ImagePath = StringHelper.LimitLength(imagePath, 200);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ChangeDescription(string id, string description)
        {
            try
            {
                var model = db.Items.Where(c => c.ItemId == new Guid(id)).FirstOrDefault();
                if (model == null)
                    return false;
                model.Description = StringHelper.LimitLength(description, 500);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeId">垃圾大类</param>
        /// <param name="trashId">垃圾二级分类</param>
        /// <param name="amount">垃圾数量，是decimal类型</param>
        /// <param name="imagePath">图片地址</param>
        /// <param name="description">垃圾的描述语言</param>
        /// <param name="name">垃圾名称</param>
        /// <param name="userId">上传者</param>
        /// <param name="address">垃圾在地图上的地址名称</param>
        /// <param name="longitude">地图上的经度</param>
        /// <param name="latitude">地图上的纬度</param>
        public void AddItem(int typeId, int trashId, decimal amount, string imagePath, string description, string name, string userId, string address, decimal longitude, decimal latitude)
        {
            try
            {
                Item addModel = new Item()
                {
                    ItemId = Guid.NewGuid(),
                    TypeId = typeId,
                    TrashId = trashId,
                    Amount = amount,
                    ImagePath=StringHelper.LimitLength(imagePath,200),
                    Description = StringHelper.LimitLength(description, 500),
                    Name = StringHelper.LimitLength(name),
                    UserId = StringHelper.LimitLength(userId),
                    Address = StringHelper.LimitLength(address, 100),
                    Longitude = longitude,
                    Latitude = latitude,
                    IsDeal = false,
                    CreateTime = DateTime.Now
                };
               
                db.AddToItems(addModel);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool DeleteItem(string id)
        {
            try
            {
                Item deleteModel = db.Items.Where(c => c.ItemId == new Guid(id)).FirstOrDefault();
                if (deleteModel != null)
                {
                    db.Items.DeleteObject(deleteModel);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Item GetItem(string id)
        {
            var returnModel = db.Items.Where(c => c.ItemId == new Guid(id)).FirstOrDefault();
            return returnModel ?? new Item();
        }
        public List<Item> GetAllItemList(string userId)
        {
            return db.Items.Where(c => c.UserId == userId).OrderByDescending(c => c.CreateTime).ToList();
        }
        public List<Item> GetUndealItemList(string userId)
        {
            return db.Items.Where(c => c.UserId == userId && c.IsDeal == false).OrderByDescending(c => c.CreateTime).ToList();
        }
        public List<Item> GetDealItemList(string userId)
        {
            return db.Items.Where(c => c.UserId == userId && c.IsDeal == true).OrderByDescending(c => c.CreateTime).ToList();
        }

        public List<Item> GetLocalItemList(decimal longitude, decimal latitude)
        {
            return db.Items.Where(c => c.Longitude == longitude && c.Latitude == latitude && c.IsDeal==false).ToList();
        }

        public List<Item> GetLocalItemList(string address)
        {
            return db.Items.Where(c => c.Address == address && c.IsDeal == false).ToList();
        }
    }
}