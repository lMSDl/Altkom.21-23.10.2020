using System;
using System.Linq;
using System.Collections.Generic;
using ConsoleApp.Properties;
using System.Globalization;
using Models;
using Services.Interfaces;

namespace ConsoleApp
{
    public class Program
    {
        private static IPeopleService PeopleService { get; }

        static void Main(string[] args)
        {
            bool isContinue = true;
            while (isContinue)
            {
                Console.Clear();
                ShowPeople();
                isContinue = ExecuteCommand(Console.ReadLine());
            }

            Console.WriteLine(Resources.Goodbye);
            Console.ReadLine();
        }

        public static bool ExecuteCommand(string command)
        {
            if (Compare(command, "exit"))
            {
                return false;
            }
            else if (Compare(command, "edit"))
            {
                var personToEdit = FindPerson();
                if (personToEdit != null)
                {
                    Edit(personToEdit);
                    PeopleService.Update(personToEdit);
                }
            }
            else if (Compare(command, "add"))
            {
                Add();
            }
            return true;
        }

        private static void ShowPeople()
        {
            //for(var i = 0; i < People.Count; i++)
            //{
            //    var person = People[i];
            //}
            foreach (var person in PeopleService.Read())
            {
                //var personInfo = string.Format("{0, -15}{1, -15}{2, -10}", person.LastName, person.FirstName, person.BirthDate.ToShortDateString());
                var personInfo = $"{person.Id,-3}{person.LastName,-15}{person.FirstName,-15}{person.BirthDate.ToShortDateString(),-10}";
                Console.WriteLine(personInfo);
            }
        }

        private static void Add()
        {
            var person = new Person();
            Edit(person);
            PeopleService.Create(person);
        }

        private static bool Compare(string input, string value)
        {
            return string.Compare(input, value, ignoreCase: true) == 0;
        }

        private static Person FindPerson()
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
            //var personToEdit = People.Where(person => person.Id == id)/*.Select(person => person)*/.SingleOrDefault();
            var personToEdit = PeopleService.Read(id);

                if (personToEdit == null)
                {
                    Console.WriteLine(Resources.IdNotFound);
                    Console.ReadLine();
                    return null;
                }
            return personToEdit;
        }

        private static void Edit(Person personToEdit)
        {
            personToEdit.LastName = ReadData(Resources.LastName, personToEdit.LastName);
            personToEdit.FirstName = ReadData(Resources.FirstName, personToEdit.FirstName);

            string data;
            DateTime birthDate;
            do
            {
                data = ReadData(Resources.BirthDate, personToEdit.BirthDate.ToShortDateString());
            }
            while(!DateTime.TryParse(data, out birthDate));
//          while (!DateTime.TryParseExact(data, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out birthDate)) ;


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
