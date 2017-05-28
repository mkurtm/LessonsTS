using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;
using TSLab.Script.Optimization;

namespace Day_10._1
{
    public class SampleScript : IExternalScript
    {
        public OptimProperty chPeriod = new OptimProperty(20, 1, 100, 1);
        public OptimProperty stop = new OptimProperty(2, 0.25, 5, 0.25);
        public OptimProperty take = new OptimProperty(2, 0.25, 5, 0.25);

        public OptimProperty posAdd = new OptimProperty(1, 0.25, 5, 0.25);

        public void Execute(IContext ctx, ISecurity sec)
        {
            var highChanel = ctx.GetData("highest", new string[] { chPeriod.ToString() },
                                        () => Series.Highest(sec.HighPrices, chPeriod));
            var lowChanel = ctx.GetData("lowest", new string[] { chPeriod.ToString() },
                               () => Series.Lowest(sec.LowPrices, chPeriod));

            var comiss = new AbsolutCommission() { Commission = 10 };
            comiss.Execute(sec);



            for (int i = chPeriod + 1; i < ctx.BarsCount; i++)
            {
                var le = sec.Positions.GetLastActiveForSignal("LE");
                var leAdd = sec.Positions.GetLastActiveForSignal("LEadd");
                //Отобрали позы, начинающийся с LE
                var lePos = sec.Positions.GetActiveForBar(i).Where(p => p.EntrySignalName.StartsWith("LE")).ToList();


                if (le == null)
                {
                    sec.Positions.BuyIfGreater(i + 1, 1, highChanel[i], 50, "LE");
                }
                else
                {
                    //пробуем нарастить позу
                    if (leAdd == null)
                    {
                    var posAddPrice = le.EntryPrice * (1 + posAdd / 100.0);
                    sec.Positions.BuyIfGreater(i + 1, 1, posAddPrice, "LEadd");
                    }

                    var avgEntry = lePos.AvgEntryPrice();
                    var stopLoss = avgEntry * (1 - stop / 100.0);
                    var takeprofit = avgEntry * (1 + take / 100.0);

                    foreach (var p in lePos)
                    {
                        p.CloseAtStop(i + 1, stopLoss, "LXS");
                        p.CloseAtProfit(i + 1, takeprofit, "LXP");
                    }
                    
                }
            }



            if (ctx.IsOptimization)
            {
                return;
            }


            var pane = ctx.CreatePane("main", 100, false);
            var color = new TSLab.Script.Color(System.Drawing.Color.Black.ToArgb());
            var lst = pane.AddList(sec.ToString(), sec, CandleStyles.BAR_CANDLE, color, PaneSides.RIGHT);

            lst = pane.AddList(highChanel.ToString(), highChanel, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            lst = pane.AddList(lowChanel.ToString(), lowChanel, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

        }
    }
}
