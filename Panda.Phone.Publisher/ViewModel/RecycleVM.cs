using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Panda.Phone.Publisher.DataBase;
using System.Linq;
using System.Collections.Generic;
using Panda.Phone.Publisher.Model;
using Panda.Phone.Publisher.PublisherServiceReference;
namespace Panda.Phone.Publisher.ViewModel
{
    public class RecycleVM
    {
        Database db;
        UserTable user;
        public string Title { set; get; }
        public List<AcquirerModel> List_Acquirers
        {
            get { return getAcquirerList(); }
        }
        public List<MessageModel> List_Messages
        {
            get { return getMessageList(); }
        }
        public List<ConfirmMessageModel> List_ConfirmMessages
        {
            get { return getConfirmMessage(); }
        }
        List<ConfirmMessageModel> getConfirmMessage()
        {
            List<ConfirmMessageModel> List_Messages = new List<ConfirmMessageModel>();
         //   List<ConfirmMessageTable> list = db.ConfirmMessages.Where(c => c.ToId == user.Id).ToList();
            foreach (ConfirmMessageTable message in db.ConfirmMessages)
            {
                ConfirmMessageModel model = new ConfirmMessageModel();
                
                model.Title = message.Title;
                model.Time = message.Time;
                if (message.IsSee)
                {
                    model.ImageUri = "/Image_Recycle/Message/m_open.png";
                }
                else
                {
                    model.ImageUri = "/Image_Recycle/Message/m_closed.png";
                }
                model.Message = message.Message;
                List_Messages.Add(model);
            }
            return List_Messages;
        }
        List<AcquirerModel> getAcquirerList()
        {
            List<AcquirerModel> List_Acquirers = new List<AcquirerModel>();
           // List<AcquirerTable> list_Acquirer = db.Acquirers.Where(c => Math.Abs(c.Latitude - user.Latitude) < 1 && Math.Abs(c.Longitude - user.Longitude) < 1).ToList<AcquirerTable>();
            foreach (AcquirerTable acquirer in db.Acquirers)
            {
                AcquirerModel model = new AcquirerModel()
                {
                    BinName = acquirer.BinName,
                    AcquirerName = acquirer.UserName,
                    Address = acquirer.Address,
                    Phone = acquirer.Phone,
                    Latitude = (double)acquirer.Latitude,
                    Longitude = (double)acquirer.Longitude,
                    Id = acquirer.Id
                };
                List_Acquirers.Add(model);
            }
            return List_Acquirers;
        }
        List<MessageModel> getMessageList()
        {
            List<MessageModel> List_Messages = new List<MessageModel>();
           // List<MessageTable> list=db.Messages.Where(c => c.ToId == user.Id).ToList();
            foreach (MessageTable message in db.Messages)
            {
                MessageModel model = new MessageModel();
                AcquirerTable acquirer=db.Acquirers.Single(c=>c.Id==message.FromId);
                model.FromName = acquirer.UserName;
                model.Time = message.Time;
                if (message.IsSee)
                {
                    model.ImageUri = "/Image_Recycle/Message/m_open.png";
                }
                else
                {
                    model.ImageUri = "/Image_Recycle/Message/m_closed.png";
                }
                model.Message = message.Message;
                List_Messages.Add(model);
            }
            return List_Messages;
        }
        public delegate void CallBack();
        CallBack callback,callback1;
        private int userId;
        PublisherServiceClient client = new PublisherServiceClient();
        public RecycleVM(int id,CallBack _callback,CallBack _callback1)
        {
            callback = _callback;
            callback1 = _callback1;
            userId = id;
            db = new Database(Database.connectStr);
            
            List<AcquirerTable> list = db.Acquirers.ToList();
            if (db.Acquirers.Count() == 0)
            {
                client.GetAcquirerAsync(userId);
                client.GetAcquirerCompleted += new EventHandler<GetAcquirerCompletedEventArgs>(client_GetAcquirerCompleted);
            }
            client.GetMessgeCompleted += new EventHandler<GetMessgeCompletedEventArgs>(client_GetMessgeCompleted);
            Title = "Recycle List";   
        }

        void client_GetMessgeCompleted(object sender, GetMessgeCompletedEventArgs e)
        {
            foreach (MessageToUser item in e.Result)
            {
                if (item.Type == 1)
                {
                    MessageTable message = new MessageTable()
                    {
                        FromId=item.FromId,
                        Message = item.MessageContent,
                        Time = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(),
                    };
                    db.Messages.InsertOnSubmit(message);
                }
                if (item.Type == 2)
                {
                    ConfirmMessageTable comfirmMessage = new ConfirmMessageTable()
                    {
                        Message = item.MessageContent,
                        Time = DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString(),
                        Title = "System Message"
                    };
                    db.ConfirmMessages.InsertOnSubmit(comfirmMessage);
                }

            }
            db.SubmitChanges();
            callback1();
        }
        void client_GetAcquirerCompleted(object sender, GetAcquirerCompletedEventArgs e)
        {
            foreach (Panda.Phone.Publisher.PublisherServiceReference.Acquirer item in e.Result)
            {
                AcquirerTable acquirerTable = new AcquirerTable()
                {
                    Address=item.Address,
                    BinName=item.BinName,
                    City=item.City,
                    Latitude=item.Latitude,
                    Longitude=item.Longitude,
                    Phone=item.Phone,
                    UserName=item.UserName,
                    Id=item.AcquirerId
                };
                db.Acquirers.InsertOnSubmit(acquirerTable);
            }
            db.SubmitChanges();
            callback();
           // client.GetMessgeAsync(userId);
        }

        
    }
}
