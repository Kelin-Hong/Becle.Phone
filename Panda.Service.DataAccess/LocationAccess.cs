using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Panda.Service.Helper;


namespace Panda.Service.DataAccess
{
    public interface ILocationAccess
    {
        void AddLocation(string address, decimal longitude, decimal latitude,decimal amount);

        bool UpdateLocation(string address, decimal amount);
        bool UpdateLocation(decimal longitude, decimal latitude, decimal amount);

        bool ClearLocation(string address);
        bool ClearLocation(decimal longitude, decimal latitude);

        
        Location GetLocation(string address);
        Location GetLocation(decimal longitude, decimal latitude);
        //List<Location> GetLocationList(decimal longitude, decimal latitude, decimal range);

        int ConfirmRecycle(string address);
        int ConfirmRecycle(decimal longitude, decimal latitude);
        int UnConfirmRecycle(string address);
        int UnConfirmRecycle(decimal longitude, decimal latitude);
        int CallRecycle(string address);
        int CallRecycle(decimal longitude, decimal latitude);
    }

    public class LocationAccess:ILocationAccess
    {
       private Database_PandaEntities db = new Database_PandaEntities();
       #region private function
       private int ChangeCount(string address, int type)
       {

           int returnInt = 0;
           try
           {
               Location updateModel = db.Locations.Where(c => c.Address == address).FirstOrDefault();
               if (updateModel != null)
               {
                   switch (type)
                   {
                       case 1: returnInt = ++updateModel.ConfirmCount; break;
                       case 2: returnInt = ++updateModel.UnConfirmCount; break;
                       default: returnInt = ++updateModel.CallRecycleCount; break;
                   }

                   db.SaveChanges();
                   return returnInt;
               }
               else
               {
                   return 0;
               }
           }
           catch
           {
               return 0;
           }
       }
       private int ChangeCount(decimal longitude, decimal latitude, int type)
       {
           //1 confirm
           //2 unconfirm
           //other callrecycle
           int returnInt = 0;
           try
           {
               Location updateModel = db.Locations.Where(c => c.Longitude == longitude && c.Latitude == latitude).FirstOrDefault();
               if (updateModel != null)
               {
                   switch (type)
                   {
                       case 1: returnInt = ++updateModel.ConfirmCount; break;
                       case 2: returnInt = ++updateModel.UnConfirmCount; break;
                       default: returnInt = ++updateModel.CallRecycleCount; break;
                   }

                   db.SaveChanges();
                   return returnInt;
               }
               else
               {
                   return 0;
               }
           }
           catch
           {
               return 0;
           }
       }
       #endregion
       public  void AddLocation(string address, decimal longitude, decimal latitude, decimal amount)
        {
            try
            {
                Location addModel = new Location()
                {
                    Id = Guid.NewGuid(),
                    Address = StringHelper.LimitLength(address, 100),
                    Longitude = longitude,
                    Latitude = latitude,
                    //callCount是一个暂定的数据，不知道有什么用
                    CallCount = -1,
                    ConfirmCount = 0,
                    UnConfirmCount = 0,
                    CallRecycleCount = 0,
                    Amount = amount,
                    LastAddTime = DateTime.Now

                };
                db.AddToLocations(addModel);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public bool UpdateLocation(string address, decimal amount)
       {
           try
           {
               Location updateModel = db.Locations.Where(c => c.Address == address).FirstOrDefault();
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
       public bool UpdateLocation(decimal longitude, decimal latitude, decimal amount)
       {
           try
           {
               Location updateModel = db.Locations.Where(c=>c.Longitude==longitude && c.Latitude==latitude).FirstOrDefault();
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

       public bool ClearLocation(string address)
       {
           try
           {
               Location updateModel = db.Locations.Where(c => c.Address == address).FirstOrDefault();
               if (updateModel != null)
               {
                   updateModel.Amount = decimal.Zero;
                   updateModel.LastClearTime = DateTime.Now;
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
       public bool ClearLocation(decimal longitude, decimal latitude)
       {
           try
           {
               Location updateModel = db.Locations.Where(c => c.Longitude == longitude && c.Latitude == latitude).FirstOrDefault();
               if (updateModel != null)
               {
                   updateModel.Amount = decimal.Zero;
                   updateModel.LastClearTime = DateTime.Now;
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

           //1 confirm
           //2 unconfirm
           //other callrecycle    

       public int ConfirmRecycle(string address)
       {
           return ChangeCount(address, 1);
       }
       public int ConfirmRecycle(decimal longitude, decimal latitude)
       {
           return ChangeCount(longitude, latitude, 1);
       }
       public int UnConfirmRecycle(string address)
       {
           return ChangeCount(address, 2);
       }

       public int UnConfirmRecycle(decimal longitude, decimal latitude)
       {
           return ChangeCount(longitude, latitude, 2);
       }
       public int CallRecycle(string address)
       {
           return ChangeCount(address, 3);
       }
       public int CallRecycle(decimal longitude, decimal latitude)
       {
           return ChangeCount(longitude, latitude, 3);
       }

       public Location GetLocation(string address)
       {
           var returnModel = db.Locations.Where(c => c.Address == address).FirstOrDefault();
           return returnModel ?? new Location();
       }
       public Location GetLocation(decimal longitude, decimal latitude)
       {
           var returnModel = db.Locations.Where(c => c.Longitude==longitude && c.Latitude==latitude).FirstOrDefault();
           return returnModel ?? new Location();
       }

       
    }
}