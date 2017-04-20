using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Day2 : Lesson
    {
        public Day2()
        {
            numberOfDay = 2;
            numberOfTasks = 6;
            description = "Работа с массивами и списками.";
            lessonsDescription = new List<string>
            {
                @"Задание №1. Массив и простейшие операции.",
                @"Задание №2. Список и вставка элемента.",
                @"Задание №3. Список и удаление элемента.",
                @"Задание №4. Массив и поиск максимума и минимума.",
                @"Задание №5. Меняем местами четные и нечетные элементы в списке.",
                @"Задание №6. Сортируем двумерный массив."
            };

            NavigateTask();
        }

        public override void Task1()
        {
            int[,] arr = new int[10, 10];
            Random rand = new Random();
            int sum5 = 0;
            int diag = 0;

            //Заполняем и сразу выводим массив
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    arr[x, y] = rand.Next(10);
                    Console.Write(" " + arr[x, y]);

                    // Если 5 строка, то сразу находим сумму
                    if (x == 4)
                    {
                        sum5 += arr[x, y];
                    }

                    //Если элемент лежит на диагонали, то сразу находим сумму
                    if (x == y)
                    {
                        diag += arr[x, y];
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("Сумма элементов пятой строки: {0}", sum5);
            Console.WriteLine("Сумма элементов на диагонали: {0}", diag);

            NextLesson();
        }

        public override void Task2()
        {
            List<int> myList = new List<int>(15);
            Random rand = new Random();

            Console.WriteLine("Первоначальный список:");

            for (int i = 0; i < 15; i++)
            {
                myList.Add(rand.Next(10));
                Console.Write(" " + myList[i]);
            }

            //Вставляем 333 в каждую третью ячейку списка.
            for (int i = 0; i < myList.Count; i++)
            {
                if (i % 3 == 0)
                {
                    myList.Insert(i, 333);
                }
            }

            Console.WriteLine("\nКонечный список:");
            foreach (var item in myList)
            {
                Console.Write(" " + item);
            }
            Console.WriteLine();

            NextLesson();
        }

        public override void Task3()
        {
            List<int> myList = new List<int>(15);
            Random rand = new Random();

            Console.WriteLine("Первоначальный список:");

            for (int i = 0; i < 15; i++)
            {
                myList.Add(rand.Next(10));
                Console.Write(" " + myList[i]);
            }

            //Удаляем третью ячейку списка.
            for (int i = 0; i < myList.Count; i++)
            {
                if (i % 3 == 0)
                {
                    myList.RemoveAt(i);
                }
            }

            Console.WriteLine("\nКонечный список:");
            foreach (var item in myList)
            {
                Console.Write(" " + item);
            }
            Console.WriteLine();

            NextLesson();
        }

        public override void Task4()
        {
            int[,] arr = new int[10, 10];
            Random rand = new Random();
            int max = 0, min = 100, maxx = 0, maxy = 0, minx = 0, miny = 0;

            //Заполняем и сразу выводим массив
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    arr[x, y] = rand.Next(100);
                    Console.Write(" " + arr[x, y]);

                    if (arr[x, y] > max)
                    {
                        max = arr[x, y];
                        maxx = x;
                        maxy = y;
                    }

                    if (arr[x, y] < min)
                    {
                        min = arr[x, y];
                        minx = x;
                        miny = y;
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("Наибольший элемент массива: {0}, расположен по адресу: [{1},{2}].", max, maxx, maxy);
            Console.WriteLine("Наименьший элемент массива: {0}, расположен по адресу: [{1},{2}].", min, minx, miny);
            Console.WriteLine("Сумма элементов равна: {0}.", max + min);

            NextLesson();
        }

        public override void Task5()
        {
            //инициализируем переменные
            List<int> list = new List<int>(10);
            Random rand = new Random();
            int buffer;

            //заполняем список произвольными значениями
            Console.WriteLine("Первоначальный список:");
            for (int i = 0; i < 10; i++)
            {
                list.Add(rand.Next(10));
                Console.Write(" " + list[i]);
            }

            //меняем местами четные и нечетные элементы
            for (int i = 0; i < 10; i++)
            {
                if (i % 2 == 0)
                {
                    buffer = list[i + 1];
                    list[i + 1] = list[i];
                    list[i] = buffer;
                }
            }

            Console.WriteLine("\nКонечный список:");
            foreach (var item in list)
            {
                Console.Write(" " + item);
            }
            Console.WriteLine();

            NextLesson();
        }

        public override void Task6()
        {
            //инициализируем переменные
            int[,] arr = new int[10, 10];
            Random rand = new Random();
            int buffer;
            int isSorted = 0;

            //заполняем произвольными
            Console.WriteLine("Первоначальный массив:");
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    arr[x, y] = rand.Next(100);
                    Console.Write(" " + arr[x, y]);
                }
                Console.WriteLine();
            }

            //проводим построчную сортировку
            do
            {
                isSorted = 0;
                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        if (y != 0)
                        {
                            if (arr[x, y] < arr[x, y - 1])
                            {
                                buffer = arr[x, y - 1];
                                arr[x, y - 1] = arr[x, y];
                                arr[x, y] = buffer;
                                isSorted++;
                            }
                        }
                        if (x != 0 && y == 0)
                        {
                            if (arr[x, y] < arr[x - 1, 9])
                            {
                                buffer = arr[x - 1, 9];
                                arr[x - 1, 9] = arr[x, y];
                                arr[x, y] = buffer;
                                isSorted++;
                            }
                        }
                    }
                }
            } while (isSorted != 0);

            Console.WriteLine("Конечный массив:");
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    Console.Write(" " + arr[x, y]);
                }
                Console.WriteLine();
            }

            NextLesson();
        }
    }
}
