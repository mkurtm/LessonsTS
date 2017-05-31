using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_12
{
    [HandlerCategory("Marat")]
    [HandlerName("LossQty")]

    public class LossQty : IBar2ValueDoubleHandler
    {
        [HandlerParameter(Name = "Минимальный Loss", Default = "1")]
        public double K { get; set; }

        public double Execute(ISecurity sec, int barNum)
        {
            var poses = sec.Positions.GetClosedOrActiveForBar(barNum);
            var count = 0;
            foreach (var pos in poses)
                if (pos.ProfitPct() < -K)
                    count++;
            return count;
        }
    }

}
