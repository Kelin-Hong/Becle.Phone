using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Panda.Service.Helper;

namespace Panda.Service.DataAccess
{
    public interface IEducationAccess
    {
        void AddEducation(string title,string editor,string content,bool isLocal,bool isSeen);
        bool DeleteEducation(string id);
        void UpdateEducation(string id, string title, string editor, string content, bool isLocal, bool isSeen);
        
        Education GetEducation(string id);
        /// <summary>
        /// 返回结果按时间排序
        /// </summary>
        /// <returns></returns>
        List<Education> GetEducationList();
    }
    public class EducationAccess:IEducationAccess
    {
        private Database_PandaEntities db = new Database_PandaEntities();
        public void AddEducation(string title, string editor, string content, bool isLocal, bool isSeen)
        {
            try
            {
                Education addModel = new Education()
                {
                    Id = Guid.NewGuid(),
                    Title = StringHelper.LimitLength(title, 100),
                    Editor = StringHelper.LimitLength(title, 100),
                    Content = content,
                    CreateTime = DateTime.Now,
                    IsLocal = isLocal,
                    IsSeen = isSeen
                };
                db.AddToEducations(addModel);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteEducation(string id)
        {
            try
            {
                var deleteModel = db.Educations.Where(c => c.Id == new Guid(id)).FirstOrDefault();
                if (deleteModel == null)
                    return true;
                db.Educations.DeleteObject(deleteModel);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void UpdateEducation(string id, string title, string editor, string content, bool isLocal, bool isSeen)
        {
            try
            {
                var updateModel = db.Educations.Where(c => c.Id == new Guid(id)).FirstOrDefault();
                if (updateModel == null)
                    return;
                updateModel.Title = StringHelper.LimitLength(title, 100);
                updateModel.Content = content;
                updateModel.Editor = StringHelper.LimitLength(editor);
                updateModel.IsLocal = isLocal;
                updateModel.IsSeen = isSeen;
                db.SaveChanges();
                
            }
            catch
            {
                throw new Exception("database save error");
            }
        }

        public List<Education> GetEducationList()
        {
            return db.Educations.OrderByDescending(c => c.CreateTime).ToList();
        }
        public Education GetEducation(string id)
        {
            var returnModel = db.Educations.Where(c => c.Id == new Guid(id)).FirstOrDefault();
            return returnModel ?? new Education();
        }
    }
}