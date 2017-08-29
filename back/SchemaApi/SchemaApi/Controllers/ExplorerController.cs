using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;

namespace SchemaApi.Controllers
{

    [Route("api/[controller]")]
    public class ExplorerController : Controller
    {

        public ExplorerController(IApiDescriptionGroupCollectionProvider apiExplorer)
        {
            _apiExplorer = apiExplorer;
        }

        public IActionResult Index()
        {
            return View(new KeyValuePair<string, IApiDescriptionGroupCollectionProvider>(SchemaApi.Models.Constants.Roots.FirstOrDefault(), _apiExplorer));
        }

        private readonly IApiDescriptionGroupCollectionProvider _apiExplorer;

    }
}
