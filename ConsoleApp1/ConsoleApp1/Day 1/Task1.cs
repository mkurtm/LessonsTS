using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Task1 : Lesson
    {
        public Task1()
        {
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

            for (whilecount = 0; whilecount < 10; whilecount++)
            {
                Console.WriteLine("Это цикл for, {0}", whilecount);
            }           

        }
    }
}
