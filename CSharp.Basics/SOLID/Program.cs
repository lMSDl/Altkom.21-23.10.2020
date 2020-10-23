using SOLID.LSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = 5;
            var b = 3;

            Square shape = new Rectangle();
            FillSquare(a, shape);

            FillRectangle(a, b, (Rectangle)shape);

            Console.ReadLine();
        }
        private static void FillSquare(int a, Square square)
        {
            square.A = a;

            Console.WriteLine($"{a} * {a} = {square.Area}");
        }
        private static void FillRectangle(int a, int b, Rectangle rectangle)
        {
            rectangle.A = a;
            rectangle.B = b;

            Console.WriteLine($"{a} * {b} = {rectangle.Area}");
        }
    }
}
