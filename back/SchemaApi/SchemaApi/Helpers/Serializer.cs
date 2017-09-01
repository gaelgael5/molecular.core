using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaApi.Helpers
{
    public static class Serializer
    {


        static Serializer()
        {
            Serializer.setting = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK",
                DateParseHandling = DateParseHandling.DateTime
            };
        }

        public static T Deserialize<T>(string payload)
        {
            T output = default(T);
            output = JsonConvert.DeserializeObject<T>(payload);
            return output;
        }

        public static string Serialize<T>(T item, bool indented = false)
        {
            var formating = indented ? Formatting.Indented : Formatting.None;
            var result = JsonConvert.SerializeObject(item, formating, Serializer.setting);
            return result;
        }

        public static void SaveFile<T>(T model, string filename)
        {
            JsonSerializer serializer = new JsonSerializer();
            string txt = JsonConvert.SerializeObject(model, Formatting.Indented);
            File.WriteAllText(filename, txt, Encoding.UTF8);
        }

        public static T LoadFile<T>(string file)
        {

            if (!File.Exists(file))
            {
                Debug.WriteLine($"missing file {file}", "Error");
                throw new FileNotFoundException(file);
            }

            new FileInfo(file).Refresh();

            string payload = File.ReadAllText(file, Encoding.UTF8);
            T _result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(payload);

            Debug.WriteLine($"File '{file}' loaded.", "Debug");

            return _result;

        }

        private static readonly JsonSerializerSettings setting;

    }
}
