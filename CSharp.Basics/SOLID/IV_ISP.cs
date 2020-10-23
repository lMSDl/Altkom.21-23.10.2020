using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    interface IExcelFormatter
    {
        void ToExcel();
    }

    interface IPdfFormatter
    {
        void ToPdf();
    }

    interface IFormatter : IPdfFormatter, IExcelFormatter
    {

    }

    class Report : IFormatter // : IPdfFormatter, IExcelFormatter
    {
        public void ToExcel()
        {
            Console.WriteLine("Generating excel...");
        }

        public void ToPdf()
        {
            Console.WriteLine("Generating pdf...");
        }
    }

    class Poem : IPdfFormatter
    {
        public void ToPdf()
        {
            Console.WriteLine("Generating pdf...");
        }
    }
}
