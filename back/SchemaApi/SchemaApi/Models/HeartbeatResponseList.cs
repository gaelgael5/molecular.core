using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaApi.Models
{


    /// <summary>
    /// response of get Hertbeatcontroller
    /// </summary>
    public class HeartbeatResponseList
    {

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public List<HeartbeatResponse> Items { get; set; }

    }

}
