using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InMemeoryService
{
    public class PeopleInMemoryService : ICrudService<Person>
    {
        private static int LastId { get; set; } = 10;

        private static ICollection<Person> People { get; set; }

        public PeopleInMemoryService(ICollection<Person> people)
        {
            People = people;
        }
        public PeopleInMemoryService()
        {
            People = new List<Person>();
            Create(new Person() { FirstName = "Ewa", LastName = "Warszawianka", BirthDate = new DateTime(1986, 1, 23) });
            Create(new Person() { FirstName = "Adam", BirthDate = new DateTime(1963, 12, 12) });
            Create(new Person() { FirstName = "Kevin", LastName = "Liu", BirthDate = new DateTime(1997, 11, 20) });
            Create(new Person() { FirstName = "Martin", LastName = "Weber", BirthDate = new DateTime(1986, 10, 20) });
            Create(new Person() { FirstName = "George", LastName = "Li", BirthDate = new DateTime(1984, 1, 19) });
            Create(new Person() { FirstName = "Lisa", LastName = "Miller", BirthDate = new DateTime(1956, 2, 3) });
            Create(new Person() { FirstName = "Run", LastName = "Liu", BirthDate = new DateTime(1965, 3, 9) });
            Create(new Person() { FirstName = "Sean", LastName = "Stewart", BirthDate = new DateTime(2000, 4, 16) });
            Create(new Person() { FirstName = "Olinda", LastName = "Turner", BirthDate = new DateTime(1999, 5, 22) });
            Create(new Person() { FirstName = "Jon", LastName = "Orton", BirthDate = new DateTime(1990, 6, 5) });
            Create(new Person() { FirstName = "Toby", LastName = "Nixon", BirthDate = new DateTime(1988, 7, 12) });
            Create(new Person() { FirstName = "Daniela", LastName = "Guinot", BirthDate = new DateTime(1980, 8, 8) });
            Create(new Person() { FirstName = "Vijay", BirthDate = new DateTime(1975, 10, 7) });
            Create(new Person() { FirstName = "Chris", LastName = "Meyer", BirthDate = new DateTime(1964, 9, 5) });
            Create(new Person() { FirstName = "Eric", LastName = "Gruber", BirthDate = new DateTime(1982, 11, 4) });
            Create(new Person() { FirstName = "Yuhong", LastName = "Li", BirthDate = new DateTime(1963, 2, 3) });
            Create(new Person() { FirstName = "Yan", LastName = "Li", BirthDate = new DateTime(1942, 1, 2) });
            Create(new Person() { FirstName = "Piotr", LastName = "Piotrowski", BirthDate = new DateTime(1950, 11, 1) });

            Read(13).Partner = Read(11);
            Read(11).Partner = Read(13);
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

        public void Delete(int id)
        {
            var localPerson = Read(id);
            if (localPerson == null)
                return;
            People.Remove(localPerson);
        }
    }
}
