using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_13
{
    [HandlerCategory("Marat")]
    [HandlerName("Drowdowncount")]
    public class DrowdownCount : ClosedPositionCache, IOneSourceHandler, IDoubleReturns, IValuesHandler, IHandler, ISecurityInputs
    {
        public double Execute(ISecurity sec, int barNum)
        {
            // return (double)GetClosedPositions(source, barNum).TakeWhile((p) => p.Profit() <= 0.0).Count();

            return sec.Positions.GetClosedForBar(barNum).TakeWhile((p) => p.Profit() > 0).Count();
        }
    }    
}
