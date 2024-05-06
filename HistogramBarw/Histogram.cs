using HistogramBarw;
using System.Drawing.Imaging;

namespace App
{
    public class Histogram
    {
        private Bitmap bmp1;

        public Bitmap resultBmp;
        Bitmap result;

        public Histogram(Bitmap bmp1)
        {
            this.bmp1 = bmp1;
        }


        public int[,] CreateRGBValues()
        {
            byte[] image_bytes;
            BitmapData bitmapData = ImageConvert.ConvertBitmap2Bytes(bmp1, out image_bytes);

            int[,] lista = new int[3, 256];

            int lineAddress = 0;
            for (int y = 0; y < bmp1.Height; y++)
            {
                int pixelAddress = 0;
                for (int j = 0; j < bmp1.Width; j++)
                {
                    byte b = image_bytes[lineAddress + pixelAddress];
                    byte g = image_bytes[lineAddress + pixelAddress + 1];
                    byte r = image_bytes[lineAddress + pixelAddress + 2];

                    lista[0, r]++;
                    lista[1, g]++;
                    lista[2, b]++;

                    pixelAddress += 3;
                }
                lineAddress += bitmapData.Stride;
            }

            return lista;
        }

        public void CreateSubHistogram(int[,] lista, bmpDrawLines bmp)
        {
            int startValue = bmp.startValue;
            int stopValue = bmp.stopValue;
            int[] histogram_r = bmp.histogramR;
            int[] histogram_g = bmp.histogramG;
            int[] histogram_b = bmp.histogramB;
            float varMAX = bmp.maxValue;

            int histHeight = 138;
            Bitmap img = new Bitmap(256, 138);

            float pct;
            float pct1;
            float pct2;
            float pct3;

            var value1 = 0;
            var value2 = 0;
            var value3 = 0;

            int bottom = img.Height - 5;
            using (Graphics g = Graphics.FromImage(img))
            {
                for (int i = startValue; i < stopValue; i++)
                {
                    pct = histogram_r[i] / varMAX;   // What percentage of the max is this value?
                    g.DrawLine(Pens.Red,
                        new Point(i, bottom),
                        new Point(i, bottom - (int)(pct * histHeight))  // Use that percentage of the height
                    );
                }
                for (int i = startValue; i < stopValue; i++)
                {
                    pct = histogram_g[i] / varMAX;   // What percentage of the max is this value?
                    g.DrawLine(Pens.Lime,
                        new Point(i, bottom),
                        new Point(i, bottom - (int)(pct * histHeight))  // Use that percentage of the height
                    );
                }
                for (int i = startValue; i < stopValue; i++)
                {
                    pct = histogram_b[i] / varMAX;   // What percentage of the max is this value?
                    g.DrawLine(Pens.Blue,
                        new Point(i, bottom),
                        new Point(i, bottom - (int)(pct * histHeight))  // Use that percentage of the height
                    );
                }

                for (int i = startValue; i < stopValue; i++)
                {
                    int minvalue = Math.Min(histogram_b[i], Math.Min(histogram_g[i], histogram_r[i]));

                    if (histogram_r[i] == minvalue)
                    {
                        value1 = Math.Min(histogram_g[i], histogram_b[i]);
                        pct1 = value1 / varMAX;
                        g.DrawLine(Pens.Cyan,
                           new Point(i, bottom),
                           new Point(i, bottom - (int)(pct1 * histHeight))  // Use that percentage of the height
                        );
                    }
                    else if (histogram_b[i] == minvalue)
                    {
                        value2 = Math.Min(histogram_g[i], histogram_r[i]);
                        pct2 = value2 / varMAX;
                        g.DrawLine(Pens.Yellow,
                           new Point(i, bottom),
                           new Point(i, bottom - (int)(pct2 * histHeight))  // Use that percentage of the height
                        );
                    }
                    else if (histogram_g[i] == minvalue)
                    {
                        value3 = Math.Min(histogram_b[i], histogram_r[i]);
                        pct3 = value2 / varMAX;
                        g.DrawLine(Pens.Magenta,
                           new Point(i, bottom),
                           new Point(i, bottom - (int)(pct3 * histHeight))  // Use that percentage of the height
                        );
                    }

                    pct = minvalue / varMAX;
                    g.DrawLine(Pens.White,
                       new Point(i, bottom),
                       new Point(i, bottom - (int)(pct * histHeight))  // Use that percentage of the height
                    );
                }


                byte[] image_bytes;
                BitmapData bitmapData = ImageConvert.ConvertBitmap2Bytes(img, out image_bytes);

                for (int i = 0; i < 138; i++)
                {
                    int pixelAddress = bitmapData.Stride * i + startValue * 3;
                    for (int k = startValue; k < stopValue; k++)
                    {
                        byte red = image_bytes[pixelAddress];
                        byte green = image_bytes[pixelAddress + 1];
                        byte blue = image_bytes[pixelAddress + 2];

                        bmp.dst_array[pixelAddress] = red;
                        bmp.dst_array[pixelAddress + 1] = green;
                        bmp.dst_array[pixelAddress + 2] = blue;

                        pixelAddress += 3;
                    }
                }
            }
        }

        /*
        public Bitmap OstatniaDzialajacaKopia(int[,] lista, int startValue, int stopValue)
        {
            int[] histogram_r = new int[256];
            int[] histogram_g = new int[256];
            int[] histogram_b = new int[256];
            for (int i = 0; i < 256; i++)
            {
                histogram_r[i] = lista[0, i];
                histogram_g[i] = lista[1, i];
                histogram_b[i] = lista[2, i];
            }

            float maxRed = histogram_r.Max();
            float maxGreen = histogram_g.Max();
            float maxBlue = histogram_b.Max();

            byte[] image_bytes, result_bytes;

            int histHeight = 128;
            Bitmap img = new Bitmap(256, 138);
            for (int y = 0; y < 138; y++)
            {
                for (int x = 0; x < 256; x++)
                {
                    img.SetPixel(x, y, Color.White);
                }
            }
            float pct;
            float pct1;
            float pct2;
            float pct3;

            var var1 = Math.Max(maxRed, maxGreen);
            var varMAX = Math.Max(var1, maxBlue);
            var value = 0;
            var value1 = 0;
            var value2 = 0;
            var value3 = 0;



            using (Graphics g = Graphics.FromImage(img))
            {
                for (int i = startValue; i < stopValue; i++)
                {
                    pct = histogram_r[i] / varMAX;   // What percentage of the max is this value?
                    g.DrawLine(Pens.Red,
                        new Point(i, bottom),
                        new Point(i, bottom - (int)(pct * histHeight))  // Use that percentage of the height
                        );
                }
                for (int i = startValue; i < stopValue; i++)
                {
                    pct = histogram_g[i] / varMAX;   // What percentage of the max is this value?
                    g.DrawLine(Pens.Green,
                        new Point(i, bottom),
                        new Point(i, bottom - (int)(pct * histHeight))  // Use that percentage of the height
                        );
                }
                for (int i = startValue; i < stopValue; i++)
                {
                    pct = histogram_b[i] / varMAX;   // What percentage of the max is this value?
                    g.DrawLine(Pens.Blue,
                        new Point(i, bottom),
                        new Point(i, bottom - (int)(pct * histHeight))  // Use that percentage of the height
                        );
                }
                for (int i = startValue; i < stopValue; i++)
                {
                    value = Math.Min(histogram_b[i], Math.Min(histogram_g[i], histogram_r[i]));
                    pct = value / varMAX;
                    g.DrawLine(Pens.Gray,
                       new Point(i, bottom),
                       new Point(i, bottom - (int)(pct * histHeight))  // Use that percentage of the height
                       );
                    if (histogram_b[i] >= value && histogram_g[i] >= value)
                    {
                        value1 = Math.Min(histogram_g[i], histogram_b[i]);
                        pct1 = value1 / varMAX;
                        g.DrawLine(Pens.Cyan,
                       new Point(i, bottom - (int)(pct * histHeight)),
                       new Point(i, bottom - (int)(pct1 * histHeight))  // Use that percentage of the height
                       );

                    }
                    if (histogram_b[i] >= value && histogram_r[i] >= value)
                    {
                        value2 = Math.Min(histogram_g[i], histogram_r[i]);
                        pct2 = value2 / varMAX;
                        g.DrawLine(Pens.Magenta,
                       new Point(i, bottom - (int)(pct * histHeight)),
                       new Point(i, bottom - (int)(pct2 * histHeight))  // Use that percentage of the height
                       );
                    }
                    if (histogram_r[i] >= value && histogram_g[i] >= value)
                    {
                        value3 = Math.Min(histogram_g[i], histogram_r[i]);
                        pct3 = value2 / varMAX;
                        g.DrawLine(Pens.Yellow,
                       new Point(i, bottom - (int)(pct * histHeight)),
                       new Point(i, bottom - (int)(pct3 * histHeight))  // Use that percentage of the height
                       );

                    }

                }

                BitmapData bitmapData = ImageConvert.ConvertBitmap2Bytes(img, out image_bytes);
                result_bytes = new byte[image_bytes.Length];

                for (int i = 0; i < 138; i++)
                {
                    int pixelAddress = bitmapData.Stride * i;
                    for (int k = 0; k < 256; k++)
                    {
                        byte red = image_bytes[pixelAddress];
                        byte green = image_bytes[pixelAddress + 1];
                        byte blue = image_bytes[pixelAddress + 2];

                        if (red == 255 && green == 255 && blue == 255)
                        {
                            pixelAddress += 3;
                        }
                        else
                        {
                            result_bytes[pixelAddress] = red;
                            result_bytes[pixelAddress + 1] = green;
                            result_bytes[pixelAddress + 2] = blue;

                            pixelAddress += 3;
                        }

                    }
                }
                Bitmap bitResult = ImageConvert.ConvertBytes2Bitmap(img, bitmapData, result_bytes);
                return bitResult;

            }
        }
        */
    }
}














