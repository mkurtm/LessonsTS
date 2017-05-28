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
using TSLab.DataSource;

namespace Day_11
{
    public class SampleScript : IExternalScript
    {
        #region Params

        public OptimProperty chPeriod = new OptimProperty(20, 1, 100, 1);
        public OptimProperty stop = new OptimProperty(2, 0.25, 5, 0.25);
        public OptimProperty take = new OptimProperty(2, 0.25, 5, 0.25);

        public OptimProperty posAdd = new OptimProperty(1, 0.25, 5, 0.25);
        public OptimProperty stopMove = new OptimProperty(0.75, 0.25, 5, 0.25);

        #endregion

        public void Execute(IContext ctx, ISecurity sec)
        {
            #region Init indicators and Setups

            //Проверка правильности базового таймфрейма
            if (sec.IntervalInstance != new Interval(5, DataIntervals.MINUTE))
                throw new InvalidOperationException("Работаем только на 5 минутках!");

            var highChanel = ctx.GetData("highest", new string[] { chPeriod.ToString() },
                                        () => Series.Highest(sec.HighPrices, chPeriod));
            var lowChanel = ctx.GetData("lowest", new string[] { chPeriod.ToString() },
                               () => Series.Lowest(sec.LowPrices, chPeriod));
            var daySec = sec.CompressTo(new Interval(1, DataIntervals.DAYS));


            //Set commiss
            var comiss = new AbsolutCommission() { Commission = 10 };
            comiss.Execute(sec);

            //Не используем незакрытый бар для пересчета.
            var count = ctx.BarsCount;
            if (!ctx.IsLastBarClosed)
                count--;

            //Торговать с бара минимального индикатора или с настройки
            var start = Math.Max(ctx.TradeFromBar, chPeriod + 1);

            #endregion

            #region Trading
            
            bool stopMoveFlag = false;

            for (int i = start; i <count; i++)
            {
                var le = sec.Positions.GetLastActiveForSignal("LE");
                var leAdd = sec.Positions.GetLastActiveForSignal("LEadd");
                var leD = sec.Positions.GetLastActiveForSignal("LEd");
                //Отобрали позы, начинающийся с LE
                var lePos = sec.Positions.GetActiveForBar(i).Where(p => p.EntrySignalName.StartsWith("LE")).ToList();

                if (le == null)
                {
                    sec.Positions.BuyIfGreater(i + 1, 1, highChanel[i], 50, "LE");
                    stopMoveFlag = false;
                }
                else
                {
                    //пробуем нарастить позу
                    if (leAdd == null)
                    {
                        var posAddPrice = le.EntryPrice * (1 + posAdd / 100.0);
                        sec.Positions.BuyIfGreater(i + 1, 1, posAddPrice, "LEadd");
                    }

                    var avgEntry = lePos.AvgEntryPrice();

                    //take
                    var takeprofit = avgEntry * (1 + take / 100.0);
                    lePos.ForEach(p => p.CloseAtProfit(i + 1, takeprofit, "LXP"));

                    //stop
                    var stopMovePrice = avgEntry * (1 + stopMove / 100.0);
                    if (sec.Bars[i].High > stopMovePrice || stopMoveFlag == true)
                    {
                        stopMoveFlag = true;
                        var stoploss = avgEntry;
                        lePos.ForEach(p => p.CloseAtStop(i + 1, stoploss, "LXSmoved"));
                    }
                    else
                    {
                        var stopLoss = avgEntry * (1 - stop / 100.0);
                        lePos.ForEach(p=> p.CloseAtStop(i + 1, stopLoss, "LXS"));
                    }
                }
            }

            #endregion

            #region Drawing

            if (ctx.IsOptimization)
            {
                return;
            }

            var pane = ctx.CreatePane("main", 100, false);
            var color = new TSLab.Script.Color(System.Drawing.Color.Black.ToArgb());
            var lst = pane.AddList(sec.ToString(), sec, CandleStyles.BAR_CANDLE, color, PaneSides.RIGHT);

            lst = pane.AddList(highChanel.ToString(), highChanel, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            lst = pane.AddList(lowChanel.ToString(), lowChanel, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            #endregion

        }
    }
}
