using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_13_Repeat
{
    public class StoreDataSaveLoadStatic : IExternalScript
    {
        public void Execute(IContext ctx, ISecurity sec)
        {
            var cache = ctx.LoadObject("counter");
            var counter = cache == null ? 0 : (int)cache;
            counter++;
            ctx.LogInfo("{0}", counter);
            ctx.StoreObject("counter", counter);

        }
    }
}
