using TSLab.Script.Handlers;

namespace Day_12_Repeat
{
    [HandlerCategory("Marat")]
    [HandlerName("SampleValuesHandler")]
    [HandlerDecimals(3)]
    public class SampleValuesHandler : IOneSourceHandler, IValuesHandler, IDoubleInputs, IDoubleReturns
    {
        public double Execute(double source, int bar)
        {
            return source + bar;
        }
    }
}
