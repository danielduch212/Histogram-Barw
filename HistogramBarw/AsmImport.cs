using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HistogramBarw
{
    static class AsmImport
    {
        [DllImport(@"..\..\..\..\x64\Debug\AsmDLL.dll")]
        public static extern void CreateSubHistogram(int[,] lista, bmpDrawLines bmp);
    }
}
