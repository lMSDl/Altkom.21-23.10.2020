using System;
using System.Linq;
using System.Collections.Generic;
using ConsoleApp.Properties;
using System.Globalization;
using Models;
using Services.Interfaces;
using Services.InMemeoryService;
using ConsoleApp.Models;
using System.Runtime.InteropServices;
using ConsoleApp.Extensions;
using System.Diagnostics;
using System.Text;

namespace ConsoleApp
{
    public class Program
    {
        private static ICrudService<Person> PeopleService { get; } = new PeopleInMemoryService();

        private delegate void StringDelegate(string @string);

        private static Logger Logger { get; } = new Logger();

        private static StringDelegate Write { get; }
        private static Action<string> WriteLine { get; }
        static Program()
        {
            Write += Console.Write;
            Write += s => Debug.Write(s);
            Write += Logger.Log;

            WriteLine += Console.WriteLine;
            WriteLine += s => Debug.WriteLine(s);
            WriteLine += Logger.LogLine;
        }

        static void Main(string[] args)
        {
            bool isContinue = true;
            while (isContinue)
            {
                Console.Clear();
                ShowPeople(PeopleService.Read());
                isContinue = ExecuteCommand(Console.ReadLine());
            }

            Console.WriteLine(Logger.GetLogs());
            WriteLine(Resources.Goodbye);
            Console.ReadLine();
        }

        public static bool ExecuteCommand(string input)
        {
            switch (input.ToCommand())
            {
                case Commands.Exit:
                    return false;
                case Commands.Edit:
                    Edit();
                    break;
                case Commands.Add:
                    Add();
                    break;
                case Commands.Delete:
                    Delete();
                    break;
                case Commands.Filter:
                    Filter();
                    break;
                default:
                    WriteLine(Resources.UnknownCommand);
                    Console.ReadLine();
                    break;
            }
            return true;
        }

        private static void Filter()
        {
            var people = PeopleService.Read().Where(person => person.LastName.Contains("i")).ToList();

            // 1. Osoby urodzone po 1950 roku (new DateTime(1951, 1, 1))
            //var people = PeopleService.Read().Where(x => x.BirthDate >= new DateTime(1951, 1, 1)).ToList();
            // 2. Osoby z 4 literami w imieniu (.Length)
            //var people = PeopleService.Read().Where(x => x.FirstName.Length == 4).ToList();
            // 3. 1. i 2.
            //var people = PeopleService.Read().Where(x => x.BirthDate >= new DateTime(1951, 1, 1)).Where(x => x.FirstName.Length == 4).ToList();
            // 4. Osoby młodsze niż 30 lat (.Year)
            //var people = PeopleService.Read().Where(x => DateTime.Today.Year - x.BirthDate.Year < 30).ToList();
            // 7.1. Wyświetlić wszystkie osoby posortowane po nazwisku
            //var people = PeopleService.Read().OrderBy(x => x.LastName).ToList();
            // 7.2. Wyświetlić wszystkie osoby posortowane po nazwisku, a następnie po imieniu
            //var people = PeopleService.Read().OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();

            ShowPeople(people);


            WriteLine(PeopleService.Read().Where(x => x.FirstName == "Lisa").Select(x => x.LastName).FirstOrDefault());
            // 5. Data urodzenia osoby, która ma na nazwisko "Li" oraz jej imię ma 3 litery
            //WriteLine(PeopleService.Read().Where(x => x.LastName == "Li").Where(x => x.FirstName.Length == 3).Select(x => x.BirthDate.ToShortDateString()).FirstOrDefault());
            // 6. Imię i nazwisko osoby urodzonej w 1950 roku.
            //WriteLine(PeopleService.Read().Where(x => x.BirthDate.Year == 1950).Select(x => $"{x.FirstName} {x.LastName}").FirstOrDefault());

            Console.ReadLine();
        }

        private static void Delete()
        {
            var person = FindPerson();
            if (person != null)
                PeopleService.Delete(person.Id);
        }

        private static void Edit()
        {
            var personToEdit = FindPerson();
            if (personToEdit != null)
            {
                Edit(personToEdit);
                PeopleService.Update(personToEdit);
            }
        }

        private static void ShowPeople(IEnumerable<Person> people)
        {
            //for(var i = 0; i < People.Count; i++)
            //{
            //    var person = People[i];
            //}
            foreach (var person in people)
            {
                //var personInfo = string.Format("{0, -15}{1, -15}{2, -10}", person.LastName, person.FirstName, person.BirthDate.ToShortDateString());
                var personInfo = $"{person.Id,-3}{person.LastName,-15}{person.FirstName,-15}{person.BirthDate.ToShortDateString(),-10}";
                WriteLine(personInfo);
            }
        }

        private static void Add()
        {
            var person = new Person();
            Edit(person);
            PeopleService.Create(person);
        }

        private static Person FindPerson()
        {
                string input;
                int id;
                do
                {
                    Write(Resources.Id);
                    input = Console.ReadLine();
                }
                while (!int.TryParse(input, out id));

            var personToEdit = PeopleService.Read(id);

                if (personToEdit == null)
                {
                    WriteLine(Resources.IdNotFound);
                    Console.ReadLine();
                    return null;
                }
            return personToEdit;
        }

        private static bool ConvertToString(string input, out string output)
        {
            output = input;
            return true;
        }

        private static bool ConvertToDateTime(string input, out DateTime output)
        {
            return DateTime.TryParse(input, out output);
        }

        private static void Edit(Person personToEdit)
        {
            personToEdit.LastName = ReadData(Resources.LastName, personToEdit.LastName, ConvertToString);
            personToEdit.FirstName = ReadData(Resources.FirstName, personToEdit.FirstName, ConvertToString);
            personToEdit.BirthDate = ReadData(Resources.BirthDate, personToEdit.BirthDate, ConvertToDateTime);

            /*string data;
            DateTime birthDate;
            do
            {
                data = ReadData(Resources.BirthDate, personToEdit.BirthDate.ToShortDateString());
            }
            while(!DateTime.TryParse(data, out birthDate));
//          while (!DateTime.TryParseExact(data, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out birthDate)) ;
            personToEdit.BirthDate = birthDate;*/
        }

        private delegate bool ConvertDelegate<T>(string input, out T output);
        private static T ReadData<T>(string label, T defaultValue, ConvertDelegate<T> converter)
        {
            string input;
            T output;
            do
            {
                Write($"{label} ({defaultValue}): ");
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    return defaultValue;
            }
            while (!converter(input, out output));
            return output;
        }
    }
}
