using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace Course_work.Additional
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
