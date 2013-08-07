using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Panda.Phone.DataModel;
namespace Panda.Phone.Data
{
    public static class DataBase
    {
       public static List<Items> Items = new List<Items>()
        {
            new Items(){Id=0,UserId=0,CategoryId=0,Name="Phone",Num=1},
            new Items(){Id=1,UserId=1,CategoryId=1,Name="Icon",Num=10},
            new Items(){Id=2,UserId=2,CategoryId=0,Name="TV",Num=1},
            new Items(){Id=3,UserId=3,CategoryId=1,Name="Aluminum",Num=10},
            new Items(){Id=4,UserId=0,CategoryId=0,Name="MP3MP4",Num=2},
            new Items(){Id=5,UserId=0,CategoryId=0,Name="Computer",Num=1},
            new Items(){Id=6,UserId=0,CategoryId=1,Name="Copper",Num=10},
        };
        public static List<Category> categorys=new List<Category>()
        {
           new Category(){Id=0,Name="E_Waste"},
           new Category(){Id=0,Name="Metal"},
           new Category(){Id=0,Name="Plastic"}
        };
        public static  List<Users> user = new List<Users>()
        {
            new Users(){UserName="1k",Longitude=23,Latitude=30.12,Id=0},
            new Users(){UserName="2k",Longitude=23.1,Latitude=30.11,Id=1},
            new Users(){UserName="3k",Longitude=23.01,Latitude=30.11,Id=2},
            new Users(){UserName="4k",Longitude=23.2,Latitude=30.12,Id=3},
            new Users(){UserName="5k",Longitude=23.1,Latitude=30.13,Id=4},
            new Users(){UserName="6k",Longitude=23.5,Latitude=30.1,Id=5},
        };
        public static List<E_Waste> Ewaste = new List<E_Waste>()
        {
            new E_Waste(){Id=0,Phone=1,computer=0,Mp3mp4=0,Iv=1,Num=2},
            new E_Waste(){Id=1,Phone=0,computer=1,Mp3mp4=0,Iv=0,Num=1},
            new E_Waste(){Id=2,Phone=0,computer=0,Mp3mp4=0,Iv=0,Other="shouyinjin",OtherNum=1,Num=1},
            new E_Waste(){Id=3,Phone=1,computer=0,Mp3mp4=0,Iv=0,Num=1},
        };
        public static List<Metal> metal = new List<Metal>()
        {
            new Metal(){Id=0,Aluminum=10,Icon=10,Copper=0,Num=20},
            new Metal(){Id=1,Aluminum=10,Icon=0,Copper=0,Num=10},
            new Metal(){Id=2,Aluminum=10,Icon=0,Copper=10,Num=20},
            new Metal(){Id=3,Aluminum=10,Icon=10,Copper=10,Num=30},
        };
    }
}
