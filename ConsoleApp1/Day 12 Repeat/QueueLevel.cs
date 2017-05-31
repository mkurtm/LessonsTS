using System.Collections.Generic;
using System;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_12_Repeat
{
    [HandlerCategory("Marat")]
    [HandlerName("Уровни стакана")]
    [HandlerDecimals(3)]
    public class QueueLevel : IBar2DoubleHandler
    {
        private int _level;

        [HandlerParameter(Name = "Уровень", Default = "1")]
        public int Level
        {
            get
            {
                return _level;
            }
            set
            {
                if (value == null)
                    throw new InvalidOperationException("Уровень не может быть равен 0.");
                _level = Level;
            }
        }

        public IList<double> Execute(ISecurity sec)
        {
            var cnt = sec.Bars.Count;
            var values = new double[cnt];

            //запросим стаканы
            var buyQ = sec.GetBuyQueue(0);
            var sellQ = sec.GetSellQueue(0);

            var result = 0.0;

            if (Level > 0 && buyQ.Count >= Math.Abs(Level))     
                result = buyQ[-Level - 1].Price;
        
            if (Level < 0 && sellQ.Count >= Math.Abs(Level))            
                result = sellQ[Level - 1].Price;            
    
            for (int i = 0; i < values.Length; i++)          
                values[i] = result;

            return values;
        }
    }
}
