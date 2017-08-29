using System.Collections.Generic;

namespace SchemaApi.Models.Structures
{

    public class StructureProperty
    {

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public StructureObjectReference Type { get; set; }

        public bool Required { get; set; }

        public bool IsArray { get; set; }

        public string DefaultValue { get; set; }


        public List<StructurePropertyValidatorReference> Validators { get; set; }

    }

}