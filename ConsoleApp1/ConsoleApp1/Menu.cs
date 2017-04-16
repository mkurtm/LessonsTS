using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Menu
    {
        /* Этот класс отвечает за комуникацию с пользователем,
        обработку ввода и в качестве диспетчера вызываемых уроков */

        public void Navigate()
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
