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
    /// Фрактальное дерево.
    /// </summary>
    class Tree : Fractal
    {
        /// <summary>
        /// Конструктор, присваивающий полям значения.
        /// </summary>
        /// <param name="depth"> Максимальная глубина рекурсии для фрактала. </param>
        /// <param name="newCanvas"> Canvas, в котором рисуются фракталы. </param>
        /// <param name="textBox"> TextBox, в котором вводится глубина рекурсии. </param>
        public Tree(int depth, Canvas canvas, TextBox textBox) : base(depth, canvas, textBox)
        { }


        /// <summary>
        /// Проверяет глубину рекурсии и вызывает метод, который рисует фрактальное дерево.
        /// </summary>
        /// <param name="depth"> Глубина рекурсии. </param>
        /// <param name="left"> Угол между левой веткой и основанием. </param>
        /// <param name="right"> Угол между правой веткой и основанием. </param>
        /// <param name="attitude"> ОТношение длины ветки к основанию </param>
        public void DrawTree(int depth, double left, double right, double attitude)
        {
            DrawFractal(CorrectDepth(depth), new Point(375, 350), new Point(375, 500), new Point(right, left), new Point(attitude, 0));
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
            Point angle = points[2];
            double angle1 = angle.x;
            double angle2 = angle.y;
            double attitude = points[3].x;
            if (depth == 0)
            {
                DrawLine(point1, point2);
            }
            else
            {
                Point point0 = point2 - point1;
                Point point3 = new Point();
                // Поворот отрезка.
                point3.x = Math.Cos(angle1) * point0.x - Math.Sin(angle1) * point0.y + point1.x;
                point3.y = Math.Sin(angle1) * point0.x + Math.Cos(angle1) * point0.y + point1.y;
                point3 += (point1 - point3) * (1 - attitude);
                DrawFractal(depth - 1, point3, point1, angle, points[3]);
                // Поворот отрезка.
                point3.x = Math.Cos(angle2) * point0.x + Math.Sin(angle2) * point0.y + point1.x;
                point3.y = -Math.Sin(angle2) * point0.x + Math.Cos(angle2) * point0.y + point1.y;
                point3 += (point1 - point3) * (1 - attitude);
                DrawFractal(depth - 1, point3, point1, angle, points[3]);
                DrawFractal(0, point2, point1, angle, points[3]);
            }
        }
    }
}
