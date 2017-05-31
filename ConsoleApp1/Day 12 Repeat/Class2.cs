using System.Collections.Generic;
using TSLab.Script.Handlers;

namespace Day_12_Repeat
{
    [HandlerCategory("Marat")]
    [HandlerName("Second")]
    [HandlerDecimals(3)]
    public class Class2 : IDouble2DoubleHandler
    {
        [HandlerParameter(Name = "Test", IsShown = true, NotOptimized =false, Default ="20")]
        public int SampleParametr { get; set; }

        public IList<double> Execute(IList<double> source)
        {
            return source;
        }
    }
}
