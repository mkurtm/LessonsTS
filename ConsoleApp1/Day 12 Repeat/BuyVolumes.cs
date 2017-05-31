using System.Collections.Generic;
using System.Linq;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.DataSource;

namespace Day_12_Repeat
{
    [HandlerCategory("Marat")]
    [HandlerName("Объемы на покупку")]
    [HandlerDecimals(3)]
    public class BuyVolumes : IBar2DoubleHandler
    {
        public IList<double> Execute(ISecurity sec)
        {
            var cnt = sec.Bars.Count;
            var value = new double[cnt];

            for (int i = 0; i < cnt; i++)
            {
                var trades = sec.GetTrades(i);
                value[i] = trades.Sum(p => p.Direction == TradeDirection.Buy ? p.Quantity : 0);
            }
            return value;
        }
    }
}
