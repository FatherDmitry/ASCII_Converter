using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIConverter
{
    public class BitmapToASCIIConverter
    {
        private readonly char[] _asciiTable = { '.', ',', ':', '+', '*', '?', '%', '$', '#', '@' }; //Символы для задования цвета
        private readonly Bitmap _bitmap;
        public BitmapToASCIIConverter(Bitmap bitmap)
        {
            _bitmap = bitmap;
        }

        public char[][] Convert()
        {
            var result = new char[_bitmap.Height][];

            //Обработка изображения и её конвертация

            for (int y = 0; y < _bitmap.Height; y++) //Перебор изображения по вертикали
            {
                result[y] = new char[_bitmap.Width];

                for (int x = 0; x < _bitmap.Width; x++)
                {
                    int mapIndex = (int)Map(_bitmap.GetPixel(x, y).R, 0, 255, 0, _asciiTable.Length - 1);
                    result[y][x] = _asciiTable[mapIndex];
                }
            }

            return result;
        }

        //Отображение изображения символами для необходимого диапозона яркости использования символа
        private float Map(float valueToMap, float start1, float stop1, float start2, float stop2)
        {
            return ((valueToMap - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        }
    }
}
