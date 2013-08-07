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
using System.IO;
using Microsoft.Phone;
using System.Windows.Media.Imaging;

namespace Panda.Phone.Publisher
{
    public static class Helper
    {
        public static Stream GetStremById(int id)
        {

            return null;
        }
     public static  byte[] StreamToBytes(Stream stream)
        {
            byte[] bytearray = null;
            using (MemoryStream ms = new MemoryStream())
            {
                BitmapImage bimg = new BitmapImage();
                bimg.SetSource(stream);
                WriteableBitmap wbitmp = new WriteableBitmap(bimg);
                wbitmp.SaveJpeg(ms, wbitmp.PixelWidth, wbitmp.PixelHeight, 0, 100);
                ms.Seek(0, SeekOrigin.Begin);
                bytearray = ms.GetBuffer();
            }
            return bytearray;
        }

     public static WriteableBitmap  BytesToBitMap(byte[] bytes)
        {
            Stream memStream = new MemoryStream(bytes);
            WriteableBitmap wbimg = PictureDecoder.DecodeJpeg(memStream);
            return wbimg;
        }
    }
}
