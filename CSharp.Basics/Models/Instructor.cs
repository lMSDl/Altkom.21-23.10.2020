using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Instructor : Person
    {
        public string Specialization { get; set; }

        public Instructor()
        {
            ProtectedValue = nameof(Instructor);
        }

        public override string GetPersonalInfo()
        {
            return $"{Specialization} {ProtectedValue}";
        }
    }
}
