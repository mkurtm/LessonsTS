using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Day8 : Lesson
    {
 
        public Day8()
        {
            numberOfDay = 8;
            numberOfTasks = 1;
            description = "Работа с делегатами.";
            lessonsDescription = new List<string>
            {
                @"Задание №1. Создание класса, записывающего и воспроизводящего Функции."
            };

            NavigateTask();
        }

       
        public override void Task1()
        {
            ManagerActions mgr = new ManagerActions();
            Console.WriteLine("Создадим экземпляр класса и запишем в него 3 Функции.");

            mgr.addCommand(() => Console.WriteLine("Hello!!!"));
            mgr.addCommand(() => Console.WriteLine("Babe"));
            mgr.addCommand(() => Console.WriteLine("ThatsIt"));

            Thread.Sleep(1000);
            Console.WriteLine("\nУспешно.");
            Thread.Sleep(1000);
            Console.WriteLine("\nА теперь вызовем метод класса, активирующий записанные функции.");
            Thread.Sleep(1000);
            mgr.printAll();
            Thread.Sleep(1000);
            Console.WriteLine("\nУспешно.");
            Thread.Sleep(1000);
            Console.WriteLine("\nА теперь вызовем вторую записанную функцию.");
            Thread.Sleep(1000);
            mgr.commands[1]();
            Thread.Sleep(1000);
            Console.WriteLine("\nУспешно.");

            NextLesson();
        }     
    }

    public class ManagerActions
    {
        public List<Action> commands = new List<Action>();

        public void addCommand(Action action)
        {
            commands.Add(action);
        }

        public void printAll()
        {
            foreach (var item in commands)
            {
                item();
            }
        }

    }
}
