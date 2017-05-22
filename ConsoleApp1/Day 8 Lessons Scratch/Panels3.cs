using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_8_Lessons_Scratch
{
    public class Panels3 : IExternalScript

    {
        public void Execute(IContext ctx, ISecurity sec)
        {

            //рисуем панель и график
            IPane pane = ctx.CreatePane("Main", 100, false);
            Color color = new Color(System.Drawing.Color.Black.ToArgb());
            IGraphList lst = pane.AddList(sec.ToString(), sec, CandleStyles.BAR_CANDLE, color, PaneSides.RIGHT);

            //открытый интерес
            //1. Через кубик
            //OpenInterest oiHandler = new OpenInterest() { Context = ctx};
            //IList<double> oi = oiHandler.Execute(sec);

            //2. Через свойство свечи sec.Bars[i].Interest
            IList<double> oi = new double[ctx.BarsCount];
            for (int i = 0; i < ctx.BarsCount; i++)
                oi[i] = sec.Bars[i].Interest;

            pane = ctx.CreatePane("OI", 25, false);
            color = new Color(System.Drawing.Color.Black.ToArgb());
            lst = pane.AddList("OI", oi, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);
            lst.Thickness = 2;

            // Работа с bid / ask
            //1. Кубик

            Bid bidHandler = new TSLab.Script.Handlers.Bid() { Context = ctx };
            IList<double> bid = bidHandler.Execute(sec);
            Ask askHandler = new TSLab.Script.Handlers.Ask() { Context = ctx };
            IList<double> ask = askHandler.Execute(sec);

            pane = ctx.CreatePane("B|A", 25, false);
            color = new Color(System.Drawing.Color.Black.ToArgb());
            lst = pane.AddList("Bid", bid, ListStyles.LINE_WO_ZERO, color, LineStyles.SOLID, PaneSides.RIGHT);
            lst = pane.AddList("Ask", ask, ListStyles.LINE_WO_ZERO, color, LineStyles.SOLID, PaneSides.RIGHT);

            ////2. Через свойства

            //IList<double> bid = new double[ctx.BarsCount];
            //IList<double> ask = new double[ctx.BarsCount];

            //for (int i = 0; i < ctx.BarsCount; i++)
            //{
            //    bid[i] = sec.Bars[i].Bid;
            //    ask[i] = sec.Bars[i].Ask;
            //}

            //Выводим на панель спред между открытием и закрытием свечи
            IList<double> spread = new double[ctx.BarsCount];
            for (int i = 0; i < ctx.BarsCount; i++)
                spread[i] = sec.Bars[i].Close - sec.Bars[i].Open;

            pane = ctx.CreatePane("Spread", 25, false);
            color = new Color(System.Drawing.Color.Black.ToArgb());
            lst = pane.AddList("Spread", spread, ListStyles.LINE, color, LineStyles.SOLID, PaneSides.RIGHT);

            //Выводим на панель Гистограмму если движение больше 100 пунктов.
            IList<double> goodmove = new double[ctx.BarsCount];
            for (int i = 0; i < ctx.BarsCount; i++)
                goodmove[i] = spread[i] > 200 ? 1 : 0;
            pane = ctx.CreatePane("GoodMove", 25, false);
            lst = pane.AddList("Goodmove", goodmove, ListStyles.HISTOHRAM, color, LineStyles.SOLID, PaneSides.RIGHT);

        }
    }
}
