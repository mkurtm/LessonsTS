using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main(string[] args)
        {
            Program lessons = new Program();
            lessons.LessonEnter();
        }

        public void FirstLesson()
        {
            #region Задание №1. Циклы.

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

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Это цикл for, {0}", i);
            }

            NextLesson();

            #endregion
        }

        public void SecondLesson()
        {
            #region Задание №2. Остаток от деления %.

            int number;

            for (int i = 0; i < 100; i++)
            {
                number = i % 2;
                if (number == 0)
                {
                    Console.WriteLine("Четное число: {0}", i);
                }
            }

            NextLesson();

            #endregion
        }

        public void ThirdLesson()
        {
            #region Задание №3. Числа Фибоначчи.

            int numFib;
            Console.WriteLine("Введите количество чисел Фибоначчи для вывода:");
            try
            {
                numFib = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                numFib = 1;
                WrongEnter();
            }

            Console.WriteLine("Решение с помощью всего трех переменных, без памяти.");
            int firstFib = 1, secondFib = 0, buffer;
            for (int i = 0; i < numFib; i++)
            {
                Console.WriteLine("{0} число Фибоначчи: {1}", i + 1, secondFib);
                buffer = firstFib + secondFib;
                firstFib = secondFib;
                secondFib = buffer;
            }

            Console.WriteLine("Решение с помощью списка значений, с памятью.");
            List<int> fibList = new List<int> { 0, 1 };
            for (int i = 0; i < numFib; i++)
            {
                Console.WriteLine("{0} число Фибоначчи: {1}", i + 1, fibList[i]);
                if (i != 0)
                {
                    fibList.Add(fibList[i] + fibList[i - 1]);
                }
            }

            NextLesson();

            #endregion
        }

        public void FourthLesson()
        {
            #region Задание №4. Найти сумму цифр положительного числа. 

            int numSum;
            Console.WriteLine("Введите целое положительное число и мы найдем сумму его цифр:");

            try
            {
                numSum = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                numSum = 1;
                WrongEnter();

            }

            int sum = 0, grade = 0, rest = 0;

            do
            {
                rest = numSum % 10;
                sum += rest;
                grade = numSum / 10;
                numSum = grade;
            } while (grade >= 1);

            Console.WriteLine("Сумма цифр равна: {0}", sum);

            NextLesson();

            #endregion
        }

        public void FifthLesson()
        {
            #region Задание №5. Простое ли число?

            int numSimple;
            Console.WriteLine("Введите целое положительное число и мы скажем простое ли оно:");

            try
            {
                numSimple = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                WrongEnter();
                numSimple = 1;
            }

            int rest, flagSimple = 0;
            for (int i = 1; i <= numSimple; i++)
            {
                rest = numSimple % i;
                if (rest == 0)
                {
                    flagSimple++;
                }
            }
            if (flagSimple == 2)
            {
                Console.WriteLine("Ваше число: {0} является простым.", numSimple);
            }
            else
            {
                Console.WriteLine("Ваше число: {0} НЕ является простым.", numSimple);
            }

            NextLesson();

            #endregion
        }

        public void NextLesson()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Конец урока, нажмите enter для перехода на следующий урок.");
            Console.ReadLine();
            Console.Clear();
            LessonEnter();
        }

        public void LessonEnter()
        {
            Console.Clear();
            Console.WriteLine("Инициализация программы. Выберите номер урока 1-5 и нажмите ENTER.");
            int LessonNumber;
            try
            {
                LessonNumber = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                LessonNumber = 10;
                WrongEnter();
            }

            switch (LessonNumber)
            {
                case 1:
                    FirstLesson();
                    break;
                case 2:
                    SecondLesson();
                    break;
                case 3:
                    ThirdLesson();
                    break;
                case 4:
                    FourthLesson();
                    break;
                case 5:
                    FifthLesson();
                    break;
                default:
                    LessonEnter();
                    break;
            }

        }

        private void WrongEnter()
        {
            Console.WriteLine("Вы ввели не число, повторите ввод.");
            Thread.Sleep(2000);
            LessonEnter();
        }
    }
}
