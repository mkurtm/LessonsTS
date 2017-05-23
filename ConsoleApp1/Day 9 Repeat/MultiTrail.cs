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

namespace Day_9_Repeat
{
    public class MultiTrail : IExternalScript
    {
        public OptimProperty uchPeriod = new OptimProperty(20, 1, 100, 1);
        public OptimProperty dchPeriod = new OptimProperty(20, 1, 100, 1);


        public void Execute(IContext ctx, ISecurity sec)
        {
            //var highChanel = Series.Highest(sec.HighPrices, uchPeriod);
            //var lowChanel = Series.Lowest(sec.LowPrices, dchPeriod);

            var highChanel = ctx.GetData("highest", new string[] { uchPeriod.ToString() },
                                    () => Series.Highest(sec.HighPrices, uchPeriod));
            var lowChanel = ctx.GetData("lowest", new string[] { dchPeriod.ToString() },
                                         () => Series.Lowest(sec.LowPrices, dchPeriod));


            for (int i = uchPeriod > dchPeriod ? uchPeriod : dchPeriod; i < ctx.BarsCount; i++)
            {
                var le = sec.Positions.GetLastActiveForSignal("LE");
                var se = sec.Positions.GetLastActiveForSignal("SE");
                var currPos = sec.Positions.GetActiveForBar(i);

                if (le == null)
                    sec.Positions.BuyIfGreater(i + 1, 1, highChanel[i], 50, "LE");

                if (se == null)
                    sec.Positions.SellIfLess(i + 1, 1, lowChanel[i], 50, "SE");

                if (!currPos.Any())
                    continue;

                foreach (var item in currPos)
                {
                    switch (item.EntrySignalName)
                    {
                        case "LE":
                            {
                                var timeInPose = i - item.EntryBarNum + 1;
                                var y = timeInPose * timeInPose;
                                var stopLoss = item.EntryPrice - 500 + y * 1;
                                item.CloseAtStop(i + 1, stopLoss, "LSX");
                            }
                            break;
                        case "SE":
                            {
                                var timeInPose = i - item.EntryBarNum + 1;
                                var y = timeInPose * timeInPose;
                                var stopLoss = item.EntryPrice + 500 - y * 1;
                                item.CloseAtStop(i + 1, stopLoss, "SX");
                            }
                            break;
                        default:
                            break;
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
