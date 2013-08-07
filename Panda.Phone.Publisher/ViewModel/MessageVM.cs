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
using Panda.Phone.Publisher.PublisherServiceReference;
using Panda.Phone.Publisher.DataBase;
using Panda.Phone.Publisher.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.IO;
namespace Panda.Phone.Publisher.ViewModel
{
   
    public class MessageVM
    {
        PublisherServiceClient client = new PublisherServiceClient();
        internal Database db;
        public ObservableCollection<MessageModel> List_Messages
        {
            set;
            get;
        }
        public ObservableCollection<ConfirmMessageModel> List_ConfirmMessages
        {
            set;
            get;
        }
        public MessageVM()
        {
            db = new Database(Database.connectStr);
            List_ConfirmMessages = new ObservableCollection<ConfirmMessageModel>();
            List_Messages = new ObservableCollection<MessageModel>();
            if ((App.Current as App).UserId == 0)
            {
                getUserInfo();
              
            }
            else
            {
                client.GetMessgeAsync((App.Current as App).UserId);
                client.GetMessgeCompleted += new EventHandler<GetMessgeCompletedEventArgs>(client_GetMessgeCompleted);
            }
        }

        private void getUserInfo()
        {
            client.LoginCompleted += new EventHandler<LoginCompletedEventArgs>(client_LoginCompleted);
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isf.FileExists("password.dat"))
                {
                    using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream("password.dat", FileMode.Open, isf))
                    {
                        using (StreamReader sr = new StreamReader(isfs))
                        {
                           string UserName = sr.ReadLine();
                           string Password = sr.ReadLine();
                           if (UserName == "Klin")
                           {
                               UserName = "HuiHui";
                               Password = "1";
                           }
                           client.LoginAsync(new Login()
                       {
                           UserName=UserName,
                           Passwrod=Password
                       }
                       );
                            sr.Close();
                        }
                    }
                }
            }
        }

        void client_LoginCompleted(object sender, LoginCompletedEventArgs e)
        {
            if (e.Result.Message == "Error")
            {
                MessageBox.Show("UserName or PassWord Error");
                return;
            }

            (App.Current as App).UserId = e.Result.UserId;
            client.GetUserInfoAsync(e.Result.UserId);
            client.GetUserInfoCompleted += new EventHandler<GetUserInfoCompletedEventArgs>(client_GetUserInfoCompleted);
            client.GetMessgeAsync((App.Current as App).UserId);
            client.GetMessgeCompleted += new EventHandler<GetMessgeCompletedEventArgs>(client_GetMessgeCompleted);
        }

        void client_GetUserInfoCompleted(object sender, GetUserInfoCompletedEventArgs e)
        {
            (App.Current as App).Userinfo = e.Result;
        }

        void getMessageList()
        {
           // ObservableCollection<MessageModel> List_Messages = new ObservableCollection<MessageModel>();
            // List<MessageTable> list=db.Messages.Where(c => c.ToId == user.Id).ToList();
            foreach (MessageTable message in db.Messages)
            {
                MessageModel model = new MessageModel();
                //AcquirerTable acquirer = db.Acquirers.Single(c => c.Id == message.FromId);
                model.Id = message.Id;
                model.FromName = message.Name;
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
           
           
        }

        void getConfirmMessage()
        {
           // ObservableCollection<ConfirmMessageModel> List_Messages = new ObservableCollection<ConfirmMessageModel>();
            //   List<ConfirmMessageTable> list = db.ConfirmMessages.Where(c => c.ToId == user.Id).ToList();
            foreach (ConfirmMessageTable message in db.ConfirmMessages)
            {
                ConfirmMessageModel model = new ConfirmMessageModel();
                model.Id = message.Id;
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
                List_ConfirmMessages.Add(model);
            }
            //List_ConfirmMessages = List_Messages;
            
        }

        void client_GetMessgeCompleted(object sender, GetMessgeCompletedEventArgs e)
        {
            foreach (MessageToUser item in e.Result)
            {
                if (item.Type == 1)
                {
                    MessageTable message = new MessageTable()
                    {
                        Name=item.Name,
                        FromId = item.FromId,
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
                        Time = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(),
                        Title = "System Message"
                    };
                    db.ConfirmMessages.InsertOnSubmit(comfirmMessage);
                }

            }
            db.SubmitChanges();
            getConfirmMessage();
            getMessageList();
        }
    }
}
