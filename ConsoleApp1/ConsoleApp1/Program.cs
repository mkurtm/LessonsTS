﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main()
        {
            int day;
            Lesson l = new Lesson();            
            do
            {
                day = l.NavigateDay();
                switch (day)
                {
                    case 1:
                        l = new Day1();
                        break;
                    case 2:
                        l = new Day2();
                        break;
                    case 3:
                        l = new Day3();
                        break;
                    case 4:
                        l = new Day4();
                        break;
                    case 5:
                        l = new Day5();
                        break;
                    case 6:
                        l = new Day6();
                        break;
                    case 7:
                        l = new Day7();
                        break;
                    case 8:
                        l = new Day8();
                        break;
                    case 9:
                        break;
                    case 0:
                        break;
                    default:
                        l.WrongEnter();                        
                        break;
                }
            } while (day!=9);

        }
    }
}
