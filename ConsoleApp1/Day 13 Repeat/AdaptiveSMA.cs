using System;
using System.Collections.Generic;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;

namespace Day_13_Repeat
{
    [HandlerCategory("Marat")]
    [HandlerName("AdaptiveSMA")]

    public class AdaptiveSMA : ITwoSourcesHandler, ISecurityInput0, IDoubleInput1, IDoubleReturns, IValuesHandlerWithNumber, IContextUses
    {
        public IContext Context { set; private get; }
        private int _period;
        private double[] _data;

        [HandlerParameter(Name = "period", Default = "20")]
        public int Period
        {
            get { return _period; }

            set
            {
                if (value < 1)
                    throw new System.InvalidOperationException("Период должен быть больше 0");
                _period = value;
            }
        }

        public double Execute(ISecurity sec, double value, int barNum)
        {
            if (_data == null)
                _data = new double[Context.BarsCount];

            _data[barNum] = value;

            return Execute(sec, _data, barNum);
        }

        private double Execute(ISecurity sec, IList<double> data, int barNum)
        {
            var adx = Context.GetData("adx", new string[] { },
                    () =>
                    {
                        var adxHnd = new ADXFull { Context = Context, Period = Period };
                        return adxHnd.Execute(sec);
                    });
            var smaPeriod = Math.Abs(Convert.ToInt32(adx[barNum]));

            var sma = Series.SMA(data, smaPeriod);
            return sma[barNum];
        }
    }
}
