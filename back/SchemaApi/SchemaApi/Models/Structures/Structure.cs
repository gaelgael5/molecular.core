using Molecular.FileStore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaApi.Models.Structures
{
    public class Structure : DataModelBase
    {

        public List<StructureObject> Objects { get; set; }

    }
}
