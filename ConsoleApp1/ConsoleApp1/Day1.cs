using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Day1 : Lesson
    {
        public override void InitData()
        {
            numberOfDay = 1;
            numberOfTasks = 5;
            description = "Работа с простейшими циклами и операторами C#";
            lessonsDescription = new List<string>
            {
                @"Задание №1. Циклы.",
                @"Задание №2. Остаток от деления %.",
                @"Задание №3. Числа Фибоначчи.",
                @"Задание №4. Найти сумму цифр положительного числа.",
                @"Задание №5. Простое ли число?"
            };
            
            NavigateTask();
        }

        public override void NavigateTask()
        {
            WelcomeDay();
            int value;
            do
            {
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
            } while (value!=9);
        }

        void Task1()
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

            NextLesson();
        }

        void Task2()
        {
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
        }

        void Task3()
        {
            Console.WriteLine("Введите количество чисел Фибоначчи для вывода:");
            int value = GetValue();

            Console.WriteLine("Решение с помощью всего трех переменных, без памяти.");
            int firstFib = 1, secondFib = 0, buffer;
            for (int i = 0; i < value; i++)
            {
                Console.WriteLine("{0} число Фибоначчи: {1}", i + 1, secondFib);
                buffer = firstFib + secondFib;
                firstFib = secondFib;
                secondFib = buffer;
            }

            Console.WriteLine("Решение с помощью списка значений, с памятью.");
            List<int> fibList = new List<int> { 0, 1 };
            for (int i = 0; i < value; i++)
            {
                Console.WriteLine("{0} число Фибоначчи: {1}", i + 1, fibList[i]);
                if (i != 0)
                {
                    fibList.Add(fibList[i] + fibList[i - 1]);
                }
            }

            NextLesson();

        }

        void Task4()
        {
            Console.WriteLine("Введите целое положительное число и мы найдем сумму его цифр:");
            int value = GetValue();

            int sum = 0, grade = 0, rest = 0;

            do
            {
                rest = value % 10;
                sum += rest;
                grade = value / 10;
                value = grade;
            } while (grade >= 1);

            Console.WriteLine("Сумма цифр равна: {0}", sum);

            NextLesson();

        }

        void Task5()
        {
            Console.WriteLine("Введите целое положительное число и мы скажем простое ли оно:");
            int numSimple = GetValue();

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
        }
    }
}
