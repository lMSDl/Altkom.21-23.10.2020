using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Win32;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.InMemeoryService;

namespace ConsoleApp
{
    public partial class Program
    {
        static JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
            Formatting = Newtonsoft.Json.Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };

        public static void ToJson()
        {

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

        public static string FromJson()
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Json files|*.json|All files|*.*"
            };
            
            if (openFileDialog.ShowDialog() != true)
                return null;

            var json = File.ReadAllText(openFileDialog.FileName);
            var people = JsonConvert.DeserializeObject<List<Person>>(json);
            PeopleService = new PeopleInMemoryService(people);

            return json;
        }

        public static void FindInJson()
        {
            var json = FromJson();
            //foreach (var item in JArray.Parse(json).Children<JObject>())
            //{
            //    if(DateTime.Parse(item.Value<string>(nameof(Person.BirthDate))) > new DateTime(1980, 1, 1))
            //    {
            //        Console.WriteLine(item);
            //    }
            //}

            JArray.Parse(json).Children<JObject>().Where(x => DateTime.Parse(x.Value<string>(nameof(Person.BirthDate))) > new DateTime(1980,1,1)).ToList().ForEach(x => Console.WriteLine(x));

            Console.ReadLine();
        }

        public static void ToXML()
        {
            var json = JsonConvert.SerializeObject(PeopleService.Read());
            Console.WriteLine(json);
            json = $"{{ \"Person\": {json} }}";
            Console.WriteLine(json);

            var xmlDocument = JsonConvert.DeserializeXmlNode(json, "People");
           
            Console.WriteLine(xmlDocument.OuterXml);
            //xmlDocument.Save("x:/abc.xml");

            //var otherXmlDocument = new XmlDocument();
            //otherXmlDocument.LoadXml(xmlDocument.OuterXml);
            //otherXmlDocument.Load("x:/abc.xml");


            Console.ReadLine();
        }
    }
}
