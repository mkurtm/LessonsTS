using System;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_12_Repeat
{
    [HandlerCategory("Marat")]
    [HandlerName("mae в %")]
    public class MaePct : IPosition2Double
    {
        public double Execute(IPosition pos, int barNum)
        {
            if (pos == null)
                return 0;

            if (pos.IsActiveForbar(barNum))
                return pos.OpenMAEPct(barNum);

            return pos.MAEPct();
        }
    }
}
