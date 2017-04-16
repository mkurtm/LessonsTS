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
        public int numberOfDay, numberOfTasks, numberOfDays = 1;
        public string description;
        public List<string> lessonsDescription = new List<string>();

        public Lesson()
        {
            InitData();  
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


        public virtual void InitData()
        {

        }

        public int NavigateDay()
        {
            Console.Clear();
            Console.WriteLine("Инициализация программы.\nВыберите номер учебного дня (от 1 до {0}) и нажмите ENTER.\nДля выхода из программы введите 9.", numberOfDays);
            int value = GetValue();
            return value;
        }

        public virtual void NavigateTask()
        {

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
    }
}
