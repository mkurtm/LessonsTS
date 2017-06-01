using System;
using System.Linq;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_14_Repeat
{
    public class InitDepo : IExternalScript
    {
        public void Execute(IContext ctx, ISecurity sec)
        {
            var depo = sec.InitDeposit;

            ctx.LogInfo("{0}", depo);
        }
    }
}
