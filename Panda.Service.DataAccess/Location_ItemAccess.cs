using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Panda.Service.Helper;

namespace Panda.Service.DataAccess
{
    public interface ILocation_ItemAccess
    {
        void AddLocation_Item(int typeId,int trashId,decimal amount,string itemName,string address,decimal longitude,decimal latitude);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="address"></param>
        /// <param name="amount">该地区增加的垃圾量</param>
        /// <param name="itemName">通过垃圾名称更新数据</param>
        bool UpdateLocation_Item(string address, decimal amount, string itemName);
        bool UpdateLocation_Item(decimal longitude, decimal latitude, decimal amount, string itemName);
        bool UpdateLocation_Item(string address, decimal amount, int trashId);
        bool UpdateLocation_Item(decimal longitude, decimal latitude, decimal amount,int trashId);


        bool ClearLocation_Item(string address, string itemName);
        bool ClearLocation_Item(decimal longitude, decimal latitude, string itemName);
        bool ClearLocation_Item(string address, int trashId);
        bool ClearLocation_Item(decimal longitude, decimal latitude, int trashId);


        List<Location_Item> GetLocation_ItemList(string address);
        List<Location_Item> GetLocation_ItemList(decimal longitude, decimal latitude);


        Location_Item GetLocation_Item(string address, string itemName);
        Location_Item GetLocation_Item(string address, int trashId);
        Location_Item GetLocation_Item(decimal longitude, decimal latitude, string itemName);
        Location_Item GetLocation_Item(decimal longitude, decimal latitude, int trashId);
        Location_Item GetLocation_Item(string id);

    }

    public class Location_ItemAccess:ILocation_ItemAccess
    {
        private Database_PandaEntities db = new Database_PandaEntities();
        public void AddLocation_Item( int typeId, int trashId, decimal amount, string itemName,string address, decimal longitude, decimal latitude)
        {
            try
            {
                Location_Item addModel = new Location_Item()
                {
                    Id = Guid.NewGuid(),
                    Address = StringHelper.LimitLength(address, 100),
                    Longitude = longitude,
                    Latitude = latitude,
                    TypeId = typeId,
                    TrashId = trashId,
                    Amount = amount,
                    ItemName = StringHelper.LimitLength(itemName),
                    LastAddTime = DateTime.Now
                };
                if (trashId > 0)
                {
                    Trash trash = db.Trashes.Where(c => c.TrashId == trashId).FirstOrDefault();
                    if (trash != null)
                    {
                        addModel.Price = trash.DefaultPrice;
                    }
                    else addModel.Price = -1.0M;
                }
                db.AddToLocation_Items(addModel);
                db.SaveChanges();
            }
            catch (Exception ex)
            { throw ex; }
        }

        public bool UpdateLocation_Item(string address, decimal amount, string itemName)
        {
            try
            {
                var updateModel = db.Location_Items.Where(c => c.Address == address && c.ItemName == itemName).FirstOrDefault();
                if (updateModel != null)
                {
                    updateModel.Amount += amount;
                    updateModel.LastAddTime = DateTime.Now;
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
        public bool UpdateLocation_Item(decimal longitude, decimal latitude, decimal amount, string itemName)
        {
            try
            {
                var updateModel = db.Location_Items.Where(c => c.Longitude == longitude && c.Latitude == latitude && c.ItemName == itemName).FirstOrDefault();
                if (updateModel != null)
                {
                    updateModel.Amount += amount;
                    updateModel.LastAddTime = DateTime.Now;
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
        public bool UpdateLocation_Item(string address, decimal amount, int trashId)
        {
            try
            {
                var updateModel = db.Location_Items.Where(c => c.Address == address && c.TrashId == trashId).FirstOrDefault();
                if (updateModel != null)
                {
                    updateModel.Amount += amount;
                    updateModel.LastAddTime = DateTime.Now;
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
        public bool UpdateLocation_Item(decimal longitude, decimal latitude, decimal amount, int trashId)
        {
            try
            {
                var updateModel = db.Location_Items.Where(c => c.Longitude == longitude && c.Latitude == latitude && c.TrashId == trashId).FirstOrDefault();
                if (updateModel != null)
                {
                    updateModel.Amount += amount;
                    updateModel.LastAddTime = DateTime.Now;
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

        public bool ClearLocation_Item(string address, string itemName)
        {
            try
            {
                var updateModel = db.Location_Items.Where(c => c.Address == address && c.ItemName == itemName).FirstOrDefault();
                if (updateModel != null)
                {
                    updateModel.Amount = decimal.Zero;
                    updateModel.LastAddTime = DateTime.Now;
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
        public bool ClearLocation_Item(string address, int trashId)
        {
            try
            {
                var updateModel = db.Location_Items.Where(c => c.Address == address &&c.TrashId==trashId).FirstOrDefault();
                if (updateModel != null)
                {
                    updateModel.Amount = decimal.Zero;
                    updateModel.LastAddTime = DateTime.Now;
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
        public bool ClearLocation_Item(decimal longitude, decimal latitude,int trashId)
        {
            try
            {
                var updateModel = db.Location_Items.Where(c=>c.Longitude==longitude && c.Latitude==latitude && c.TrashId==trashId).FirstOrDefault();
                if (updateModel != null)
                {
                    updateModel.Amount = decimal.Zero;
                    updateModel.LastAddTime = DateTime.Now;
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
        public bool ClearLocation_Item(decimal longitude, decimal latitude, string itemName)
        {
            try
            {
                var updateModel = db.Location_Items.Where(c => c.Longitude == longitude && c.Latitude == latitude && c.ItemName==itemName).FirstOrDefault();
                if (updateModel != null)
                {
                    updateModel.Amount = decimal.Zero;
                    updateModel.LastAddTime = DateTime.Now;
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

        public List<Location_Item> GetLocation_ItemList(string address)
        {
            return db.Location_Items.Where(c => c.Address == address).OrderByDescending(c=>c.Amount).ToList();
        }
        public List<Location_Item> GetLocation_ItemList(decimal longitude, decimal latitude)
        {
            return db.Location_Items.Where(c => c.Longitude == longitude && c.Latitude == latitude).OrderByDescending(c=>c.Amount).ToList();
        }


        public Location_Item GetLocation_Item(string address, string itemName)
        {
            var returnModel = db.Location_Items.Where(c => c.Address == address && c.ItemName == itemName).FirstOrDefault();
            return returnModel ?? new Location_Item();
        }
        public Location_Item GetLocation_Item(string address, int trashId)
        {
            var returnModel = db.Location_Items.Where(c => c.Address == address &&c.TrashId==trashId).FirstOrDefault();
            return returnModel ?? new Location_Item();
        }
        public Location_Item GetLocation_Item(decimal longitude, decimal latitude, string itemName)
        {
            var returnModel = db.Location_Items.Where(c=>c.Longitude==longitude && c.Latitude==latitude && c.ItemName == itemName).FirstOrDefault();
            return returnModel ?? new Location_Item();
        }
        public Location_Item GetLocation_Item(decimal longitude, decimal latitude, int trashId)
        {
            var returnModel = db.Location_Items.Where(c => c.Longitude == longitude && c.Latitude == latitude && c.TrashId==trashId).FirstOrDefault();
            return returnModel ?? new Location_Item();
        }
        public Location_Item GetLocation_Item(string id)
        {
            var returnModel = db.Location_Items.Where(c=>c.Id==new Guid(id)).FirstOrDefault();
            return returnModel ?? new Location_Item();
        }
       
    }

}