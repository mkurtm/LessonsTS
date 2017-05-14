using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Day3 : Lesson
    {
        public Day3()
        {
            numberOfDay = 3;
            numberOfTasks = 2;
            description = "Вывод массивов и списков через специальные созданные сервисные функции";
            lessonsDescription = new List<string>
            {
                @"Задание №1. Вывод массива.",
                @"Задание №2. Вывод списка."
            };

            NavigateTask();
        }

        public override void Task1()
        {
            Console.WriteLine("Сейчас создадим массив 10х10.");
            Thread.Sleep(1000);
            int[,] arr = new int[10, 10];
            Random rand = new Random();

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    arr[x, y] = rand.Next(10);
                }
            }
            Console.WriteLine("Массив создан. Теперь передадим его в функцию форматированного вывода.\n");

            PrintArray(arr);

            NextLesson();
        }

        public override void Task2()
        {
            Console.WriteLine("Сейчас создадим список из 100 элементов.");
            Thread.Sleep(1000);
            List<int> list = new List<int>(100);
            Random rand = new Random();

            for (int x = 0; x < 100; x++)
            {
                list.Add(rand.Next(10));
            }
            Console.WriteLine("Список создан. Теперь передадим его в функцию форматированного вывода,\nс параметрами вывода в 10 строк и 5 строк.\n");

            PrintArray(list, 10);
            PrintArray(list, 5);

            NextLesson();
        }

        private static void PrintArray(List<int> list, int NumOfLines)
        {
            Thread.Sleep(3000);
            Console.Clear();

            for (int x = 0; x < list.Count / NumOfLines; x++)
            {
                for (int y = 0; y < NumOfLines; y++)
                {
                    Console.Write(" " + list[x + y]);
                }
                Console.WriteLine();
            }
        }

        private static void PrintArray(int[,] arr)
        {
            Thread.Sleep(3000);
            Console.Clear();
            int lengthx = arr.GetLength(0);
            int lengthy = arr.GetLength(1);

            for (int x = 0; x < lengthx; x++)
            {

                for (int y = 0; y < lengthy; y++)
                {
                    Console.Write(" " + arr[x, y]);
                }
                Console.WriteLine();
            }
        }

    }
}
