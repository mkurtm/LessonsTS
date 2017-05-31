using System;
using System.Collections.Generic;
using System.Linq;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace Day_13_Repeat
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
                return null;

            // Создаем массив, и забиваем его разностями элементов списков.
            var res = new double[list.Count];
            for (var i = 0; i < list.Count; i++)
                res[i] = list[i] - subtrList[i];


            return res;
        }

        /// <summary>
        /// Производит вычитание двух коллекций. 
        /// Из первой вычитает вторую и возвращает коллекцию с элементами равными разности элементов коллекций 1 и 2.
        /// Если колллекции разной длины, то вернет null.
        /// </summary>
        /// <param name="list">Коллекция из которой вычитать</param>
        /// <param name="subtrList">Колллекция которую будет вычитать.</param>
        /// <returns></returns>
        public static IList<int> Subtract(this IList<int> list, IList<int> subtrList)
        {
            // Если длины коллекций различаются то просто вернем null как знак ошибки.
            if (list.Count != subtrList.Count)
                return null;

            // Создаем массив, и забиваем его разностями элементов списков.
            var res = new int[list.Count];
            for (var i = 0; i < list.Count; i++)
                res[i] = list[i] - subtrList[i];


            return res;
        }

        /// <summary>
        /// Производит сложение двух коллекций. 
        /// К первой коллекции прибавляет вторую и возвращает коллекцию с элементами равными сумме элементов коллекций 1 и 2.
        /// Если колллекции разной длины, то вернет null.
        /// </summary>
        /// <param name="list">Коллекция к которой прибавлять</param>
        /// <param name="subtrList">Колллекция которую будем прибавлять.</param>
        /// <returns></returns>
        public static IList<double> Add(this IList<double> list, IList<double> subtrList)
        {
            // Если длины коллекций различаются то просто вернем null как знак ошибки.
            if (list.Count != subtrList.Count)
                return null;

            // Создаем массив, и забиваем его суммами элементов списков.
            var res = new double[list.Count];
            for (var i = 0; i < list.Count; i++)
                res[i] = list[i] + subtrList[i];


            return res;
        }

        /// <summary>
        /// Производит сложение двух коллекций. 
        /// К первой коллекции прибавляет вторую и возвращает коллекцию с элементами равными сумме элементов коллекций 1 и 2.
        /// Если колллекции разной длины, то вернет null.
        /// </summary>
        /// <param name="list">Коллекция к которой прибавлять</param>
        /// <param name="subtrList">Колллекция которую будем прибавлять.</param>
        /// <returns></returns>
        public static IList<int> Add(this IList<int> list, IList<int> subtrList)
        {
            // Если длины коллекций различаются то просто вернем null как знак ошибки.
            if (list.Count != subtrList.Count)
                return null;

            // Создаем массив, и забиваем его суммами элементов списков.
            var res = new int[list.Count];
            for (var i = 0; i < list.Count; i++)
                res[i] = list[i] + subtrList[i];

            return res;
        }

        /// <summary>
        /// Выводит в лог информационное сообщение.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="str"></param>
        public static void LogInfo(this IContext ctx, string str)
        {
            // Задаем цвет соообщения
            var color = new Color(System.Drawing.Color.Blue.ToArgb());

            ctx.Log(str, color);
        }

        public static bool IsMaribozu(this Bar candle)
        {
            return candle.NoHighShadow() && candle.NoLowShadow();
        }

        public static bool IsMaribozuWhite(this Bar candle)
        {
            return candle.IsWhite() && candle.IsMaribozu();
        }

        public static bool IsMaribozuBlack(this Bar candle)
        {
            return candle.IsBlack() && candle.IsMaribozu();
        }

        public static bool NoHighShadow(this Bar candle)
        {
            if (candle.IsWhite())
                return candle.High == candle.Close;

            return candle.High == candle.Open;
        }

        public static bool NoLowShadow(this Bar candle)
        {
            if (candle.IsWhite())
                return candle.Low == candle.Open;

            return candle.Low == candle.Open;
        }

        public static bool IsWhite(this Bar candle)
        {
            return (candle.Close > candle.Open);
        }

        public static bool IsBlack(this Bar candle)
        {
            return (candle.Close > candle.Open);
        }

        public static double PosSize(this IPosition pos)
        {
            return pos.Shares * pos.Security.LotSize;
        }

        public static double PositionEntryPrice(this IPosition pos)
        {
            return pos.EntryPrice * pos.PosSize();
        }

        public static double AvgEntryPrice(this IList<IPosition> positions)
        {
            var totalPrice = positions.Sum(p => p.PositionEntryPrice());
            var totalSize = positions.Sum(p => p.PosSize());

            return totalPrice / totalSize;
        }

        public static double TotalSize(this IList<IPosition> positions)
        {
            return positions.Sum(p => p.PosSize());
        }

        public static IList<double> IncrementsPcnt(this IList<double> list)
        {
            if (list.Count <2)
                throw new ArgumentException("Число элементов должно быть больше 2.");
          
            var result = new double[list.Count];
            result[0] = 0;

            for (int i = 1; i < list.Count; i++)            
                result[i] = list[i - 1] == 0 ? 0 : (list[i] - list[i - 1]) / list[i - 1];
            
            return result;
        }
    }
}

