using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;
using TSLab.Script.Optimization;

namespace My
{
    public class My : IExternalScript
    {

        #region Parameters

        public OptimProperty smaPeriodSmall = new OptimProperty(20, 10, 100, 5);
        public OptimProperty smaPeriodBig = new OptimProperty(220, 100, 500, 10);
        public OptimProperty atrPeriod = new OptimProperty(20, 10, 100, 5);
        public OptimProperty uCh6Delta = new OptimProperty(6, 0, 10, 0.1);
        public OptimProperty dCh6Delta = new OptimProperty(-6, -10, 0, 0.1);
        public OptimProperty uCh2Delta = new OptimProperty(2.33, 0, 10, 0.1);
        public OptimProperty dCh2Delta = new OptimProperty(-2.33, -10, 0, 0.1);
        public OptimProperty uCh1Delta = new OptimProperty(1.33, 0, 10, 0.1);
        public OptimProperty dCh1Delta = new OptimProperty(-1.33, -10, 0, 0.1);
        public OptimProperty uCh0Delta = new OptimProperty(0.33, 0, 10, 0.1);
        public OptimProperty dCh0Delta = new OptimProperty(-0.33, -10, 0, 0.1);

        #endregion


        public void Execute(IContext ctx, ISecurity sec)
        {
            #region Indicators

            var smaSmall = ctx.GetData("smaSmall", new string[] { },
                () => Series.SMA(sec.ClosePrices, smaPeriodSmall));
            var smaBig = ctx.GetData("smaBig", new string[] { },
                () => Series.SMA(sec.ClosePrices, smaPeriodBig));
            var atr = ctx.GetData("atr", new string[] { },
                () => Series.AverageTrueRange(sec.Bars, atrPeriod));
            var uCh6 = ctx.GetData("uch6", new string[] { },
                () => MyHelper.KeltnerChanel(smaSmall, atr, uCh6Delta));
            var dCh6 = ctx.GetData("dch6", new string[] { },
                () => MyHelper.KeltnerChanel(smaSmall, atr, dCh6Delta));
            var uCh2 = ctx.GetData("uch2", new string[] { },
                () => MyHelper.KeltnerChanel(smaSmall, atr, uCh2Delta));
            var dCh2 = ctx.GetData("dch2", new string[] { },
                () => MyHelper.KeltnerChanel(smaSmall, atr, dCh2Delta));
            var uCh1 = ctx.GetData("uch1", new string[] { },
                () => MyHelper.KeltnerChanel(smaSmall, atr, uCh1Delta));
            var dCh1 = ctx.GetData("dch1", new string[] { },
                () => MyHelper.KeltnerChanel(smaSmall, atr, dCh1Delta));
            var uCh0 = ctx.GetData("uch0", new string[] { },
                () => MyHelper.KeltnerChanel(smaSmall, atr, uCh0Delta));
            var dCh0 = ctx.GetData("dch0", new string[] { },
                () => MyHelper.KeltnerChanel(smaSmall, atr, dCh0Delta));

            #endregion
            





            #region Drawing

            if (ctx.IsOptimization)
            {
                return;
            }

            var pane = ctx.CreateGraphPane("main", "aaa", false);
            pane.HideLegend = true;
            var color = new Color(System.Drawing.Color.Green.ToArgb());
            var lst = pane.AddList(sec.ToString(), "sec", sec, CandleStyles.BAR_CANDLE, color, PaneSides.RIGHT);
            lst.AlternativeColor = System.Drawing.Color.Red.ToArgb();

            color = new Color(System.Drawing.Color.White.ToArgb());
            lst = pane.AddList(sec.ToString(), "smaSmall", smaSmall, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            
            color = new Color(System.Drawing.Color.CornflowerBlue.ToArgb());
            lst = pane.AddList(sec.ToString(), "smaBig", smaBig, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            lst.Thickness = 2;

            color = new Color(System.Drawing.Color.Red.ToArgb());
            lst = pane.AddList(sec.ToString(), "K", uCh6, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            lst = pane.AddList(sec.ToString(), "K", dCh6, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            color = new Color(System.Drawing.Color.BlueViolet.ToArgb());
            lst = pane.AddList(sec.ToString(), "K", uCh2, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            lst = pane.AddList(sec.ToString(), "K", dCh2, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            color = new Color(System.Drawing.Color.Blue.ToArgb());
            lst = pane.AddList(sec.ToString(), "K", uCh1, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            lst = pane.AddList(sec.ToString(), "K", dCh1, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            color = new Color(System.Drawing.Color.Gold.ToArgb());
            lst = pane.AddList(sec.ToString(), "K", uCh0, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            lst = pane.AddList(sec.ToString(), "K", dCh0, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            pane = ctx.CreateGraphPane("vol", "v",  false);
            pane.HideLegend = true;
            color = new Color(System.Drawing.Color.Red.ToArgb());
            lst = pane.AddList("vol", "11", sec.Volumes, ListStyles.HISTOHRAM, color, LineStyles.SOLID, PaneSides.RIGHT);

            pane = ctx.CreateGraphPane("atr", "atr", false);
            pane.HideLegend = true;
            color = new Color(System.Drawing.Color.Red.ToArgb());
            lst = pane.AddList("vol", "11", atr, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            #endregion
        }
    }
}
