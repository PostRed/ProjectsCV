using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fractals
{
    /// <summary>
    /// Ковёр Серпинского.
    /// </summary>
    class Carpet : Fractal
    {
        /// <summary>
        /// Конструктор, присваивающий полям значения.
        /// </summary>
        /// <param name="depth"> Максимальная глубина рекурсии для фрактала. </param>
        /// <param name="newCanvas"> Canvas, в котором рисуются фракталы. </param>
        /// <param name="textBox"> TextBox, в котором вводится глубина рекурсии. </param>
        public Carpet(int depth, Canvas canvas, TextBox textBox) : base(depth, canvas, textBox)
        { }

        /// <summary>
        /// Рисует главный квадрат ковра Серпинского и вызывает метод,
        /// который рисует остальные шаги фрактала.
        /// </summary>
        /// <param name="depth"> Глубина рекурсии. </param>
        /// <param name="coordinats"> Координаты левой верхней точки квадрата. </param>
        /// <param name="len"> Длина стороны квадрата. </param>
        public void CarpetStart(int depth, Point coordinats, double len)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Fill = Brushes.SaddleBrown;
            rectangle.Width = len;
            rectangle.Height = len;
            canvas.Children.Add(rectangle);
            Canvas.SetLeft(rectangle, coordinats.x);
            Canvas.SetTop(rectangle, coordinats.y);
            if (depth >= 1)
            {
                len /= 3f;
                coordinats += len;
                DrawFractal(depth, coordinats, new Point(len, 0));
            }

        }


        /// <summary>
        /// Рисует фрактал.
        /// </summary>
        /// <param name="depth"> Глубина рекурсии. </param>
        /// <param name="points"> Параметры для отрисовки.</param>
        public override void DrawFractal(int depth, Point[] points)
        {
            Point coordinats = points[0];
            Point len = points[1];
            if (depth == 1)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Fill = Brushes.AntiqueWhite;
                rectangle.Width = len.x;
                rectangle.Height = len.x;
                canvas.Children.Add(rectangle);
                Canvas.SetLeft(rectangle, coordinats.x);
                Canvas.SetTop(rectangle, coordinats.y);
            }
            else
            {
                DrawFractal(1, coordinats, len);
                len /= 3f;
                DrawFractal(depth - 1, coordinats - 2 * len.x, len);
                DrawFractal(depth - 1, coordinats + (len.x, -2 * len.x), len);
                DrawFractal(depth - 1, coordinats + (4 * len.x, -2 * len.x), len);
                DrawFractal(depth - 1, coordinats + (-2 * len.x, len.x), len);
                DrawFractal(depth - 1, coordinats + (4 * len.x, len.x), len);
                DrawFractal(depth - 1, coordinats + (-2 * len.x, 4 * len.x), len);
                DrawFractal(depth - 1, coordinats + (len.x, 4 * len.x), len);
                DrawFractal(depth - 1, coordinats + (4 * len.x, 4 * len.x), len);
            }
        }


        /// <summary>
        /// Проверяет глубину рекурсии и вызывает метод, который рисует ковёр Серпинского.
        /// </summary>
        /// <param name="depth"> Глубина рекурсии. </param>
        public void DrawCarpet(int depth)
        {
            CarpetStart(CorrectDepth(depth), new Point(170, 30), 450);
        }

    }
}
