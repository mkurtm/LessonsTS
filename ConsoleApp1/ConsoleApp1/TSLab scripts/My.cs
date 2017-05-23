using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;

namespace ConsoleApp1
{
    public class My : IExternalScript
    {
        public void Execute(IContext ctx, ISecurity sec)
        {
            double averageVolume = sec.Volumes.Average();
            

            var pane = ctx.CreatePane("main", 100, false);
            var color = new Color(System.Drawing.Color.Blue.ToArgb());
            var lst = pane.AddList(sec.ToString(), sec, CandleStyles.BAR_CANDLE, color, PaneSides.RIGHT);

            pane = ctx.CreatePane("volume", 10, false);            
            lst = pane.AddList(sec.ToString(), sec.Volumes, ListStyles.HISTOHRAM, color, LineStyles.SOLID, PaneSides.RIGHT);
            for (int i = 0; i < ctx.BarsCount; i++)
                if (sec.Volumes[i] > 5 * averageVolume)
                    lst.SetColor(i, new Color(System.Drawing.Color.Red.ToArgb()));


        }
        
    }
}
