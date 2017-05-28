using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_10_Homework
{ 
    public static class MyTradeHelper
    {
        public static double Average (this IList<double> volumes)
        {
            double average=0;

            for (int i = 0; i < volumes.Count; i++)
            {
                average = (average + volumes[i]);
            }

            average = average / (volumes.Count+1);

            return average;
        }

        public static bool IsFirstBar(this TimeSpan TimeOfDay)
        {
            if (TimeOfDay > new TimeSpan(23, 40, 00) || TimeOfDay < new TimeSpan(10, 05, 00))
                return true;
            else
                return false;

        }
    }
}
