using System.Collections.Generic;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;

namespace Day_12_Repeat
{
    [HandlerCategory("Marat")]
    [HandlerName("Sma")]
    [HandlerDecimals(3)]
    public class sma : IDouble2DoubleHandler
    {
        private int _period;

        [HandlerParameter(Name = "period",  Default = "20")]
        public int Period {
            get { return _period; }

            set
            {
                if (value < 1)
                    throw new System.InvalidOperationException("Период должен быть больше 0");
                _period = value;
            }
        }

        public IList<double> Execute(IList<double> source)
        {
            return Series.SMA(source, Period);
        }
    }
}
