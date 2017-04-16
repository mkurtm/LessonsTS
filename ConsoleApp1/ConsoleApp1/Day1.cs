using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Day1 : Lesson
    {
        public Day1()
        {
            // Класс Заданий первого дня
            
            // Инициализация параметров класса.
            number = 1;
            numberoftasks = 5;
            description = "День 1. Решенные задачи.";
            lessonsDescription = new List<string>
            {
                @"Задание №1. Циклы.",
                @"Задание №2. Остаток от деления %.",
                @"Задание №3. Числа Фибоначчи.",
                @"Задание №4. Найти сумму цифр положительного числа.",
                @"Задание №5. Простое ли число?"
            };


        }

        public override void Navigate()
        {
            base.Navigate();
        }
    }
}
