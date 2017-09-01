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

    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(StoreController))]
    public class StoreController : GenericCrudController<StoreServerData>
    {

        public StoreController(ILogger<StoreController> logger) : base(logger)
        {
            
        }

    }
}
