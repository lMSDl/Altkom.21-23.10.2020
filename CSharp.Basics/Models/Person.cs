using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Person : Base
    {
        public int Id { get; set; }
        public DateTime BirthDate { get; set; }

        protected string ProtectedValue { get; set; } = nameof(Person);
        private string PrivateValue { get; set; } = "private";

        [JsonIgnore]
        public Person Partner { get; set; }

        public override string GetPersonalInfo()
        {
            return BirthDate.ToShortDateString();
        }

        public virtual string SomeMethod()
        {
            return "";
        }

        public override string ToString()
        {
            //var personInfo = string.Format("{0, -15}{1, -15}{2, -10}", LastName, FirstName, BirthDate.ToShortDateString());
            var personInfo = $"{Id,-3}{LastName,-15}{FirstName,-15}{BirthDate.ToShortDateString(),-10}, {GetPersonalInfo()}, {ProtectedValue}, {PrivateValue}";

            return personInfo;
        }
    }
}
