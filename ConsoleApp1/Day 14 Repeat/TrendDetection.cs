using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Realtime;
using TSLab.DataSource;
using TSLab.Script.Optimization;
using TSLab.Script.Helpers;

namespace Day_14_Repeat
{
    public class TrendDetection : IExternalScript
    {
        public OptimProperty smaPeriod = new OptimProperty(50, 10, 100, 10);
        public OptimProperty detectPeriod = new OptimProperty(300, 10, 100, 10);

        public void Execute(IContext ctx, ISecurity sec)
        {
            var sma = ctx.GetData("sma", new string[] { smaPeriod.ToString() },
                () => Series.SMA(sec.ClosePrices, smaPeriod));

            var crossCount = sec.ClosePrices.CrossCount(sma, detectPeriod);

            for (int i = smaPeriod; i < ctx.BarsCount; i++)
            {

            }



            #region Drawing

            if (ctx.IsOptimization)
            {
                return;
            }

            var pane = ctx.CreatePane("main", 100, false);
            var color = new TSLab.Script.Color(System.Drawing.Color.Black.ToArgb());
            var lst = pane.AddList(sec.ToString(), sec, CandleStyles.BAR_CANDLE, color, PaneSides.RIGHT);

            lst = pane.AddList(sma.ToString(), sma, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            pane = ctx.CreatePane("2", 25, false);
            lst = pane.AddList(crossCount.ToString(), crossCount, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            #endregion
        }
    }
}
