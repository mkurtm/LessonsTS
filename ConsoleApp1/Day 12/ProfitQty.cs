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

    [HandlerCategory("Marat")]
    [HandlerName("MAEDate")]
    public class MAEDate :  IPosition2Double
    //public class MAEDate : IOneSourceHandler, IValuesHandler, IPositionInputs, IDoubleReturns
    {
        public double Execute(IPosition pos, int barNum)
        {
            if (pos == null)
            {
                return 0.0;
            }            
            return (double)(pos.MAEDate().Year % 100) * 10000.0 + (double)pos.MAEDate().Month * 100.0 + (double)pos.MAEDate().Day;            
        }           
    }

    [HandlerCategory("Marat")]
    [HandlerName("MAETime")]
    public class MAETime : IPosition2Double
    //public class MAETime : IOneSourceHandler, IValuesHandler, IPositionInputs, IDoubleReturns
    //public class SampleValuesHandler : IOneSourceHandler, IValuesHandler, IDoubleInputs, IDoubleReturns
    {
        public double Execute(IPosition pos, int barNum)
        {
            if (pos == null)
            {
                return 0.0;
            }
            return (double)pos.MAEDate().Hour * 10000.0 + (double)pos.MAEDate().Minute * 100.0 + (double)pos.MAEDate().Second;
        }
    }
}



   

