using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaApi.Models.Environments
{
    public class EnvironmentConfig
    {

        public string Name { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public int username { get; set; }
        
        public int pwd { get; set; }

        public int Catalog { get; set; }        

    }
}
