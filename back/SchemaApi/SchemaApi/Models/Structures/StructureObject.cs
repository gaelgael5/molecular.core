using Molecular;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaApi.Models.Structures
{

    public class StructureObject
    {

        public string Name { get; set; }

        public List<StructureProperty> Properties { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TypeKind Kind { get;  set; }

    }

}
