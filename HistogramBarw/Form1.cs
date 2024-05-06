using App;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HistogramBarw
{
    public partial class Form1 : Form
    {
        const int ColumnsNumber = 256;
        const int RowsNumber = 138;

        private string filename = "";
        int numberOfThreads = 1;
        Histogram histogram;

        public Form1()
        {
            InitializeComponent();
            trackThreads.Value = Environment.ProcessorCount;
            labelThreads.Text = trackThreads.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(filename);
                buttonCreate.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            histogram = new Histogram((Bitmap)pictureBox1.Image);
            SetThreads(pictureBox1.Image);
            Cursor = Cursors.Default;
        }

        private void trackThreads_Scroll(object sender, EventArgs e)
        {
            labelThreads.Text = trackThreads.Value.ToString();
        }


        private void SetThreads(Image image)
        {
            int[,] lista;
            numberOfThreads = trackThreads.Value;

            byte[] image_bytes, result_bytes;
            Bitmap bit1 = new Bitmap(ColumnsNumber, RowsNumber);
            BitmapData bitmapData = ImageConvert.ConvertBitmap2Bytes(bit1, out image_bytes);
            result_bytes = new byte[image_bytes.Length];
           
            lista = histogram.CreateRGBValues();

            // Alokacja adresu danych w tablicy result_bytes - do zapisu
            GCHandle h_image_bytes = GCHandle.Alloc(image_bytes, GCHandleType.Pinned);
            IntPtr pointer_image_bytes = h_image_bytes.AddrOfPinnedObject();

            // Alokacja adresu danych w tablicy result_bytes - do zapisu
            GCHandle h_result_bytes = GCHandle.Alloc(result_bytes, GCHandleType.Pinned);
            IntPtr pointer_result_bytes = h_result_bytes.AddrOfPinnedObject();

            int[] histogram_r = CustomArray<int>.GetRow(lista, 0);
            int[] histogram_g = CustomArray<int>.GetRow(lista, 1);
            int[] histogram_b = CustomArray<int>.GetRow(lista, 2);
            int maxR = histogram_r.Max();
            int maxG = histogram_g.Max();
            int maxB = histogram_b.Max();
            int maxValue = Math.Max(maxR, Math.Max(maxG, maxB));

            // Stopwatch - mierzenie czasu wykonania
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // Inicjalizacja zmiennych interujących po pętli - zarządzanie wątkami
            int colsToDrawPerThread = Math.Max(ColumnsNumber / numberOfThreads, 1);
            int startLineToDraw = 0;
            List<Task> threadsList = new List<Task>();
            while (startLineToDraw < ColumnsNumber)
            {
                int stopValue = Math.Min(startLineToDraw + colsToDrawPerThread, ColumnsNumber);
                // Korekta końcowej liczby kolumn dla ostatniego wątku
                if (threadsList.Count == trackThreads.Value - 1)
                {
                    stopValue = ColumnsNumber;
                }

                bmpDrawLines bmp = new bmpDrawLines
                {
                    dst_array = result_bytes,
                    dst = pointer_result_bytes,
                    startValue = startLineToDraw,
                    stopValue = stopValue,

                    maxValue = maxValue,

                    histogramR = histogram_r,
                    histogramG = histogram_g,
                    histogramB = histogram_b,
                };
                startLineToDraw = stopValue;
                Task thread = radioButtonCs.Checked
                    ? new TaskFactory().StartNew(() => histogram.CreateSubHistogram(lista, bmp))
                    : new TaskFactory().StartNew(() => AsmImport.CreateSubHistogram(lista, bmp));

                threadsList.Add(thread); 
            }
            
            Task.WaitAll(threadsList.ToArray());
            sw.Stop();

            h_image_bytes.Free();
            h_result_bytes.Free();

            Image hist = ImageConvert.ConvertBytes2Bitmap(bit1, bitmapData, result_bytes);
            histogramBox.Image = hist;
            labelTime.Text = $"{sw.ElapsedMilliseconds} ms";
        }
    }
}