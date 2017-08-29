using SchemaApi.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaApi.Models.Generators
{

    public class Scenarii
    {

        private Scenarii(string path, List<KeyValuePair<string, string>> paths)
        {
            this.path = path;
            this._paths =  paths.ToDictionary(c => c.Key, c => c.Value);
            this.Dir = new DirectoryInfo(this.path);
            if (!this.Dir.Exists)
                this.Dir.Create();
        }

        public static void Initialize(string path, List<KeyValuePair<string, string>> paths)
        {
            Scenarii._instance = new Scenarii(path, paths);
        }

        public static Scenarii Instance { get { return Scenarii._instance; } }

        private readonly string path;
        private static Scenarii _instance;
        private readonly DirectoryInfo Dir;
        private readonly Dictionary<string, string> _paths;

        public void Save(Scenario s, string name)
        {
            Serializer.SaveFile(s, Path.Combine(this.Dir.FullName, name) + ".json");
        }

        public Scenario Load(string name)
        {
            var result = Serializer.LoadFile<Scenario>(Path.Combine(this.Dir.FullName, name) + ".json");
            if (result != null)
            {
                if (result.Target.StartsWith("::"))
                    result.Target = this._paths[result.Target];
            }
            return result;
        }

    }


}
