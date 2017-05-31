using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_13_Repeat
{
    public class StoreDataGlobalClient : IExternalScript
    {
        public void Execute(IContext ctx, ISecurity sec)
        {
            var cache = ctx.LoadGlobalObject("counter");
            var counter = cache == null ? 0 : (int)cache;          
            
            ctx.LogInfo("111   {0}", counter);         
        }
    }
}
