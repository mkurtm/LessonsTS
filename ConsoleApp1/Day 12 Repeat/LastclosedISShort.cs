using System;
using System.Linq;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_12_Repeat
{
    [HandlerCategory("Marat")]
    [HandlerName("LastclosedISShort")]
    public class LastclosedISShort : IBar2BoolHandler
    {
        public bool Execute(ISecurity sec, int barNum)
        {
            if (!sec.Positions.Any())            
                return false;

            var close = sec.Positions.GetLastPositionClosed(barNum);
            if (close==null)            
                return false;
            
            return close.IsShort;            
        }
    }
}
