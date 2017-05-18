using System;

namespace ConsoleApp1
{
    /// <summary>
    /// ����������� �����. ����� ������� ����� ���������� ������� ���� � ����� �����
    /// </summary>
    interface ICandle
    {
        #region ��������

        /// <summary>
        /// ���� �������� �����.
        /// </summary>
        double Open { get; }
        /// <summary>
        /// ���� �������� �����.
        /// </summary>
        double Close { get; }
        /// <summary>
        /// �������� ����.
        /// </summary>
        double High { get; }
        /// <summary>
        ///  ������� ����.
        /// </summary>
        double Low { get; }

        /// <summary>
        /// ���� ��������
        /// </summary>
        DateTime OpenDate { get; }
        /// <summary>
        /// ���� ��������
        /// </summary>
        DateTime CloseDate { get; }

        /// <summary>
        /// ���������� ����� ���� �����. 
        /// ���� ����� ����� �� ������������� �����, ��� ������ ����� �������������.
        /// </summary>
        double Body { get; }
        /// <summary>
        /// ������ ����� �����. 
        /// ��� ����� �������������, ��� ������ �������������.
        /// </summary>
        double Length { get; }

        /// <summary>
        /// ���������� ������ ���� ���� �������� ������ ��������.
        /// </summary>
        bool IsWhite { get; }
        /// <summary>
        /// ���������� ������ ���� ���� �������� ������ ���� ��������.
        /// </summary>
        /// <returns></returns>
        bool IsBlack { get; }
        /// <summary>
        /// ���������� ������ ���� ���� �������� ����� ���� ��������.
        /// </summary>
        bool IsDodj { get; }

        #endregion

        #region ������


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