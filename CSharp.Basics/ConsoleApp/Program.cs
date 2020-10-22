﻿using System;
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
    public partial class Program
    {
        private static ICrudService<Person> PeopleService { get; set; } = new PeopleInMemoryService();

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

        [STAThread]
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
                case Commands.ToJson:
                    ToJson();
                    break;
                case Commands.FromJson:
                    FromJson();
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
                //var personInfo = string.Format("{0, -15}{1, -15}{2, -10}", person.LastName, person.FirstName, person.BirthDate.ToShortDateString());
                var personInfo = $"{person.Id,-3}{person.LastName,-15}{person.FirstName,-15}{person.BirthDate.ToShortDateString(),-10}";
                WriteLine(personInfo);
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
