using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_2_Lessons_Scratch
{
    class Program
    {
        static void Main()
        {
            




            Console.ReadLine();

        }

        public void Task1()
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
        }

        public void Task2()
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
        }

        public void Task3()
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
        }

        public void Task4()
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
        }

        public void Task5()
        {

        }

        public void Task6()
        {

        }
    }
}
