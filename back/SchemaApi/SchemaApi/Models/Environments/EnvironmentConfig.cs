using Molecular.FileStore;
using Newtonsoft.Json;
using SchemaApi.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaApi.Models.Environments
{
    public class EnvironmentConfig : DataModelBase
    {

        public string Prefix { get; set; }

        [JsonConverter(typeof(DataNameJsonConverter))]
        public string Store { get; set; }

    }
}
