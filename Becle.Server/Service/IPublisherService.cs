using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Becle.Server.DataModel;
using System.ServiceModel;
using System.ServiceModel.Web;
namespace Becle.Server.Service
{
    [ServiceContract]
    public interface IPublisherService
    {
        #region{Register&Login]
        [OperationContract]
        string Register(RegisterUser user);
        [OperationContract]
        LoginBack Login(Login login);
        [OperationContract]
        UserInfo GetUserInfo(int userId);
        #endregion

        #region[PostResourse]
        [OperationContract]
        string Post(PostItem postItem);
        #endregion

        #region[FriendTrend]
        [OperationContract]
        List<PostTrend> GetPostTrend(int userId);
        [OperationContract]
        List<PhotoTrend> GetPhotoTrend(int userId);
        [OperationContract]
        List<AchievementTrend> GetAchievementTrend(int userId);
        #endregion

        #region[Recycle&Reward]
        [OperationContract]
        List<Reward> GetReward(int userId);
        [OperationContract]
        List<Acquirer> GetAcquirer(int userId);
        [OperationContract]
        List<MessageToUser> GetMessge(int userId);
        [OperationContract]
        string SendMessageToAcquirer(Message message);
        #endregion

        #region[GetImage]
        [OperationContract]
        UserImage GetImageByUserId(int userId);
        [OperationContract]
        ItemImage GetImageByItemId(int itemId);
        [OperationContract]
        AcquirerImage GetImageByAcquirerId(int acquirerId);
        #endregion

        #region[Data]
        [OperationContract]
        int GetMyPoints(int userId);
         [OperationContract]
        int GetAveragePointsAllUser();
         [OperationContract]
        int GethighestPointsAlluser();
         [OperationContract]
        int GetHighestPointsInCity(string city);
         [OperationContract]
        int GetAveragePointsInCity(string city);
           [OperationContract]
        string GetMyRank(int userId);
         [OperationContract]
         string GetMyCityRank(int userId);
        [OperationContract]
        List<City> GetRankCity();
        [OperationContract]
        List<DataUser> GetRankUser();
        #endregion

        #region[AcquirerService]
       
        [OperationContract]
        Acquirer GetAcquirerInfo(int acquirerId);
        [OperationContract]
        List<Resident> GetResidents(int acquirerId);
        [OperationContract]
        List<Item> GetItems(int acquirerId);
        [OperationContract]
        List<Message> GetAciquirer_Messages(int acquirerId);
        [OperationContract]
        string SendMessageToUser(MessageToUser message);
        #endregion

        #region[Volunteer]
        [OperationContract]
        List<Resident> GetResidents_V(int acquirerId);
        [OperationContract]
        string SendMessageToUser_V(MessageToUser message);
        [OperationContract]
        List<Item> GetItems_V(int acquirerId);
        #endregion
    }
}
