using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            #region Задание №1. Циклы.

            int whilecount = 0;
            do
            {
                Console.WriteLine("Это цикл DO WHILE, {0}", whilecount);
                whilecount++;
            } while (whilecount < 10);

            whilecount = 0;

            while (whilecount < 10)
            {
                Console.WriteLine("Это цикл WHILE, {0}", whilecount);
                whilecount++;
            }

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Это цикл for, {0}", i);
            }

            Thread.Sleep(2000);

            #endregion

            #region Задание №2. Остаток от деления %.

            int number;

            for (int i = 0; i < 100; i++)
            {
                number = i % 2;
                if (number == 0)
                {
                    Console.WriteLine("Четное число: {0}", i);
                    Thread.Sleep(500);
                }
            }

            #endregion



            Console.ReadLine();
        }
    }
}
