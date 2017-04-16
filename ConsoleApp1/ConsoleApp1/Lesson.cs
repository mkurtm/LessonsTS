using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Lesson
    {
        /* Базовый класс для всех уроков */
        public int numberOfDay, numberOfTasks, numberOfDays = 2;
        public string description;
        public List<string> lessonsDescription = new List<string>();

        public Lesson()
        {
            
        }

        public void WelcomeDay()
        {
            Console.Clear();
            Console.WriteLine("Добро пожаловать в День №{0}.\nТема: {1}.\nСегодня было решено {2} задач:", numberOfDay, description, numberOfTasks);
            foreach (string item in lessonsDescription)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            Thread.Sleep(1000);
            
        }
        
        public int NavigateDay()
        {
            Console.Clear();
            Console.WriteLine("Инициализация программы.\nВыберите номер учебного дня (от 1 до {0}) и нажмите ENTER.\nДля выхода из программы введите 9.", numberOfDays);
            int value = GetValue();
            return value;
        }

        public int GetValue()
        {
            int value = 0;
            try
            {
                value = Convert.ToInt32(Console.ReadLine());
                return value;
            }
            catch (Exception)
            {
                WrongEnter();
                return value;
            }
        }

        public void NextLesson()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Конец урока, нажмите enter для возврата в меню.");
            Console.ReadLine();
            Console.Clear();
        }

        public void WrongEnter()
        {
            Console.WriteLine("Вы ввели не число или не попали в диапазон, повторите ввод.");
            Thread.Sleep(2000);
        }

        public virtual void NavigateTask()
        {
            int value;
            do
            {
                WelcomeDay();
                Console.WriteLine("Выберите номер Задания (от 1 до {0}) и нажмите ENTER.\nДля возврата на выбор Дня введите 9.", numberOfTasks);
                value = GetValue();
                switch (value)
                {
                    case 1:
                        Task1();
                        break;
                    case 2:
                        Task2();
                        break;
                    case 3:
                        Task3();
                        break;
                    case 4:
                        Task4();
                        break;
                    case 5:
                        Task5();
                        break;
                    case 9:
                        break;
                    case 0:
                        break;
                    default:
                        WrongEnter();
                        break;
                }
            } while (value != 9);
        }

        public virtual void Task1()
        {
            
        }

        public virtual void Task2()
        {
            
        }

        public virtual void Task3()
        {
           

        }

        public virtual void Task4()
        {
            

        }

        public virtual void Task5()
        {
           
        }
    }
}
