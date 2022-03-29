using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractals
{
    /// <summary>
    /// Класс, который задаёт точку и помогает в отрисовке фракталов.
    /// </summary>
    class Point
    {
        /// <summary>
        /// Х-координата.
        /// </summary>
        public double x;

        /// <summary>
        /// Y-координата.
        /// </summary>
        public double y;


        /// <summary>
        /// Конструктор, присваивающий координатам значения.
        /// </summary>
        /// <param name="x"> Х-координата. </param>
        /// <param name="y"> Y-координата. </param>
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Конструктор без параметоров.
        /// </summary>
        public Point()
        {

        }


        /// <summary>
        /// Перегрузка оперетора сложения для двух точек.
        /// </summary>
        /// <param name="first"> Первая точка. </param>
        /// <param name="second"> первая точка. </param>
        /// <returns> Сумму точек. </returns>
        public static Point operator +(Point first, Point second)
        {
            return new Point(first.x + second.x, first.y + second.y);
        }


        /// <summary>
        /// Перегрузка оперетора сложения точки и числа.
        /// </summary>
        /// <param name="first"> Точка, к которой прибавляется число. </param>
        /// <param name="second"> Число, которое прибавляется к точке. </param>
        /// <returns> Точку, координату которой увеличены на заданное число. </returns>
        public static Point operator +(Point first, double second)
        {
            return new Point(first.x + second, first.y + second);
        }



        /// <summary>
        /// Перегрузка оперетора разности точки и числа.
        /// </summary>
        /// <param name="first"> Точка, из которой вычитается число. </param>
        /// <param name="second"> Число, которое вычитается. </param>
        /// <returns> Точку, кооординаты которой уменьшены на заданное число. </returns>
        public static Point operator -(Point first, double second)
        {
            return new Point(first.x - second, first.y - second);
        }


        /// <summary>
        /// Перегрузка оперетора сложения для точки и кортежа.
        /// </summary>
        /// <param name="first"> Точка. </param>
        /// <param name="second"> Кортеж. </param>
        /// <returns> Точку, с х координатой, увеличенной на первое значение кортежа,
        /// и у координатой увеличенной на второе значение кортежа. </returns>
        public static Point operator +(Point first, (double, double) second)
        {
            return new Point(first.x + second.Item1, first.y + second.Item2);
        }


        /// <summary>
        /// Перегрузка оперетора разности двух точек.
        /// </summary>
        /// <param name="first"> Первая точка. </param>
        /// <param name="second"> Вторая точка. </param>
        /// <returns> Разность точек. </returns>
        public static Point operator -(Point first, Point second)
        {
            return new Point(first.x - second.x, first.y - second.y);
        }


        /// <summary>
        /// Перегрузка оперетора для точки и числа.
        /// </summary>
        /// <param name="first"> Точка. </param>
        /// <param name="num"> Число. </param>
        /// <returns> Точку, координаты которой умножены на заданное число. </returns>
        public static Point operator *(Point first, double num)
        {
            return new Point(first.x * num, first.y * num);
        }


        /// <summary>
        /// Перегрузка оперетора деления для точки и числа.
        /// </summary>
        /// <param name="first"> точка. </param>
        /// <param name="num"> Число. </param>
        /// <returns> Точку, координаты которой поделены на заданное число. </returns>
        public static Point operator /(Point first, double num)
        {
            return new Point(first.x / num, first.y / num);
        }
    }
}
