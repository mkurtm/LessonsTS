using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Candle
    {
        double _open;
        double _close;
        double _high;
        double _low;

        double _volume;
        DateTime _openDate;
        DateTime _closeDate;

        public double Open
        {
            get
            {
                GetCounter++;
                return _open;
            }
            set
            {
                _open = value;
                SetCounter++;
            }
        }
       
        public double Close
        {
            get
            {
                GetCounter++;
                return _close;
            }
            set
            {
                _close = value;
                SetCounter++;
            }
        }

        public double High
        {
            get
            {
                GetCounter++;
                return _high;
            }
            set
            {
                _high = value;
                SetCounter++;
            }
        }

        public double Low
        {
            get
            {
                GetCounter++;
                return _low;
            }
            set
            {
                _low = value;
                SetCounter++;
            }
        }

        public double Volume
        {
            get
            {
                GetCounter++;
                return _volume;
            }
            set
            {
                _volume = value;
                SetCounter++;
            }
        }

        public DateTime OpenDate
        {
            get
            {
                GetCounter++;
                return _openDate;
            }
            set
            {
                _openDate = value;
                SetCounter++;
            }
        }

        public DateTime CloseDate
        {
            get
            {
                GetCounter++;
                return _closeDate;
            }
            set
            {
                _closeDate = value;
                SetCounter++;
            }
        }

        public ulong GetCounter { get; private set; }
        public ulong SetCounter { get; private set; }

        public Candle()
        {

            
        }

        public Candle(double open, double close, double high,
                       double low, double volume, DateTime openDate,
                       DateTime closeDate)
        {
            Open = open;
            Close = close;
            High = high;
            Low = low;
            Volume = volume;
            OpenDate = openDate;
            CloseDate = closeDate;
        }

        protected void GetPlus()
        {
            GetCounter++;
        }

        protected void SetPlus()
        {
            SetCounter++;
        }
    }
    


    class TickCandle : Candle
    {
        int _tickFrame;

        public int TickFrame
        {
            get
            {
                GetPlus();
                return _tickFrame;
            }
            set
            {
                _tickFrame = value;
                SetPlus();
            }
        }        

        public TickCandle(): base()
        {
            Open = 1;
            Close = 1;
            High = 1;
            Low = 1;
            Volume = 1;
            OpenDate = new DateTime(2001, 1, 1, 0, 0, 0);
            CloseDate = new DateTime(2001, 1, 1, 0, 0, 0);
        }

        public TickCandle(double open, double close, double high, double low, double volume, DateTime openDate, DateTime closeDate) : base(open, close, high, low, volume, openDate, closeDate)
        {
            
        }

        public TickCandle(double open, double close, double high, double low) : this ()
        {
            Open = open;
            Close = close;
            High = high;
            Low = low;
        }
    }
}
