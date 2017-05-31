using System.Collections.Generic;
using TSLab.Script.Handlers;
using TSLab.Utils;

namespace Day_12_Repeat
{
    [HandlerCategory("Marat")]
    [HandlerName("MultiplyK")]
    [HandlerDecimals(3)]
    public class MultiplyK : IDouble2DoubleHandler
    {
        private int _k;

        [HandlerParameter(Name = "k", Default = "20")]
        public int k
        {
            get { return _k; }

            set
            {
                if (value < 1)
                    throw new System.InvalidOperationException("Период должен быть больше 0");
                _k = value;
            }
        }

        public IList<double> Execute(IList<double> source)
        {
            var values = new List<double>();
            source.ForEach(d=>values.Add(d*k));
            return values;
        }
    }
}
