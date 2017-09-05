using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Molecular.Exceptions
{


    [Serializable]
    public class FailedAuthorizationException : Exception
    {

        public FailedAuthorizationException() { }

        public FailedAuthorizationException(string message) : base(message) { }

        public FailedAuthorizationException(string message, Exception inner) : base(message, inner) { }

    }
}
