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
    /// Множество Кантора.
    /// </summary>
    class Cantor : Fractal
    {
        /// <summary>
        /// Конструктор, присваивающий полям значения.
        /// </summary>
        /// <param name="depth"> Максимальная глубина рекурсии для фрактала. </param>
        /// <param name="newCanvas"> Canvas, в котором рисуются фракталы. </param>
        /// <param name="textBox"> TextBox, в котором вводится глубина рекурсии. </param>
        public Cantor(int depth, Canvas canvas, TextBox textBox) : base(depth, canvas, textBox)
        { }


        /// <summary>
        /// Проверяет глубину рекурсии и вызывает метод, который рисует множество Кантора.
        /// </summary>
        /// <param name="depth"> Глубина рекурсии. </param>
        /// <param name="dist"> Расстояние между отрезками. </param>
        public void DrawCantor(int depth, double dist)
        {
            DrawFractal(CorrectDepth(depth), new Point(20, 40), new Point(750, 40), new Point(dist, 0));
        }

        /// <summary>
        /// Рисует фрактал.
        /// </summary>
        /// <param name="depth"> Глубина рекурсии. </param>
        /// <param name="points"> Параметры для отрисовки.</param>
        public override void DrawFractal(int depth, Point[] points)
        {
            Point start = points[0];
            Point finish = points[1];
            double dist = points[2].x;
            if (depth == 0)
            {
                DrawLine(start, finish);
            }
            else
            {
                double len = (finish.x - start.x) / 3f;
                Point point1 = start + (0, dist);
                Point point2 = point1 + (len, 0);
                Point point3 = point2 + (len, 0);
                Point point4 = point3 + (len, 0);
                DrawFractal(depth - 1, point1, point2, points[2]);
                DrawFractal(depth - 1, point3, point4, points[2]);
                DrawFractal(0, start, finish, points[2]);
            }
        }

    }
}
