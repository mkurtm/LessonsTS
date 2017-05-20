using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_7_Lessons_Scratch
{
    public class Orders : IExternalScript

    {
        public void Execute(IContext ctx, ISecurity sec)
        {            
            for (int i = 0; i < ctx.BarsCount; i++)
            {
                IPosition le = sec.Positions.GetLastActiveForSignal("LE");

                if (le == null)
                {
                    if (sec.Bars[i].Close - sec.Bars[i].Open > 100)

                    {
                        sec.Positions.BuyAtMarket(i+1, 1, "LE");
                    }
                }

                else
                {
                    le.CloseAtStop(i+1, le.EntryPrice - 100, "LXS");
                    le.CloseAtProfit(i+1, le.EntryPrice + 1000, "LXP");
                }                
            }           

        }
    }
}
