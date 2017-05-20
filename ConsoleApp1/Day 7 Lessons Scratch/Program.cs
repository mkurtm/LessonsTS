using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Day_7_Lessons_Scratch
{
    class Program
    {
        static void Main()
        {


            Console.ReadKey();

        }

        public void Task1()
        {
            Computer comp = new Computer();
            comp.Launch("WIN 10");

            Console.WriteLine("Создадим объект типа Singletone.\n");
            Thread.Sleep(1000);
            Console.WriteLine("На компьютере установлена ОС:{0}.", comp.OS.Name);
            Thread.Sleep(1000);
            Console.WriteLine("Попробуем изменить ОС.");
            Thread.Sleep(1000);
            comp.Launch("LINUX");
            Console.WriteLine("На компьютере установлена ОС:{0}.", comp.OS.Name);

        }

        class Computer
        {
            public OS OS { get; set; }

            public void Launch(string osname)
            {
                OS = OS.getInstance(osname);
            }
        }


        class OS
        {
            private static OS instance;

            public string Name { get; private set; }

            protected OS(string name)
            {
                this.Name = name;
            }

            public static OS getInstance(string osname)
            {
                if (instance == null)
                    instance = new OS(osname);
                return instance;
            }
        }
    }
}
