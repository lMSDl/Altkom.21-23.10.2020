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
using Services.DbService.Services;

namespace ConsoleApp
{
    public partial class Program
    {
        private static ICrudService<Person> PeopleService { get; set; } = new EntityService<Person>(); // = new PeopleInMemoryService();

        private delegate void StringDelegate(string @string);
        private static Logger Logger { get; } = new Logger();

        private static StringDelegate Write { get; }
        private static Action<string> WriteLine { get; }
        //private static Func<int, float, string, string> Test { get; }
        static Program()
        {
            Write += Console.Write;
            Write += s => Debug.Write(s);
            Write += Logger.Log;

            WriteLine += Console.WriteLine;
            WriteLine += s => Debug.WriteLine(s);
            WriteLine += Logger.LogLine;

            //Test += (intParam, floatParam, stringParam) => $"{intParam} {floatParam} {stringParam}";
        }

        [STAThread]
        static void Main(string[] args)
        {
            bool isContinue = true;
            while (isContinue)
            {
                Console.Clear();
                ShowPeople(PeopleService.Read());
                isContinue = ExecuteCommand(Console.ReadLine());
                //Test();
            }

            Console.WriteLine(Logger.GetLogs());
            WriteLine(Resources.Goodbye);
            Console.ReadLine();
        }

        private static void Test()
        {
            try
            {
                ((Student)PeopleService.Read(11)).GoToSchool();
            }
            catch (IndexOutOfRangeException e)
            {

            }
            catch (InvalidCastException e)
            {
                Console.WriteLine("Osoba o podanym id, nie jest studentem");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            var student = PeopleService.Read(11) as Student;
            //if (student != null)
            //    student.GoToSchool();
            student?.GoToSchool();
            var index = student?.IndexNo;
            //student?.IndexNo = 1000; - tak nie można

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
                case Commands.ToJson:
                    ToJson();
                    break;
                case Commands.FromJson:
                    FromJson();
                    break;
                case Commands.ToXml:
                    ToXML();
                    break;
                case Commands.FindInJson:
                    FindInJson();
                    break;
                default:
                    WriteLine(Resources.UnknownCommand);
                    Console.ReadLine();
                    break;
            }
            return true;
        }

        

        private static void ShowPeople(IEnumerable<Person> people)
        {
            //for(var i = 0; i < People.Count; i++)
            //{
            //    var person = People[i];
            //}
            foreach (var person in people)
            {
                WriteLine(person.ToString());
            }
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
