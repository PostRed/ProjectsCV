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
    /// Кривая Коха.
    /// </summary>
    class Koch : Fractal
    {

        /// <summary>
        /// Конструктор, присваивающий полям значения.
        /// </summary>
        /// <param name="depth"> Максимальная глубина рекурсии для фрактала. </param>
        /// <param name="newCanvas"> Canvas, в котором рисуются фракталы. </param>
        /// <param name="textBox"> TextBox, в котором вводится глубина рекурсии. </param>
        public Koch(int depth, Canvas canvas, TextBox textBox) : base(depth, canvas, textBox)
        { }


        /// <summary>
        /// Рисует фрактал.
        /// </summary>
        /// <param name="depth"> Глубина рекурсии. </param>
        /// <param name="points"> Параметры для отрисовки.</param>
        public override void DrawFractal(int depth, Point[] points)
        {
            Point point1 = points[0];
            Point point5 = points[1];
            if (depth == 0)
            {
                DrawLine(point1, point5);
            }
            else
            {
                Point distance = (point5 - point1) / 3;
                Point point2 = point1 + distance;
                Point point4 = point2 + distance;
                Point point0 = point4 - point2;
                Point point3 = new Point();
                // Поворот отрезка.
                point3.x = Math.Cos(-Math.PI / 3) * point0.x - Math.Sin(-Math.PI / 3) * point0.y;
                point3.y = Math.Sin(-Math.PI / 3) * point0.x + Math.Cos(-Math.PI / 3) * point0.y;
                point3 += point2;
                DrawFractal(depth - 1, point1, point2);
                DrawFractal(depth - 1, point2, point3);
                DrawFractal(depth - 1, point3, point4);
                DrawFractal(depth - 1, point4, point5);
            }
        }


        /// <summary>
        /// Проверяет глубину рекурсии и вызывает метод, который рисует кривую Коха.
        /// </summary>
        /// <param name="depth"> Глубина рекурсии. </param>
        public void DrawKoch(int depth)
        {
            DrawFractal(CorrectDepth(depth), new Point(20, 320), new Point(750, 320));
        }
    }
}
