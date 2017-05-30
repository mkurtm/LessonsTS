using System;
using System.Collections.Generic;
using System.Linq;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_12
{

    [HandlerCategory("Marat")]
    [HandlerName("ProfitQty")]
    public class ProfitQty : IBar2ValueDoubleHandler
    {
        [HandlerParameter(Name = "Минимальный профит", Default = "1")]
        public double K { get; set; }

        public double Execute(ISecurity sec, int barNum)
        {
            var poses = sec.Positions.GetClosedOrActiveForBar(barNum);
            var count = 0;
            foreach (var pos in poses)            
                if (pos.ProfitPct()>K)                
                    count++;  
            return count;
        }
    }

    //[HandlerCategory("Marat")]
    //[HandlerName("ProfitQty")]
    //public class ProfitQty : IBar2ValueDoubleHandler
    //{
    //    [HandlerParameter(Name = "Минимальный профит", Default = "1")]
    //    public double K { get; set; }

    //    public double Execute(ISecurity sec, int barNum)
    //    {
    //        var count = 0;
    //        for (int i = 0; i < barNum; i++)
    //        {
    //            var positions = sec.Positions.GetClosedOrActiveForBar(i);
    //            foreach (var pos in positions)
    //                if (pos.ProfitPct() > K)
    //                    count++;
    //        }
    //        return count;
    //    }
    //}

    //[HandlerCategory("Marat")]
    //[HandlerName("ProfitQty2")]
    //public class ProfitQty2 : IBar2DoubleHandler
    //{
    //    [HandlerParameter(Name = "Минимальный профит", Default = "1")]
    //    public double M { get; set; }

    //    public IList<double> Execute(ISecurity sec)
    //    {
    //        var numPlus = new List<Double>();

    //        for (int i = 0; i < sec.Bars.Count; i++)
    //        {
    //            var count = 0;
    //            var positions = sec.Positions.GetClosedOrActiveForBar(i);

    //            foreach (var pos in positions)
    //                if (pos.ProfitPct() > M)
    //                    count++;

    //            numPlus.Add(count);
    //        }
    //        return numPlus;
    //    }
    //}           
        
 
}
   

