using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;
using TSLab.Script.Optimization;

namespace Day_9_Lessons_Scratch
{
    public class Script3 : IExternalScript
    {
        //Написать в ТСЛаб скрипт.За основу взять решение с
        //предыдущего урока. Вход в лонг, по пробою верхней границы
        //канала.Выход по стандартному трейл стопу.Но есть
        //дополнительные условия. Если последняя закрытая позиция была
        //убыточной (< 0.1%), то следующая позиция двойного размера от
        //предыдущей.В остальных случаях размер не изменяется, то есть
        //входим стандартным размером.Для определения профита в профентах используем метод IPosition.ProfitPct()

        public OptimProperty uCh = new OptimProperty(10, 1, 50, 1);
        public OptimProperty _stoploss = new OptimProperty(1, 0.1, 5, 0.1);
        public OptimProperty _trailenable = new OptimProperty(1, 0.1, 5, 0.1);
        public OptimProperty _trailloss = new OptimProperty(1, 0.1, 5, 0.1);

        public void Execute(IContext ctx, ISecurity sec)
        {
            var uChanel = ctx.GetData("highest", new string[] { uCh.ToString() },
                            () => Series.Highest(sec.HighPrices, uCh));

            var trailHandler = new TrailStop() { StopLoss = _stoploss, TrailEnable = _trailenable, TrailLoss = _trailloss };

            var comiss = new AbsolutCommission() { Commission = 20 };
            comiss.Execute(sec);

            for (int i = uCh; i < ctx.BarsCount; i++)
            {
                var le = sec.Positions.GetLastActiveForSignal("LE");

                if (le == null)
                {
                    if (sec.Bars[i].Date.TimeOfDay.IsFirstBar() == false &&
                        sec.Bars[i].Date.TimeOfDay<new TimeSpan(18,45,00) &&
                        (sec.Bars[i].Date.TimeOfDay < new TimeSpan(13, 45, 00) ||
                        sec.Bars[i].Date.TimeOfDay > new TimeSpan(16, 15, 00)))
                    {

                        var lastpose = sec.Positions.GetLastPositionClosed(i);
                        if (lastpose != null)
                        {
                            if (lastpose.ProfitPct() < 0.1)
                            {
                                sec.Positions.BuyIfGreater(i + 1, 2 * lastpose.Shares, uChanel[i], "LE");
                            }
                            else
                                sec.Positions.BuyIfGreater(i + 1, 1, uChanel[i], "LE");
                        }
                        else
                            sec.Positions.BuyIfGreater(i + 1, 1, uChanel[i], "LE");
                    }

                }
                else
                {
                    var stop = trailHandler.Execute(le, i);
                    le.CloseAtStop(i + 1, stop, "LX");
                }

            }

            var pane = ctx.CreatePane("main", 100, false);
            var color = new Color(System.Drawing.Color.Black.ToArgb());
            var lst = pane.AddList(sec.ToString(), sec, CandleStyles.BAR_CANDLE, color, PaneSides.RIGHT);
            lst = pane.AddList(uChanel.ToString(), uChanel, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
        }
    }
}
