using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchemaApi.Models.Structures;
using Microsoft.Extensions.Logging;
using SchemaApi.Models;
using SchemaApi.Models.Stores;

namespace SchemaApi.Controllers
{
    
    /// <summary>
    /// provide crud methods for design structure model for application
    /// </summary>
    /// <seealso cref="T:SchemaApi.Controllers.GenericCrudInApplicationController{SchemaApi.Models.Structures.Structure}" />
    public class StructureController : GenericCrudInApplicationController<Structure>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public StructureController(ILogger<StructureController> logger) : base(logger)
        {
            
        }

    }
}
