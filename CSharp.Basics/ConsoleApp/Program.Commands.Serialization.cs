using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Models;
using Newtonsoft.Json;
using Services.InMemeoryService;

namespace ConsoleApp
{
    public partial class Program
    {
        public static void ToJson()
        {
            var settings = new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            var json = JsonConvert.SerializeObject(PeopleService.Read(), settings);

            var saveFileDialog = new SaveFileDialog()
            {
                FileName = "people.json",
                Filter = "Json files|*.json|All files|*.*"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, json);
                /*using (var file = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    using (var streamWriter = new StreamWriter(file))
                    {
                        streamWriter.Write(json);
                    }
                }*/
            }

            WriteLine(json);
            Console.ReadLine();
        }

        public static void FromJson()
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Json files|*.json|All files|*.*"
            };
            
            if (openFileDialog.ShowDialog() != true)
                return;

            var json = File.ReadAllText(openFileDialog.FileName);
            var people = JsonConvert.DeserializeObject<List<Person>>(json);
            PeopleService = new PeopleInMemoryService(people);
        }
    }
}
