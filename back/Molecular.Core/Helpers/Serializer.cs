using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molecular.Helpers
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

        public static T Deserialize<T>(this string payload)
        {
            T output = default(T);
            output = JsonConvert.DeserializeObject<T>(payload);
            return output;
        }

        public static string Serialize<T>(this T item, bool indented = false)
        {
            var formating = indented ? Formatting.Indented : Formatting.None;
            var result = JsonConvert.SerializeObject(item, formating, Serializer.setting);
            return result;
        }

        public static void SaveFileWithLock<T>(this T model, string filename)
        {

            using (var l = Lock(filename, false))
            {

                JsonSerializer serializer = new JsonSerializer();
                string txt = JsonConvert.SerializeObject(model, Formatting.Indented);
                byte[] ar = System.Text.Encoding.UTF8.GetBytes(txt);

                l.Stream.Write(ar, 0, ar.Length);

            }

        }

        public static _lock Lock(string filename, bool autoRemove)
        {
            return new _lock(filename, autoRemove);
        }


        public static void SaveFile<T>(this T model, string filename)
        {
            JsonSerializer serializer = new JsonSerializer();
            string txt = JsonConvert.SerializeObject(model, Formatting.Indented);
            File.WriteAllText(filename, txt, Encoding.UTF8);
        }


        public static T LoadFile<T>(this string file)
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


        public class _lock : IDisposable
        {

            public _lock(string filename, bool autoRemove)
            {
                this.filename = filename;
                this.fileLocked = new FileInfo(filename);
                this.autoRemove = autoRemove;
                this.Lock();
            }

            public void Dispose()
            {
                Stream.Dispose();
                if (autoRemove)
                    this.fileLocked.Delete();
            }

            private void Lock()
            {

                if (!this.fileLocked.Exists)
                {
                    try
                    {
                        this.Stream = fileLocked.Open(FileMode.Create, FileAccess.Write, FileShare.Read);
                    }
                    catch (System.IO.IOException e)
                    {
                        throw new Exceptions.LockException($"file {filename} allready locked");
                    }
                }
                else
                    throw new Exceptions.LockException($"file {filename} allready locked");

            }

            public FileStream Stream { get; private set; }


            private string filename;
            private readonly FileInfo fileLocked;
            private readonly bool autoRemove;
        }

        private static readonly JsonSerializerSettings setting;

    }
}
