using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaApi.Models.Structures
{
    public class LockApplicationModel
    {

        public LockApplicationModel()
        {

        }

        public string LockId { get; set; }

        public DateTime Created { get; set; }

        public string LockedBy { get; set; }


    }


}
