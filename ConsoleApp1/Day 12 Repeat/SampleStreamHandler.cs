﻿using System.Collections.Generic;
using TSLab.Script.Handlers;

namespace Day_12_Repeat
{
    [HandlerCategory("Marat")]
    [HandlerName("SampleStreamHandler")]  
    public class SampleStreamHandler : IOneSourceHandler, IStreamHandler, IDoubleInputs, IDoubleReturns
    {        
        public IList<double> Execute(IList<double> source)
        {
            return source;
        }
    }
}
