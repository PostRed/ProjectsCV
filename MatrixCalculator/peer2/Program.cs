using System;
using System.IO;

namespace peer2
{
    class Program
    {
        /// <summary>
        /// Выводит приветствие и показвает доступные операции.
        /// </summary>
        public static void Hellow()
        {
            Console.WriteLine("||  Добро пожаловать в калькулятор матриц!");
            Console.WriteLine("||  Обратите внимание на то, что все дробные числа округляются до 2-го знака.");
            Console.WriteLine("||  Выберите операцию, которую вы будете использовать:");
            Console.WriteLine("||  1  Нахождение следа матрицы (только для квадратных матриц);");
            Console.WriteLine("||  2  Транспонирование матрицы;");
            Console.WriteLine("||  3  Сумма двух матриц (толко для матриц одинакого размера);");
            Console.WriteLine("||  4  Разность двух матриц (толко для матриц одинакого размера);");
            Console.WriteLine("||  5  Произведение двух матриц (ширина первой матрицы должна быть равна высоте второй);");
            Console.WriteLine("||  6  Умножение матрицы на число;");
            Console.WriteLine("||  7  Нахождение определителя матрицы (толко для квадратной матрицы);");
            Console.WriteLine("||  8  Решение СЛАУ;");
            Console.WriteLine("||  8* Вводить СЛАУ нужно в виде матрицы, то есть опускать x_i!");
        }

        /// <summary>
        /// Создаёт матрицу с заданными параметрами,
        /// заполняет её рандомными числами.
        /// Печатает результат.
        /// </summary>
        /// <param name="mHeight"> Высота генерируемой матрицы. </param>
        /// <param name="mWidth"> Ширина генерируемой матрицы. </param>
        /// <param name="min"> Минимальное число, которое будет сгенерировано. </param>
        /// <param name="max"> Максимальное число, которое будет сгенерировано. </param>
        /// <returns>Возвращает матрицу с рандомными числами.</returns>
        public static double[,] RandomInput(int mHeight, int mWidth, int min, int max)
        {
            double[,] res = new double[mHeight, mWidth];
            Random generator = new Random();
            for (int line = 0; line < mHeight; line++)
            {
                for (int column = 0; column < mWidth; column++)
                {
                    // Так как, нельзя генерировать большие дробные числа -
                    // сначала генерируется дробная часть, а потом целая.
                    // Далее получаем исходно чило и проверяем, что оно не больше максимального.
                    double factPart = generator.NextDouble();
                    double wholePart = generator.Next(min, max + 1);
                    double num = wholePart + factPart;
                    if (num > max)
                    {
                        num = max;
                    }
                    res[line, column] = Math.Round(num, 2);
                }
            }
            Console.WriteLine("||  Исходная матрица:");
            PrintMatrix(res);
            return res;
        }

        /// <summary>
        /// Выводит матрицу в виде таблице.
        /// Также форматирует и заменят -0 на 0.
        /// Для красоты вывода все элемента матрицы округляются до 2 знака после запятой.
        /// </summary>
        /// <param name="list"> Матрица, которую метод выводит.</param>
        public static void PrintMatrix(double[,] list)
        {
            for (int line = 0; line < list.GetLength(0); line++, Console.WriteLine())
            {
                for (int column = 0; column < list.GetLength(1); column++)
                {
                    if (list[line, column] == -0)
                    {
                        list[line, column] = 0;
                    }
                    Console.Write("{0,10}", Math.Round(list[line, column], 2));
                }
            }
        }

        /// <summary>
        /// По введенным параметрам считавает матрицу из консоли.
        /// Проверяет корректность каждого вводимого элемента.
        /// Печатает результат.
        /// </summary>
        /// <param name="mHeight"> Высота матрицы.</param>
        /// <param name="mWidth"> Ширина матрицы. </param>
        /// <returns> Возвращает введенную матрицу. </returns>
        public static double[,] UserInput(int mHeight, int mWidth)
        {
            double[,] res = new double[mHeight, mWidth];
            Console.WriteLine("||  Далее вводите элементы матрицы по одному: ");
            Console.WriteLine("||  Обратите внимание, что все числа не могут по модулю привышать 100");
            for (int line = 0; line < mHeight; line++)
            {
                for (int column = 0; column < mWidth; column++)
                {
                    Console.Write($"||  Введите элементы стоящий на позиции [{line};{column}]:  ");
                    double num;
                    // проверка на корректность.
                    while (!double.TryParse(Console.ReadLine(), out num) | Math.Abs(num) > 100)
                    {
                        Console.WriteLine("||  Ошибка ввода!");
                    }
                    res[line, column] = num;
                }
            }
            Console.WriteLine("||  Исходная матрица:");
            PrintMatrix(res);
            return res;
        }

        /// <summary>
        /// Считывает номер операции, которую выбрал пользователь.
        /// Проверяет введенные данные на корректность.
        /// </summary>
        /// <returns> Возвращает номер операции, которую выбрал пользователь. </returns>
        public static uint Start()
        {
            Console.WriteLine("||  Введите номер выбранной операции: ");
            uint numOper;
            // проверка на корректность.
            while (!uint.TryParse(Console.ReadLine(), out numOper) | numOper < 1 | numOper > 8)
            {
                Console.WriteLine("||  Ошибка ввода!");
            }
            return numOper;
        }

        /// <summary>
        /// Нахождение следа матрицы.
        /// Проверяет является ли матрица квадратной,
        /// так как посчитать след можно только у квадратной матрицы.
        /// Складывает числа на главной диагонали.
        /// Печатает след матрицы.
        /// </summary>
        /// <param name="matrix"> Обрабатываемая матрица.</param>
        public static void Trace(double[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                Console.WriteLine("||  посчитать след можно только у квадратной матрицы!");
            }
            else
            {
                double res = 0;
                for (int line = 0; line < matrix.GetLength(0); line++)
                {
                    for (int column = 0; column < matrix.GetLength(1); column++)
                    {
                        if (line == column)
                        {
                            res += matrix[line, column];
                        }
                    }
                }
                Console.WriteLine($"||  След введенной матрицы = {Math.Round(res, 3)}!");
            }
        }

        /// <summary>
        /// Транспонирование матрицы.
        /// Для каждого элемента меняет значение солбца на значение строки и наоборот.
        /// Печатает транспонированную матрицу.
        /// </summary>
        /// <param name="matrix">Обрабатываемая матрица.</param>
        public static void Transpose(double[,] matrix)
        {
            double[,] res = new double[matrix.GetLength(1), matrix.GetLength(0)];
            for (int line = 0; line < matrix.GetLength(1); line++)
            {
                for (int column = 0; column < matrix.GetLength(0); column++)
                {
                    res[line, column] = matrix[column, line];
                }
            }
            Console.WriteLine("||  Результат транспонирования матрицы:");
            PrintMatrix(res);
        }

        /// <summary>
        /// Умножение матрицы на число.
        /// Считывает число и проверяет его на корректность.
        /// Далее каждый элемент матрицы умножает на данное число.
        /// Выводит матрицу, умноженную на число.
        /// </summary>
        /// <param name="matrix"> Обрабатываемая матрица.</param>
        public static void MultiplicationByNum(double[,] matrix)
        {
            Console.WriteLine("||  Число, на которое умножается матрица не может по модулю превышать 100!");
            Console.Write("||  Введите число, на которое вы хотите умножить матрицу:  ");
            string stringNum = Console.ReadLine();
            double num;
            // Проверка на корректность.
            while (!double.TryParse(stringNum, out num) | Math.Abs(num) > 100)
            {
                Console.WriteLine("||  Ошибка ввода!");
                Console.WriteLine("||  Число, на которое умножается матрица не может по модулю превышать 100!");
                Console.Write("||  Введите число, на которое вы хотите умножить матрицу:  ");
                stringNum = Console.ReadLine();
            }
            double[,] res = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int line = 0; line < matrix.GetLength(0); line++)
            {
                for (int column = 0; column < matrix.GetLength(1); column++)
                {
                    res[line, column] = num * matrix[line, column];
                }
            }
            Console.WriteLine($"||  Результат умножения матрицы на число {num}:");
            PrintMatrix(res);
        }

        /// <summary>
        /// Меняет строки в матрице местами.
        /// Создается матрица, которая копирует исходную матрицу, кроме 2х заданных строк.
        /// Эти строки меняются местами.
        /// Затем исходной матрице присваивается значение новой матрицы.
        /// </summary>
        /// <param name="matrix">Обрабатываемая матрица.</param>
        /// <param name="firsLine"> Первая строка, которую мы меняем местами со второй. </param>
        /// <param name="secondLine"> Вторая строка, которую мы меняем местами с первой. </param>
        public static void SwapLines(ref double[,] matrix, int firsLine, int secondLine)
        {
            double[,] res = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int line = 0; line < matrix.GetLength(0); line++)
            {
                for (int column = 0; column < matrix.GetLength(1); column++)
                {
                    if (line == firsLine)
                    {
                        res[firsLine, column] = matrix[secondLine, column];
                    }
                    else if (line == secondLine)
                    {
                        res[secondLine, column] = matrix[firsLine, column];
                    }
                    else
                    {
                        res[line, column] = matrix[line, column];
                    }
                }
            }
            matrix = res;
        }

        /// <summary>
        /// Уиножение строки матрицы на число.
        /// Изменяется только заданная строка.
        /// Каждый её элемент умножается на заданное число.
        /// </summary>
        /// <param name="matrix">Обрабатываемая матрица.</param>
        /// <param name="line"> Строка, которая умножается на число.</param>
        /// <param name="num">Число, на которое умножается заданная строка.</param>
        public static void LineMultiplicationByNum(ref double[,] matrix, int line, double num)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[line, j] = num * matrix[line, j];
            }
        }

        /// <summary>
        /// Прибавляет к первой строке матрицы вторую строку, умноженную на заданное число.
        /// Изменяется только строка, к которой прибавляют вторую строку.
        /// Можно было бы сделать метод обычного сложения строк, но так получится меньше операций.
        /// Иначе бы пришлось сначла умножать одну строку на число, а потом делить.
        /// А так меняется только одна  строка.
        /// </summary>
        /// <param name="matrix">Обрабатываемая матрица.</param>
        /// <param name="firstLine"> Строка, к которой прибавляется другая строка, умноженная на число.</param>
        /// <param name="secondLine"> Строка, которая прибавляется к первой строке.</param>
        /// <param name="num"> Число, на которое умножается вторая строка.</param>
        public static void SumLine(ref double[,] matrix, int firstLine, int secondLine, double num)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[firstLine, j] = matrix[firstLine, j] + matrix[secondLine, j] * num;
            }

        }

        /// <summary>
        /// Метод Гаусса, который приводит матрицу к улучшенному ступенчатому виду.
        /// Сначала ищется ведущий элемент (тот, который не равен нулю).
        /// Затем этот элемент обращается в единицу.
        /// Также есть переменная, которая считает определитель.
        /// Определитель меняется, когда происходит умножение или деление строки на число.
        /// Затем все элементы столбца обращаются в нули с помощью ведущего элемента.
        /// </summary>
        /// <param name="matrix"> Обрабатываемая матрица.</param>
        /// <param name="det"> Переменная, которая сичтает определитель.</param>
        public static void GaussMethod(ref double[,] matrix, ref double det)
        {
            int line = 0;
            int column = 0;
            while (line < matrix.GetLength(0) & column < matrix.GetLength(1))
            {
                int leadLine = line;
                // Поиск ведущего элемента.
                for (int newLine = line; newLine < matrix.GetLength(0); newLine++)
                {
                    if (matrix[newLine, column] != 0)
                    {
                        leadLine = newLine;
                        break;
                    }
                }
                // Проверка того, что в столбце не все нули.
                if (matrix[leadLine, column] != 0)
                {
                    det *= matrix[leadLine, column];
                    // Ведущий элемент становится единицей.
                    LineMultiplicationByNum(ref matrix, leadLine, 1 / matrix[leadLine, column]);
                    SwapLines(ref matrix, leadLine, line);
                    // Остальные элементы в столбце становятся нулями.
                    for (int newLine = 0; newLine < matrix.GetLength(0); newLine++)
                    {
                        if (newLine != leadLine)
                        {
                            SumLine(ref matrix, newLine, leadLine, -matrix[newLine, column]);
                        }
                    }
                }
                column++;
                line++;
            }
        }

        /// <summary>
        /// Решает СЛАУ.
        /// Приводит матрицу к улучшенному ступенчатому виду с помощью метода Гаусса.
        /// Есть флаги, которые сообщают о том, что в матрице бесконечно много решений или,
        /// что в матрице нет решений.
        /// Бесконечно много решений, когда в строке (без последнеего элемента) - два ненулевых элемента.
        /// Нет решений, когда все элементы строки кроме последнего равны нулю.
        /// В конце вызывает метод, который по данным печатает ответ.
        /// </summary>
        /// <param name="matrix">Обрабатываемая матрица.</param>
        public static void Equations(double[,] matrix)
        {
            double det = 1;
            GaussMethod(ref matrix, ref det);
            Console.WriteLine("|| Улучшенный ступенчатый вид матрицы:");
            PrintMatrix(matrix);
            string[] ans = new string[matrix.GetLength(0)];
            bool infans = false, noans = false;
            for (int line = 0; line < matrix.GetLength(0); line++)
            {
                int count0 = 0, countans = 0;
                for (int column = 0; column < matrix.GetLength(1) - 1; column++)
                {
                    if (matrix[line, column] != 0)
                    {
                        countans++;
                        ans[line] = $"||  x{line} = {matrix[line, matrix.GetLength(1) - 1]}";
                    }
                    else
                    {
                        count0++;
                    }
                }
                // Проверка на отсутствие решений.
                if (count0 == matrix.GetLength(1) - 1 & matrix[line, matrix.GetLength(1) - 1] != 0)
                {
                    noans = true;
                }
                // Проверка на бесконечное число решений.
                else if (countans > 1 | (count0 == matrix.GetLength(1) - 1 & matrix[line, matrix.GetLength(1) - 1] == 0))
                {

                    infans = true;
                }
            }
            // Вызов метода, который печатает ответ.
            AnsEquations(noans, infans, ans);
        }

        /// <summary>
        /// Метод печатает ответ для СЛАУ.
        /// Если переменная, которая указывает на отсутствие ответов - верна, то
        /// сообщается об отсутствии ответов.
        /// Если переменная, которая указывает на бесконечное число ответов - верна, то
        /// сообщается о бесконечном числе ответов.
        /// Иначе - ответы выводятся.
        /// </summary>
        /// <param name="noans"></param>
        /// <param name="infans"></param>
        /// <param name="ans"></param>
        public static void AnsEquations(bool noans, bool infans, string[] ans)
        {
            if (noans)
            {
                Console.WriteLine("|| В системе нет решений!");
            }
            else if (infans)
            {
                Console.WriteLine("|| В системе Бесконечно много решений!");
            }
            else
            {
                foreach (string x in ans)
                {
                    Console.WriteLine(x);
                }
            }
        }

        /// <summary>
        /// Вычисление определителя матрицы.
        /// Вызывается метод Гаусаа, которые приводит матрицу к каноническому виду и считает определитель.
        /// Подробнее о подсчете определителя - в комментарии к методу Гаусса.
        /// Также проверяется является ли матрица квадратной.
        /// Это нужно, так как определитель можно посчитать только у квадратной матрицы.
        /// </summary>
        /// <param name="matrix">Обрабатываемая матрица.</param>
        public static void Determinant(double[,] matrix)
        {
            // Проверка того, что матрица квадратная.
            if (matrix.GetLength(0) == matrix.GetLength(1))
            {
                double det = 1;
                GaussMethod(ref matrix, ref det);
                Console.Write("||  Определитель матрицы =  ");
                Console.WriteLine(Math.Round(det, 2));
            }
            else
            {
                Console.WriteLine("||  Определитель можно посчитать только у квадратной матрицы!");
            }
        }

        /// <summary>
        /// Операции, в которых используется только одна матрица.
        /// По номеру операции, которую ввел пользователь вызывается соответствующий метод.
        /// </summary>
        /// <param name="matrix">Обрабатываемая матрица.</param>
        /// <param name="numOper">Номер операции.</param>
        public static void OperationFor1(double[,] matrix, uint numOper)
        {
            if (numOper == 1)
            {
                Trace(matrix);
            }
            else if (numOper == 2)
            {
                Transpose(matrix);
            }
            else if (numOper == 6)
            {
                MultiplicationByNum(matrix);
            }
            else if (numOper == 7)
            {
                Determinant(matrix);
            }
            else
            {
                Equations(matrix);
            }
        }

        /// <summary>
        /// Сумма двух матриц.
        /// Создается матрица-ответ, каждый элемент которой 
        /// - равен сумме соответствующих элементов данных матриц.
        /// Также проверяется являются ли матрицы одинакового размера, так как
        /// Складывать можно только одинаковые матрицы.
        /// Затем выводится результат.
        /// </summary>
        /// <param name="matrix1"> Первая обрабатываемая матрица.</param>
        /// <param name="matrix2"> Вторая обрабатываемая матрица.</param>
        public static void Sum(double[,] matrix1, double[,] matrix2)
        {
            // Проверка того, что матрицы одинакого размера.
            if (matrix1.GetLength(0) != matrix2.GetLength(0) | matrix1.GetLength(1) != matrix2.GetLength(1))
            {
                Console.WriteLine("||  Складывать можно только одинаковые матрицы!");
            }
            else
            {
                double[,] res = new double[matrix1.GetLength(0), matrix1.GetLength(1)];
                for (int i = 0; i < matrix1.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix1.GetLength(1); j++)
                    {
                        res[i, j] = matrix1[i, j] + matrix2[i, j];
                    }
                }
                Console.WriteLine("||  Сумма матриц:");
                PrintMatrix(res);
            }
        }

        /// <summary>
        /// Разность двух матриц.
        /// Создается матрица-ответ, каждый элемент которой 
        /// - равен разности соответствующих элементов данных матриц.
        /// Также проверяется являются ли матрицы одинакового размера, так как
        /// Вычитать можно только одинаковые матрицы.
        /// Затем выводится результат.
        /// </summary>
        /// <param name="matrix1"> Первая обрабатываемая матрица.</param>
        /// <param name="matrix2"> Вторая обрабатываемая матрица.</param>
        public static void Subtraction(double[,] matrix1, double[,] matrix2)
        {
            // Проверка того, что матрицы одинакого размера.
            if (matrix1.GetLength(0) != matrix2.GetLength(0) | matrix1.GetLength(1) != matrix2.GetLength(1))
            {
                Console.WriteLine("||  Вычитать можно только одинаковые матрицы!");
            }
            else
            {
                double[,] res = new double[matrix1.GetLength(0), matrix1.GetLength(1)];
                for (int i = 0; i < matrix1.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix1.GetLength(1); j++)
                    {
                        res[i, j] = matrix1[i, j] - matrix2[i, j];
                    }
                }
                Console.WriteLine("||  Разность матриц:");
                PrintMatrix(res);
            }
        }

        /// <summary>
        /// Произведение матриц.
        /// Создается матрица-ответ, каждый элемент которой 
        /// - Вычисляется по формуле произведения матриц.
        /// Также проверяется являются ли матрицы подходяего размера, так как
        /// Для умножения одной матрицы на другую - нужно, чтобы ширина первой матрицы была равна высоте второй.
        /// Затем выводится результат.
        /// </summary>
        /// <param name="matrix1"> Первая обрабатываемая матрица.</param>
        /// <param name="matrix2"> Вторая обрабатываемая матрица.</param>
        public static void Multiplication(double[,] matrix1, double[,] matrix2)
        {
            // Проверка того, что матрицы подходящего размера.
            if (matrix1.GetLength(1) != matrix2.GetLength(0))
            {
                Console.WriteLine("||  Для умножения одной матрицы на другую - нужно,");
                Console.WriteLine("||  чтобы ширина первой матрицы была равна высоте второй!");
            }
            else
            {
                double[,] res = new double[matrix1.GetLength(0), matrix2.GetLength(1)];
                for (int i = 0; i < matrix1.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix2.GetLength(1); j++)
                    {
                        double num = 0;
                        for (int q = 0; q < matrix1.GetLength(1); q++)
                        {
                            num += matrix1[i, q] * matrix2[q, j];
                        }
                        res[i, j] = num;
                    }
                }
                Console.WriteLine("||  Произведение матриц:");
                PrintMatrix(res);
            }
        }

        /// <summary>
        /// Операции, в которых используются две матрицы.
        /// По номеру операции, которую ввел пользователь вызывается соответствующий метод.
        /// </summary>
        /// <param name="matrix1"> Первая обрабатываемая матрица.</param>
        /// <param name="matrix2">Вторая обрабатываемая матрица.</param>
        /// <param name="numOper"> Номер операции.</param>
        public static void OperationFor2(double[,] matrix1, double[,] matrix2, uint numOper)
        {
            if (numOper == 3)
            {
                Sum(matrix1, matrix2);
            }
            else if (numOper == 4)
            {
                Subtraction(matrix1, matrix2);
            }
            else
            {
                Multiplication(matrix1, matrix2);
            }
        }

        /// <summary>
        /// Вводятся размеры и они проверяются на корректность.
        /// Размеры не превышают 10, так как большие матрицы вряд ли понадобятся.
        /// Затем вызывается метод, который по параметрам считывает или сознает матрицу.
        /// </summary>
        /// <returns> Возвращает матрицу, с которой дальше будет работь программа.</returns>
        public static double[,] ProcessInput()
        {
            Console.WriteLine("||  Введите размеры матрицы: ");
            Console.WriteLine("||  Обратите внимание, что матрица размеры матрицы не могут превышать 10.");
            int mWidth, mHeight;
            Console.Write("||  Высота матрицы = ");
            string stringHeight = Console.ReadLine();
            Console.Write("||  Ширина матрицы = ");
            string stringWidth = Console.ReadLine();
            // Проверка на корректность.
            while (!int.TryParse(stringHeight, out mHeight) | !int.TryParse(stringWidth, out mWidth) | mHeight > 10 | mWidth > 10 | mHeight < 0 | mWidth < 0)
            {
                Console.WriteLine("||  Ошибка ввода!");
                Console.Write("||  Высота матрицы = ");
                stringHeight = Console.ReadLine();
                Console.Write("||  Ширина матрицы = ");
                stringWidth = Console.ReadLine();
            }
            // Вызов метода, который создает матрицу.
            return InputMatrix(mHeight, mWidth);
        }

        /// <summary>
        /// Выводит правила, по которым осуществляется ввод матрицы с помощью файла.
        /// </summary>
        public static void FileRegulations()
        {
            Console.WriteLine("||  Матрица должна состоят только из действительных чисел разделенных пробелами!");
            Console.WriteLine("||  Также в файле матрица должна быть записана по строкам!");
            Console.WriteLine("||  При несовпадении введенных параметров матрицы и параметров матрицы в файле");
            Console.WriteLine("||  или при указании неверного пути к файлу - будет сообщено об ошибке!");
            Console.WriteLine("||  Введите путь к файлу и его название:");
        }

        /// <summary>
        /// Создание экземпляра класса StreamReader для чтения из файла.
        /// Оператор using обрамляет код, где используется StreamReader.
        /// Если возникает ошибка при открывании файла - сообщается об ошибке.
        /// Далее вызывается метод, который выводит матрицу или сообщает об ошибке
        /// и заново просит пользователя ввести путь к файлу.
        /// </summary>
        /// <param name="mHeight"> Высота обрабатываемой матрицы.</param>
        /// <param name="mWidth"> Ширина обрабатываемой матрицы</param>
        /// <returns> На выходе вы </returns>
        public static double[,] FileInput(int mHeight, int mWidth)
        {
            double[,] res = new double[mHeight, mWidth];
            FileRegulations();
            bool matrixOK = true;
            string fileName = Console.ReadLine();
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    int numLine = 0;
                    // Построчное чтение файла и отображение этих строк,
                    // пока не будет достигнут конец файла.
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (numLine <= mHeight)
                        {
                            string[] nums = line.Split();
                            if (nums.Length == mWidth)
                            {
                                for (int j = 0; j < mWidth; j++)
                                {
                                    if (!double.TryParse(nums[j], out res[numLine, j]))
                                    {
                                        matrixOK = false;
                                    }
                                }
                            }
                            else
                            {
                                matrixOK = false;
                            }
                        }
                        else
                        {
                            matrixOK = false;
                        }
                        numLine++;
                    }
                }
            }
            // Обработка исключения даст пользователю информацию,
            // что пошло не так.
            catch (Exception e)
            {
                Console.WriteLine("||  Невозможно прочесть файл!");
                Console.WriteLine(e.Message);
                matrixOK = false;
            }
            return FileOutput(res, matrixOK, mHeight, mWidth);
        }

        /// <summary>
        /// Метод, который выводит матрицу или сообщает об ошибке
        /// и заново просит пользователя ввести путь к файлу.
        /// </summary>
        /// <param name="matrix"> Обрабатываемая матрица.</param>
        /// <param name="matrixOK"> Булевская переменная, в которая сообщает, что матрица подходит по всем критериям.</param>
        /// <param name="mHeight"> Высота матрицы для вызова метода считывания в случае ошибки. </param>
        /// <param name="mWidth"> Ширина матрицы для вызова метода считывания в случае ошибки.</param>
        /// <returns></returns>
        public static double[,] FileOutput(double[,] matrix, bool matrixOK, int mHeight, int mWidth)
        {
            if (matrixOK)
            {
                Console.WriteLine("||  Исходная матрица:");
                PrintMatrix(matrix);
                return matrix;
            }
            else
            {
                Console.WriteLine("||  Ошибка ввода!");
                return FileInput(mHeight, mWidth);
            }
        }

        /// <summary>
        /// Метод считывает номер операции, которая отвечает за ввод матрицы.
        /// Идет проверка того, что номер операции равен 1, 2 или 3.
        /// </summary>
        /// <returns> Возвращает номер операции.</returns>
        public static string OperIsOk()
        {
            Console.WriteLine("||  Если хотите самостоятельно ввести матрицу - нажмите 1");
            Console.WriteLine("||  Если хотите, чтобы матрица рандомно сгенерировалась - нажмите 2");
            Console.WriteLine("||  Если хотите, чтобы матрица считалась из файла - нажмите 3");
            string input = Console.ReadLine();
            // Проверка номера операции на корректность.
            while (input != "1" & input != "2" & input != "3")
            {
                Console.WriteLine("||  Ошибка ввода!");
                input = Console.ReadLine();
            }
            return input;
        }

        /// <summary>
        /// Считываются границв генерации, затем идет проверка их на корректность.
        /// </summary>
        /// <returns>Возвращает список с двумя границами. </returns>
        public static int[] GenerationIsOk()
        {
            int[] res = new int[2];
            Console.WriteLine("||  Введите границы для генерации чисел:");
            Console.WriteLine("||  Обратите внимание, что модуль границ генерации не может превышать 100.");
            Console.WriteLine("||  А также нижняя граница должна быть меньше верхней.");
            int min, max;
            Console.Write("||  Минимальное число = ");
            string stringMin = Console.ReadLine();
            Console.Write("||  Максимальное число = ");
            string stringMax = Console.ReadLine();
            // Проверка границ генерации на корректность.
            while (!int.TryParse(stringMin, out min) | !int.TryParse(stringMax, out max) | min > 100 | max > 100 | min >= max)
            {
                Console.WriteLine("||  Ошибка ввода!");
                Console.Write("||  Минимальное число = ");
                stringMin = Console.ReadLine();
                Console.Write("||  Максимальное число = ");
                stringMax = Console.ReadLine();
            }
            res[0] = min;
            res[1] = max;
            return res;
        }


        /// <summary>
        /// Метод считывает номер операции, которая отвечает за ввод матрицы.
        /// Сначала идет проверка того, что номер операции равен 1, 2 или 3.
        /// Если операция равна 1, то вызывается метод, 
        /// который соответствует самостоятельному вводу матрицы пользователем.
        /// Если равна 2, то матрица генерируется рандомно.
        /// Иначе - вызывается метод, который отвечает за чтение матрицы из файла.
        /// </summary>
        /// <param name="mHeight"> Высота матрицы, которая будет считываться или создаваться. </param>
        /// <param name="mWidth"> Ширина матрицы, которая будет считываться или создаваться.</param>
        /// <returns> На выходе получается матрица, которую далее программа будет обрабатывать</returns>
        public static double[,] InputMatrix(int mHeight, int mWidth)
        {
            string input = OperIsOk();
            if (input == "1")
            {
                // Матрица вводится вользователем.
                return UserInput(mHeight, mWidth);
            }
            else if (input == "2")
            {
                int[] minAndMax = GenerationIsOk();
                int min = minAndMax[0];
                int max = minAndMax[1];
                // Матрица генерируется рандомно.
                return RandomInput(mHeight, mWidth, min, max);
            }
            else
            {
                // Матрица считывается из файла.
                return FileInput(mHeight, mWidth);
            }
        }

        /// <summary>
        /// Идет вызов всех методов.
        /// Сначала приветствие.
        /// Затем ситывается номер операции, которой пользователь хочет воспользоваться.
        /// Затем по номеру операции вызывает метод, который отвечает за обработку одной или двух матриц.
        /// Далее организован повтор решения
        /// </summary>
        static void Main()
        {
            ConsoleKeyInfo keyToExit;
            do
            {
                Hellow();
                uint numOper = Start();
                if (numOper == 1 | numOper == 2 | numOper == 6 | numOper == 7 | numOper == 8)
                {
                    double[,] matrix1 = ProcessInput();
                    OperationFor1(matrix1, numOper);
                }
                else
                {
                    double[,] matrix1 = ProcessInput();
                    double[,] matrix2 = ProcessInput();
                    OperationFor2(matrix1, matrix2, numOper);
                }
                Console.WriteLine("||  Для выхода из программы нажмите Escape");
                Console.WriteLine("||  Чтобы начать новую игру нажмите Enter");
                Console.WriteLine();
                // Реализация повтора решения.
                keyToExit = Console.ReadKey();
            } while (keyToExit.Key != ConsoleKey.Escape);
        }
    }
}