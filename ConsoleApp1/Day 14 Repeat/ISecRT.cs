using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Realtime;
using TSLab.DataSource;
using System.Linq;
using TSLab.Script.Realtime;

namespace Day_14_Repeat
{
    public class ISecRT : IExternalScript
    {
        public void Execute(IContext ctx, ISecurity sec)
        {
            var rtSec = sec as ISecurityRt;

            if (rtSec== null)          
                ctx.LogInfo("Мы в лабаратории");
            else
                ctx.LogInfo("We are at the agent");

            var pos = sec.Positions.Where(p => p.IsActive);
            foreach (var position in pos)
            {
                var rtPos = position as IPositionRt;
                var entry = rtPos.EntryOrders;
            }

            var bid = sec.FinInfo.Bid;
            var ask = sec.FinInfo.Ask;

            var buyDepo = sec.FinInfo.BuyDeposit;
            var sellDepo = sec.FinInfo.SellDeposit;

            var prevClose = sec.FinInfo.ClosePrice;

            var upPlank = sec.FinInfo.PriceMax;
            var downPlank = sec.FinInfo.PriceMin;

            //portfolio, только в режиме агента

            var cb = rtSec.CurrencyBalance; // свободных денег
            var bc = rtSec.BalanceQuantity; //сколько лотов бумаги в портфеле
            var eb = rtSec.EstimatedBalance; // чистая стоимость
            var pn = rtSec.PortfolioName; //имя портфеля

            //как посчитать лоты

            var secPrice = sec.SecurityDescription.ActiveType ==ActiveType.Futures 
                ? sec.FinInfo.BuyDeposit 
                : sec.FinInfo.Ask;
            var posSize = cb / (secPrice * sec.LotSize);


        }
    }
}
