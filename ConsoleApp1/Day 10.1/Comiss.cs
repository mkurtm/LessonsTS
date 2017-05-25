using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_10._1
{
    public class Comiss : IExternalScript
    {
        public void Execute(IContext ctx, ISecurity sec)
        {
            //через кубик
            //var comHnd = new AbsolutCommission() { Commission = 10 };
            //comHnd.Execute(sec);

            //через АПИ
            //sec.Commission = (pos, entry) =>
            //{
            //    if (entry)
            //        return 0;
            //    return pos.Shares * pos.Security.LotSize * 10;
            //};

            sec.Commission = (pos, entry) =>
            {
                if (pos.EntrySignalName == "LE")
                    return pos.Shares * pos.Security.LotSize * 5 ;
                return pos.Shares * pos.Security.LotSize * 10;
            };


            for (int i = 0; i < ctx.BarsCount; i++)
            {
                var le = sec.Positions.GetLastActiveForSignal("LE");
                if (i==10)
                {
                    sec.Positions.BuyAtMarket(i + 1, 1, "LE");
                }

                if (i==20)
                {
                    le.CloseAtMarket(i + 1, "LX");
                }
            }            
        }
    }
}
