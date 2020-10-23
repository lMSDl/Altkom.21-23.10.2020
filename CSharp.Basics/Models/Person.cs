using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        [JsonIgnore]
        public Person Partner { get; set; }


        public override string ToString()
        {
            //var personInfo = string.Format("{0, -15}{1, -15}{2, -10}", LastName, FirstName, BirthDate.ToShortDateString());
            var personInfo = $"{Id,-3}{LastName,-15}{FirstName,-15}{BirthDate.ToShortDateString(),-10}";

            return personInfo;
        }
    }
}
