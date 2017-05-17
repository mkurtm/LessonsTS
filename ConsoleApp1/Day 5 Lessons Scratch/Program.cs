using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_5_Lessons_Scratch
{
    class Program
    {
        static void Main()
        {
            







            Console.ReadLine(); 

        }

        public void Task1()
        {
            Console.WriteLine("Создадим объект - TickCandle, через базовый конструктор.");
            TickCandle tickCandle = new TickCandle();

            Console.WriteLine("\nНа текущий момент мы обратились к данным {0} раз, и записали {1} раз.", tickCandle.GetCounter, tickCandle.SetCounter);

            Console.WriteLine("\nТеперь 1 раз запишем и 1 раз обратимся к данным родительского класса.");
            tickCandle.Open = 10;
            Console.WriteLine("Мы записали в Open значение 10, а теперь считываем: {0}", tickCandle.Open);

            Console.WriteLine("\nНа текущий момент мы обратились к данным {0} раз, и записали {1} раз.", tickCandle.GetCounter, tickCandle.SetCounter);

            Console.WriteLine("\nТеперь 1 раз запишем и 1 раз обратимся к данным дочернего класса.");
            tickCandle.TickFrame = 5;
            Console.WriteLine("Мы записали в TickFrame значение 5, а теперь считываем: {0}", tickCandle.TickFrame);

            Console.WriteLine("\nНа текущий момент мы обратились к данным {0} раз, и записали {1} раз.", tickCandle.GetCounter, tickCandle.SetCounter);

            Console.WriteLine("\nТеперь в цикле 5 раз запишем и 5 раз считаем информацию.");
            for (int i = 1; i <= 5; i++)
            {
                tickCandle.TickFrame = 5;
                int a = tickCandle.TickFrame;
                Console.WriteLine("Запись №{0}", i);
            }

            Console.WriteLine("\nНа текущий момент мы обратились к данным {0} раз, и записали {1} раз.", tickCandle.GetCounter, tickCandle.SetCounter);
            Console.WriteLine("\nДемонстрация завершена.");
        }
    }
}
