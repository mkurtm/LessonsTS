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
    public class AdaptiveSMATrailStop : IExternalScript
    {
        public OptimProperty chPeriod = new OptimProperty(20, 1, 100, 1);
        public OptimProperty StopSMAPeriod = new OptimProperty(20, 1, 100, 1);

        public void Execute(IContext ctx, ISecurity sec)
        {
            var highChanel = Series.Highest(sec.HighPrices, chPeriod);
            var stop = Series.SMA(sec.ClosePrices, StopSMAPeriod);

            for (int i = 21; i < ctx.BarsCount; i++)
            {
                var le = sec.Positions.GetLastActiveForSignal("LE");

                if (le == null)
                {
                    if (sec.ClosePrices[i] > stop[i])
                        sec.Positions.BuyIfGreater(i + 1, 1, highChanel[i], 50, "LE");
                }
                else
                {
                    var timeInPose = i - le.EntryBarNum + 1;
                    var smaPeriod = StopSMAPeriod - timeInPose;
                    var adoptSMA = ctx.GetData("sma", new string[] { smaPeriod.ToString() }, () => Series.SMA(sec.ClosePrices, smaPeriod));
                   // var adoptSMA = Series.SMA(sec.ClosePrices, smaPeriod);

                    var stopLoss = adoptSMA[i];
                    le.CloseAtStop(i + 1, stopLoss, "LSX");
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
            lst = pane.AddList(stop.ToString(), stop, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

        }
    }
}
