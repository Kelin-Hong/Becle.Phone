using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Panda.Service.Helper;

namespace Panda.Service.DataAccess
{
    public interface ITrashAccess
    {
        List<Trash> GetTrashList(int typeId);
        void AddTrash(int typeId,string name, string description, bool canCount,decimal price);
        bool DeleteTrash(int trashId);
        void UpdateTrash(int trashId, string name, string description,bool canCount, decimal price);
        Trash GetTrash(int trashId);
    }

    public class TrashAccess:ITrashAccess
    {
        private Database_PandaEntities db = new Database_PandaEntities();

        public Trash GetTrash(int trashId)
        {
            Trash returnModel= db.Trashes.Where(c => c.TrashId == trashId).FirstOrDefault();
            return returnModel ?? new Trash();
        }

        public List<Trash> GetTrashList(int typeId)
        {
            return db.Trashes.Where(c => c.TypeId == typeId).ToList();
        }

        public void AddTrash(int typeId,string name,string description,bool canCount,decimal price)
        {
           try
            {
                Trash addModel=new Trash()
                {
                    Name= StringHelper.LimitLength(name,50),
                    Description=StringHelper.LimitLength(description,500),
                    CanCount=canCount,
                    DefaultPrice=price
                };
                db.AddToTrashes(addModel);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteTrash(int trashId)
        {
            try
            {
                Trash deleteModel = db.Trashes.Where(c => c.TrashId == trashId).FirstOrDefault();
                if (deleteModel != null)
                {
                    db.Trashes.DeleteObject(deleteModel);
                }
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void UpdateTrash(int trashId,string name,string description,bool canCount,decimal price)
        {
            try
            {
                Trash updateModel = db.Trashes.Where(c => c.TrashId == trashId).FirstOrDefault();
                if (updateModel == null)
                    return;
                updateModel.Name = StringHelper.LimitLength(name, 50);
                updateModel.Description = StringHelper.LimitLength(description, 500);
                updateModel.CanCount = canCount;
                updateModel.DefaultPrice = price;
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}