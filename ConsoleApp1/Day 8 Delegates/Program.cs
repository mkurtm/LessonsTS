using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_8_Delegates
{
    class Program
    {
        static void Main()
        {
            #region Решение с помощью Списка Actions

            ManagerActions mgr = new ManagerActions();

            mgr.addCommand(() => Console.WriteLine("Hello!!!"));
            mgr.addCommand(() => Console.WriteLine("Babe"));
            mgr.addCommand(() => Console.WriteLine("ThatsIt"));

            mgr.printAll();

            mgr.commands[1]();

            #endregion

            Console.ReadKey();


        }
    }
}
