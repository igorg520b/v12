using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

/// <summary>
/// The funciton c3 takes a double value and returns corresponding color.
/// Used for rendering.
/// </summary>
namespace icFlow
{
    public static class MC
    {
        static Color[][] CS; // color scheme
        static int n_themes = 12;

        static MC()
        {
            Console.WriteLine("Starting constructor for MC");
            CS = new Color[n_themes][];
            for (int i = 0; i < n_themes; i++)
            {
                string path_to_bitmap = @"color_palettes/" + i.ToString() + ".png";
                Bitmap b = new Bitmap(path_to_bitmap);
                if (b == null)
                {
                    Console.WriteLine("cannot open color palette bitmaps");
                    throw new Exception("color maker");
                }
                CS[i] = new Color[b.Height];
                for (int y = 0; y < b.Height; y++) CS[i][y] = b.GetPixel(b.Width / 2, b.Height - y - 1);
            }
        }

        static public bool Test()
        {
            return true;
        }

        static public Color c3(double v, double min, double max, int theme)
        {
            int size = CS[theme].Length;
            double vr = ((double)size) * (v - min) / (max - min);
            int idx = (int)vr;
            if (idx < 0) idx = 0;
            else if (idx >= size) idx = size - 1;
            return CS[theme][idx];
        }

        static public Color c3f(float v, float min, float max, int theme)
        {
            int size = CS[theme].Length;
            float vr = ((float)size) * (v - min) / (max - min);
            int idx = (int)vr;
            if (idx < 0) idx = 0;
            else if (idx >= size) idx = size - 1;
            return CS[theme][idx];
        }
    }
}