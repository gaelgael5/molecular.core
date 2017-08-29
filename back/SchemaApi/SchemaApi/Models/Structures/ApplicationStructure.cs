using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaApi.Models.Structures
{
    public class ApplicationStructure
    {

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
        
        public List<StructureObject> Objects { get; set; }
        public string UpdatedBy { get; set; }
        public int Version { get; set; }
    }
}
