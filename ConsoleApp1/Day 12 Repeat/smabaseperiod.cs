using System.Collections.Generic;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;

namespace Day_12_Repeat
{
    [HandlerCategory("Marat")]
    [HandlerName("Sma+BasePeriod")]
    [HandlerDecimals(3)]
    public class smabaseperiod : BasePeriodIndicatorHandler
    {       
        public IList<double> Execute(IList<double> source)
        {
            return Series.SMA(source, Period);
        }
    }
}
