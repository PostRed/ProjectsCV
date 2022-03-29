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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Инициализация.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  Отлавливает ошибки, которые погут возникнуть при нарисовании фрактала.
        /// </summary>
        /// <param name="fractalName"> Имя фрактала. </param>
        private void DrawFractalCheck(string fractalName)
        {
            try
            {
                DrawFractal(fractalName);
            }
            catch (Exception)
            {
                string text = "К сожалению нарисовать фрактал не получилось(";
                MessageBox.Show(text, "Небольшая проблемка :(", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }


        /// <summary>
        /// Если есть возможность нарисовать фрактал - вызывает метод, которые отвечает за отрисовку данного фрактала.
        /// </summary>
        /// <param name="fractalName"> Имя фрактала. </param>
        private void DrawFractal(string fractalName)
        {
            canvas.Children.Clear();
            RecursionChack(out int depth);
            if (fractalName.IndexOf("Кривая Коха") > -1)
            {
                Koch koch = new Koch(7, canvas, textRecursionDepth);
                koch.DrawKoch(depth);
            }
            else if (fractalName.IndexOf("Ковёр Серпинского") > -1)
            {
                Carpet carpet = new Carpet(5, canvas, textRecursionDepth);
                carpet.DrawCarpet(depth);
            }
            else if (fractalName.IndexOf("Фрактальное дерево") > -1)
            {
                Tree tree = new Tree(14, canvas, textRecursionDepth);
                (double left, double right) = Int2Angle();
                tree.DrawTree(depth, left, right, GetAttitude());
            }
            else if (fractalName.IndexOf("Треугольник Серпинского") > -1)
            {
                Triangle triange = new Triangle(7, canvas, textRecursionDepth);
                triange.DrawTriangle(depth);
            }
            else if (fractalName.IndexOf("Множество Кантора") > -1)
            {
                Cantor kantor = new Cantor(9, canvas, textRecursionDepth);
                kantor.DrawCantor(depth, KantorDistantionChaked());
            }
        }


        /// <summary>
        /// Считывает и проверяет длины отношения отрезков для фрактального дерева.
        /// Если значение некорректно - возвращает значение по умолчанию.
        /// </summary>
        /// <returns> Отношение отрезков для фрактального дерева. </returns>
        private double GetAttitude()
        {
            double res = 0;
            if (attitudeInput.Text != "" && (!double.TryParse(attitudeInput.Text, out res) || res > 70 || res < 0))
            {
                string text = "Неверное значение отношения!";
                MessageBox.Show(text, "Небольшая проблемка :(", MessageBoxButton.OK, MessageBoxImage.Warning);
                attitudeInput.Text = "0";
                res = 0;
            }
            return res / 100;
        }


        /// <summary>
        ///  Считывает и возвращает значения правого и левого углов для фрактальног дерева.
        /// </summary>
        /// <returns> Значения правого и левого углов для фрактальног дерева. </returns>
        private (double, double) Int2Angle()
        {
            ComboBoxItem leftItem = (ComboBoxItem)leftAngle.SelectedItem;
            string s = leftItem.Content.ToString();
            double left = 0, right = 0;
            if (leftItem.Content.ToString() == "157.5")
            {
                left = 157.5;
            }
            else
            {
                left = double.Parse(leftItem.Content.ToString());
            }
            ComboBoxItem rightItem = (ComboBoxItem)rightAngle.SelectedItem;
            if (rightItem.Content.ToString() == "157.5")
            {
                right = 157.5;
            }
            else
            {
                right = double.Parse(rightItem.Content.ToString());
            }
            left = (left / 10f) * Math.PI / 18f;
            right = (right / 10f) * Math.PI / 18f;
            return (left, right);
        }


        /// <summary>
        /// Проверка введенного значения глубины рекурсии.
        /// Если значение некорректно - устаналивает значение по умолчанию.
        /// </summary>
        /// <param name="recursionDepth"> Глубина рекурсии. </param>
        private void RecursionChack(out int recursionDepth)
        {
            recursionDepth = 0;
            if (textRecursionDepth.Text != "" && (!int.TryParse(textRecursionDepth.Text, out recursionDepth) || recursionDepth < 0))
            {
                string text = "Неверный ввод глубины рекурсии";
                MessageBox.Show(text, "Небольшая проблемка :(", MessageBoxButton.OK, MessageBoxImage.Warning);
                recursionDepth = 0;
                textRecursionDepth.Text = "0";
            }
        }


        /// <summary>
        /// При изменении имени фрактала - вызывает метод, который отрисовывает выбранный фрактал.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void FractalNameChanged(object sender, SelectionChangedEventArgs e)
        {

            string fractalName;
            canvas.Children.Clear();
            fractalName = fractalNameChoice.SelectedItem.ToString();
            DrawFractalCheck(fractalName);

        }


        /// <summary>
        /// При изменении глубины рекурсии - проверяет, что выбран какой-то фрактал и
        /// отрисовывает выбранный фрактал.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void RecursionChanged(object sender, TextChangedEventArgs e)
        {
            string fractalName;
            if (canvas != null)
            {
                canvas.Children.Clear();
            }
            if (fractalNameChoice.SelectedItem != null)
            {
                fractalName = fractalNameChoice.SelectedItem.ToString();
                DrawFractalCheck(fractalName);
            }
        }


        /// <summary>
        /// Проверяет введённое значение расстояния между отрезками для множества Кантора.
        /// Если значение некорректно - возвращается значение по умолчанию.
        /// </summary>
        /// <returns> Значение расстояния между отрезками для множества Кантора. </returns>
        private double KantorDistantionChaked()
        {
            double dist = 25;
            if (distance.Text != "" && (!double.TryParse(distance.Text, out dist) || dist < 0 || dist > 50))
            {
                string text = "Ошибка ввода расстояния между отрезками!";
                MessageBox.Show(text, "Небольшая проблемка :(", MessageBoxButton.OK, MessageBoxImage.Warning);
                dist = 25;
                distance.Text = "25";
            }
            if (distance.Text != "" && dist < 5)
            {
                dist = 25;
            }
            return dist;
        }


        /// <summary>
        /// При изменении расстояния между отрезками для множества кантора, если выбран какой-нибудь фрактал -
        /// вызывает метод, которые отрисовывает данный фрактал.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void KantorDistantionChanged(object sender, TextChangedEventArgs e)
        {
            if (fractalNameChoice.SelectedItem != null)
            {
                string fractalName = fractalNameChoice.SelectedItem.ToString();
                if (fractalName.IndexOf("Множество Кантора") > -1)
                {
                    DrawFractalCheck(fractalName);
                }
            }
        }


        /// <summary>
        /// При изменении угла для Пифагорова дерева, если выбран какой-нибудь фрактал -
        /// вызывает метод, которые отрисовывает данный фрактал.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void AngleChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fractalNameChoice.SelectedItem != null)
            {
                string fractalName = fractalNameChoice.SelectedItem.ToString();
                if (fractalName.IndexOf("Фрактальное дерево") > -1)
                {
                    DrawFractalCheck(fractalName);
                }
            }
        }


        /// <summary>
        /// При изменении отношения отрезков для Пифагорова дерева, если выбран какой-нибудь фрактал -
        /// вызывает метод, которые отрисовывает данный фрактал.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void AttitudeChanged(object sender, RoutedEventArgs e)
        {
            if (fractalNameChoice.SelectedItem != null)
            {
                string fractalName = fractalNameChoice.SelectedItem.ToString();
                if (fractalName.IndexOf("Фрактальное дерево") > -1)
                {
                    DrawFractalCheck(fractalName);
                }
            }
        }
    }
}

