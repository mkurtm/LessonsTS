using TSLab.Script;
using TSLab.Script.Handlers;
using System.Linq;
using System.Collections.Generic;
using TSLab.DataSource;

namespace Day_12_Repeat
{
    [HandlerCategory("Marat")]
    [HandlerName("WhiteCandleAndSecurityBase")]
    public class IsWhiteCandle : SecurityBase
    {      
        protected override IEnumerable<double> GetData(IEnumerable<Bar> source)
        {
            return source.Select(b => GetData(b));
        }

        protected override double GetData(DataBar bar)
        {
            return bar.Close > bar.Open ? 1:0;
        }
    }
}
