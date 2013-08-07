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
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using Panda.Phone.Publisher.PublisherServiceReference;
using RenrenSDKLibrary.WidgetDialog;
using RenrenSDKLibrary;
using System.Diagnostics;
namespace Panda.Phone.Publisher.Views
{
    public partial class PostSuccessPage : PhoneApplicationPage
    {
        PublisherServiceClient client;
        public Dictionary<string, int> Items_Post { set; get; }
        public Dictionary<string, Stream> Items_Image { set; get; }
        internal Dictionary<string, string> items_Describe { set; get; }
        internal Dictionary<string, int> items_Category { set; get; }
        int postCount = 0;
        string shareContent;
        private void Post()
        {
             Affect affect;
            
             foreach (string name in Items_Post.Keys)
             {

                 PostItem postItem = new PostItem();
                 postItem.Name = name;
                 postItem.PostAffect = "";
                 postItem.Amount = Items_Post[name];
                 postItem.CategoryId = items_Category[name];
                 if (items_Describe.Keys.Contains(name))
                 {
                     postItem.Describe = items_Describe[name];
                 }
                 else
                 {
                     postItem.Describe = "";
                 }
                 postItem.UserId = (App.Current as App).UserId;
                 if (Items_Image.Keys.Contains(name))
                 {
                     postItem.PostImage = Helper.StreamToBytes(Items_Image[name]);
                 }
                 else
                 {

                 }
                 if (!Constant.getDictionary_Affect().ContainsKey(name))
                 {
                     affect = Constant.getDictionary_Affect()["other"];
                 }
                 else
                 {
                     affect = Constant.getDictionary_Affect()[name];
                 }
                 postItem.GetPoints = (int)Math.Round(2 * affect.carbon_emissions, 0);
                 if (affect.carbon_emissions != 0)
                 {
                     postItem.PostAffect += "reduce " + affect.carbon_emissions * Items_Post[name] +" kg of carbon emissions";
                 }
                 if (affect.electricity != 0)
                 {
                     postItem.PostAffect += "save " + affect.electricity * Items_Post[name] + " kWh of electricity!";
                 }
                 if (affect.forest != 0)
                 {
                     postItem.PostAffect += "prevent " + affect.forest * Items_Post[name] + " m² of forest from being destory!!";
                 }
                 if (affect.gasoline != 0)
                 {
                     postItem.PostAffect += "save " + affect.gasoline * Items_Post[name] + " litres of gasoline!";
                 }
                 if (affect.soil != 0)
                 {
                     postItem.PostAffect += "prevent " + affect.soil * Items_Post[name] + " m³ of soil from being polluted!";
                 }
                 if (affect.water != 0)
                 {
                     postItem.PostAffect += "prevent " + affect.water * Items_Post[name] + " m³ of water from being polluted!";
                 }
                 shareContent += postItem.PostAffect;
                 client.PostAsync(postItem);
              
             }
        }
        void client_PostCompleted(object sender, PostCompletedEventArgs e)
        {
            postCount++;
          //  MessageBox.Show(postCount+ " Unload Success!");
           
            if (postCount == Items_Post.Keys.Count)
            {
                MessageBox.Show("Unload Success!");
                client.CloseAsync();
            }
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            Post();
            Affect affect;
            int points=1; 
            foreach (string name in Items_Post.Keys)
            {
                if (!Constant.getDictionary_Affect().ContainsKey(name))
                {
                    affect = Constant.getDictionary_Affect()["other"];
                }
                else
                {
                    affect = Constant.getDictionary_Affect()[name];
                }
                TextBlock text=new TextBlock(){Text="You "};
                
                StackPanel sp = new StackPanel();
                Border border = new Border();
                sp.Margin = new Thickness(5);
                sp.Children.Add(text);
                if (affect.carbon_emissions != 0)
                {
                    points=(int)Math.Round(10*affect.carbon_emissions,0);
                    TextBlock tbk = new TextBlock();
                    tbk.Inlines.Add("reduce ");
                    Run run = new Run(){Text=affect.carbon_emissions * Items_Post[name]+" kg "};
                    run.Foreground = new SolidColorBrush(new Color(){A=255,R=107,B=53,G=194});                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
                    
                    tbk.Inlines.Add(run);
                    tbk.Inlines.Add("of carbon emissions!");
                    //tbk.Inlines.Add(new LineBreak());
                    sp.Children.Add(tbk);
                    Image image = new Image() { Source = new BitmapImage(new Uri("/Image_Recycle/Menu/line.png", UriKind.Relative)) };
                    sp.Children.Add(image);
                }
                if (affect.electricity!= 0)
                {
                    TextBlock tbk = new TextBlock();
                    tbk.Inlines.Add("save ");
                    Run run = new Run() { Text = affect.electricity * Items_Post[name] + " kWh " };
                    run.Foreground = new SolidColorBrush(new Color() { A = 255, R = 107, B = 53, G = 194 });
                    tbk.Inlines.Add(run);
                    tbk.Inlines.Add("of electricity!");
                    sp.Children.Add(tbk);
                    Image image = new Image() { Source = new BitmapImage(new Uri("/Image_Recycle/Menu/line.png",UriKind.Relative)) };
                    sp.Children.Add(image);
                }
                if (affect.forest != 0)
                {
                    TextBlock tbk = new TextBlock();
                    tbk.Inlines.Add("prevent ");
                    Run run = new Run() { Text = affect.forest * Items_Post[name] + " m² " };
                    run.Foreground = new SolidColorBrush(new Color() { A = 255, R = 107, B = 53, G = 194 });
                    tbk.Inlines.Add(run);
                    tbk.Inlines.Add("of forest from being destory!");
                    sp.Children.Add(tbk);
                    Image image = new Image() { Source = new BitmapImage(new Uri("/Image_Recycle/Menu/line.png", UriKind.Relative)) };
                    sp.Children.Add(image);
                }
                if (affect.gasoline != 0)
                {
                    TextBlock tbk = new TextBlock();
                    tbk.Inlines.Add("save ");
                    Run run = new Run() { Text = affect.gasoline * Items_Post[name] + " litres " };
                    run.Foreground = new SolidColorBrush(new Color() { A = 255, R = 107, B = 53, G = 194 });
                    tbk.Inlines.Add(run);
                    tbk.Inlines.Add("of gasoline!");
                    sp.Children.Add(tbk);
                    Image image = new Image() { Source = new BitmapImage(new Uri("/Image_Recycle/Menu/line.png", UriKind.Relative)) };
                    sp.Children.Add(image);
                }
                  if (affect.soil!= 0)
                    {
                     TextBlock tbk = new TextBlock();
                     tbk.Inlines.Add("prevent ");
                     Run run = new Run() { Text = affect.soil * Items_Post[name] + " m³ " };
                     run.Foreground = new SolidColorBrush(new Color() { A = 255, R = 107, B = 53, G = 194 });
                     tbk.Inlines.Add(run);
                     tbk.Inlines.Add("of soil from being polluted!");
                     sp.Children.Add(tbk);
                     Image image = new Image() { Source = new BitmapImage(new Uri("/Image_Recycle/Menu/line.png", UriKind.Relative)) };
                     sp.Children.Add(image);
                   }
                  if (affect.water != 0)
                 {
                     TextBlock tbk = new TextBlock();
                     tbk.Inlines.Add("prevent ");
                     Run run = new Run() { Text = affect.water * Items_Post[name] + " m³ " };
                     run.Foreground = new SolidColorBrush(new Color() { A = 255, R = 107, B = 53, G = 194 });
                     tbk.Inlines.Add(run);
                     tbk.Inlines.Add("of water from being polluted!");
                     sp.Children.Add(tbk);
                     Image image = new Image() { Source = new BitmapImage(new Uri("/Image_Recycle/Menu/line.png", UriKind.Relative)) };
                     sp.Children.Add(image);
                  }
                  TextBlock text1 = new TextBlock();
                  text1.Inlines.Add("by recycling ");
                  Run run1 = new Run() { Text = name };
                  Run run2 = new Run() { Text = Items_Post[name]+" " };
                  run1.Foreground = new SolidColorBrush(new Color() { A = 255, R = 107, B = 53, G = 194 });
                  run2.Foreground = new SolidColorBrush(new Color() { A = 255, R = 107, B = 53, G = 194 });
                  text1.Inlines.Add(run2);
                  text1.Inlines.Add("of ");
                  text1.Inlines.Add(run1);
                  sp.Children.Add(text1);
                  Image image1 = new Image() { Source = new BitmapImage(new Uri("/Image_Recycle/Menu/line.png", UriKind.Relative)) };
                  sp.Children.Add(image1);
                  TextBlock text2 = new TextBlock() { FontSize = 20 };
                  text2.Inlines.Add("And you get ");
                  Run run3 = new Run() { Text = points+" Points" };
                  text2.Inlines.Add(run3);
                  text2.Inlines.Add("by this behavior!");
                  sp.Children.Add(text2);
                  run3.Foreground = new SolidColorBrush(new Color() { A = 255, R = 107, B = 53, G = 194 });
                  lb_PostAffect.Items.Add(sp);
            }
            base.OnNavigatedTo(e);
        }
        public PostSuccessPage()
        {
            InitializeComponent();
            client = new PublisherServiceClient();
            client.PostCompleted += new EventHandler<PostCompletedEventArgs>(client_PostCompleted);  
        }

        private void btn_RenRen_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            RenrenAPI api = App.api;
             List<APIParameter> param= new List<APIParameter>();
             //param.Add(new APIParameter("url", "http://dev.renren.com"));
             //param.Add(new APIParameter("name", "Becle"));
             //param.Add(new APIParameter("action_name", "You Can Green The World！"));
             ////param.Add(new APIParameter("action_link", "http://www.aaronke.co.cc"));
             ////param.Add(new APIParameter("description","   "));
             //param.Add(new APIParameter("caption", "The earth thanks for you contributions!"));
             //param.Add(new APIParameter("image", "http://www.aaronke.co.cc/ic/img/home/facebook_logo.jpg"));

             param.Add(new APIParameter("url", "http://dev.renren.com"));
             param.Add(new APIParameter("name", "Becle"));
             param.Add(new APIParameter("action_name", "访问我们"));
             param.Add(new APIParameter("action_link", "http://dev.renren.com"));
             param.Add(new APIParameter("description", shareContent));
             param.Add(new APIParameter("caption", ""));
             param.Add(new APIParameter("image", "http://www.aaronke.co.cc/ic/img/home/facebook_logo.jpg"));
             api.WidgetDialog(this, WidgetDialogType.FeedDialog, param, WidgetDialogComplete);
        }
        void WidgetDialogComplete(object sender, RenrenSDKLibrary.DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
                MessageBox.Show(e.Error.Message);
            else
                MessageBox.Show(e.Result);
        }
        private void btn_OK_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/MainPage.xaml", UriKind.Relative));
        }
    }
    
    
}