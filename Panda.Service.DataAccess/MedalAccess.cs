using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Panda.Service.Helper;


namespace Panda.Service.DataAccess
{
    public interface IMedalAccess
    {
        void AddMedal(int type,int rank,string name,string description,string imagePath);
        bool DeleteMedal(int medalId);
        void UpdateMedal(int medalId,string name,string description,string imagePath);
        List<Medal> GetMedalList();
        List<Medal> GetMedalList(int type);
        Medal GetMedal(int medalId);
        
        
    }

    public class MedalAccess:IMedalAccess
    {

        private Database_PandaEntities db = new Database_PandaEntities();
        public void AddMedal(int type,int rank, string name, string description, string imagePath)
        {
            try
            {
                Medal addModel = new Medal()
                {
                    Type=type,
                    Rank=rank,
                    Name = StringHelper.LimitLength(name),
                    Description = StringHelper.LimitLength(description,500),
                    ImagePath =StringHelper.LimitLength(imagePath,200)
                };
                db.AddToMedals(addModel);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateMedal(int medalId, string name, string description, string imagePath)
        {
            try
            {
                Medal updateModel = db.Medals.Where(c => c.MedalId == medalId).FirstOrDefault();
                if (updateModel == null)
                    return;
                updateModel.Name = StringHelper.LimitLength(name);
                updateModel.Description = StringHelper.LimitLength(description, 500);
                updateModel.ImagePath = StringHelper.LimitLength(imagePath, 200);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteMedal(int medalId)
        {
            try
            {
                Medal deleteModel = db.Medals.Where(c => c.MedalId == medalId).FirstOrDefault();

                if (deleteModel != null)
                {
                    db.Medals.DeleteObject(deleteModel);
                    db.SaveChanges();
                }
                return true;
            }
            catch 
            {
                return false;
            }
        }
        public List<Medal> GetMedalList(int type)
        {
            return db.Medals.Where(c => c.Type == type).OrderBy(c => c.Rank).ToList();
        }
        public List<Medal> GetMedalList()
        {
            return db.Medals.OrderBy(c => c.Type).ThenBy(c => c.Rank).ToList();
        }

        public Medal GetMedal(int medalId)
        {
            var returnModel = db.Medals.Where(c => c.MedalId == medalId).FirstOrDefault();
            return returnModel ?? new Medal();
        }

    }
}