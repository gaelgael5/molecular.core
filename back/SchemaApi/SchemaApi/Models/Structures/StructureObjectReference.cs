using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaApi.Models.Structures
{
    public class StructureObjectReference
    {

        public string Name { get; set; }

        public StructureObject ResolveStructure(ApplicationStructure application)
        {
            return application.Objects.FirstOrDefault(c => this.Name == c.Name);
        }

    }
}
