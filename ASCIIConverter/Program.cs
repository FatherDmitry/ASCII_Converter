using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ASCIIConverter
{
    static class Program
    {
        private const double WIDTH_OFFSET = 0.55; //Константа пропорции

        [STAThread] //Атрибут для открытия файлов
        static void Main(string[] args)
        {
            //Открытие файлов формата изображения
            var openFieledDialog = new OpenFileDialog
            {
                Filter = "Images| *.bmp; *.png; *,jpg; *.JPEG" //Форматы открывающегося файла (изображение)
            };

            //Цикл постоянной работы программы полсе запуска, чтобы открывать новые картинки без перезапуска программы
            while (true)
            {
                Console.ReadLine();

                if (openFieledDialog.ShowDialog() != DialogResult.OK) //Процесс открытия файла
                    continue; //Возврат на начало цикла, если файл не откроется

                Console.Clear();

                var bitmap = new Bitmap(openFieledDialog.FileName);
                bitmap = ResizeBitmap(bitmap); //Вызов изображеия из редактирования
                bitmap.ToGrayscale(); //Вызов конвертации в чёрно-белый

                var converter = new BitmapToASCIIConverter(bitmap); //Вызов метода конвертирования в символы
                var rows = converter.Convert(); // Присваиваем строкам конвертированные символы

                foreach (var row in rows)
                    Console.WriteLine(row);

                Console.SetCursorPosition(0, 0);
            }
        }

        //Функция редактирования размера изображения
        private static Bitmap ResizeBitmap(Bitmap bitmap)
        {
            var maxWidth = 350;
            var maxHeight = (int)(maxWidth * WIDTH_OFFSET);
            var aspectRatio = (double)bitmap.Width / bitmap.Height;

            if (bitmap.Width > maxWidth || bitmap.Height > maxHeight)
            {
                var newWidth = maxWidth;
                var newHeight = (int)(newWidth / aspectRatio);
                if (newHeight > maxHeight)
                {
                    newHeight = maxHeight;
                    newWidth = (int)(newHeight * aspectRatio);
                }

                bitmap = new Bitmap(bitmap, new Size(newWidth, newHeight));
            }
            return bitmap;
        }
    }
}
