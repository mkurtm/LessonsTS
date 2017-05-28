using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Optimization;
using TSLab.Helper;
using TSLab.Script.Helpers;

namespace Day_10_Homework
{
    public class Script2 : IExternalScript
    {
        #region Description

        //Задание 2: Написать скрипт ТСЛаб.Вход в ЛОНГ и в ШОРТ.
        //Допустим вход в разнонаправленную позицию, то есть 
        //сразу и в лонг и в шорт.Дальше не примере лонга описаны
        //правила. Есть два канала дончиана. Один быстрый другой 
        //медленный. Период у них разный. У быстрого канала период 
        //МЕНЬШЕ.Когда ПРОБИЛИ верхнюю границу быстрого канала, входим 
        //в позицию 1 лотом.Если после этого пробили еще и границу 
        //медленного канала, наращиваемся 1 лотом.Закрываемся полностью
        //по пробою нижней границы быстрого канала. Для ЛОНГА и для ШОРТА
        //каналы могут быть разными!!!

        //Resume

        //1. Long And Short
        //2. 2 Donchian Chanels : fast and slow : different for LE and SE
        //3. Fast Donch has smaller period than Slow, more than 20%
        //4. ENTRY : BO fast Donch, 1 lot
        //5. ADD : BO Slow Donch, 1 lot
        //6. Stop : BO low fast Donch

        #endregion

        #region Optimized

        public OptimProperty LongFastPeriod = new OptimProperty(20, 1, 100, 2);
        public OptimProperty LongSlowPeriod = new OptimProperty(20, 1, 100, 2);
        public OptimProperty ShortFastPeriod = new OptimProperty(20, 1, 100, 2);
        public OptimProperty ShortSlowPeriod = new OptimProperty(20, 1, 100, 2);

        #endregion

        public void Execute(IContext ctx, ISecurity sec)
        {
            #region Preparing

            //Контролируем, чтобы Быстрые каналы были меньше, чем Медленные            

            if (LongFastPeriod * 1.5 > LongSlowPeriod)
                return;
            if (ShortFastPeriod * 1.5 > ShortSlowPeriod)
                return;

            //Определим наименьший из периодов для инициализации

            //List<double> biggestLong = new List<double> { LongFastPeriod, LongSlowPeriod };
            //List<double> biggestShort = new List<double> { ShortFastPeriod,  ShortSlowPeriod};
            List<double> biggestPeriod = new List<double> { LongFastPeriod, LongSlowPeriod, ShortFastPeriod, ShortSlowPeriod };

            //Set commis

            if (!ctx.IsOptimization)
            {
                var commis = new AbsolutCommission { Commission = 10 };
                commis.Execute(sec);
            }

            #endregion

            #region Primitives

            var LongFastUp = ctx.GetData("longfastup", new string[] { LongFastPeriod.ToString() },
                                () => Series.Highest(sec.HighPrices, LongFastPeriod));
            var LongFastDown = ctx.GetData("longfastdown", new string[] { LongFastPeriod.ToString() },
                                () => Series.Lowest(sec.LowPrices, LongFastPeriod));
            var LongSlowUp = ctx.GetData("longslowup", new string[] { LongSlowPeriod.ToString() },
                                () => Series.Highest(sec.HighPrices, LongSlowPeriod));
            var LongSlowDown = ctx.GetData("longslowdown", new string[] { LongSlowPeriod.ToString() },
                                () => Series.Lowest(sec.LowPrices, LongSlowPeriod));

            var ShortFastUp = ctx.GetData("Shortfastup", new string[] { ShortFastPeriod.ToString() },
                                () => Series.Highest(sec.HighPrices, ShortFastPeriod));
            var ShortFastDown = ctx.GetData("Shortfastdown", new string[] { ShortFastPeriod.ToString() },
                                () => Series.Lowest(sec.LowPrices, ShortFastPeriod));
            var ShortSlowUp = ctx.GetData("Shortslowup", new string[] { ShortSlowPeriod.ToString() },
                                () => Series.Highest(sec.HighPrices, ShortSlowPeriod));
            var ShortSlowDown = ctx.GetData("Shortslowdown", new string[] { ShortSlowPeriod.ToString() },
                                () => Series.Lowest(sec.LowPrices, ShortSlowPeriod));
            #endregion

            #region Trading  

            for (int i = (int)biggestPeriod.Max() + 1; i < ctx.BarsCount; i++)
            {

                //Longs

                var le = sec.Positions.GetLastActiveForSignal("LE");
                var leAdd = sec.Positions.GetLastActiveForSignal("LEadd");
                var longs = sec.Positions.GetActiveForBar(i).Where(p => p.EntrySignalName.StartsWith("LE")).ToList();

                if (le == null)
                {
                    if (sec.Bars[i].Date.TimeOfDay.IsFirstBar() == true)
                        continue;
                    sec.Positions.BuyIfGreater(i + 1, 1, LongFastUp[i], "LE");
                }

                else
                {
                    if (leAdd == null)
                    {
                        if (sec.Bars[i].Date.TimeOfDay.IsFirstBar() == true)
                            continue;
                        sec.Positions.BuyIfGreater(i + 1, 1, LongSlowUp[i], "LEadd");
                    }

                    foreach (var pos in longs)
                    {
                        pos.CloseAtStop(i + 1, LongFastDown[i], "LS");
                    }
                }

                //Shorts

                var se = sec.Positions.GetLastActiveForSignal("SE");
                var seAdd = sec.Positions.GetLastActiveForSignal("SEadd");
                var shorts = sec.Positions.GetActiveForBar(i).Where(p => p.EntrySignalName.StartsWith("SE")).ToList();

                if (se == null)
                {
                    if (sec.Bars[i].Date.TimeOfDay.IsFirstBar() == true)
                        continue;
                    sec.Positions.SellIfLess(i + 1, 1, ShortFastDown[i], "SE");
                }

                else
                {
                    if (seAdd == null)
                    {
                        if (sec.Bars[i].Date.TimeOfDay.IsFirstBar() == true)
                            continue;
                        sec.Positions.SellIfLess(i + 1, 1, ShortSlowDown[i], "SEadd");
                    }

                    foreach (var pos in shorts)
                    {
                        pos.CloseAtStop(i + 1, ShortFastUp[i], "SS");
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
            var color = new Color(System.Drawing.Color.Black.ToArgb());
            var lst = pane.AddList(sec.ToString(), sec, CandleStyles.BAR_CANDLE, color, PaneSides.RIGHT);

            //lst = pane.AddList(LongFastUp.ToString(), LongFastUp, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            //lst = pane.AddList(LongFastDown.ToString(), LongFastDown, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            //lst = pane.AddList(LongSlowUp.ToString(), LongSlowUp, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            //lst = pane.AddList(LongSlowDown.ToString(), LongSlowDown, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            //lst = pane.AddList(ShortFastUp.ToString(), ShortFastUp, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            //lst = pane.AddList(ShortFastDown.ToString(), ShortFastDown, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            //lst = pane.AddList(ShortSlowUp.ToString(), ShortSlowUp, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            //lst = pane.AddList(ShortSlowDown.ToString(), ShortSlowDown, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            #endregion

        }
    }
}
