using System.Collections.Generic;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;
using ConsoleApp1;

namespace Day_12
{
    [HandlerCategory("Marat")]
    [HandlerName("KeltnerChanelDown")]
    [HandlerDecimals(1)]
    public class KeltnerChanelDown : IBar2DoubleHandler
    {
        [HandlerParameter(Name = "Период", Default = "20")]
        public int Period { get; set; }

        [HandlerParameter(Name = "Коэффициент", Default = "1")]
        public double K { get; set; }


        public IList<double> Execute(ISecurity sec)
        {
            List<double> Chanel = new List<double>();

            var sma = Series.SMA(sec.ClosePrices, Period);
            var atr = Series.AverageTrueRange(sec.Bars, Period);

            for (int i = 0; i < sec.Bars.Count; i++)
            {
                Chanel.Add(sma[i] - atr[i] * K);
            }
            return Chanel;
        }
    }
}
