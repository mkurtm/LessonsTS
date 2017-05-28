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
        //3. Fast Donch has smaller period than Slow
        //4. ENTRY : BO fast Donch, 1 lot
        //5. ADD : BO Slow Donch, 1 lot
        //6. Stop : BO low slow Donch

        #endregion

        #region Optimized

        public OptimProperty LongFastUpPeriod = new OptimProperty(20, 1, 100, 1);
        public OptimProperty LongFastDownPeriod = new OptimProperty(20, 1, 100, 1);
        public OptimProperty LongSlowUpPeriod = new OptimProperty(20, 1, 100, 1);
        public OptimProperty LongSlowDownPeriod = new OptimProperty(20, 1, 100, 1);
        public OptimProperty ShortFastUpPeriod = new OptimProperty(20, 1, 100, 1);
        public OptimProperty ShortFastDownPeriod = new OptimProperty(20, 1, 100, 1);
        public OptimProperty ShortSlowUpPeriod = new OptimProperty(20, 1, 100, 1);
        public OptimProperty ShortSlowDownPeriod = new OptimProperty(20, 1, 100, 1);

        #endregion

        public void Execute(IContext ctx, ISecurity sec)
        {
            //Контролируем, чтобы Быстрые каналы были меньше, чем Медленные



            #region Primitives

            var LongFastUp = ctx.GetData("longfastup", new string[] { LongFastUpPeriod.ToString() },
                                () => Series.Highest(sec.HighPrices, LongFastUpPeriod));
            var LongFastDown = ctx.GetData("longfastdown", new string[] { LongFastDownPeriod.ToString() },
                                () => Series.Lowest(sec.LowPrices, LongFastDownPeriod));
            var LongSlowUp = ctx.GetData("longslowup", new string[] { LongSlowUpPeriod.ToString() },
                                () => Series.Highest(sec.HighPrices, LongSlowUpPeriod));
            var LongSlowDown = ctx.GetData("longslowdown", new string[] { LongSlowDownPeriod.ToString() },
                                () => Series.Lowest(sec.LowPrices, LongSlowDownPeriod));

            var ShortFastUp = ctx.GetData("Shortfastup", new string[] { ShortFastUpPeriod.ToString() },
                                () => Series.Highest(sec.HighPrices, ShortFastUpPeriod));
            var ShortFastDown = ctx.GetData("Shortfastdown", new string[] { ShortFastDownPeriod.ToString() },
                                () => Series.Lowest(sec.LowPrices, ShortFastDownPeriod));
            var ShortSlowUp = ctx.GetData("Shortslowup", new string[] { ShortSlowUpPeriod.ToString() },
                                () => Series.Highest(sec.HighPrices, ShortSlowUpPeriod));
            var ShortSlowDown = ctx.GetData("Shortslowdown", new string[] { ShortSlowDownPeriod.ToString() },
                                () => Series.Lowest(sec.LowPrices, ShortSlowDownPeriod));
            #endregion

            #region Trading

            //Определим наименьший из периодов для инициализации

            List<double> biggestLong = new List<double> { LongFastUpPeriod, LongFastDownPeriod, LongSlowUpPeriod, LongSlowDownPeriod };
            List<double> biggestShort = new List<double> { ShortFastUpPeriod, ShortFastDownPeriod, ShortSlowUpPeriod, ShortSlowDownPeriod };
            List<double> biggestPeriod = new List<double> { LongFastUpPeriod, LongFastDownPeriod, LongSlowUpPeriod, LongSlowDownPeriod, ShortFastUpPeriod, ShortFastDownPeriod, ShortSlowUpPeriod, ShortSlowDownPeriod };

            for (int i = (int)biggestPeriod.Max()+1; i < ctx.BarsCount; i++)
            {



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

            lst = pane.AddList(LongFastUp.ToString(), LongFastUp, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            lst = pane.AddList(LongFastDown.ToString(), LongFastDown, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            lst = pane.AddList(LongSlowUp.ToString(), LongSlowUp, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            lst = pane.AddList(LongSlowDown.ToString(), LongSlowDown, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            lst = pane.AddList(ShortFastUp.ToString(), ShortFastUp, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            lst = pane.AddList(ShortFastDown.ToString(), ShortFastDown, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            lst = pane.AddList(ShortSlowUp.ToString(), ShortSlowUp, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            lst = pane.AddList(ShortSlowDown.ToString(), ShortSlowDown, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            
            #endregion

        }
    }
}
