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
    public class StandartTrailStop : IExternalScript
    {
        public OptimProperty chPeriod = new OptimProperty(20,1,100,1);
        public OptimProperty StopLoss = new OptimProperty(2, 0, 5, 0.1);
        public OptimProperty TrailEnable = new OptimProperty(1.5, 0, 5, 0.1);
        public OptimProperty TrailLoss = new OptimProperty(1, 0, 5, 0.1);


        public void Execute(IContext ctx, ISecurity sec)
        {
            var highChanel = Series.Highest(sec.HighPrices, chPeriod);
            var trailHandler = new TrailStop() { StopLoss = StopLoss, TrailEnable = TrailEnable, TrailLoss = TrailEnable };
   
            for (int i = 21; i < ctx.BarsCount; i++)
            {
                var le = sec.Positions.GetLastActiveForSignal("LE");

                if (le == null)
                {
                    sec.Positions.BuyIfGreater(i + 1, 1, highChanel[i], 50, "LE");
                }
                else
                {
                    var stop = trailHandler.Execute(le, i);
                    le.CloseAtStop(i + 1, stop, "LSX");
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

        }
    }
}
