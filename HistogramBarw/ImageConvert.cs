using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HistogramBarw
{
    static class ImageConvert
    {
        public static BitmapData ConvertBitmap2Bytes(Image image, out byte[] image_bytes)
        {
            Bitmap bitmap = new Bitmap(image);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int array_size = bitmapData.Stride * bitmapData.Height;

            image_bytes = new byte[array_size];
            Marshal.Copy(bitmapData.Scan0, image_bytes, 0, array_size);
            bitmap.UnlockBits(bitmapData);

            return bitmapData;
        }

        public static Bitmap ConvertBytes2Bitmap(Image image, BitmapData bitmapData, byte[] image_bytes)
        {
            Bitmap bitmap = new Bitmap(image);
            BitmapData resultData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            Marshal.Copy(image_bytes, 0, resultData.Scan0, image_bytes.Length);
            bitmap.UnlockBits(resultData);
            return bitmap;
        }

    }
}
