using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using ConsoleApp.Properties;

namespace ConsoleApp
{
    public partial class Program
    {
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
            if (personToEdit == null)
                return;

            Edit(personToEdit);
            PeopleService.Update(personToEdit);
        }

        private static void Add()
        {
            var person = new Person();
            Edit(person);
            PeopleService.Create(person);
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

    }
}
