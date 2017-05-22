using System.Linq;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;
using TSLab.Script.Optimization;

namespace Day_8_Lessons_Scratch
{
    public class SmaLongOptimized : IExternalScript
    {
        public OptimProperty FastSmaPeriod = new OptimProperty(12, 1, 100, 5);
        public OptimProperty SlowSmaPeriod = new OptimProperty(80, 1, 100, 5);

        public void Execute(IContext ctx, ISecurity sec)
        {

            //Не провожу, если быстрая больше медленной
            if (FastSmaPeriod >= SlowSmaPeriod)
                return;

            //1. Использую кубики

            //var smaHandler = new SMA() { Context = ctx, Period = 20 };
            //var fastSma = smaHandler.Execute(sec.ClosePrices);
            //smaHandler.Period = 100;
            //var slowSma = smaHandler.Execute(sec.ClosePrices);

            //2. С помощью Series

            //var fastSma = Series.SMA(sec.ClosePrices, FastSmaPeriod);
            //var slowSma = Series.SMA(sec.ClosePrices, SlowSmaPeriod);


            //Использую кэш, вместо того, чтобы каждый раз рассчитывать
            var fastSma = ctx.GetData("SMA", new string[] { FastSmaPeriod.ToString() },
                               () => Series.SMA(sec.ClosePrices, FastSmaPeriod));
            var slowSma = ctx.GetData("SMA", new string[] { SlowSmaPeriod.ToString() },
                               () => Series.SMA(sec.ClosePrices, SlowSmaPeriod));


            //Trading

            for (int i = SlowSmaPeriod+1; i < ctx.BarsCount; i++)
            {
                var le = sec.Positions.GetLastActiveForSignal("LE");
                if (le == null)
                {
                    if (fastSma[i] > slowSma[i] && fastSma[i - 1] <= slowSma[i-1])
                    {
                        sec.Positions.BuyAtMarket(i + 1, 1, "LE");
                    }
                }
                else
                {
                    if (fastSma[i] < slowSma[i] && fastSma[i - 1] >= slowSma[i - 1])
                        le.CloseAtMarket(i + 1, "LX");
                }   
            }


            //Не выводим если в режиме оптимизации
            if (ctx.IsOptimization)
                return;

            //Вывод информации
            var pane = ctx.CreatePane(sec.ToString(), 100, false);
            var color = new Color(System.Drawing.Color.Lavender.ToArgb());
            var lst = pane.AddList(sec.ToString(), sec, CandleStyles.BAR_CANDLE, color, PaneSides.RIGHT);

            for (int i = 0; i < ctx.BarsCount; i++)
            {
                var pos = sec.Positions.GetActiveForBar(i);
                if (pos.Any())
                {
                    lst.SetColor(i, new Color(System.Drawing.Color.Red.ToArgb()));
                }            
            }
            
            color = new Color(System.Drawing.Color.Black.ToArgb());
            pane.AddList("sma", fastSma, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            color = new Color(System.Drawing.Color.Blue.ToArgb());
            pane.AddList("sma2", slowSma, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

        }
    }
}
