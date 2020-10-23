using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Student : Person
    {
        public int IndexNo { get; set; }

        public void GoToSchool()
        {
            Console.WriteLine("Idę do szkoły");
        }

        public override string GetPersonalInfo()
        {
            return base.GetPersonalInfo() + ", " + IndexNo.ToString();
        }
    }
}
