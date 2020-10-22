using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InMemeoryService
{
    public class PeopleInMemoryService : IPeopleService
    {
        private static int LastId { get; set; }

        private static ICollection<Person> People { get; } = new List<Person>();

        public PeopleInMemoryService()
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
            /*foreach(var person in People)
            {
                if (person.Id == id)
                    return person;
            }
            return null;*/

            //return from person in People where person.Id == id select person;

            return People.Where(person => person.Id == id)/*.Select(person => person)*/.SingleOrDefault();
        }

        public IEnumerable<Person> Read()
        {
            return new List<Person>(People);
        }

        public void Update(Person person)
        {
            var localPerson = Read(person.Id);
            if (localPerson == null)
                return;

            People.Remove(localPerson);
            People.Add(person);
        }
    }
}
