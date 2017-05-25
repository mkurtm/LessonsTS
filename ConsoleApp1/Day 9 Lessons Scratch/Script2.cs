using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Optimization;

namespace Day_9_Lessons_Scratch
{
    public class Script2 : IExternalScript
    {
        //Создать скрипт входа по следующим условиям.Если  
        //свеча белая марибозу то входим в лонг.Выход из позиции по стопу
        //и тейку.Величина обоих 0,5%. Оптимизацию делаем по желанию. 
        //Можно без нее.

        public OptimProperty stop = new OptimProperty(0.5, 0.1, 5, 0.1);
        public OptimProperty take = new OptimProperty(0.5, 0.1, 5, 0.1);

        public void Execute(IContext ctx, ISecurity sec)
        {
            var comiss = new AbsolutCommission() { Commission = 20 };
            comiss.Execute(sec);


            for (int i = 0; i < ctx.BarsCount; i++)
            {
                var le = sec.Positions.GetLastActiveForSignal("LE");

                if (le == null)
                {
                    if (sec.Bars[i].IsMaribozuWhite())
                    {
                        sec.Positions.BuyAtMarket(i + 1, 1, "LE");
                    }
                }
                else
                {                    
                    le.CloseAtStop(i + 1, le.EntryPrice-le.EntryPrice*stop/100, "LX");
                    le.CloseAtProfit(i + 1, le.EntryPrice+le.EntryPrice*take/100, "LT");
                }

            }

            var pane = ctx.CreatePane("main", 100, false);
            var color = new Color(System.Drawing.Color.Black.ToArgb());
            var lst = pane.AddList(sec.ToString(), sec, CandleStyles.BAR_CANDLE, color, PaneSides.RIGHT);
            
        }
    }
}
