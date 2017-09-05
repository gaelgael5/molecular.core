using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Molecular.FileStore
{
    public class LockModel
    {

        public LockModel()
        {

        }

        public string LockId { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public string LockedBy { get; set; }

        public string Type { get;  set; }

    }


}
