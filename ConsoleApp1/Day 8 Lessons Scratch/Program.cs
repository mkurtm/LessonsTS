using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_8_Lessons_Scratch
{
    class Program
    {
        delegate double TestDeligate(double first, int second);

        static void Main()
        {
            TestDeligate d0 = new TestDeligate(Summ);
            TestDeligate d1 = Summ;


            Console.WriteLine("Результат делегата: {0}", d0(15.5, 5));
            Console.ReadKey();

            var d2 = new TestDeligate(Sub);
            Console.WriteLine("Результат делегата: {0}", d2(15.5, 5));

            SomeAction("Результат = ", 15.5, 5, Summ);
            Console.ReadKey();

            //Анонимные методы

            d0 = delegate (double f, int s) { return f / s; };
            Console.WriteLine("{0}", d0(15.5, 5));

            d0 = (f, s) => f / s;
            Console.WriteLine("{0}", d0(15.5, 5));
            Console.ReadKey();

            //пример лямбд и анонимных методов
            int x = 10;
            d0 = (first, second) => first * x / second;

            x = 0;
            Console.WriteLine("{0}", d0(15.5, 5));
            x = 5;
            Console.WriteLine("{0}", d0(15.5, 5));

            Action act = () => Console.WriteLine("ACT!");
            Action<int> act1= (i)=> Console.WriteLine("ACT! {0}", i);

            Func<int, int> act2 = (i) => i;
            Func<int, Double, bool> act3 = (i, d) => true;
        }

        static double Sub(double f, int s)
        {
            return f - s;
        }

        static double Summ(double f, int s)
        {
            return f + s;
        }

        static void SomeAction(string msg, double f, int s, TestDeligate func)
        {
            Console.WriteLine("{0} {1}", msg, func(f, s));
        }
    }
}
