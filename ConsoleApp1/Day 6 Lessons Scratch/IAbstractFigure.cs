using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_6_Lessons_Scratch
{
    interface IAbstractFigure
    {
        string Name { get; }

        double Area();
        double Lenght();
    }

    class AbstractFigure : IAbstractFigure
    {
        public string Name { get; private set; }

        public virtual double Area()
        {
            throw new NotImplementedException();
        }

        public virtual double Lenght()
        {
            throw new NotImplementedException();
        }

        public AbstractFigure(string name)
        {
            Name = name;
        }

        public AbstractFigure()
        {

        }
    }

    class Point : AbstractFigure
    {
        private int _x;
        private int _y;

        public Point(string name, int x, int y) : base(name)
        {
            _x = x;
            _y = y;
        }

        public override double Lenght()
        {
            return 1;
        }

        public override double Area()
        {
            return 1;
        }
    }

    class Line : AbstractFigure
    {
        private int _x1, _y1, _x2, _y2;
        
        public Line(string name, int x1, int y1, int x2, int y2) : base(name)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
        }

        public override double Area()
        {
            return Lenght();
        }

        public override double Lenght()
        {
            double lenght = Math.Sqrt(Math.Pow((_x2-_x1),2)+Math.Pow((_y2-_y1),2));
            return lenght;
        }
    }

    class Circle : AbstractFigure
    {
        private int _x;
        private int _y;
        private int _radius;

        public Circle(string name, int x, int y, int radius) : base(name)
        {
            _x = x;
            _y = y;
            _radius = radius;
        }

        public override double Area()
        {
            return Math.PI * Math.Pow(_radius,2);

        }

        public override double Lenght()
        {
            return 2 * Math.PI * _radius;
        }
    }

    class Square : AbstractFigure
    {
        private int _x1;
        private int _y1;
        private int _x2;
        private int _y2;
        private double _sidelenght;

        public Square(string name, int x1, int y1, int x2, int y2) : base(name)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
            _sidelenght = BuildSquare();
        }

        public override double Lenght()
        {
            return _sidelenght*4;
        }

        public override double Area()
        {
            return Math.Pow(_sidelenght,2);
        }

        double BuildSquare()
        {
            Line line = new Line(this.Name,_x1, _y1, _x2, _y2);
            return line.Lenght();
        }
    }
}
