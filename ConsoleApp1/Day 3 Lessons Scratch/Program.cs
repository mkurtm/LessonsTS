using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Day_3_Lessons_Scratch
{
    class Program
    {
        static void Main()
        {
            int i = 5;

            double d = 10.5;
            i = Convert.ToInt32(d);

            bool logic = false;
            i = Convert.ToInt32(logic);




            Console.ReadLine();
        }

        private static void PrintArray(List<int> list, int NumOfLines)
        {
            Thread.Sleep(2000);
            Console.Clear();

            for (int x = 0; x < list.Count/NumOfLines; x++)
            {
                for (int y = 0; y < NumOfLines; y++)
                {
                    Console.Write(" " + list[x+y]);                
                }
                Console.WriteLine();
            }
        }

        private static void PrintArray(int[,] arr)
        {
            Thread.Sleep(2000);
            Console.Clear();
            int lengthx = arr.GetLength(0);
            int lengthy = arr.Length / lengthx;

            for (int x = 0; x < lengthx; x++)
            {

                for (int y = 0; y < lengthy; y++)
                {
                    Console.Write(" " + arr[x, y]);
                }
                Console.WriteLine();
            }
        }
        
        public void Task1()
        {
            Console.WriteLine("Сейчас создадим массив 10х10.");
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
        }

        public void Tsk2()
        {
            Console.WriteLine("Сейчас создадим список из 100 элементов.");
            List<int> list = new List<int>(100);
            Random rand = new Random();

            for (int x = 0; x < 100; x++)
            {
                list.Add(rand.Next(10));
            }
            Console.WriteLine("Список создан. Теперь передадим его в функцию форматированного вывода, с параметрами вывода в 10 строк и 5 строк.\n");

            PrintArray(list, 10);
            PrintArray(list, 5);
        }
    }
}
