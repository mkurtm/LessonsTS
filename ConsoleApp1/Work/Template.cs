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

namespace Work
{
    public class Template : IExternalScript
    {
        public delegate bool Conditions();

        #region Optimized Parameters & Static things

        //-------------
        //Набор оптимизируемых параметров
        //-------------

        public OptimProperty smaPeriodSmall = new OptimProperty(20, 10, 100, 5);
        //Если нужна оптимизация
        //public OptimProperty smaPeriodBig = new OptimProperty(100, 100, 500, 10);

        public OptimProperty atrPeriod = new OptimProperty(20, 10, 100, 5);

        public OptimProperty uCh6Delta = new OptimProperty(6, 0, 10, 0.1);
        public OptimProperty uCh2Delta = new OptimProperty(2.33, 0, 10, 0.1);
        public OptimProperty uCh1Delta = new OptimProperty(1.33, 0.0, 10.0, 0.1);
        public OptimProperty uCh0Delta = new OptimProperty(0.25, 0, 10, 0.1);

        public OptimProperty dCh0Delta = new OptimProperty(-0.25, -10, 0, 0.1);
        public OptimProperty dCh1Delta = new OptimProperty(-1.33, -10, 0, 0.1);
        public OptimProperty dCh2Delta = new OptimProperty(-2.33, -10, 0, 0.1);
        public OptimProperty dCh6Delta = new OptimProperty(-6, -10, 0, 0.1);

        public OptimProperty takePcntShort = new OptimProperty(2, 0, 30, 0.25);
        public OptimProperty takePcntLong = new OptimProperty(2, 0, 30, 0.25);

        //public OptimProperty numShortPoses = new OptimProperty(3, 1, 20, 1);
        //public OptimProperty numLongPoses = new OptimProperty(3, 1, 20, 1);

        //-------------
        //Инициализируем Stops, статичные, чтобы сохранялись между пересчетами скрипта.
        //-------------

        static double stopShort = 0.0;
        static double stopLong = 0.0;

        #endregion

        public void Execute(IContext ctx, ISecurity sec)
        {
            #region Indicators            

            //-------------
            //Меняем Цвет и период скользящих в зависимости от выбранного таймфрема.
            //-------------

            var colorSMAbig = new Color();
            var colorSMAsmall = new Color();
            var smaPeriodBig = 0;
            if (sec.IntervalInstance == new Interval(1, DataIntervals.MINUTE))
            {
                smaPeriodBig = 100;
                colorSMAbig = new Color(System.Drawing.Color.White.ToArgb());
                colorSMAsmall = new Color(System.Drawing.Color.Orange.ToArgb());
            }
            else if (sec.IntervalInstance == new Interval(5, DataIntervals.MINUTE))
            {
                smaPeriodBig = 220;
                colorSMAbig = new Color(System.Drawing.Color.CornflowerBlue.ToArgb());
                colorSMAsmall = new Color(System.Drawing.Color.White.ToArgb());
            }
            else
            {
                throw new ArgumentException("Не работаю на этом таймфрейме.");
            }

            //-------------
            //Создаем все используемые индикаторы
            //-------------

            var smaBig = Series.SMA(sec.ClosePrices, smaPeriodBig);
            var smaSmall = Series.SMA(sec.ClosePrices, smaPeriodSmall);
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

            #region Script Setups

            //-------------
            //Настройки для Шорта
            //-------------

            bool workShort = true;
            int numShortPoses = 5;

            //-------------
            //Натсройки для Лонга
            //-------------

            bool workLong = true;
            int numLongPoses = 5;

            //-------------
            //Общие настройки
            //-------------

            double slippage = sec.Tick * 20;

            #endregion

            #region Trading

            for (int i = smaPeriodBig; i < ctx.BarsCount; i++)
            {

                //-------------
                // Создаю списки Лонг и Шорт позиций, а также переменные для
                // отслеживания крайней открытой позиции
                //-------------

                var sePoses = new List<IPosition>();
                bool wasShortEntryThisBar = false;
                var lastShortEntryBar = 0;
                var lastShortEntryPrice = 0.0;

                var lePoses = new List<IPosition>();
                bool wasLongEntryThisBar = false;
                var lastLongEntryBar = 0;
                var lastLongEntryPrice = 0.0;

                //-------------
                // Заполняю списки открытыми позициями и запоминаю
                // данные крайней открытой позиции
                //-------------

                for (int j = 0; j < numShortPoses; j++)
                {
                    sePoses.Add(sec.Positions.GetLastActiveForSignal("SE" + j, i));
                    if (sePoses[j] != null)
                    {
                        lastShortEntryBar = sePoses[j].EntryBarNum;
                        lastShortEntryPrice = sePoses[j].EntryPrice;
                    }
                }

                for (int j = 0; j < numLongPoses; j++)
                {
                    lePoses.Add(sec.Positions.GetLastActiveForSignal("LE" + j, i));
                    if (lePoses[j] != null)
                    {
                        lastLongEntryBar = lePoses[j].EntryBarNum;
                        lastLongEntryPrice = lePoses[j].EntryPrice;
                    }
                }

                //-------------
                // Шорт. Определяю ключевые моменты стратегии через делегаты
                //-------------

                Conditions isShortEntryPoint = () =>
                {
                    return
                        sec.Bars[i].Close < smaSmall[i] &&
                        sec.Bars[i - 1].Close > smaSmall[i - 1];
                };

                Conditions isShortConditionsForAdd = () =>
                {
                    return
                        !wasShortEntryThisBar &&
                        (lastShortEntryPrice - sec.Bars[i].Close) > 2 * atr[i];
                };

                //-------------
                // Лонг. Определяю ключевые моменты стратегии через делегаты
                //-------------

                Conditions isLongEntryPoint = () =>
                {
                    return
                        sec.Bars[i].Close < smaSmall[i] &&
                        sec.Bars[i - 1].Close > smaSmall[i - 1];
                };

                Conditions isLongConditionsForAdd = () =>
                {
                    return
                        !wasLongEntryThisBar &&
                        (sec.Bars[i].Close - lastLongEntryPrice) > 2 * atr[i];
                };


                //-------------
                // Шорт. Торговая логика 
                //-------------

                if (workShort)
                    for (int j = 0; j < numShortPoses; j++)   // Прохожу по всем позициям
                    {
                        if (sePoses[j] == null)               // Если позиция не открыта - жду точку входа
                        {
                            if (j == 0)                       // Если первая позиция, то проверка такая
                            {
                                if (
                                    isShortEntryPoint()
                                    )
                                {
                                    sec.Positions.SellAtPrice(i + 1, 1, sec.Bars[i].Close - slippage, "SE" + j, null);
                                    wasShortEntryThisBar = true;
                                }
                            }
                            else                               // Если позиция не первая, то проверка такая
                            {
                                if (
                                    isShortEntryPoint() &&
                                    isShortConditionsForAdd()
                                    )
                                {
                                    sec.Positions.SellAtPrice(i + 1, 1, sec.Bars[i].Close - slippage, "SE" + j, null);
                                    wasShortEntryThisBar = true;
                                }
                            }
                        }
                        else                                    // Если позиция открыта - выставляю стоп и тейк
                        {
                            var stopLoss = sePoses[j].EntryPrice < uCh1[i] ? uCh1[i] : sePoses[j].EntryPrice;
                            sePoses[j].CloseAtStop(i + 1, stopLoss, "SX" + j, null);

                            sePoses[j].CloseAtProfit(i + 1, (sePoses[j].EntryPrice * (1 - (takePcntShort / 100.0))), "ST" + j);
                        }
                    }

                //-------------
                // Лонг. Торговая логика 
                //-------------

                if (workLong)
                    for (int j = 0; j < numLongPoses; j++)          // Прохожу по всем позициям
                    {
                        if (lePoses[j] == null)                     // Если позиция не открыта - жду точку входа
                        {
                            if (j == 0)                             // Если первая позиция, то проверка такая
                            {
                                if (
                                    isLongEntryPoint()
                                    )
                                {
                                    sec.Positions.BuyAtPrice(i + 1, 1, sec.Bars[i].Close + slippage, "LE" + j, null);
                                    wasLongEntryThisBar = true;
                                }
                            }
                            else                                    // Если позиция не первая, то проверка такая
                            {
                                if (
                                    isLongEntryPoint() &&
                                    isLongConditionsForAdd()
                                    )
                                {
                                    sec.Positions.BuyAtPrice(i + 1, 1, sec.Bars[i].Close + slippage, "LE" + j, null);
                                    wasLongEntryThisBar = true;
                                }
                            }
                        }
                        else                                        // Если позиция открыта - выставляю стоп и тейк
                        {
                            var takeProfit = lePoses[j].EntryPrice > dCh1[i] ? dCh1[i] : lePoses[j].EntryPrice;
                            lePoses[j].CloseAtStop(i + 1, takeProfit, "LX" + j, null);

                            lePoses[j].CloseAtProfit(i + 1, (lePoses[j].EntryPrice * (1 + (takePcntLong / 100.0))), "LT" + j);
                        }
                    }
            }

            #endregion

            #region Drawing

            //-------------
            // Нет отрисовки, если в режиме оптимизации
            //-------------

            if (ctx.IsOptimization)
            {
                return;
            }

            //-------------
            // Отрисовка графика и скользящих
            //-------------

            var pane = ctx.CreateGraphPane("main", "main", false);
            var color = new Color(System.Drawing.Color.Green.ToArgb());
            var lst = pane.AddList(sec.ToString(), "sec", sec, CandleStyles.BAR_CANDLE, color, PaneSides.RIGHT);
            lst.AlternativeColor = System.Drawing.Color.Red.ToArgb();

            lst = pane.AddList(sec.ToString(), "smaSmall", smaSmall, ListStyles.LINE, colorSMAsmall, LineStyles.SOLID, PaneSides.RIGHT);
            lst = pane.AddList(sec.ToString(), "smaBig", smaBig, ListStyles.LINE, colorSMAbig, LineStyles.SOLID, PaneSides.RIGHT);
            lst.Thickness = 2;

            //-------------
            // Отрисовка каналов Кельтнера
            //-------------

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

            //-------------
            // Вспомогательные или тестовые панели
            //-------------

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
