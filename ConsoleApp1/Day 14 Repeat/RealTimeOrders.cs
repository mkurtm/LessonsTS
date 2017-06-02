using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Realtime;
using TSLab.DataSource;


namespace Day_14_Repeat
{
    public class RealTimeOrders : IExternalScript
    {
        public void Execute(IContext ctx, ISecurity sec)
        {
            var rtSec = sec as ISecurityRt;
            if (rtSec == null)
            {
                ctx.LogInfo("Not real time");
                return;
            }

            var currPos = 0.0;
            var cancelFlag = false;
            foreach (var order in rtSec.Orders)
            {
                if (order.IsActive)
                {
                    ctx.LogInfo("entry bar {0}", order.FindBarNumber(sec.Bars));
                    rtSec.CancelOrder(order);
                    cancelFlag = true;
                }

                if (order.IsExecuted)
                    currPos += (order.Quantity - order.RestQuantity);
            }

            if (!cancelFlag)
                rtSec.NewOrder(OrderType.Limit, true, (double)rtSec.FinInfo.Bid * 0.998, 1, "LE");
        }
    }
}
