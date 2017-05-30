using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Realtime;
using ConsoleApp1;

namespace Day_12_Repeat
{
    [HandlerCategory("Marat")]
    [HandlerName("freemoney")]
    public class FreeMoney : IBar2ValueDoubleHandler
    {     
        public double Execute(ISecurity sec, int barNum)
        {
            var secRt = sec as ISecurityRt;

            if (secRt != null)            
                return secRt.CurrencyBalance;

            var freeDepo = sec.InitDeposit;
            foreach (var position in sec.Positions.GetClosedOrActiveForBar(barNum))
            {
                if (position.IsActiveForbar(barNum))                
                    freeDepo -= position.PositionEntryPrice();                
                else
                    freeDepo += position.Profit();
            }
            return freeDepo;
        }
    }
}
