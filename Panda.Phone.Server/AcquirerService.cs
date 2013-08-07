using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Panda.Phone.DataModel;
using Panda.Phone.Data;
namespace Panda.Phone.Server
{
   public class AcquirerService:IAcquirerService
    {

        public List<Users> GetChoicedPoint(List<Adrress> list)
        {
            List<Users > choicedUserList = new List<Users>();
            foreach (Users item in DataBase.user)
            {
                bool alarge=true ;
                bool alow=true;
                bool llarge=true;
                bool llow=true;
                foreach (Adrress a in list)
                {
                    if (item.Longitude < a.Longitude) alarge = false;
                    if (item.Longitude > a.Longitude) alow = false;
                    if (item.Latitude < a.Latitude) llarge = false;
                    if (item.Latitude > a.Latitude) llow = false;
                }
                if (!(alarge | alow | llarge | llow))
                {
                    choicedUserList.Add(item);
                }
            }
            return choicedUserList;
        }
        public List<Users> getAllUser()
        {
            return DataBase.user;
        }

        public List<Items> getItemsByUserId(int Id)
        {
            List<Items> list =DataBase.Items.Where(c=>c.UserId==Id).ToList<Items>();
            return list;
        }


        public List<Items> getItemsByCategoryId(int Id)
        {
            List<Items> list = DataBase.Items.Where(c => c.CategoryId == Id).ToList<Items>();
            return list;
        }
    }
}
