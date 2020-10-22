using System;
using System.Linq;
using System.Collections.Generic;
using ConsoleApp.Properties;
using System.Globalization;
using Models;
using Services.Interfaces;
using Services.InMemeoryService;
using ConsoleApp.Models;

namespace ConsoleApp
{
    public class Program
    {
        // 1. Dodać metodę Delete(int id) do IPeopleService
        // 2. Zaimplementować brakującą metodę z IPeopleService
        // 3. Dodać komendę Delete do Commands
        // 4. Zapewnić obsługę komendy Delete

        private static IPeopleService PeopleService { get; } = new PeopleInMemoryService();

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

        public static bool ExecuteCommand(string input)
        {
            var command = GetCommand(input);
            switch (command)
            {
                case Commands.Exit:
                    return false;
                case Commands.Edit:
                        var personToEdit = FindPerson();
                        if (personToEdit != null)
                        {
                            Edit(personToEdit);
                            PeopleService.Update(personToEdit);
                        }
                        break;
                case Commands.Add:
                    Add();
                    break;
                default:
                    Console.WriteLine(Resources.UnknownCommand);
                    Console.ReadLine();
                    break;
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

        //private static Nullable<Commands> GetCommand(string input)
        private static Commands? GetCommand(string input)
        {
            if (Enum.TryParse(input, true, out Commands command))
                return command;
            return null;
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
            Console.Write($"{label} ({defaultValue}): ");
            input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return defaultValue;
            return input;
        }
    }
}
