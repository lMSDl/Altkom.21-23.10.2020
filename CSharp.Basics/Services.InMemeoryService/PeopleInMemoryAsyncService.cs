using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.InMemeoryService
{
    public class PeopleInMemoryAsyncService : ICrudAsyncService<Person>, IDisposable
    {
        private static int LastId { get; set; } = 10;

        private static ICollection<Person> People { get; set; }

        public PeopleInMemoryAsyncService(ICollection<Person> people)
        {
            People = people;
        }
        public PeopleInMemoryAsyncService()
        {
            People = new List<Person>();
            CreateAsync(new Person() { FirstName = "Ewa", LastName = "Warszawianka", BirthDate = new DateTime(1986, 1, 23) });
            CreateAsync(new Person() { FirstName = "Adam", BirthDate = new DateTime(1963, 12, 12) });
            CreateAsync(new Person() { FirstName = "Kevin", LastName = "Liu", BirthDate = new DateTime(1997, 11, 20) });
            CreateAsync(new Person() { FirstName = "Martin", LastName = "Weber", BirthDate = new DateTime(1986, 10, 20) });
            CreateAsync(new Person() { FirstName = "George", LastName = "Li", BirthDate = new DateTime(1984, 1, 19) });
            CreateAsync(new Person() { FirstName = "Lisa", LastName = "Miller", BirthDate = new DateTime(1956, 2, 3) });
            CreateAsync(new Person() { FirstName = "Run", LastName = "Liu", BirthDate = new DateTime(1965, 3, 9) });
            CreateAsync(new Person() { FirstName = "Sean", LastName = "Stewart", BirthDate = new DateTime(2000, 4, 16) });
            CreateAsync(new Person() { FirstName = "Olinda", LastName = "Turner", BirthDate = new DateTime(1999, 5, 22) });
            CreateAsync(new Instructor() { FirstName = "Jon", LastName = "Orton", BirthDate = new DateTime(1990, 6, 5), Specialization = "Cooking" });
            CreateAsync(new Person() { FirstName = "Toby", LastName = "Nixon", BirthDate = new DateTime(1988, 7, 12) });
            CreateAsync(new Person() { FirstName = "Daniela", LastName = "Guinot", BirthDate = new DateTime(1980, 8, 8) });
            CreateAsync(new Person() { FirstName = "Vijay", BirthDate = new DateTime(1975, 10, 7) });
            CreateAsync(new Person() { FirstName = "Chris", LastName = "Meyer", BirthDate = new DateTime(1964, 9, 5) });
            CreateAsync(new Person() { FirstName = "Eric", LastName = "Gruber", BirthDate = new DateTime(1982, 11, 4) });
            CreateAsync(new Person() { FirstName = "Yuhong", LastName = "Li", BirthDate = new DateTime(1963, 2, 3) });
            CreateAsync(new Person() { FirstName = "Yan", LastName = "Li", BirthDate = new DateTime(1942, 1, 2) });
            CreateAsync(new Student() { FirstName = "Piotr", LastName = "Piotrowski", BirthDate = new DateTime(1950, 11, 1), IndexNo = 802112 });
        }

        public Task CreateAsync(Person person)
        {
            person.Id = ++LastId;
            People.Add(person);
            return Task.FromResult(0);
        }

        public Task<Person> ReadAsync(int id)
        {
            /*foreach(var person in People)
            {
                if (person.Id == id)
                    return person;
            }
            return null;*/

            //return from person in People where person.Id == id select person;

            return Task.FromResult(People.Where(person => person.Id == id)/*.Select(person => person)*/.SingleOrDefault());
        }

        public Task<IEnumerable<Person>> ReadAsync()
        {
            return Task.FromResult((IEnumerable<Person>)new List<Person>(People));
        }

        public async Task UpdateAsync(Person person)
        {
            var localPerson = await ReadAsync(person.Id);
            if (localPerson == null)
                return;

            People.Remove(localPerson);
            People.Add(person);
        }

        public async Task DeleteAsync(int id)
        {
            var localPerson = await ReadAsync(id);
            if (localPerson == null)
                return;
            People.Remove(localPerson);
        }

        public void Dispose()
        {
            People = null;
        }
    }
}
