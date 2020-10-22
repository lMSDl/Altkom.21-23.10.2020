using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InMemeoryService
{
    public class PeopleService : IPeopleService
    {
        private static int LastId { get; set; }

        static ICollection<Person> People { get; } = new List<Person>();

        public PeopleService()
        {
            Create(new Person() { FirstName = "Ewa", LastName = "Warszawianka", BirthDate = new DateTime(1986, 1, 3) });
            Create(new Person() { FirstName = "Adam", LastName = "Adamski" });
        }

        public void Create(Person person)
        {
            person.Id = ++LastId;
            People.Add(person);
        }

        public Person Read(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> Read()
        {
            return new List<Person>(People);
        }

        public void Update(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
