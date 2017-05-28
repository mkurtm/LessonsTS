using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.DataSource;
using TSLab.Script.Optimization;

namespace Day_10._1
{
    public class Timeframe : IExternalScript
    {
        public OptimProperty multi = new OptimProperty(1, 1, 10, 1); 


        public void Execute(IContext ctx, ISecurity sec)
        {
            var iBase = sec.IntervalBase;
            iBase = sec.IntervalInstance.Base;

            var iPeriod = sec.Interval;
            iPeriod = sec.IntervalInstance.Value;

            var iShift = sec.IntervalInstance.Shift;
            var msg = string.Format("Base = {0}, Period = {1}, Shift = {2}", iBase, iPeriod, iShift);
            ctx.LogInfo(msg);
            
            //сжатие
            ISecurity compressed;

            //compressed = sec.CompressTo(60);
            //compressed1 = sec.CompressTo(new Interval(60, DataIntervals.MINUTE));

            //if (sec.IntervalBase == DataIntervals.MINUTE && sec.Interval == 5)
            if (sec.IntervalInstance == new Interval(5, DataIntervals.MINUTE))
            {
                compressed = sec.CompressTo(new Interval(10, DataIntervals.MINUTE));
                var msg2 = string.Format("Base = {0}, Period = {1}, Shift = {2}", compressed.IntervalBase, compressed.Interval, compressed.IntervalInstance.Shift);
                ctx.LogInfo(msg2);
            }

            compressed = sec.CompressTo(new Interval(2 * sec.Interval, sec.IntervalBase));
            var msg3 = string.Format("Base = {0}, Period = {1}, Shift = {2}", compressed.IntervalBase, compressed.Interval, compressed.IntervalInstance.Shift);
            ctx.LogInfo(msg3);

            compressed = sec.CompressTo(new Interval(multi * sec.Interval, sec.IntervalBase));
            msg3 = string.Format("Base = {0}, Period = {1}, Shift = {2}", compressed.IntervalBase, compressed.Interval, compressed.IntervalInstance.Shift);
            ctx.LogInfo(msg3);


            var pane = ctx.CreatePane("main", 100, false);
            var color = new Color(System.Drawing.Color.Black.ToArgb());
            var lst = pane.AddList(sec.ToString(), sec, CandleStyles.BAR_CANDLE, color, PaneSides.RIGHT);

            var pane2 = ctx.CreatePane("main", 100, false);
            lst = pane2.AddList(compressed.ToString(), compressed, CandleStyles.BAR_CANDLE, color, PaneSides.RIGHT);

            var dec = compressed.Decompress(compressed.ClosePrices);
            lst = pane2.AddList("closeprices", dec, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
        }
    }
}


