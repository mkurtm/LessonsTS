using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Day5 : Lesson
    {
 
        public Day5()
        {
            numberOfDay = 5;
            numberOfTasks = 2;
            description = "Работа с классами.";
            lessonsDescription = new List<string>
            {
                @"Задание №1. Создаем дочерний класс.",
                @"Задание №2. Создаем массив экземпляров класса."
            };

            NavigateTask();
        }

       
        public override void Task1()
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

            NextLesson();
        }

        public override void Task2()
        {
            Candle[] candleArray = new Candle[10];
            Random rand = new Random();

            Console.WriteLine("Создадим массив. Заполним его 10 экзеплярами Candle. С инициализацией по умолчанию.");

            for (int i = 0; i < 10; i++)
            {
                Candle candle = new Candle();
                candleArray[i] = candle;
            }

            Thread.Sleep(1000);

            Console.WriteLine("\nТеперь заполним все Open в соответствии с условием задачи.");

            for (int i = 0; i < 10; i++)
            {
                if (i == 0)
                {
                    candleArray[0].Open = rand.Next(1, 10);
                }
                else
                {
                    candleArray[i].Open = candleArray[i - 1].Open + rand.Next(1, 10);
                }
            }

            Thread.Sleep(1000);

            Console.WriteLine("\nТеперь выведем значения Open всех свечей массива.\n");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Свеча№{0}. Значение Open равно: {1}.", i, candleArray[i].Open);
            }

            NextLesson();
        }
        
    }
}
