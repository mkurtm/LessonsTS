using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;
using TSLab.Script.Optimization;

namespace Day_8_Lessons_Scratch
{
    public class Donchian : IExternalScript
    {
        public OptimProperty uCh = new OptimProperty(20, 1, 50, 1);
        public OptimProperty dCh = new OptimProperty(20, 1, 50, 1);

        public void Execute(IContext ctx, ISecurity sec)
        {

            //Через кубики
            //var uChHandler = new Highest() { Context = ctx, Period = uCh };
            //var uChanel = uChHandler.Execute(sec.HighPrices);
            //var dChHandler = new Lowest() { Context = ctx, Period = dCh };
            //var dChanel = dChHandler.Execute(sec.LowPrices);

            //через code
            var uChanel = Series.Highest(sec.HighPrices, uCh);
            var dChanel = Series.Lowest(sec.LowPrices, dCh);
            
            for (int i = uCh; i < ctx.BarsCount; i++)
            {
                var le = sec.Positions.GetLastActiveForSignal("LE");

                if (le == null)
                {
                    if (sec.Bars[i].Date.TimeOfDay <= new TimeSpan(23,40,00) && sec.Bars[i].Date.TimeOfDay >= new TimeSpan(10, 10, 00))
                        sec.Positions.BuyIfGreater(i + 1, 1, uChanel[i], "LE");
                }
                else
                    le.CloseAtStop(i + 1, dChanel[i], "LSX");
            }

            if (ctx.IsOptimization)
            {
                return;
            }

            var pane = ctx.CreatePane("main", 100, false);
            var color = new Color(System.Drawing.Color.Black.ToArgb());
            var lst = pane.AddList(sec.ToString(), sec, CandleStyles.BAR_CANDLE, color, PaneSides.RIGHT);

            for (int i = 0; i < ctx.BarsCount; i++)
            {
                var pose = sec.Positions.GetActiveForBar(i);
                if (pose.Any())
                   lst.SetColor(i,new Color(System.Drawing.Color.Red.ToArgb()));
            }
            
            lst = pane.AddList(uChanel.ToString(), uChanel, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            lst = pane.AddList(dChanel.ToString(), dChanel, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

        }

        
    }

}
            
 
