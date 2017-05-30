using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;
using TSLab.Script.Optimization;
using TSLab.DataSource;

namespace Day_12
{
    [HandlerCategory("Marat")]
    [HandlerName("KeltnerChanelUp")]
    [HandlerDecimals(1)]
    public class KeltnerChanelUp : IBar2DoubleHandler
    {
        [HandlerParameter(Name = "Период", Default = "20")]
        public int Period { get; set; }

        [HandlerParameter(Name = "Коэффициент", Default = "1")]
        public double K { get; set; }

       
        public IList<double> Execute(ISecurity sec)
        {
            List<double> Chanel= new List<double>();

            var sma = Series.SMA(sec.ClosePrices, Period);
            var atr = Series.AverageTrueRange(sec.Bars, Period);

            for (int i = 0; i < sec.Bars.Count; i++)
            {
                Chanel.Add(sma[i]+atr[i]* K);
            }
            return Chanel;         
        }
    }
}
