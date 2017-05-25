using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_9._2_Repeat
{
    class Program
    {
        static void Main()
        {
            var ex = new Exception("Exception");
            var ex1 = new ArgumentException("wrong argument", "args");
            var ex2 = new ArgumentNullException("args", "arg is null");

            //обработка исключений

            List<double> list = new List<double>() { 1, 1, 1, 1 };
            var list1 = new List<double>() { 1, 1, 1 };
            
            try
            {
                list.Subtract(list1);
            }
            catch (Exception exc)
            {
                Console.WriteLine("HORRROR!!!, {0}", exc.Message);
                Console.ReadKey();
            }


            try
            {
                list.Subtract(list1);
            }
            catch (ArgumentException aAx)
            {
                Console.WriteLine("Argument!!!, {0}", aAx.Message);
                Console.ReadKey();
            }
            catch (Exception exc)
            {
                Console.WriteLine("HORRROR!!!, {0}", exc.Message);
                Console.ReadKey();
            }
            finally
            {

            }


            var flag0 = false;
            var flag1 = false;

            try
            {
                flag0 = true;
                list.Subtract(list1);
                flag1 = true;
            }
            catch (Exception exc)
            {
                Console.WriteLine("HORRROR!!!, {0}", exc.Message);
            }
            finally
            {
                Console.WriteLine("{0} | {1}", flag0, flag1);
                Console.ReadKey();
            }
            

            //throw ex1;

            
        }
    }
}
