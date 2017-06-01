using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_14_Repeat
{
    public class ProfitAtThisDay : IExternalScript
    {
        public void Execute(IContext ctx, ISecurity sec)
        {
            var maxLoss = 1000;
            var startTime = DateTime.Now;
            var precCloseDayBar = 0;

            for (int i = 0; i < ctx.BarsCount; i++)
            {                
                //var closed = sec.Positions.GetClosedForBar(i);
                //var currDate = sec.Bars[i].Date.Date;
                //var currBarClosed = closed.Where(p => p.ExitBar.Date.Date == currDate);

                var currDate = sec.Bars[i].Date.Date;
                var prevDate = sec.Bars[precCloseDayBar].Date.Date;
                if (currDate > prevDate)                
                    precCloseDayBar = i - 1;                

                var closed = sec.Positions.Where(p => p.ExitBar.Date.Date == currDate&&!p.IsActive);

                var totalProfit = sec.Positions.TotalProfit(i);
                var totalProfitPrev = sec.Positions.TotalProfit(precCloseDayBar);

                var dayProfit = totalProfit - totalProfitPrev;

            }

            var stopTime = DateTime.Now;
            ctx.LogInfo("{0}", stopTime - startTime);

        }
    }
}
