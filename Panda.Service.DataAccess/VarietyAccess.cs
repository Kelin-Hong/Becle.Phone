using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Panda.Service.Helper;

namespace Panda.Service.DataAccess
{
    public interface IVarietyAccess
    {
         void AddType(string name,string description);
         bool DeleteType(int typeId);
         void UpdateType(int typeId, string description);
         List<Variety> GetVarietyList();
         Variety GetVariety(int typeId);
    }

    public class VarietyAccess : IVarietyAccess
    {
        private Database_PandaEntities db = new Database_PandaEntities();

        public Variety GetVariety(int typeId)
        {
            var returnModel = db.Varieties.Where(c => c.TypeId == typeId).FirstOrDefault();
            return returnModel ?? new Variety();
        }

        public List<Variety> GetVarietyList()
        {
            return db.Varieties.ToList();
        }

        

        public void AddType(string name, string description)
        {
            try
            {
                Variety addModel = new Variety()
                {
                    Name =StringHelper.LimitLength(name,50),
                    Description = StringHelper.LimitLength(description,500)
                };
                db.AddToVarieties(addModel);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteType(int typeId)
        {
            try
            {
                Variety deleteModel = db.Varieties.Where(c => c.TypeId == typeId).FirstOrDefault();
                if (deleteModel != null)
                {
                    db.Varieties.DeleteObject(deleteModel);
                }
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void UpdateType(int typeId, string description)
        {
            try
            {
                Variety updateModel = db.Varieties.Where(c => c.TypeId == typeId).FirstOrDefault();
                if (updateModel == null)
                    return;
                updateModel.Description = StringHelper.LimitLength(description,500);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    
}