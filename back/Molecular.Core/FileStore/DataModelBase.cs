using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Molecular.FileStore
{

    public class DataModelBase
    {

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public string UpdatedBy { get; set; }

        public int Version { get; set; }

    }

}
