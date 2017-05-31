using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_13_Repeat
{
    public class StoreDataStatic : IExternalScript
    {
        private static int _counter;

        public void Execute(IContext ctx, ISecurity sec)
        {
            _counter++;
            ctx.LogInfo("{0}", _counter);
        }
    }
}
