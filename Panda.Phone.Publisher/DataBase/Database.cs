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
using System.Data.Linq;
namespace Panda.Phone.Publisher.DataBase
{
    public class Database:DataContext
    {
         public static string connectStr = "DataSource=isostore:/data.sdf";
         public Database(string str):base(str)
	     {

	     }
         public Table<UserTable> Users;
         public Table<ItemTable> Items;
         public Table<CategoryTable> Categorys;
         public Table<CityTable> Citys;
         public Table<FriendTable> Friends;
         public Table<PostTrendTable> PostTrends;
         public Table<PhotoTrendTable> PhotoTrends;
         public Table<AchievementTrendTable> AchievementTrends;
         public Table<AcquirerTable> Acquirers;
         public Table<RewardTable> Rewards;
         public Table<MessageTable> Messages;
         public Table<ConfirmMessageTable> ConfirmMessages;
         public Table<DataUserTable> DataUsers;
         public Table<MSMToAcqurer> MSMToAcqurers;
         public Table<RecyclePlanTable> RecyclePlans;
         public Table<RecyclePlanShipTable> RecyclePlanShips;
         public Table<VolunteerPlanTable> VolunteerPlans;
         public Table<VolunteerPlanShipTable> VolunteerPlanShips;
    }
}
