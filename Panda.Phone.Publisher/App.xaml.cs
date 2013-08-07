using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Panda.Phone.Publisher.DataBase;
using Panda.Phone.Publisher.PublisherServiceReference;
using RenrenSDKLibrary;
namespace Panda.Phone.Publisher
{
    public partial class App : Application
    {
        public static RenrenAPI api;
       // public static PublisherServiceClient client = new PublisherServiceClient();
        public int UserId { set; get; }
        public int AcquirerId { set; get; }
        public UserInfo Userinfo { set; get; }
        public Panda.Phone.Publisher.PublisherServiceReference.Acquirer AcquirerInfo { set; get; }
        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }
            api = new RenrenAPI("188569", "772f04d32997471f8837d9a16748d4cb", "e48d571b2fdd4399a5009fd146c0d1c0");
            api.Cleanlog();
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            Database db=new Database(Database.connectStr);
            if (!db.DatabaseExists())
            {
                db.CreateDatabase();
                //List<ItemTable> Items = new List<ItemTable>()
                //   {
                //  new ItemTable(){UserId=1,CategoryId=1,Name="Phone",Num=1},
                //  new ItemTable(){UserId=2,CategoryId=4,Name="Icon",Num=10},
                //  new ItemTable(){UserId=3,CategoryId=1,Name="TV",Num=1},
                //  new ItemTable(){UserId=4,CategoryId=4,Name="Aluminum",Num=10},
                //  new ItemTable(){UserId=1,CategoryId=1,Name="MP3MP4",Num=2},
                //  new ItemTable(){UserId=1,CategoryId=1,Name="Computer",Num=1},
                //  new ItemTable(){UserId=3,CategoryId=2,Name="Cloths",Num=10},
                //  new ItemTable(){UserId=3,CategoryId=4,Name="Copper",Num=10},
                //  new ItemTable(){UserId=4,CategoryId=2,Name="Shoes",Num=10},
                //  new ItemTable(){UserId=4,CategoryId=3,Name="Bottle",Num=10},
                //  new ItemTable(){UserId=2,CategoryId=3,Name="Bottle",Num=10},

                //   };
              //#region[data]
                List<CategoryTable> categorys = new List<CategoryTable>()
                {
                   new CategoryTable(){Name="E_Waste"},
                   new CategoryTable(){Name="Fabric"},
                   new CategoryTable(){Name="Glass"},
                   new CategoryTable(){Name="Metal"},
                   new CategoryTable(){Name="Paper"},
                   new CategoryTable(){Name="Plastic"}
                 
                };
              //  //List<UserTable> users = new List<UserTable>()
              //  //   {
              //  //     new UserTable(){UserName="Kate",Longitude=-73.996847,Latitude=40.737665},
              //  //     new UserTable(){UserName="Klin",Longitude=-73.995441,Latitude=40.739271},
              //  //     new UserTable(){UserName="Jenny",Longitude=-73.998671,Latitude=40.739913},
              //  //     new UserTable(){UserName="Jim",Longitude=-73.997083,Latitude=40.737421},
              //  //     new UserTable(){UserName="Xucai",Longitude=-73.996900,Latitude=40.739868},
              //  //     new UserTable(){UserName="Xinjie",Longitude=-73.994669,Latitude=40.7404470},
              //  //   };
              //  List<ItemTable> Items = new List<ItemTable>()
              //     {
              //    new ItemTable(){UserId=1,CategoryId=1,Name="Phone",Num=1,AvatarUri="/Image_Acquirer/Detail/ItemImage/5.jpg",Time="3/22/2012"},
              //    new ItemTable(){UserId=2,CategoryId=4,Name="Icon",Num=10,AvatarUri="/Image_Acquirer/Detail/ItemImage/4.jpg",Time="3/22/2012"},
              //    new ItemTable(){UserId=3,CategoryId=1,Name="TV",Num=1,AvatarUri="/Image_Acquirer/Detail/ItemImage/5.jpg",Time="3/22/2012"},
              //    new ItemTable(){UserId=4,CategoryId=4,Name="Aluminum",Num=30,AvatarUri="/Image_Acquirer/Detail/ItemImage/4.jpg",Time="3/22/2012"},
              //    new ItemTable(){UserId=5,CategoryId=1,Name="MP3MP4",Num=2,AvatarUri="/Image_Acquirer/Detail/ItemImage/5.jpg",Time="3/22/2012"},
              //    new ItemTable(){UserId=6,CategoryId=1,Name="Computer",Num=1,AvatarUri="/Image_Acquirer/Detail/ItemImage/5.jpg",Time="3/22/2012"},
              //    new ItemTable(){UserId=2,CategoryId=2,Name="Cloths",Num=10,AvatarUri="/Image_Acquirer/Detail/ItemImage/1.jpg",Time="3/22/2012"},
              //    new ItemTable(){UserId=3,CategoryId=4,Name="Copper",Num=10,AvatarUri="/Image_Acquirer/Detail/ItemImage/4.jpg",Time="3/22/2012"},
              //    new ItemTable(){UserId=4,CategoryId=2,Name="Shoes",Num=10,AvatarUri="/Image_Acquirer/Detail/ItemImage/1.jpg",Time="3/22/2012"},
              //    new ItemTable(){UserId=5,CategoryId=3,Name="Bottle",Num=10,AvatarUri="/Image_Acquirer/Detail/ItemImage/2.jpg",Time="3/22/2012"},
              //    new ItemTable(){UserId=1,CategoryId=3,Name="Bottle",Num=10,AvatarUri="/Image_Acquirer/Detail/ItemImage/2.jpg",Time="3/22/2012"},
              //    new ItemTable(){UserId=6,CategoryId=5,Name="Paper",Num=30,AvatarUri="/Image_Acquirer/Detail/ItemImage/3.jpg"},

              //     };
              //    List<UserTable> users = new List<UserTable>()
              //     {
              //       new UserTable(){UserName="Kate",Longitude=-73.998847m,Latitude=40.737665m,Point=130,City="WuHan",LastRank=6,AvatarUri="/Image_Home/Avatar/0.jpg"},
              //       new UserTable(){UserName="Klin",Longitude=-73.995441m,Latitude=40.737271m,Point=230,City="XiaMen",LastRank=2,AvatarUri="/Image_Home/Avatar/1.jpg"},
              //       new UserTable(){UserName="Jenny",Longitude=-73.998671m,Latitude=40.739913m,Point=120,City="ChangSha",LastRank=1,AvatarUri="/Image_Home/Avatar/2.jpg"},
              //       new UserTable(){UserName="Jim",Longitude=-73.997083m,Latitude=40.735421m,Point=112,City="XiaMen",LastRank=3,AvatarUri="/Image_Home/Avatar/3.jpg"},
              //       new UserTable(){UserName="Xucai",Longitude=-73.992900m,Latitude=40.739868m,Point=113,City="WuHan",LastRank=4,AvatarUri="/Image_Home/Avatar/4.jpg"},
              //       new UserTable(){UserName="Xinjie",Longitude=-73.994669m,Latitude=40.7404470m,Point=202,City="GuangZhou",LastRank=5,AvatarUri="/Image_Home/Avatar/5.jpg"},
              //     };
              //    List<CityTable> citys = new List<CityTable>()
              //     {
              //       new CityTable(){Name="WuHan",Point=120,LastRank=6},
              //       new CityTable(){Name="ChangSha",Point=200,LastRank=2},
              //       new CityTable(){Name="XiaMen",Point=100,LastRank=1},
              //       new CityTable(){Name="BeiJing",Point=210,LastRank=3},
              //       new CityTable(){Name="SiChuan",Point=130,LastRank=4},
              //       new CityTable(){Name="GuangZhou",Point=200,LastRank=5},
              //     };

              //    List<FriendTable> friends = new List<FriendTable>() 
              //    { 
              //      new FriendTable(){UserId=1,FriendId=2},
              //      new FriendTable(){UserId=1,FriendId=3},
              //      new FriendTable(){UserId=1,FriendId=4},
              //      new FriendTable(){UserId=1,FriendId=5},
              //      new FriendTable(){UserId=1,FriendId=6}
              //    };
              //      List<PostTrendTable> Posttrends = new List<PostTrendTable>()
              //     {
              //         new PostTrendTable(){UserId=2,Describe="Post Some new Resource Information",Time="3/22/2012 ",PostAffect="This behavior will prevent 1000m forest from destroy and prevent 1000m water from being pulluted"},
              //         new PostTrendTable(){UserId=2,Describe="Post Some new Resource Information",Time="3/21/2012 ",PostAffect="This behavior will prevent 1000m forest from destroy and prevent 1000m water from being pulluted"},
              //         new PostTrendTable(){UserId=2,Describe="Post Some new Resource Information",Time="3/22/2012",PostAffect="This behavior will prevent 1000m forest from destroy and prevent 1000m water from being pulluted"},
              //         new PostTrendTable(){UserId=2,Describe="Post Some new Resource Information",Time="3/22/2012",PostAffect="This behavior will prevent 1000m forest from destroy and prevent 1000m water from being pulluted"},
              //         new PostTrendTable(){UserId=2,Describe="Post Some new Resource Information",Time="3/12/2012",PostAffect="This behavior will prevent 1000m forest from destroy and prevent 1000m water from being pulluted"},
              //         new PostTrendTable(){UserId=2,Describe="Post Some new Resource Information",Time="3/21/2012",PostAffect="This behavior will prevent 1000m forest from destroy and prevent 1000m water from being pulluted"},
              //     };
              //      List<PhotoTrendTable> Phototrends = new List<PhotoTrendTable>()
              //     {
              //         new PhotoTrendTable(){UserId=2,ImageUri="/Image_Acquirer/Detail/ItemImage/5.jpg",Describe="Post Some new Resource Information",Time="3/22/2012 "},
              //         new PhotoTrendTable(){UserId=2,ImageUri="/Image_Acquirer/Detail/ItemImage/3.jpg",Describe="Post Some new Resource Information",Time="3/21/2012 "},
              //         new PhotoTrendTable(){UserId=2,ImageUri="/Image_Acquirer/Detail/ItemImage/2.jpg",Describe="Post Some new Resource Information",Time="3/22/2012"},
              //         new PhotoTrendTable(){UserId=2,ImageUri="/Image_Acquirer/Detail/ItemImage/1.jpg",Describe="Post Some new Resource Information",Time="3/22/2012",},
              //         new PhotoTrendTable(){UserId=2,ImageUri="/Image_Acquirer/Detail/ItemImage/4.jpg",Describe="Post Some new Resource Information",Time="3/22/2012"},
              //         new PhotoTrendTable(){UserId=2,ImageUri="/Image_Acquirer/Detail/ItemImage/1.jpg",Describe="Post Some new Resource Information",Time="3/21/2012"},
              //     };
              //      List<AchievementTrendTable> Achievementtrends = new List<AchievementTrendTable>()
              //     {
              //         new AchievementTrendTable(){UserId=2,ImageUri="/Image_Friends/crown.png",Time="3/22/2012",Achievement="rose to No.4"},
              //         new AchievementTrendTable(){UserId=2,ImageUri="/Image_Friends/crown.png",Time="3/21/2012",Achievement="rose to No.7"},
              //         new AchievementTrendTable(){UserId=2,ImageUri="/Image_Friends/crown.png",Time="3/22/2012",Achievement="get a title of"},
              //         new AchievementTrendTable(){UserId=2,ImageUri="/Image_Friends/crown.png",Time="3/22/2012",Achievement="rose to No.2"},
              //         new AchievementTrendTable(){UserId=2,ImageUri="/Image_Friends/crown.png",Time="3/21/2012",Achievement="rose to No.4"},
              //         new AchievementTrendTable(){UserId=2,ImageUri="/Image_Friends/crown.png",Time="3/21/2012",Achievement="rose to No.4"},
              //     };

              //      List<AcquirerTable> acquirers = new List<AcquirerTable>()
              //     {
              //       new AcquirerTable(){
              //           UserName="Kate",
              //           BinName="Hust Resource bin",
              //           Longitude=114.454m,
              //           Latitude=30.50m, 
              //           City="WuHan",
              //           AvatarUri="/Image_Home/Avatar/0.jpg",
              //           Address="Hust qin yuan shi dong 430",
              //           Phone="15527263237"},
              //       new AcquirerTable(){
              //           UserName="Klin",
              //           BinName="Hust Resource bin",
              //           Longitude=114.452m,
              //           Latitude=30.501m,
              //           City="XiaMen",
              //           AvatarUri="/Image_Home/Avatar/1.jpg",
              //           Address="Hust qin yuan shi dong 430",
              //           Phone="15527263237"},
              //       new AcquirerTable(){
              //           UserName="Jenny",
              //           BinName="Hust Resource bin",
              //            Longitude=114.451m,
              //           Latitude=30.507m, 
              //           City="ChangSha",
              //           AvatarUri="/Image_Home/Avatar/2.jpg",
              //           Address="Hust qin yuan shi dong 430",
              //           Phone="15527263237"},
              //       new AcquirerTable(){
              //           UserName="Jim",
              //           BinName="Hust Resource bin",
              //          Longitude=114.451m,
              //           Latitude=30.507m,
              //           City="XiaMen",
              //           AvatarUri="/Image_Home/Avatar/3.jpg",
              //           Address="Hust qin yuan shi dong 430",
              //           Phone="15527263237"},
              //       new AcquirerTable(){
              //           UserName="Xucai",
              //           BinName="Hust Resource bin",
              //           Longitude=114.453m,
              //           Latitude=30.505m, 
              //           City="WuHan",
              //           AvatarUri="/Image_Home/Avatar/4.jpg",
              //           Address="Hust qin yuan shi dong 430",Phone="15527263237"},
              //       new AcquirerTable(){UserName="Xinjie",BinName="Hust Resource bin",
              //           Longitude=-73.994669m,
              //           Latitude=40.7404470m,
              //           City="GuangZhou",
              //           AvatarUri="/Image_Home/Avatar/5.jpg",
              //           Address="Hust qin yuan shi dong 430",
              //           Phone="15527263237"},
              //     };

                    List<RewardTable> rewards = new List<RewardTable>()
                   {
                     new RewardTable(){StoreName="视佳医眼镜",
                         Summary="换取50元的优惠券",
                         Points=1000,
                         Describe="即日起，凡在本店购买价值超过150元眼镜的顾客，均可使用Becle的1000积分换取50元的优惠。每人仅可使用一次。本次活动最终解释权归视佳医眼镜所有",
                         Longitude=114.41835250865400000m,
                         Latitude=30.51626398490010000m, 
                         City="武汉",
                         AvatarUri="/Image_Recycle/RewardItem/glasses.jpg",
                         Address="光谷/鲁巷/视佳医眼镜",
                         Phone="15578458241"},
                     new RewardTable(){
                         StoreName="青苹果宝贝",
                         Summary="换20元减价的优惠券",
                         Points=300,
                         Describe="2012年3月1日起至9月1日，本体验中心推出Becle网站300积分换20元减价的优惠活动。关爱宝宝，从Becle开始",
                         Longitude=114.40866406921800000m,
                         Latitude=30.50970770947070000m, 
                         City="武汉",
                        AvatarUri="/Image_Recycle/RewardItem/baby.jpg",
                         Address="光谷/鲁巷/视佳医眼镜",
                         Phone="027-85413254"},
                     new RewardTable(){
                         StoreName="中百仓储",
                         Summary="兑换享受20元优惠券",
                         Points=100,
                         Describe="即日起至2012年9月1日，凡购买本商场超过200元的顾客，可使用Becle上的100积分兑换享受20元优惠",
                         Longitude=114.43153115537700000m,
                         Latitude=30.50533823583970000m, 
                         City="武汉",
                         AvatarUri="/Image_Recycle/RewardItem/discount.png",
                         Address="光谷/鲁巷",
                         Phone="027-85471212"},
                     new RewardTable(){
                         StoreName="喻园超市",
                         Summary="换取20元优惠券",
                         Points=200,
                         Describe="即日起至2013年1月1日，在喻园小区超市一次性消费超过50元的本校学生，可用Becle的200积分换取20元优惠。每次消费仅可使用一次。",
                         Longitude=114.40866406921800000m,
                         Latitude=30.50533823583970000m, 
                         City="武汉",
                         AvatarUri="/Image_Recycle/RewardItem/surpermarket.jpg",
                         Address="关山口/华科",
                         Phone="027-85471212"
                     },
                     
                      };

              //      List<MessageTable> messages = new List<MessageTable>()
              //     {
              //       new MessageTable(){FromId=1,ToId=1,IsSee=false,Time="3/22/2012",Message="Hust recycle bin will come to recycle paper & metal in your area in 3/24/2012 10:00 AM!"},
              //       new MessageTable(){FromId=2,ToId=1,IsSee=false,Time="3/23/2012",Message="Yuyuan recycle bin will come to recycle plastic & metal & paper in your area in 3/24/2012 10:00 AM! "},
              //       new MessageTable(){FromId=3,ToId=1,IsSee=false,Time="3/22/2012",Message="QiMing recycle bin will come to recycle plastic & metal & paper in your area in 3/24/2012 10:00 AM!  "},
                     
              //     };
              //      List<ConfirmMessageTable> confrimMessages = new List<ConfirmMessageTable>()
              //     {
              //       new ConfirmMessageTable(){FromId=1,ToId=1,IsSee=false,Title="e-waste & metal", Time="3/22/2012",Message="Hust recycle bin recycle paper resourse in your area and your paper cups,paper box are in the confines .Do you want to confirm that these resourses have been recycled? "},
              //       new ConfirmMessageTable(){FromId=2,ToId=1,IsSee=false,Title="paper", Time="3/23/2012",Message="Hust recycle bin recycle paper resourse in your area and your paper cups,paper box are in the confines .Do you want to confirm that these resourses have been recycled? "},
              //       new ConfirmMessageTable(){FromId=3,ToId=1,IsSee=false, Title="metal & glass & plastic",Time="3/22/2012",Message="Hust recycle bin recycle paper resourse in your area and your paper cups,paper box are in the confines .Do you want to confirm that these resourses have been recycled? "},
              //       new ConfirmMessageTable(){FromId=3,ToId=1,IsSee=false, Title="metal & glass & plastic",Time="3/22/2012",Message="Hust recycle bin recycle paper resourse in your area and your paper cups,paper box are in the confines .Do you want to confirm that these resourses have been recycled? "},
              //       new ConfirmMessageTable(){FromId=3,ToId=1,IsSee=false, Title="metal & fabric & plastic",Time="3/22/2012",Message="Hust recycle bin recycle paper resourse in your area and your paper cups,paper box are in the confines .Do you want to confirm that these resourses have been recycled? "},
              //       new ConfirmMessageTable(){FromId=3,ToId=1,IsSee=false, Title="Paper & glass & plastic & metal",Time="3/22/2012",Message="Hust recycle bin recycle paper resourse in your area and your paper cups,paper box are in the confines .Do you want to confirm that these resourses have been recycled? "},
                     
              //     };
              //  #endregion
                  //  db.ConfirmMessages.InsertAllOnSubmit(confrimMessages);
                  //  db.Messages.InsertAllOnSubmit(messages);
                  //  db.Acquirers.InsertAllOnSubmit(acquirers);
                    db.Rewards.InsertAllOnSubmit(rewards);
                  //  db.PostTrends.InsertAllOnSubmit(Posttrends);
                  //  db.PhotoTrends.InsertAllOnSubmit(Phototrends);
                  //  db.AchievementTrends.InsertAllOnSubmit(Achievementtrends);
                  //db.Friends.InsertAllOnSubmit(friends);
                  //db.Citys.InsertAllOnSubmit(citys);
                  //db.Items.InsertAllOnSubmit(Items);
                  //db.Users.InsertAllOnSubmit(users);
                  db.Categorys.InsertAllOnSubmit(categorys);
                  //db.SubmitChanges();

                
            }
            //db.ConfirmMessages.DeleteAllOnSubmit(db.ConfirmMessages);
            //db.Messages.DeleteAllOnSubmit(db.Messages);
            db.Acquirers.DeleteAllOnSubmit(db.Acquirers);
           // db.Rewards.DeleteAllOnSubmit(db.Rewards);
            db.PostTrends.DeleteAllOnSubmit(db.PostTrends);
            db.PhotoTrends.DeleteAllOnSubmit(db.PhotoTrends);
            db.AchievementTrends.DeleteAllOnSubmit(db.AchievementTrends);
            db.Friends.DeleteAllOnSubmit(db.Friends);
            db.Citys.DeleteAllOnSubmit(db.Citys);
            db.Items.DeleteAllOnSubmit(db.Items);
            db.Users.DeleteAllOnSubmit(db.Users);
           // db.Categorys.DeleteAllOnSubmit(db.Categorys);
            db.DataUsers.DeleteAllOnSubmit(db.DataUsers);
            db.SubmitChanges();
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = new TransitionFrame();

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}