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
using System.Windows.Media.Imaging;
namespace Panda.Phone.Publisher.Controls
{
    public partial class Tile3 : UserControl
    {
      public DependencyProperty TileImageProperty=DependencyProperty.Register("TileImage",typeof(string),typeof(Tile1),
          new PropertyMetadata(new PropertyChangedCallback((e1,e2)=>
          {
              var tile = e1 as Tile1;
              if(tile!=null&& e2.NewValue!=null)
              {
                  tile.img_Image.Source = new BitmapImage(new Uri((string)e2.NewValue, UriKind.Relative));
              }
              
          })));
      public DependencyProperty TileImageWidthProperty = DependencyProperty.Register("TileImageWidth", typeof(string), typeof(Tile1),
       new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
       {
           var tile = e1 as Tile1;
           if (tile != null && e2.NewValue != null)
           {
               //tile.img_Image.Source = new BitmapImage(new Uri((string)e2.NewValue, UriKind.Relative));
               tile.img_Image.Width = Int32.Parse((string)e2.NewValue);
               tile.img_Image.Height = Int32.Parse((string)e2.NewValue);
           }

       })));
      public DependencyProperty TileImageHeightProperty = DependencyProperty.Register("TileImageHeight", typeof(string), typeof(Tile1),
    new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
    {
        var tile = e1 as Tile1;
        if (tile != null && e2.NewValue != null)
        {
            //tile.img_Image.Source = new BitmapImage(new Uri((string)e2.NewValue, UriKind.Relative));
            tile.img_Image.Height =Int32.Parse((string)e2.NewValue);
            tile.img_Image.Height = Int32.Parse((string)e2.NewValue);
        }

    })));
      public DependencyProperty TileBackgroundProperty = DependencyProperty.Register("TileBackground", typeof(string), typeof(Tile1),
       new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
       {
           var tile = e1 as Tile1;
           if (tile != null && e2.NewValue != null)
           {
               tile.LayoutRoot.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri((string)e2.NewValue, UriKind.Relative)) };
           }

       })));
      public DependencyProperty TileNumProperty = DependencyProperty.Register("TileNum", typeof(string), typeof(Tile1),
      new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
      {
          var tile = e1 as Tile1;
          if (tile != null && e2.NewValue != null)
          {
              tile.tbl_Num.Text =(string) e2.NewValue;
          }

      })));

      public DependencyProperty TileNameProperty = DependencyProperty.Register("TiltName", typeof(string), typeof(Tile1),
    new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
    {
        var tile = e1 as Tile1;
        if (tile != null && e2.NewValue != null)
        {
            tile.tbl_Name.Text = (string)e2.NewValue;
        }

    })));
      public string TileBackground
      {
          get
          {

              return base.GetValue(TileBackgroundProperty) as string;
          }
          set
          {
              base.SetValue(TileBackgroundProperty, value);
          }
      }

      public string TileImage
      {
          get 
          {

              return base.GetValue(TileImageProperty) as string ;
          }
          set
          {
              base.SetValue(TileImageProperty, value);
          }
      }
      public string TileImageWidth
      {
          get
          {

              return base.GetValue(TileImageWidthProperty) as string;
          }
          set
          {
              base.SetValue(TileImageWidthProperty, value);
          }
      }
      public string TileNum
      {
          get
          {

              return base.GetValue(TileNumProperty) as string;
          }
          set
          {
              base.SetValue(TileNumProperty, value);
          }
      }

      public string TileName
      {
          get
          {

              return base.GetValue(TileNameProperty) as string;
          }
          set
          {
              base.SetValue(TileNameProperty, value);
          }
      }
        public Tile3()
        {
            InitializeComponent();
        }
    }
}
