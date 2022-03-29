using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractals
{
    /// <summary>
    /// Базовый класс, от которого наследуются фракталы.
    /// </summary>
    abstract class Fractal
    {
        /// <summary>
        /// Максимальная глубина рекурсии для фрактала.
        /// </summary>
        public int maxDepth;


        /// <summary>
        /// Canvas, в котором рисуются фракталы.
        /// </summary>
        public Canvas canvas;


        /// <summary>
        /// TextBox, в котором вводится глубина рекурсии.
        /// </summary>
        public TextBox recursionTextBox;


        /// <summary>
        /// Конструктор, присваивающий полям значения.
        /// </summary>
        /// <param name="depth"> Максимальная глубина рекурсии для фрактала. </param>
        /// <param name="newCanvas"> Canvas, в котором рисуются фракталы. </param>
        /// <param name="textBox"> TextBox, в котором вводится глубина рекурсии. </param>
        public Fractal(int depth, Canvas newCanvas, TextBox textBox)
        {
            maxDepth = depth;
            canvas = newCanvas;
            recursionTextBox = textBox;
        }

        /// <summary>
        /// Рисует линию, проходящую через две точки.
        /// </summary>
        /// <param name="point1"> Начало линии. </param>
        /// <param name="point2"> Конец линии. </param>
        public void DrawLine(Point point1, Point point2)
        {
            Line line = new Line();
            line.X1 = point1.x;
            line.Y1 = point1.y;
            line.X2 = point2.x;
            line.Y2 = point2.y;
            line.Stroke = Brushes.SaddleBrown;
            line.StrokeThickness = 1;
            canvas.Children.Add(line);
        }

        /// <summary>
        /// Проверяет возможно ли отрисовать заданный фрактал с данной рекурсией.
        /// Если проверка не пройдена - присваивает глубине рекурсии значение по умолчанию.
        /// </summary>
        /// <param name="currentdepth"> Текущая глубина рекурсии. </param>
        /// <returns> Корректное значение глубины рекурсии. </returns>
        public int CorrectDepth(int currentdepth)
        {
            if (currentdepth > maxDepth)
            {
                string text = "Для данного фрактала значение глубины рекурсии слишком велико, нарисовать не получится";
                MessageBox.Show(text, "Небольшая проблемка :(", MessageBoxButton.OK, MessageBoxImage.Warning);
                recursionTextBox.Text = "0";
                currentdepth = 0;
            }
            return currentdepth;
        }


        /// <summary>
        /// Рисует фрактал.
        /// </summary>
        /// <param name="depth"> Глубина рекурсии. </param>
        /// <param name="points"> Параметры для отрисовки.</param>
        abstract public void DrawFractal(int depth, params Point[] points);
    }
}
