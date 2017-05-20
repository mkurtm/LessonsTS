using System.Linq;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;

namespace Day_8_Lessons_Scratch
{
    public class SmaLong : IExternalScript
    {
        public void Execute(IContext ctx, ISecurity sec)
        {
            //1. Использую кубики

            //var smaHandler = new SMA() { Context = ctx, Period = 20 };
            //var fastSma = smaHandler.Execute(sec.ClosePrices);
            //smaHandler.Period = 100;
            //var slowSma = smaHandler.Execute(sec.ClosePrices);

            //2. С помощью Series

            var fastSma = Series.SMA(sec.ClosePrices, 20);
            var slowSma = Series.SMA(sec.ClosePrices, 100);

            //Trading

            for (int i = 101; i < ctx.BarsCount; i++)
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
