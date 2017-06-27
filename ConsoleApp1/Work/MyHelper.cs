using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace Work
{
    public static class MyHelper
    {
        public static IList<double> KeltnerChanel(this IList<double> list0, IList<double> list1, double delta)
        {
            if (list0.Count != list1.Count)
                throw new ArgumentException("Списки должны быть одинаковой длины.");

            var result = new List<double>();
            for (int i = 0; i < list1.Count; i++)
            {
                result.Add(list0[i] + (delta * list1[i]));
            }
            return result;
        }

        
    }
}
