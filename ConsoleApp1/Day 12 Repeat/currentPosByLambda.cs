using TSLab.Script;
using TSLab.Script.Handlers;
using System.Linq;
using System;
using ConsoleApp1;

namespace Day_12_Repeat
{
    [HandlerCategory("Marat")]
    [HandlerName("currentPosByLambda")]
    public class currentPosByLambda : IBar2ValueDoubleHandler
    {
        public double Execute(ISecurity sec, int barNum)
        {
            var pos = sec.Positions.GetActiveForBar(barNum);
            return pos.Sum(p => p.IsLong ? p.Shares : -p.Shares);
        }
    }
}
