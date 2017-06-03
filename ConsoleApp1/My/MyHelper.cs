using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace My
{
    public static class MyHelper
    {
        public static IList<double> KeltnerChanel(IList<double> list0, IList<double> list1, double delta)
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

        public static bool isUnderMT (this ISecurity sec, IList<double> smaBig, int barNum)
        {
            if (sec.Bars[barNum].Close < smaBig[barNum] && sec.Bars[barNum].Open < smaBig[barNum])
                return true;
            else return false;
        }

        public static void LogInfo(this IContext ctx, string str, params object[] args)
        {
            // Задаем цвет соообщения
            var color = System.Drawing.Color.Blue;

            // Формируем строку, вставляем спец слово Info.
            var msg = string.Format("Info: " + str, args);

            ctx.Log (msg, new Color(color.ToArgb()));
        }
    }
}
