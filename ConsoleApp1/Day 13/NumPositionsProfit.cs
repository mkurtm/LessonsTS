using System.Linq;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_13
{
    [HandlerCategory("Marat")]
    [HandlerName("NumPositionsProfit")]
    public class NumPositionsProfit : BasePeriodIndicatorHandler, IBar2ValueDoubleHandler, IOneSourceHandler, IDoubleReturns, IValuesHandler, IHandler, ISecurityInputs
    {
        public double Execute(ISecurity sec, int barNum)
        {
            var num = 0.0;
            var num2 = (double)Period;
            foreach (IPosition current in from pos in sec.Positions
                                          where !pos.IsActiveForbar(barNum)
                                          orderby pos.ExitBarNum descending
                                          select pos)
            {
                num += WholeTimeProfit.ProfitForBar(current,barNum);
                if ((num2 -= 1.0) <= 0.0)
                {
                    break;
                }
            }
            return num;            
        }
    }




}
