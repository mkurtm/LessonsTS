using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_8_Lessons_Scratch
{
    public class TwoStocks : IExternalScript2
    {
        public void Execute(IContext ctx, ISecurity sec1, ISecurity sec2)
        {
            var pane = ctx.CreatePane("Main", 100, false);
            var color = new Color(System.Drawing.Color.Black.ToArgb());

            //Первый вариант.
            //var spread = new double[ctx.BarsCount];
            //for (int i = 0; i < ctx.BarsCount; i++)
            //{
            //    spread[i] = sec1.Bars[i].Close - sec2.Bars[i].Close;
            //}
            //var lst = pane.AddList("spread", spread, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            //Второй вариант
            var spread = sec1.ClosePrices.Subtract(sec2.ClosePrices);
            var lst = pane.AddList("spread", spread, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
        }
    }
}
