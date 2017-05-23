using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
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
    }
}
