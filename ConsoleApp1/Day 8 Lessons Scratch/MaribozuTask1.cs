using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_8_Lessons_Scratch
{
    public class MaribozuTask1 : IExternalScript

    {
        public void Execute(IContext ctx, ISecurity sec)
        {
            for (int i = 0; i < ctx.BarsCount; i++)
            {
                var le = sec.Positions.GetLastActiveForSignal("LE");

                if (le == null)
                {
                    if (sec.Bars[i].IsMaribozuWhite())
                    {
                        sec.Positions.BuyAtMarket(i + 1, 1, "LE");
                    }
                }
                else
                    if (sec.Bars[i].IsMaribozuBlack())
                    le.CloseAtMarket(i + 1, "LSX");
            }

            var pane = ctx.CreatePane("main", 100, false);
            var color = new Color(System.Drawing.Color.Black.ToArgb());
            var lst = pane.AddList(sec.ToString(), sec, CandleStyles.BAR_CANDLE, color, PaneSides.RIGHT);
        }
    }
}
