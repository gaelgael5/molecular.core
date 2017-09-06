using Molecular.FileStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaApi.Models
{

    /// <summary>
    /// Model dedicated for store serialized data molecular api server list
    /// </summary>
    /// <seealso cref="Molecular.FileStore.DataModelBase" />
    public class ApiServer : DataModelBase
    {

        /// <summary>
        /// Gets or sets the host of the server.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        public string Host { get; set; }

    }

}
