using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace CourseWork
{
    static class DataConverter
    {
        public static BitmapFrame GetImageFromByteArray(byte[] array)
        {
            return GetImageFromMemoryStream(new MemoryStream(array));
        }

        public static BitmapFrame GetImageFromMemoryStream(MemoryStream memoryStream)
        {
            return BitmapFrame.Create(memoryStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
        }
    }
}
