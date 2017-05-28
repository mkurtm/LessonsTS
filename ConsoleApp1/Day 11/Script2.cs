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
using ConsoleApp1;


namespace Day_11
{
    public class Script2 : IExternalScript
    {
        #region Params

        public OptimProperty chPeriod = new OptimProperty(20, 1, 100, 1);
        public OptimProperty chPeriodDay = new OptimProperty(1, 1, 10, 1);

        public OptimProperty stop = new OptimProperty(2, 0.25, 5, 0.25);
        public OptimProperty take = new OptimProperty(2, 0.25, 5, 0.25);

        public OptimProperty posAdd = new OptimProperty(1, 0.25, 5, 0.25);
        public OptimProperty stopMove = new OptimProperty(0.75, 0.25, 5, 0.25);

        public OptimProperty TrStopLoss = new OptimProperty(1.5, 0.25, 5, 0.25);
        public OptimProperty TrEnable = new OptimProperty(1, 0.25, 5, 0.25);
        public OptimProperty TrLoss = new OptimProperty(0.5, 0.25, 5, 0.25);

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

            //Дневной канал
            var daySec = sec.CompressTo(new Interval(1, DataIntervals.DAYS));
            var dayHighest = ctx.GetData("dayhighest", new string[] { chPeriodDay.ToString() },
                                        () =>
                                        {
                                            var h = Series.Highest(daySec.HighPrices, chPeriodDay);
                                            return daySec.Decompress(h);
                                        });
            var dayLowest = ctx.GetData("daylowest", new string[] { chPeriodDay.ToString() },
                                        () =>
                                        {
                                            var h = Series.Lowest(daySec.LowPrices, chPeriodDay);
                                            return daySec.Decompress(h);
                                        });

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

            bool stopMoveFlagLong = false;
            bool trEnableFlagLong = false;
            var lastStopLossLong = 0.0;

            bool stopMoveFlagShort = false;
            bool trEnableFlagShort = false;
            var lastStopLossShort = 0.0;

            for (int i = start; i < count; i++)
            {
                var le = sec.Positions.GetLastActiveForSignal("LE");
                var leAdd = sec.Positions.GetLastActiveForSignal("LEadd");
                var leD = sec.Positions.GetLastActiveForSignal("LEd");
                //Отобрали позы, начинающийся с LE
                var lePos = sec.Positions.GetActiveForBar(i).Where(p => p.EntrySignalName.StartsWith("LE")).ToList();

                if (le == null)
                {
                    sec.Positions.BuyIfGreater(i + 1, 1, highChanel[i], 50, "LE");
                    stopMoveFlagLong = false;
                    trEnableFlagLong = false;
                    lastStopLossLong = 0.0;
                }
                else
                {
                    //пробуем нарастить позу
                    if (leAdd == null && leD == null)
                    {
                        var posAddPrice = le.EntryPrice * (1 + posAdd / 100.0);
                        sec.Positions.BuyIfGreater(i + 1, 1, posAddPrice, "LEadd");
                    }

                    if (sec.Bars[i - 1].Close < dayHighest[i - 1] && sec.Bars[i].Close > dayHighest[i])
                    {
                        var lots = lePos.TotalSize() / sec.LotSize;
                        sec.Positions.BuyAtMarket(i + 1, lots, "LEd");
                    }

                    var avgEntry = lePos.AvgEntryPrice();

                    //stop
                    if (leD == null)
                    {
                        //take
                        var takeprofit = avgEntry * (1 + take / 100.0);
                        lePos.ForEach(p => p.CloseAtProfit(i + 1, takeprofit, "LXP"));

                        var stopMovePrice = avgEntry * (1 + stopMove / 100.0);
                        if (sec.Bars[i].High > stopMovePrice || stopMoveFlagLong == true)
                        {
                            stopMoveFlagLong = true;
                            var stoploss = avgEntry;
                            lePos.ForEach(p => p.CloseAtStop(i + 1, stoploss, "LXSmoved"));
                        }
                        else
                        {
                            var stopLoss = avgEntry * (1 - stop / 100.0);
                            lePos.ForEach(p => p.CloseAtStop(i + 1, stopLoss, "LXS"));
                        }
                    }
                    else
                    {
                        var enablePrice = avgEntry * (1 + TrEnable / 100.0);
                        if (enablePrice < sec.HighPrices[i])
                            trEnableFlagLong = true;

                        if (trEnableFlagLong)
                        {
                            var trStop = sec.HighPrices[i] * (1 - TrLoss / 100.0);
                            var stopLoss = Math.Max(trStop, lastStopLossLong);
                            lastStopLossLong = stopLoss;
                            lePos.ForEach(p => p.CloseAtStop(i + 1, stopLoss, "LXtrail"));
                        }
                        else
                        {
                            var stopLoss = avgEntry * (1 - TrStopLoss / 100.0);
                            lastStopLossLong = stopLoss;
                            lePos.ForEach(p => p.CloseAtStop(i + 1, stopLoss, "LXStr"));
                        }

                    }

                }

                //SHORT

                var se = sec.Positions.GetLastActiveForSignal("SE");
                var seAdd = sec.Positions.GetLastActiveForSignal("SEadd");
                var seD = sec.Positions.GetLastActiveForSignal("SEd");
                //Отобрали позы, начинающийся с LE
                var sePos = sec.Positions.GetActiveForBar(i).Where(p => p.EntrySignalName.StartsWith("SE")).ToList();

                if (se == null)
                {
                    sec.Positions.SellIfLess(i + 1, 1, lowChanel[i], 50, "SE");
                    stopMoveFlagShort = false;
                    trEnableFlagShort = false;
                    lastStopLossShort = 0.0;
                }
                else
                {
                    //пробуем нарастить позу
                    if (seAdd == null && seD == null)
                    {
                        var posAddPrice = se.EntryPrice * (1 - posAdd / 100.0);
                        sec.Positions.SellIfLess(i + 1, 1, posAddPrice, "LEadd");
                    }

                    if (sec.Bars[i - 1].Close > dayLowest[i - 1] && sec.Bars[i].Close < dayLowest[i])
                    {
                        var lots = sePos.TotalSize() / sec.LotSize;
                        sec.Positions.SellAtMarket(i + 1, lots, "SEd");
                    }

                    var avgEntry = sePos.AvgEntryPrice();

                    //stop
                    if (seD == null)
                    {
                        //take
                        var takeprofit = avgEntry * (1 - take / 100.0);
                        sePos.ForEach(p => p.CloseAtProfit(i + 1, takeprofit, "SXP"));

                        var stopMovePrice = avgEntry * (1 - stopMove / 100.0);
                        if (sec.Bars[i].Low < stopMovePrice || stopMoveFlagShort == true)
                        {
                            stopMoveFlagShort = true;
                            var stoploss = avgEntry;
                            sePos.ForEach(p => p.CloseAtStop(i + 1, stoploss, "SXSmoved"));
                        }
                        else
                        {
                            var stopLoss = avgEntry * (1 + stop / 100.0);
                            sePos.ForEach(p => p.CloseAtStop(i + 1, stopLoss, "SXS"));
                        }
                    }
                    else
                    {
                        var enablePrice = avgEntry * (1 - TrEnable / 100.0);
                        if (enablePrice > sec.LowPrices[i])
                            trEnableFlagShort = true;

                        if (trEnableFlagShort)
                        {
                            var trStop = sec.LowPrices[i] * (1 - TrLoss / 100.0);
                            var stopLoss = Math.Min(trStop, lastStopLossShort);
                            lastStopLossShort = stopLoss;
                            sePos.ForEach(p => p.CloseAtStop(i + 1, stopLoss, "SXtrail"));
                        }
                        else
                        {
                            var stopLoss = avgEntry * (1 + TrStopLoss / 100.0);
                            lastStopLossShort = stopLoss;
                            sePos.ForEach(p => p.CloseAtStop(i + 1, stopLoss, "SXStr"));
                        }

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

            color = new TSLab.Script.Color(System.Drawing.Color.Blue.ToArgb());
            lst = pane.AddList(dayHighest.ToString(), dayHighest, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            lst.Thickness = 3;

            color = new TSLab.Script.Color(System.Drawing.Color.Blue.ToArgb());
            lst = pane.AddList(dayLowest.ToString(), dayLowest, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            lst.Thickness = 3;

            #endregion

        }
    }
}
