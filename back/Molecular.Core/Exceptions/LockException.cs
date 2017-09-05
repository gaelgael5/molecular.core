using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Molecular.Exceptions
{


    [Serializable]
    public class LockException : Exception
    {
        public LockException() { }

        public LockException(string message) : base(message) { }

        public LockException(string message, Exception inner) : base(message, inner) { }
    
    }

}
