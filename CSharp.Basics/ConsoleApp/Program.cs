using ConsoleApp.Models;
using System;
using System.Linq;
using System.Collections;
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

        // 1. Zastąpić ciągi tekstowe za pomocą Resource
        // 2. Dodać polecenie "add", które będzie tworzyć nową osobę i dodawać do kolekcji (People.Add(obj))
        // 3. Utworzyć merodę, która porównuje dwa stringi (caseIgnore) i zwraca prawda/fałsz

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
                    var personInfo = $"{person.Id, -3}{person.LastName,-15}{person.FirstName,-15}{person.BirthDate.ToShortDateString(),-10}";
                    Console.WriteLine(personInfo);
                }

                var input = Console.ReadLine();
                if (string.Compare(input, "exit", ignoreCase: true) == 0)
                {
                    isContinue = false;
                }
                else if(string.Compare(input, "edit", ignoreCase: true) == 0)
                {
                    Edit();
                }
            }

            Console.WriteLine(Resources.Goodbye);
            Console.ReadLine();
        }

        private static void Edit()
        {
            Console.Write("Id: ");
            var input = Console.ReadLine();
            int id = int.Parse(input);

            //var personToEdit = from person in People where person.Id == id select person;
            var personToEdit = People.Where(person => person.Id == id)/*.Select(person => person)*/.SingleOrDefault();

            if (personToEdit == null)
            {
                Console.WriteLine("Id not found");
                Console.ReadLine();
                return;
            }

            personToEdit.LastName = ReadData("Last name", personToEdit.LastName);
            personToEdit.FirstName = ReadData("First name", personToEdit.FirstName);
            personToEdit.BirthDate = DateTime.Parse(ReadData("Birth name", personToEdit.BirthDate.ToShortDateString()));
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
