using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_13_Repeat
{
    [HandlerCategory("Marat")]
    [HandlerName("OICandles")]    
    public class OICandles : IOneSourceHandler, IStreamHandler, ISecurityInputs, ISecurityReturns
    {
        public ISecurity Execute(ISecurity sec)
        {
            var OIBars = new Bar[sec.Bars.Count];

            OIBars[0] = new Bar(sec.Bars[0].Color, sec.Bars[0].Date, sec.Bars[0].Interest, sec.Bars[0].Interest, sec.Bars[0].Interest, sec.Bars[0].Interest,0);
            for (int i = 1; i < sec.Bars.Count; i++)
            {
                var color = sec.Bars[i].Color;
                var date = sec.Bars[i].Date;
                var open = sec.Bars[i-1].Interest;
                var close = sec.Bars[i].Interest;
                var high = Math.Max(open, close);
                var low = Math.Min(open, close);
                var volume = high - low;

                var bar = new Bar(color, date, open, high, low, close, volume);
                OIBars[i] = bar;
            }

            var oisec = sec.CloneAndReplaceBars(OIBars);
            return oisec;
        }
    }
}
