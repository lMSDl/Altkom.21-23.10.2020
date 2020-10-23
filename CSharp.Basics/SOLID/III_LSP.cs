using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.LSP
{
    abstract class Shape
    {
        public abstract double Area { get; }
    }

    class Square : Shape
    {
        public int A { get; set; }
        public override double Area => A * A;
    }

    class Rectangle : Square
    {
        public int B { get; set; }

        public new double Area => A * B;
    }

    //class Square : Rectangle
    //{
    //    public override int A { set { base.A = base.B = value; } }
    //    public override int B { set { A = value; } }
    //}

    //class Rectangle : Shape
    //{
    //    public virtual int A { get; set; }
    //    public virtual int B { get; set; }

    //    public override double Area => A * B;
    //}
}
