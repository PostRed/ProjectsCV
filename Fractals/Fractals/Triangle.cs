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
    /// Треугоник Серпинского.
    /// </summary>
    class Triangle : Fractal
    {
        /// <summary>
        /// Конструктор, присваивающий полям значения.
        /// </summary>
        /// <param name="depth"> Максимальная глубина рекурсии для фрактала. </param>
        /// <param name="newCanvas"> Canvas, в котором рисуются фракталы. </param>
        /// <param name="textBox"> TextBox, в котором вводится глубина рекурсии. </param>
        public Triangle(int depth, Canvas canvas, TextBox textBox) : base(depth, canvas, textBox)
        { }

        /// <summary>
        /// Рисует главный треугольник Серпинского и вызывает метод, 
        /// который отрисовывает все остальные шаги.
        /// </summary>
        /// <param name="depth"> Глубина рекурсии. </param>
        /// <param name="point1"> Первая вершина треугольника. </param>
        /// <param name="point2"> Вторая вершина треугольника. </param>
        /// <param name="point3"> Третья вершина треугольника. </param>
        public void TriangleStart(int depth, Point point1, Point point2, Point point3)
        {
            DrawLine(point1, point2);
            DrawLine(point3, point2);
            DrawLine(point1, point3);
            if (depth > 0)
            {
                Point point4 = point3 + (point1 - point3) / 2;
                Point point5 = point3 + (point2 - point3) / 2;
                Point point6 = point1 + (point2 - point1) / 2;
                DrawFractal(depth, point4, point5, point6);
            }

        }


        /// <summary>
        /// Рисует фрактал.
        /// </summary>
        /// <param name="depth"> Глубина рекурсии. </param>
        /// <param name="points"> Параметры для отрисовки.</param>
        public override void DrawFractal(int depth, Point[] points)
        {
            Point point1 = points[0];
            Point point2 = points[1];
            Point point3 = points[2];
            if (depth == 1)
            {
                DrawLine(point1, point2);
                DrawLine(point3, point2);
                DrawLine(point1, point3);
            }
            else
            {
                double len = (point2.x - point1.x) / 2;
                len /= 2.0;
                DrawFractal(1, point1, point2, point3);
                Point point4 = point1 + (-len, len * Math.Sqrt(3));
                Point point5 = point1 + (len, len * Math.Sqrt(3));
                Point point6 = point1 + (0, 2 * len * Math.Sqrt(3));
                DrawFractal(depth - 1, point4, point5, point6);
                point4 = point1 + (len, -len * Math.Sqrt(3));
                point5 = point1 + (3 * len, -len * Math.Sqrt(3));
                point6 = point1 + (2 * len, 0);
                DrawFractal(depth - 1, point4, point5, point6);
                point4 = point2 + (-len, len * Math.Sqrt(3));
                point5 = point2 + (len, len * Math.Sqrt(3));
                point6 = point2 + (0, 2 * len * Math.Sqrt(3));
                DrawFractal(depth - 1, point4, point5, point6);
            }
        }


        /// <summary>
        /// Проверяет глубину рекурсии и вызывает метод, который рисует треугольник Серпинского.
        /// </summary>
        /// <param name="depth"> Глубина рекурсии. </param>
        public void DrawTriangle(int depth)
        {
            TriangleStart(CorrectDepth(depth), new Point(100, 490), new Point(680, 490), new Point(390, 490 - 290 * Math.Sqrt(3)));
        }
    }
}
