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

        public int number, numberoftasks;
        public string description;
        public List<string> lessonsDescription;

        public virtual void Execution()
        {

        }

        public virtual void Navigate()
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
            Console.WriteLine("Вы ввели не число, повторите ввод.");
            Thread.Sleep(2000);
        }
    }
}
