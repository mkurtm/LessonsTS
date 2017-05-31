using System;
using System.Collections.Generic;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;

namespace Day_13_Repeat
{
    [HandlerCategory("Marat")]
    [HandlerName("AdaptiveSMABest")]

    public class AdaptiveSMABest : ITwoSourcesHandler, ISecurityInput0, IDoubleInput1, IDoubleReturns, IStreamHandler, IContextUses
    {
        public IContext Context { set; private get; }
        private int _period;
        
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

        public IList<double> Execute(ISecurity sec, IList<double> data)
        {
            var adx = Context.GetData("adx", new string[] { Period.ToString()},
                    () =>
                    {
                        var adxHnd = new ADXFull { Context = Context, Period = Period };
                        return adxHnd.Execute(sec);
                    });

            var values = new double[Context.BarsCount];

            for (int i = 0; i < Context.BarsCount; i++)
            {
                var smaPeriod = Math.Abs(Convert.ToInt32(adx[i]));
                var sma = Context.GetData("sma",new string[] { smaPeriod.ToString()},
                    ()=> Series.SMA(data, smaPeriod));
                values[i] = sma[i];
            }
            return values;
        }
    }
}
