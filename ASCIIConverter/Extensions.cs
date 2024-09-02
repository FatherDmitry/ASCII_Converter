using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIConverter
{
    public static class Extensions
    {
        //Класс приобразования изображения в чёрно-белый тон
        public static void ToGrayscale(this Bitmap bitmap)
        {
            //Перебор пикселей изображения
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    int avg = (pixel.R + pixel.G + pixel.B) / 3; //Усреднение значения цветов Red, Green, Blue
                    bitmap.SetPixel(x, y, Color.FromArgb(pixel.A, avg, avg, avg));
                }
            }
        }
    }
}
