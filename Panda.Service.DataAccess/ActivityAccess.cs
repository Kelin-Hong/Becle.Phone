using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Panda.Service.Helper;
namespace Panda.Service.DataAccess
{

    public interface IActivityAccess
    {
        void AddActivity(string summary, string content, string host, bool isLocal, DateTime? startTime, DateTime? endTime);
        void UpdateActivity(string id,string summary, string content, string host, bool isLocal, DateTime? startTime, DateTime? endTime);
        bool ChangeIsSeen(string id, bool isSeen);
        bool DeleteActivity(string id);
        List<string> AddCityList(string id,List<int> cities);
        Activity GetActivity(string id);

        
    }
    public class ActivityAccess:IActivityAccess
    {
        private Database_PandaEntities db = new Database_PandaEntities();
        public void AddActivity(string summary, string content, string host, bool isLocal, DateTime? startTime, DateTime? endTime)
        {
            try
            {
                Activity model = new Activity()
                {
                    Id = Guid.NewGuid(),
                    Summary = StringHelper.LimitLength(summary, 500),
                    Content = content,
                    CreateTime = DateTime.Now,
                    Host = StringHelper.LimitLength(host, 100),
                    IsLocal = isLocal,
                    IsSeen = true,
                    StartTime = startTime ?? DateTime.Now,
                    EndTime = startTime ?? DateTime.Now
                };
                db.AddToActivities(model);
                db.SaveChanges();
            }
            catch (Exception ex) { throw ex; }
        }
        public void UpdateActivity(string id, string summary, string content, string host, bool isLocal, DateTime? startTime, DateTime? endTime)
        {
            try
            {
                var model = db.Activities.Where(c => c.Id == new Guid(id)).FirstOrDefault();
                if (model == null)
                    return;
                model.Summary = summary;
                model.Content = content;
                model.Host = host;
                model.IsLocal = isLocal;
                model.StartTime = startTime ?? DateTime.Now;
                model.StartTime = endTime ?? DateTime.Now;
                db.SaveChanges();
            }
            catch { throw new Exception("database save error"); }
        }
        public bool ChangeIsSeen(string id, bool isSeen)
        {
            try
            {
                var model = db.Activities.Where(c => c.Id == new Guid(id)).FirstOrDefault();
                if (model == null)
                    return false;
                model.IsSeen = isSeen;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public bool DeleteActivity(string id)
        {
            try
            {
                var model = db.Activities.Where(c => c.Id == new Guid(id)).FirstOrDefault();
                if (model == null)
                    return true;
                db.Activities.DeleteObject(model);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public Activity GetActivity(string id)
        {
            return db.Activities.Where(c => c.Id == new Guid(id)).FirstOrDefault() ?? new Activity();
        }
        public List<string> AddCityList(string id,List<int> cities)
        {
            Guid guid=new Guid(id);
            List<string> returnModel = new List<string>();
            foreach (int city in cities)
            {
                try
                {
                    Activity_City model = new Activity_City()
                    {
                        Id = Guid.NewGuid(),
                        CityId = city,
                        ActivityId = guid
                    };
                   
                    returnModel.Add(db.Cities.Where(c => c.CityId == city).First().Name);
                    db.AddToActivity_Citys(model);
                    
                }
                catch
                {
                    continue;
                }

            }
            db.SaveChanges();
            return returnModel;

        }
        public List<Activity> GetActivityList(int pagesize,int pagecount)
        {
            return db.Activities.OrderByDescending(c => c.CreateTime).Skip((pagecount - 1) * pagesize).Take(pagecount).ToList();
        }

    }
}