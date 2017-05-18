using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Day4 : Lesson
    {
        public enum EvenOdd
        {
            Even, Odd
        }

        public Day4()
        {
            numberOfDay = 4;
            numberOfTasks = 5;
            description = "Работа с функциями.";
            lessonsDescription = new List<string>
            {
                @"Задание №1. Удаление четных по индексу членов списка.",
                @"Задание №2. Определение количества дней в месяце с учетом високосного года.",
                @"Задание №3. Практика с созданным классом.",
                @"Задание №4. Использование рекурсии.",
                @"Задание №5. Раскладываем по коробкам список целых чисел."
            };

            NavigateTask();
        }

        private static void F2(int n)
        {
            if (n >= 0)
            {
                Console.Write("{0} |", n);
                n--;
                F2(n);
            }
            else
                Console.WriteLine("\nРасчет окончен.");
        }

        private static int DayInMonth(DateTime dateTime)
        {
            switch (dateTime.Month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;
                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
                case 2:
                    if (IsLeap(dateTime.Year) == true)
                        return 29;
                    else
                        return 28;
                default:
                    return 0;
            }
        }

        private static bool IsLeap(int year)
        {
            if ((year - 45) % 4 == 0)
                return true;

            else
                return false;
        }

        private static List<int> F1(List<int> list, EvenOdd evenOdd)
        {
            List<int> rlist = new List<int>();

            //Вначале копируем данные

            for (int i = 0; i < list.Count; i++)
            {
                if (evenOdd == EvenOdd.Even && (i % 2 == 0 || i == 0))
                    rlist.Add(list[i]);

                else if (evenOdd == EvenOdd.Odd && i % 2 == 1)
                    rlist.Add(list[i]);
            }

            //Потом удаляем          

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (evenOdd == EvenOdd.Even && (i % 2 == 0 || i == 0))
                    list.RemoveAt(i);

                else if (evenOdd == EvenOdd.Odd && i % 2 == 1)
                    list.RemoveAt(i);
            }

            return rlist;
        }

        public override void Task1()
        {
            List<int> list = new List<int>();
            List<int> rlist = new List<int>();
            Random rand = new Random();
            EvenOdd evenOdd = EvenOdd.Even;

            Console.WriteLine("Стартовый список:");
            for (int i = 0; i < 20; i++)
            {
                list.Add(rand.Next(10));
                Console.Write("{0} ", list[i]);
            }

            Thread.Sleep(1000);

            Console.WriteLine("\nКонечный список, удаляем четные:");
            rlist = F1(list, evenOdd);

            foreach (int item in rlist)
            {
                Console.Write("{0} ", item);
            }

            Console.WriteLine("\nНачальный список, после редактирования:");

            foreach (int item in list)
            {
                Console.Write("{0} ", item);
            }

            NextLesson();
        }

        public override void Task2()
        {
            DateTime dateTime;
            while (true)
            {
                Console.WriteLine("Введите дату в формате: гггг.мм");

                if (DateTime.TryParse(Console.ReadLine(), out dateTime) == false)
                {
                    Console.WriteLine("Вы ввели дату в неверном формате. Повторите ввод.");
                    continue;
                }
                else
                {
                    Console.WriteLine("В {0} году, в {1} месяце, количество дней: {2}.", dateTime.Year, dateTime.Month, DayInMonth(dateTime));
                    break;
                }
            }

            NextLesson();
        }

        public override void Task3()
        {
            Trade trade = new Trade();
            trade.TradeId = 1215;
            trade.Price = 20000;
            trade.Info();

            NextLesson();
        }

        public override void Task4()
        {
            int n;
            Console.WriteLine("Введите число:");
            int.TryParse(Console.ReadLine(), out n);
            F2(n);

            NextLesson();
        }

        public override void Task5()
        {
            List<int> list = new List<int>();
            Random rand = new Random();
            int Size;
            List<Box> Shelf = new List<Box>();
            Box box = new Box();

            Console.WriteLine("Генерируем список:");
            for (int i = 0; i < 30; i++)
            {
                list.Add(rand.Next(1, 50));
                Console.Write("{0}| ", list[i]);
            }

            Size = list.Max() * 2;
            Console.WriteLine("\nМаксимальный размер коробки: {0}.", Size);

            list.Sort();
            Console.WriteLine("\nСортируем список:");
            for (int i = 0; i < 30; i++)
            {
                Console.Write("{0}| ", list[i]);
            }

            while (true)
            {
                Console.WriteLine("\n");
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (box.things.Sum() + list[i] <= Size)
                    {
                        box.things.Add(list[i]);
                        Console.WriteLine("Положили в коробку: {0}.", list[i]);
                        list.RemoveAt(i);
                    }
                }

                if (box.things.Sum() == 0)
                    break;
                else
                {
                    Shelf.Add(box);
                    box = new Box();
                }

                if (list.Count == 0)
                    break;

                Console.WriteLine("Нет больше места.");
                Console.WriteLine("\nНовый список:");
                for (int i = 0; i < list.Count; i++)
                {
                    Console.Write("{0}| ", list[i]);
                }
            }

            Console.WriteLine("\nСортировка окончена.\nКонечный результат таков:\nКоличество ящиков: {0}.", Shelf.Count);
            Console.Write("\nНажмите Enter, чтобы увидеть содержимое каждого ящика.");
            Console.ReadLine();

            for (int j = 0; j < Shelf.Count; j++)
            {
                Thread.Sleep(1000);

                Console.Write("\nСодержимое ящика №{0}:\t|", j + 1);

                for (int i = 0; i < Shelf[j].things.Count; i++)
                {
                    Console.Write("{0}| ", Shelf[j].things[i]);
                }
            }

            Console.WriteLine("\n\nЗадание завершено.");

            NextLesson();
        }

    }

    class Box
    {
        public List<int> things = new List<int>();
    }

    class Trade
    {
        public int TradeId;
        public int Price;
        enum _direction
        {
            Buy, Sell
        }
        string _strategy;
        int _account;

        public void Info()
        {
            Console.WriteLine("Номер данного трейда {0}, по цене {1}", this.TradeId, this.Price);
        }
    }
}
