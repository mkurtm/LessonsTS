using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;
using TSLab.Script.Optimization;
using TSLab.DataSource;
using ConsoleApp1;

namespace Day_11
{
    public class Script1 : IExternalScript
    {

        #region Task

        //Не решены 2 задачи:
        //    1. Расчет комиссии если есть прибыль.
        //    2. Направление скользящих старших таймфреймов.


        //Вход в позицию производится тогда, когда SMA
        //периода 24 на дневном тайм фрейме направлена вверх, SMA 14 на
        //часовом смотрит вверх и цена пересекает снизу вверх SMA 12
        //периода(тайм фрейм меньше часовика любой). При соблюдении
        //всех этих условий проверяем чтобы свеча была белой, тогда можно
        //входить в позицию.Входим одним лотом.
        //Выход производим по оптимизируемым стопу и тейку.
        //Дополнительным условием будет выход через 30 бар после входа в
        //позицию (так называемый таймстоп) если еще позиция не закрыта
        //по стопу и тейку.
        //Добавляем расчет комиссии.Если удержание было больше 10 бар и
        //есть прибыль, То комиссия 5 пунктов за вход и 0,03% за выход.
        //Если удержание больше 10 бар и нет прибыли, то просто за вход и
        //выход по 5 пунктов.Если удержание было меньше 10 бар, то
        //комиссия просто по 2 пункта за вход и выход.        
        ///
        //1. Entry:
        //SMA 24 on Daily is UP
        //SMA 14 on Hour is UP
        //Price cross SMA 12 on 5 min
        //Candle is white
        //1 lot

        //2. Exit
        //Stop and take optimized

        //3. Additional exit
        //Time stop = 30 bars

        //4. Comiss
        //hold more 10 bars & isprofit?
        //5 points enter, 0.03% exit
        //hold more 10 bars and NopRofit?
        //5 & 5 points
        //hold less 10 bars?
        //2 & 2 points
        #endregion

        #region OptParams

        public OptimProperty stop = new OptimProperty(0.5, 0.1, 5, 0.25);
        public OptimProperty take = new OptimProperty(0.5, 0.1, 5, 0.25);

        #endregion

        public void Execute(IContext ctx, ISecurity sec)
        {
            #region Indic and Setups

            //comiss

            //sec.Commission = (pos, entry) =>
            //{
            //    if (entry)
            //        return 0;
            //    if (pos.BarsHeld > 10)
            //        return pos.Shares * pos.Security.LotSize * 20;
            //    else if (pos.ProfitPct() < 0)
            //        return pos.Shares * pos.Security.LotSize * 10;
            //    else
            //        return pos.Shares * pos.Security.LotSize * 4;
            //};


            //indics

            var sma12 = ctx.GetData("sma12", new string[] { }, () => Series.SMA(sec.ClosePrices, 12));

            var hourSec = sec.CompressTo(new Interval(60, DataIntervals.MINUTE));
            var sma14hour = ctx.GetData("sma14hour", new string[] { },
                            () =>
                {
                    var h = Series.SMA(hourSec.ClosePrices, 14);
                    return hourSec.Decompress(h);
                });

            var daySec = sec.CompressTo(new Interval(1, DataIntervals.DAYS));
            var sma24day = ctx.GetData("sma24day", new string[] { },
                            () =>
                            {
                                var h = Series.SMA(daySec.ClosePrices, 24);
                                return daySec.Decompress(h);
                            });

            //Создаем два массива с направлениями скользящих
            List<bool> isDayUP = new List<bool>();
            isDayUP.Add(false);
            bool isUpDay = false;
            for (int i = 1; i < ctx.BarsCount; i++)
            {
                if (sec.Bars[i].Date.Day > sec.Bars[i - 1].Date.Day)
                {
                    if (sma24day[i] > sma24day[i - 1])
                        isUpDay = true;
                    else
                        isUpDay = false;
                    isDayUP.Add(isUpDay);
                }
                else
                    isDayUP.Add(isUpDay);
            }

            List<bool> isHourUP = new List<bool>();
            isHourUP.Add(false);
            bool isUpHour = false;
            for (int i = 1; i < ctx.BarsCount; i++)
            {
                if (sec.Bars[i].Date.Hour > sec.Bars[i - 1].Date.Hour)
                {
                    if (sma14hour[i] > sma14hour[i - 1])
                        isUpHour = true;
                    else
                        isUpHour = false;
                    isHourUP.Add(isUpHour);
                }
                else
                    isHourUP.Add(isUpHour);
            }



            //flags

            bool isDayUp = false;
            bool isHourUp = false;

            #endregion





            #region Trading          

            for (int i = 24 * 14 * 12; i < ctx.BarsCount; i++)
            {

                var le = sec.Positions.GetLastActiveForSignal("LE");

                //Проверка направленности старших ТФ
                if (sec.Bars[i].Date.Day > sec.Bars[i - 1].Date.Day)
                    isDayUp = sma24day[i] > sma24day[i - 1] ? true : false;

                if (sec.Bars[i].Date.Hour > sec.Bars[i - 1].Date.Hour)
                    isHourUp = sma14hour[i] > sma14hour[i - 1] ? true : false;








                if (le == null)
                {
                    if (isDayUp == true && isHourUp == true &&
                       sec.ClosePrices[i - 1] < sma12[i - 1] &&
                        sec.ClosePrices[i] > sma12[i] &&
                        sec.Bars[i].IsWhite())
                    {
                        sec.Positions.BuyAtMarket(i + 1, 1, "LE");
                    }
                }
                else
                {
                    if (i - le.EntryBarNum >= 30)
                        le.CloseAtMarket(i + 1, "LXtime");
                    //take and stop
                    le.CloseAtStop(i + 1, le.EntryPrice * (1 - stop / 100.0), "LXS");
                    le.CloseAtProfit(i + 1, le.EntryPrice * (1 + take / 100.0), "LXP");
                }
            }

            #endregion


            #region Drawing

            if (ctx.IsOptimization)
            {
                return;
            }

            var pane = ctx.CreatePane("main", 100, false);
            var color = new Color(System.Drawing.Color.Black.ToArgb());
            var lst = pane.AddList(sec.ToString(), sec, CandleStyles.BAR_CANDLE, color, PaneSides.RIGHT);

            lst = pane.AddList("sma12", sma12, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            lst = pane.AddList("sma14h", sma14hour, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            lst = pane.AddList("sma24d", sma24day, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            // lst = pane.AddList("IS", isDayUP, ListStyles.HISTOHRAM, color, LineStyles.SOLID, PaneSides.LEFT);
            lst = pane.AddList("IS", isHourUP, ListStyles.HISTOHRAM, color, LineStyles.SOLID, PaneSides.LEFT);

            #endregion
        }
    }
}
