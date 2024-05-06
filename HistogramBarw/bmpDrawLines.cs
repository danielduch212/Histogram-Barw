namespace HistogramBarw
{
    public struct bmpDrawLines
    {
        public IntPtr dst;
        public byte[] dst_array;

        public int startValue;
        public int stopValue;

        public int maxValue;

        public int[] histogramR;
        public int[] histogramG;
        public int[] histogramB;
    }
}
