using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_12_Repeat
{
    [HandlerCategory("Marat")]
    [HandlerName("currentPosByCycle")]
    public class currentPosByCycle : IBar2ValueDoubleHandler
    {
        public double Execute(ISecurity sec, int barNum)
        {
            var pos = sec.Positions.GetActiveForBar(barNum);

            var overall = 0;
            foreach (var position in pos)
            {
                if (position.IsLong)                
                    overall += (int)position.Shares;                
                else
                    overall -= (int)position.Shares;
            }

            return overall;
        }
    }
}
