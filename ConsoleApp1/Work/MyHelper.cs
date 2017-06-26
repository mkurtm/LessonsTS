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

        public static IList<double> flatBarsCount(this ISecurity sec, IList<double> uCh1, IList<double> uCh2, IList<double> dCh1, IList<double> dCh2)
        {
            var result = new List<double>();
            result.Add(0);

            for (int i = 1; i < uCh1.Count; i++)
            {
                if (sec.Bars[i].Close <= uCh2[i] && sec.Bars[i].Close >= dCh2[i])
                    result.Add(result[i - 1] + 1);
                else
                    result.Add(0);
            }
            return result;            
        }

        public static bool isInTimeSpan(this TimeSpan time, TimeSpan timeMin, TimeSpan timeMax)
        {
            return time >= timeMin && time <= timeMax;
        }

        public static IList<bool> isInTimeSpanAll(this ISecurity sec, TimeSpan timeMin, TimeSpan timeMax)
        {
            var list = new List<bool>();
            for (int i = 0; i < sec.Bars.Count; i++)
            {
                if (sec.Bars[i].Date.TimeOfDay >= timeMin && sec.Bars[i].Date.TimeOfDay <= timeMax)
                    list.Add(true);
                else
                    list.Add(false);
            }
            return list;            
        }

        public static bool isInTimeDouble(this double time, double timeMin, double timeMax)
        {
            return time >= timeMin && time <= timeMax;
        }

        public static bool isUnderSMA(this ISecurity sec, IList<double> smaBig, int barNum)
        {
            if (sec.Bars[barNum].High < smaBig[barNum])
                return true;
            else return false;
        }

        public static bool isAboveSMA(this ISecurity sec, IList<double> smaBig, int barNum)
        {
            if (sec.Bars[barNum].Low > smaBig[barNum])
                return true;
            else return false;
        }

        public static bool isUnderSMADelta(this ISecurity sec, IList<double> smaBig, double delta, int barNum)
        {
            if (sec.Bars[barNum].High < (smaBig[barNum] * (1 + (delta / 100.0))))
                return true;
            else return false;
        }

        public static bool isAboveSMADelta(this ISecurity sec, IList<double> smaBig, double delta, int barNum)
        {
            if (sec.Bars[barNum].Low > (smaBig[barNum] * (1 + (delta / 100.0))))
                return true;
            else return false;
        }

        public static IList<bool> isAboveSMADeltaAll(this ISecurity sec, IList<double> smaBig, double delta)
        {
            var list = new List<bool>();
            for (int i = 0; i < sec.Bars.Count; i++)
            {
                if (sec.Bars[i].Low > (smaBig[i] * (1 + (delta / 100.0))))
                    list.Add(true);
                else
                    list.Add(false);
            }
            return list ;
        }

        public static IList<bool> isUnderSMADeltaAll(this ISecurity sec, IList<double> smaBig, double delta)
        {
            var list = new List<bool>();
            for (int i = 0; i < sec.Bars.Count; i++)
            {
                if (sec.Bars[i].High < (smaBig[i] * (1 + (delta / 100.0))))
                    list.Add(true);
                else
                    list.Add(false);
            }
            return list;            
        }

        public static bool isNearSMA(this ISecurity sec, IList<double> smaBig, double deltaPcnt, int barNum)
        {
            if ((Math.Abs(sec.Bars[barNum].Close - smaBig[barNum]) / smaBig[barNum]) * 100 <= deltaPcnt)
                return true;
            else
                return false;
        }

        public static IList<bool> isNearSMAAll(this ISecurity sec, IList<double> smaBig, double deltaPcnt)
        {
            var list = new List<bool>();
            for (int i = 0; i < sec.Bars.Count; i++)
            {
                if ((Math.Abs(sec.Bars[i].Close - smaBig[i]) / smaBig[i]) * 100 <= deltaPcnt)
                    list.Add(true);
                else
                    list.Add(false);
            }
            return list;            
        }
        
        public static bool isCrossSmaUp(this ISecurity sec, IList<double> sma, int barNum)
        {
            if (barNum != 0)
                return sec.Bars[barNum].Close > sma[barNum] && sec.Bars[barNum - 1].Close < sma[barNum - 1];
            return false;

        }

        public static bool isCrossSmaDown(this ISecurity sec, IList<double> sma, int barNum)
        {
            if (barNum != 0)
                return sec.Bars[barNum].Close < sma[barNum] && sec.Bars[barNum - 1].Close > sma[barNum - 1];
            return false;

        }

        public static List<double> barsAboveSma(this ISecurity sec, IList<double> sma)
        {
            var list = new List<double>();
            list.Add(0);
            var barsAbove = 0;
            for (int i = 1; i < sec.Bars.Count; i++)
            {
                if (sec.isAboveSMA(sma, i))
                    barsAbove++;
                else
                    barsAbove = 0;
                list.Add(barsAbove);
            }
            return list;
        }

        public static IList<bool> isCrossSmaAll(this ISecurity sec, IList<double> sma)
        {
            var list = new List<bool>();
            for (int i = 0; i < sec.Bars.Count; i++)
            {
                list.Add(sec.isCrossSmaUp(sma, i));
            }
            return list;
        }

        public static IList<bool> IsCrossSmaBarsAgoAll(this ISecurity sec, IList<double> sma, int barsAgo)
        {
            var listIn = sec.isCrossSmaAll(sma);
            var listOut = new List<bool>();

            for (int i = 0; i < sec.Bars.Count; i++)
            {
                var isTrue = false;

                if (i - barsAgo >= 0)
                {
                    for (int j = i - barsAgo; j < i; j++)
                    {
                        if (listIn[j])
                            isTrue = true;
                    }
                }

                listOut.Add(isTrue);
            }
            return listOut;
        }

        public static bool isCrossSmaBarsAgo(this ISecurity sec, IList<double> sma, int barNum, int barsAgo)
        {
            var isCrossSmaBarsAgo = false;
            for (int i = barNum - barsAgo; i < barNum; i++)
            {
                if (sec.isCrossSmaUp(sma, i))
                    isCrossSmaBarsAgo = true;
            }
            return isCrossSmaBarsAgo;
        }

        public static void LogInfo(this IContext ctx, string str, params object[] args)
        {
            // Задаем цвет соообщения
            var color = System.Drawing.Color.Blue;

            // Формируем строку, вставляем спец слово Info.
            var msg = string.Format("Info: " + str, args);

            //ctx.Log(msg, new Color(color.ToArgb()));
            ctx.Log(msg, MessageType.Warning, false, null);
            
        }
    }
}
