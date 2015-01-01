using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RozkladyBackend.Lib.API;
using RozkladyBackend.Models;
using RozkladyBackend.Models.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace RozkladyBackend.Controllers
{
    public class ExportController : Controller
    {
        private JsonSerializerSettings jsonSerializerSettings;

        public ExportController()
        {
            jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        public String DumpJsons()
        {
            DateTime start = DateTime.Now;
            List<Stop> stops;
            using (BackendContext db = new BackendContext())
            {
                 stops = db.Stops.ToList();
            }

            foreach (var singleStop in stops)
            {
                using (BackendContext db = new BackendContext())
                {
                    // TODO: check if necessary
                    db.Configuration.LazyLoadingEnabled = false;
                    ExportStopTimetableToFile(db, singleStop);
                }
            }
            return (DateTime.Now - start).TotalMilliseconds + "ms";
        }

        private void ExportStopTimetableToFile(BackendContext db, Stop stop)
        {
            var filename = RemovePolishCharacters(RemoveNonAlphanumericCharacters(stop.Name));
            var filepath = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\Exports\\" + filename + ".json";
            APITimetable apiTimetable = APITimetableBuilder.Build(db, stop.Id);
            String serializedStop = JsonConvert.SerializeObject(apiTimetable, jsonSerializerSettings);

            using (StreamWriter writer = new StreamWriter(filepath, false, Encoding.UTF8))
            {
                writer.Write(serializedStop);
                writer.Close();
            }
        }

        private String RemoveNonAlphanumericCharacters(String input)
        {
            char[] array = input.ToCharArray();

            array = Array.FindAll<char>(array, (c => (char.IsLetterOrDigit(c)
                                                || c == '-')));

            return new String(array);
        }

        private String RemovePolishCharacters(String input)
        {
            return input.Replace('ą', 'a')
                .Replace('ć', 'c')
                .Replace('ę', 'e')
                .Replace('ł', 'l')
                .Replace('ś', 's')
                .Replace('ń', 'n')
                .Replace('ó', 'o')
                .Replace('ż', 'z')
                .Replace('ź', 'z')
                .Replace('Ą', 'A')
                .Replace('Ć', 'C')
                .Replace('Ę', 'E')
                .Replace('Ł', 'L')
                .Replace('Ś', 'S')
                .Replace('Ń', 'N')
                .Replace('Ó', 'O')
                .Replace('Ż', 'Z')
                .Replace('Ź', 'Z');
        }
    }
}