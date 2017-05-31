using System;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_13
{
    [HandlerCategory("Marat"), HandlerName("Last Closed Named Position Exit Date")]
    public class LastClosedNamePositionExitDate : IOneSourceHandler, IDoubleReturns, IValuesHandler, IHandler, ISecurityInputs
    {
        [HandlerParameter(true, NotOptimized = true)]
        public string Name { get; set; }

        public double Execute(ISecurity sec, int barNum)
        {
            var pos = sec.Positions.GetLastClosedForSignal(Name, barNum);
            if (pos != null && pos.IsActive)            
                pos = null;           
            var dateTime = (pos == null) ? DateTime.MinValue : pos.ExitBar.Date;
            return (double)(dateTime.Year % 100) * 10000.0 + (double)dateTime.Month * 100.0 + (double)dateTime.Day;
        }
    }
}
