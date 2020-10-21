using ConsoleApp.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using ConsoleApp.Properties;

namespace ConsoleApp
{
    public class Program
    {
        static ICollection<Person> People { get; } = new List<Person>() {
            new Person() { FirstName = "Ewa", LastName = "Warszawianka", BirthDate = new DateTime(1986, 1, 3) },
            new Person() { FirstName = "Adam", LastName = "Adamski" }
        };

        static void Main(string[] args)
        {
            //for(var i = 0; i < People.Count; i++)
            //{
            //    var person = People[i];
            //}

            bool isContinue = true;
            while (isContinue)
            {
                Console.Clear();
                foreach (var person in People)
                {
                    //var personInfo = string.Format("{0, -15}{1, -15}{2, -10}", person.LastName, person.FirstName, person.BirthDate.ToShortDateString());
                    var personInfo = $"{person.Id,-3}{person.LastName,-15}{person.FirstName,-15}{person.BirthDate.ToShortDateString(),-10}";
                    Console.WriteLine(personInfo);
                }

                var input = Console.ReadLine();
                if (Compare(input, "exit"))
                {
                    isContinue = false;
                }
                else if (Compare(input, "edit"))
                {
                    Edit();
                }
                else if (Compare(input, "add"))
                {
                    Add();
                }
            }

            Console.WriteLine(Resources.Goodbye);
            Console.ReadLine();
        }

        private static void Add()
        {
            var person = new Person();
            Edit(person);
            People.Add(person);
        }

        private static bool Compare(string input, string value)
        {
            return string.Compare(input, value, ignoreCase: true) == 0;
        }

        private static void Edit(Person personToEdit = null)
        {
            if (personToEdit == null)
            {
                string input;
                int id;
                do
                {
                    Console.Write(Resources.Id);
                    input = Console.ReadLine();
                }
                while (!int.TryParse(input, out id));

                //var personToEdit = from person in People where person.Id == id select person;
                personToEdit = People.Where(person => person.Id == id)/*.Select(person => person)*/.SingleOrDefault();

                if (personToEdit == null)
                {
                    Console.WriteLine(Resources.IdNotFound);
                    Console.ReadLine();
                    return;
                }
            }

            personToEdit.LastName = ReadData(Resources.LastName, personToEdit.LastName);
            personToEdit.FirstName = ReadData(Resources.FirstName, personToEdit.FirstName);

            string data;
            DateTime birthDate;
            do
            {
                data = ReadData(Resources.BirthDate, personToEdit.BirthDate.ToShortDateString());
            }
            while(!DateTime.TryParse(data, out birthDate));

            personToEdit.BirthDate = birthDate;
        }

        private static string ReadData(string label, string defaultValue)
        {
            string input;
            Console.Write($"{label}: ");
            input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return defaultValue;
            return input;
        }
    }
}
