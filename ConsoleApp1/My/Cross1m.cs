using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.DataSource;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;
using TSLab.Script.Optimization;
using My;

namespace My
{
    public class Cross1m : IExternalScript
    {

        #region Parameters

        public OptimProperty smaPeriodSmall = new OptimProperty(20, 10, 100, 5);
        public OptimProperty smaPeriodBig = new OptimProperty(100, 100, 500, 10);

        public OptimProperty atrPeriod = new OptimProperty(20, 10, 100, 5);

        public OptimProperty uCh6Delta = new OptimProperty(6, 0, 10, 0.1);
        public OptimProperty uCh2Delta = new OptimProperty(2.33, 0, 10, 0.1);
        public OptimProperty uCh1Delta = new OptimProperty(1, 0.0, 10.0, 0.1);
        public OptimProperty uCh0Delta = new OptimProperty(0.25, 0, 10, 0.1);

        public OptimProperty dCh6Delta = new OptimProperty(-6, -10, 0, 0.1);
        public OptimProperty dCh2Delta = new OptimProperty(-2.33, -10, 0, 0.1);
        public OptimProperty dCh1Delta = new OptimProperty(-1.2, -10, 0, 0.1);
        public OptimProperty dCh0Delta = new OptimProperty(-0.15, -10, 0, 0.1);

        public OptimProperty takePcntShort = new OptimProperty(0.5, 0, 30, 0.25);
        public OptimProperty takePcntLong = new OptimProperty(0.5, 0, 30, 0.25);

        #endregion

        public void Execute(IContext ctx, ISecurity sec)
        {
            #region Indicators            

            var smaSmall = Series.SMA(sec.ClosePrices, smaPeriodSmall);
            var smaBig = Series.SMA(sec.ClosePrices, smaPeriodBig);
            var atr = Series.AverageTrueRange(sec.Bars, atrPeriod);
            var uCh6 = smaSmall.KeltnerChanel(atr, uCh6Delta);
            var uCh2 = smaSmall.KeltnerChanel(atr, uCh2Delta);
            var uCh1 = smaSmall.KeltnerChanel(atr, uCh1Delta);
            var uCh0 = smaSmall.KeltnerChanel(atr, uCh0Delta);
            var dCh6 = smaSmall.KeltnerChanel(atr, dCh6Delta);
            var dCh2 = smaSmall.KeltnerChanel(atr, dCh2Delta);
            var dCh1 = smaSmall.KeltnerChanel(atr, dCh1Delta);
            var dCh0 = smaSmall.KeltnerChanel(atr, dCh0Delta);

            #endregion

            #region Checks & Setups

            //Выдаем ошибку если не базовый таймфрейм
            if (sec.IntervalInstance != new Interval(1, DataIntervals.MINUTE))
                throw new ArgumentException("Работаем только на 1 мин.");

            //Stop
            var stopShort = 0.0;
            var stopLong = 0.0;

            #endregion

            #region Trading

            for (int i = 1; i < ctx.BarsCount; i++)
            {

                //Позиции

                var se = sec.Positions.GetLastActiveForSignal("SE");
                var le = sec.Positions.GetLastActiveForSignal("LE");

                //Торговля   

                if (se == null)

                {
                    if (sec.isCrossSmaDown(smaSmall, i))
                    {
                        sec.Positions.SellAtMarket(i + 1, 1, "SE", null);
                        //sec.Positions.SellAtPrice(i + 1, 1, sec.Bars[i].Close - 10, "SE", null);
                        stopShort = uCh0[i];
                        // stopShort = uCh1[i];
                        // stopShort = smaSmall[i];
                    }
                }

                else
                {
                    se.CloseAtStop(i + 1, uCh1[i] > se.EntryPrice ? uCh1[i] : stopShort, "SX");
                    se.CloseAtStop(i + 1, stopShort, "SX");
                    se.CloseAtProfit(i + 1, (se.EntryPrice * (1 - (takePcntShort / 100.0))), "SP");

                    if (i - se.EntryBarNum >= 20)
                    {
                        se.CloseAtMarket(i + 1, "STX", "asdsad");
                    }
                }

                if (le == null)
                {
                    if (sec.isCrossSmaUp(smaSmall, i))
                    {
                        sec.Positions.BuyAtMarket(i + 1, 1, "LE", null);
                        //sec.Positions.BuyAtPrice(i + 1, 1, sec.Bars[i].Close + 10, "LE", null);
                        stopLong = dCh0[i];
                    }
                }

                else
                {
                    le.CloseAtStop(i + 1, dCh1[i] < le.EntryPrice ? dCh1[i] : stopLong, "LX");
                    //le.CloseAtStop(i + 1, stopLong, "LX");
                    le.CloseAtProfit(i + 1, le.EntryPrice * (1 + (takePcntLong / 100.0)), "LP");

                    if (i - le.EntryBarNum >= 20)
                    {
                        le.CloseAtMarket(i + 1, "LTX", "asdsad");
                    }
                }
            }

            #endregion

            #region Drawing

            if (ctx.IsOptimization)
            {
                return;
            }

            var pane = ctx.CreateGraphPane("main", "aaa", false);
            pane.HideLegend = false;
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

            //pane = ctx.CreateGraphPane("vol", "v", false);
            //pane.HideLegend = true;
            //color = new Color(System.Drawing.Color.Red.ToArgb());
            //lst = pane.AddList("vol", "11", sec.Volumes, ListStyles.HISTOHRAM, color, LineStyles.SOLID, PaneSides.RIGHT);

            //pane = ctx.CreateGraphPane("atr", "atr", false);
            //pane.HideLegend = true;
            //color = new Color(System.Drawing.Color.Red.ToArgb());
            //lst = pane.AddList("vol", "11", atr, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            //pane = ctx.CreateGraphPane("atr", "atr", false);
            //pane.HideLegend = true;
            //color = new Color(System.Drawing.Color.Red.ToArgb());
            //lst = pane.AddList("vol", "11", flatBarsCount, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            #endregion
        }
    }
}
