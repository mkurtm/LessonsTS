using System;

namespace ConsoleApp1
{
    /// <summary>
    /// Сферическая свеча. Самый базовый набор параметров которые есть в любой свече
    /// </summary>
    interface ICandle
    {
        #region Свойства

        /// <summary>
        /// Цена открытия свечи.
        /// </summary>
        double Open { get; }
        /// <summary>
        /// Цена закрытия свечи.
        /// </summary>
        double Close { get; }
        /// <summary>
        /// Максимум цены.
        /// </summary>
        double High { get; }
        /// <summary>
        ///  Минимум цены.
        /// </summary>
        double Low { get; }

        /// <summary>
        /// Дата открытия
        /// </summary>
        DateTime OpenDate { get; }
        /// <summary>
        /// Дата закрытия
        /// </summary>
        DateTime CloseDate { get; }

        /// <summary>
        /// Возвращает длину тела свечи. 
        /// Если свеча белая то положительное число, для черной свечи отрицательное.
        /// </summary>
        double Body { get; }
        /// <summary>
        /// Полная длина свечи. 
        /// Для белой положительная, для черной отрицательная.
        /// </summary>
        double Length { get; }

        /// <summary>
        /// Возвращает истину если цена закрытия больше открытия.
        /// </summary>
        bool IsWhite { get; }
        /// <summary>
        /// Возвращает истину если цена открытия больше цены закрытия.
        /// </summary>
        /// <returns></returns>
        bool IsBlack { get; }
        /// <summary>
        /// Возвращает истину если цена закрытия равна цене открытия.
        /// </summary>
        bool IsDodj { get; }

        #endregion

        #region Методы


        #endregion

    }

    interface IMarketVolume
    {
        double Volume { get; }
    }

    interface IForts
    {
        double OpenInterest { get; }
        double CloseInterest { get; }
        double HighInterest { get; }
        double LowInterest { get; }
    }

    interface IMarketSecurityName
    {
        string SecurityName { get; }
    }

    class StockCandle : ICandle, IMarketVolume, IMarketSecurityName
    {
        public double Open => throw new NotImplementedException();

        public double Close => throw new NotImplementedException();

        public double High => throw new NotImplementedException();

        public double Low => throw new NotImplementedException();

        public DateTime OpenDate => throw new NotImplementedException();

        public DateTime CloseDate => throw new NotImplementedException();

        public double Body => throw new NotImplementedException();

        public double Length => throw new NotImplementedException();

        public bool IsWhite => throw new NotImplementedException();

        public bool IsBlack => throw new NotImplementedException();

        public bool IsDodj => throw new NotImplementedException();

        public double Volume => throw new NotImplementedException();

        public string SecurityName => throw new NotImplementedException();
    }

    class FortsCandle : StockCandle, IForts
    {
        public double OpenInterest => throw new NotImplementedException();

        public double CloseInterest => throw new NotImplementedException();

        public double HighInterest => throw new NotImplementedException();

        public double LowInterest => throw new NotImplementedException();
    }
}