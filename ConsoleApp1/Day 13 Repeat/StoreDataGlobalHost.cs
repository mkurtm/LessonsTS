using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_13_Repeat
{
    public class StoreDataGlobalHost : IExternalScript
    {
        public void Execute(IContext ctx, ISecurity sec)
        {
            var cache = ctx.LoadGlobalObject("counter");
            var counter = cache == null ? 0 : (int)cache;
            counter++;
            
            //ctx.LogInfo("{0}", counter);
            ctx.StoreGlobalObject("counter", counter);
        }
    }
}
