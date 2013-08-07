using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Panda.Service.Helper;


namespace Panda.Service.DataAccess
{
    public interface IRecycleAccess
    {
        void AddRecycle(string recycleMan,string description, string address, decimal longitude, decimal latitude, DateTime? startTime, DateTime? endTime);
        void UpdateRecycle(string id, string description, DateTime? startTime, DateTime? endTime);
        bool DeleteRecycle(string id);
        Recycle GetRecycle(string id);
        /// <summary>
        /// 返回还没有过时的收购活动
        /// </summary>
        /// <param name="recycleMan"></param>
        /// <returns></returns>
        List<Recycle> GetRecycleList(string recycleMan);
    }
    public class RecycleAccess:IRecycleAccess
    {
        private Database_PandaEntities db = new Database_PandaEntities();
        public void AddRecycle(string recycleMan,string description,string address,decimal longitude,decimal latitude,DateTime?startTime,DateTime? endTime)
        {
            try
            {
                
                Recycle addModel = new Recycle()
                {
                    Id = Guid.NewGuid(),
                    RecycleMan = StringHelper.LimitLength(recycleMan),
                    Description = StringHelper.LimitLength(description, 500),
                    Address = StringHelper.LimitLength(address, 100),
                    Longitude = longitude,
                    Latitude = latitude,
                    StartTime = startTime ?? DateTime.Now,
                    EndTime = endTime ?? startTime.Value.AddDays(1)
                };
                db.AddToRecycles(addModel);
                db.SaveChanges();

            }
            catch(Exception ex)
            { throw ex; }
        }
        public void UpdateRecycle(string id, string description, DateTime? startTime, DateTime? endTime)
        {
            try
            {
                var updateModel = db.Recycles.Where(c => c.Id == new Guid(id)).FirstOrDefault();
                if (updateModel == null)
                    return;

                updateModel.Description = StringHelper.LimitLength(description, 500);
                updateModel.StartTime = startTime ?? DateTime.Now;
                updateModel.EndTime = endTime ?? startTime.Value.AddDays(1);
                db.SaveChanges();
            }
            catch
            {
                new Exception("database save error");
            }
        }
        public bool DeleteRecycle(string id)
        {
            try
            {
                var deleteModel = db.Recycles.Where(c => c.Id == new Guid(id)).FirstOrDefault();
                if (deleteModel == null)
                    return true;

                db.Recycles.DeleteObject(deleteModel);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Recycle GetRecycle(string id)
        {
            var returnModel = db.Recycles.Where(c => c.Id == new Guid(id)).FirstOrDefault();
            return returnModel ?? new Recycle();
        }
        public List<Recycle> GetRecycleList(string recycleMan)
        {
            DateTime current=DateTime.Now;
            return db.Recycles.Where(c => c.RecycleMan == recycleMan && c.EndTime>current).OrderBy(c => c.StartTime).ToList();
        }
    }
}