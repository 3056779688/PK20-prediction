using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace 拯救小姐姐
{
    public partial class MainWindow : Window
    {
        public short[] R = new short[] {7, 230,0,75,255,23,82, 191,255,120};
        public short[] G = new short[] { 191,222, 146,75,118,226,52,191,38,11};
        public short[] B = new short[] {0, 0, 221, 75, 0, 229, 255, 191, 0, 0 };
        //from 0 to 9

        public List<List<int>> GetNumbersByPictureColor(string filename)
        {
            Bitmap bm = new Bitmap(filename);
            BitmapSource bms = Imaging.CreateBitmapSourceFromHBitmap(bm.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            Console.WriteLine(bms.PixelHeight + "," + bms.PixelWidth); //获取高和宽

            double side = (double)bms.PixelWidth / 10;
            double terms = bms.PixelHeight / side;
            int height; int width = 10;
            if (terms - (int)terms <= 0.5)
                height = (int)terms;
            else
                height = (int)terms + 1;

            List<List<int>> result = new List<List<int>>();
            for(int i=0;i<height;++i)
            {
                List<int> stage = new List<int>();
                for(int j=0;j<width;++j)
                {
                    double LTX = side * j;
                    double LTY = side * i;
                    Color color =  bm.GetPixel((int)(LTX + side / 4), (int)(LTY+side/4));
                    int number = GetNumberByColor(color.R, color.G, color.B);
                    stage.Add(number);
                }
                result.Add(stage);
            }

            return result;
        }

        private int GetNumberByColor(int r,int g,int b)
        {
            int minvalue = int.MaxValue;
            int minindex = -1;
            for(int i=0;i<10;++i)
            {
                int distance = (R[i] - r) * (R[i] - r) + (G[i] - g) * (G[i] - g) + (B[i] - b) * (B[i] - b);
                if(distance<minvalue)
                {
                    minvalue = distance;
                    minindex = i;
                }
            }
            return minindex;
        }

        public void WriteToFile(List<List<int>> numbers)
        {
            StreamWriter sw = new StreamWriter("result.txt");
            int height = numbers.Count;
            for(int i=0;i<height;++i)
            {
                int width = numbers[i].Count;
                for(int j=0;j<width;++j)
                {
                    sw.Write(numbers[i][j] + " ");
                }
                sw.WriteLine();
            }

            sw.Close();
        }
    }
}
