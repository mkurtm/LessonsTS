﻿using System;
using System.Collections.Generic;
using TSLab.Script.Handlers;

namespace Day_13_Repeat
{
    [HandlerCategory("Marat")]
    [HandlerName("Correlation")]
    public class Correlation : ITwoSourcesHandler, IStreamHandler, IDoubleInputs, IDoubleReturns
    {
        private int _period;

        [HandlerParameter(Name = "period", Default = "20")]
        public int Period
        {
            get { return _period; }

            set
            {
                if (value < 5)
                    throw new System.InvalidOperationException("Период должен быть больше 0");
                _period = value;
            }
        }


        public IList<double> Execute(IList<double> list0, IList<double> list1)
        {
            if (list0.Count != list1.Count)
                throw new ArgumentException("Списки разной длины.");

            var count = list0.Count;

            var values = new double[count];

            var incr0 = list0.IncrementsPcnt();
            var incr1 = list1.IncrementsPcnt();

            var input0 = new double[Period];
            var input1 = new double[Period];

            for (int i = 0; i < count; i++)
            {
                if (i < Period)
                {
                    values[i] = 0;
                    continue;
                }

                for (int j = 0; j < Period; j++)
                {
                    input0[Period - j - 1] = incr0[i - j - 1];
                    input1[Period - j - 1] = incr0[i - j ];
                }

                var corrCoeff = alglib.pearsoncorr2(input0, input1);
                values[i] = corrCoeff;

            }
            return values;
        }
    }
}
