using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;
using TSLab.Script.Optimization;
using TSLab.DataSource;
using ConsoleApp1;

namespace Day_12_Repeat
{
    [HandlerCategory("Marat")]
    [HandlerName ("First")]
    [HandlerDecimals(3)]

    public class Class1 : IDouble2DoubleHandler
    {     
        public IList<double> Execute(IList<double> source)
        {
            return source;
        }
    }
}
