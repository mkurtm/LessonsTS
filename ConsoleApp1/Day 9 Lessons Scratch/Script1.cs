using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;
using TSLab.Script.Optimization;

namespace Day_9_Lessons_Scratch
{
    public class Script1 : IExternalScript
    {
        public OptimProperty hChanelProperty = new OptimProperty(10, 1, 50, 1);
        public OptimProperty lChanelProperty = new OptimProperty(10, 1, 50, 1);

        public OptimProperty stopLoss = new OptimProperty(0.1, 0.1, 2, 0.1);
        public OptimProperty trailEnable = new OptimProperty(0.1, 0.1, 2, 0.1);
        public OptimProperty trailLoss = new OptimProperty(0.1, 0.1, 2, 0.1);

        public void Execute(IContext ctx, ISecurity sec)
        {
            var trailHandler = new TrailStop() { StopLoss = stopLoss, TrailEnable = trailEnable, TrailLoss = trailLoss };
            var commisHandler = new AbsolutCommission() { Commission = 20 };
            commisHandler.Execute(sec);

            var hChanel = ctx.GetData("highes", new string[] { hChanelProperty.ToString() },
                                () => Series.Highest(sec.HighPrices, hChanelProperty));
            var lChanel = ctx.GetData("lowest", new string[] { lChanelProperty.ToString() },
                                    () => Series.Lowest(sec.LowPrices, lChanelProperty));

            for (int i = hChanelProperty > lChanelProperty ? hChanelProperty : lChanelProperty; i < ctx.BarsCount; i++)
            {
                var le = sec.Positions.GetLastActiveForSignal("LE");

                if (sec.Bars[i].Date.TimeOfDay.IsFirstBar())
                    continue;

                if (le == null)
                {
                    if (sec.Bars[i].Close > hChanel[i - 1])
                    {
                        sec.Positions.BuyAtMarket(i + 1, 1, "LE");
                    }
                }

                else
                {
                    var trailStop = trailHandler.Execute(le, i + 1);
                    var stop = lChanel[i] < trailStop ? lChanel[i] : trailStop;
                    le.CloseAtStop(i + 1, stop, "LX");
                }

            }

            if (ctx.IsOptimization)
                return;

            var pane = ctx.CreatePane("main", 100, false);
            var color = new Color(System.Drawing.Color.Black.ToArgb());
            var lst = pane.AddList(sec.ToString(), sec, CandleStyles.BAR_CANDLE, color, PaneSides.RIGHT);

            lst = pane.AddList(hChanel.ToString(), hChanel, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            lst = pane.AddList(lChanel.ToString(), lChanel, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);



        }
    }
}
