using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Day6 : Lesson
    {
 
        public Day6()
        {
            numberOfDay = 6;
            numberOfTasks = 2;
            description = "Работа с Интерфейсами.";
            lessonsDescription = new List<string>
            {
                @"Задание №1. Множественное наследование интерфейсов.",
                @"Задание №2. Создаем и работаем с классами от интерфейса."
            };

            NavigateTask();
        }

       
        public override void Task1()
        {
            StockCandle stockCandle = new StockCandle();
            FortsCandle fortsCandle = new FortsCandle();

            stockCandle = fortsCandle;

            if (stockCandle is FortsCandle)
            {
                Console.WriteLine("FortsCandle");
            }

            FortsCandle fortsCandle2 = stockCandle as FortsCandle;

            NextLesson();
        }

        public override void Task2()
        {
            string Name;
            Random rand = new Random();

            Name = "PointTest";
            Console.WriteLine("Создадим точку, с именем {0} и произвольными координатами.", Name);
            Point point = new Point(Name, rand.Next(1, 10), rand.Next(1, 10));
            Thread.Sleep(1000);
            Console.WriteLine("Мы создали точку с именем {0}.", point.Name);
            Console.WriteLine("Запрашиваем площадь: {0}, запрашиваем длинну: {1}.", point.Area(), point.Lenght());

            Name = "LineTest";
            Console.WriteLine("\nСоздадим точку, с именем {0} и произвольными координатами.", Name);
            Line line = new Line(Name, rand.Next(1, 10), rand.Next(1, 10), rand.Next(1, 10), rand.Next(1, 10));
            Thread.Sleep(1000);
            Console.WriteLine("Мы создали точку с именем {0}.", line.Name);
            Console.WriteLine("Запрашиваем площадь: {0:#.###}, запрашиваем длинну: {1:#.###}.", line.Area(), line.Lenght());

            Name = "CircleTest";
            Console.WriteLine("\nСоздадим точку, с именем {0} и произвольными координатами.", Name);
            Circle circle = new Circle(Name, rand.Next(1, 10), rand.Next(1, 10), rand.Next(1, 10));
            Thread.Sleep(1000);
            Console.WriteLine("Мы создали точку с именем {0}.", circle.Name);
            Console.WriteLine("Запрашиваем площадь: {0:#.###}, запрашиваем длинну: {1:#.###}.", circle.Area(), circle.Lenght());

            Name = "SquareTest";
            Console.WriteLine("\nСоздадим точку, с именем {0} и произвольными координатами.", Name);
            Square square = new Square(Name, rand.Next(1, 10), rand.Next(1, 10), rand.Next(1, 10), rand.Next(1, 10));
            Thread.Sleep(1000);
            Console.WriteLine("Мы создали точку с именем {0}.", square.Name);
            Console.WriteLine("Запрашиваем площадь: {0:#.###}, запрашиваем длинну: {1:#.###}.", square.Area(), square.Lenght());

            NextLesson();
        }
        
    }
}
