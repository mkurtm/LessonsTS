using System;
using System.Collections.Generic;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_9._2_Repeat
{
    /// <summary>
    /// Вспомогательный класс, содержащий хелпер методы. В том числе и методы расширения.
    /// </summary>
    public static class TradeHelper
    {
        /// <summary>
        /// Производит вычитание двух коллекций. 
        /// Из первой вычитает вторую и возвращает коллекцию с элементами равными разности элементов коллекций 1 и 2.
        /// Если колллекции разной длины, то вернет null.
        /// </summary>
        /// <param name="list">Коллекция из которой вычитать</param>
        /// <param name="subtrList">Колллекция которую будет вычитать.</param>
        /// <returns></returns>
        public static IList<double> Subtract(this IList<double> list, IList<double> subtrList)
        {
            // Если длины коллекций различаются то просто вернем null как знак ошибки.
            if (list.Count != subtrList.Count)
                throw new ArgumentException("Длинна списка должна быть одинакова"); 

            // Создаем массив, и забиваем его разностями элементов списков.
            var res = new double[list.Count];
            for (var i = 0; i < list.Count; i++)
                res[i] = list[i] - subtrList[i];


            return res;
        }
        
    }
}

