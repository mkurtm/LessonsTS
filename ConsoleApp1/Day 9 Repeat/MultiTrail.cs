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
        public OptimProperty StopLoss = new OptimProperty(1, 0.1, 5, 0.1);
        public OptimProperty TrailEnable = new OptimProperty(1, 0.1, 5, 0.1);
        public OptimProperty TrailLoss = new OptimProperty(1, 0.1, 5, 0.1);
        public OptimProperty parSpreadLE = new OptimProperty(500, 50, 1000, 10);
        public OptimProperty parSpreadSE = new OptimProperty(500, 50, 1000, 10);

        public void Execute(IContext ctx, ISecurity sec)
        {
            //var highChanel = Series.Highest(sec.HighPrices, uchPeriod);
            //var lowChanel = Series.Lowest(sec.LowPrices, dchPeriod);

            var highChanel = ctx.GetData("highest", new string[] { uchPeriod.ToString() },
                                    () => Series.Highest(sec.HighPrices, uchPeriod));
            var lowChanel = ctx.GetData("lowest", new string[] { dchPeriod.ToString() },
                                         () => Series.Lowest(sec.LowPrices, dchPeriod));

            var trailHnd = new TrailStop() { StopLoss = StopLoss, TrailEnable = TrailEnable, TrailLoss = TrailLoss };
            
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
                                var stopLossParabolicLE = item.EntryPrice - parSpreadLE + y * 1;
                                var stopLossTrailLE = trailHnd.Execute(le, i);
                                if (stopLossParabolicLE > stopLossTrailLE)
                                    item.CloseAtStop(i + 1, stopLossParabolicLE, "LXpar");
                                else
                                    item.CloseAtStop(i + 1, stopLossTrailLE, "LXtrail");
                            }
                            break;
                        case "SE":
                            {
                                var timeInPose = i - item.EntryBarNum + 1;
                                var y = timeInPose * timeInPose;
                                var stopLossParabolicSE = item.EntryPrice + parSpreadSE - y * 1;
                                var stopLossTrailSE = trailHnd.Execute(se, i);

                                if (stopLossParabolicSE < stopLossTrailSE)
                                    item.CloseAtStop(i + 1, stopLossParabolicSE, "SXpar");
                                else
                                    item.CloseAtStop(i + 1, stopLossTrailSE, "SXtrail");
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
