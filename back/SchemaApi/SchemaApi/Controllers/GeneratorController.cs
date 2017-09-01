using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;
using System.IO;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using SchemaApi.Helpers;
using SchemaApi.Models.Generators;

namespace SchemaApi.Controllers
{

    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(GeneratorController))]
    public class GeneratorController : Controller
    {

        public GeneratorController(IApiDescriptionGroupCollectionProvider apiExplorer, Microsoft.AspNetCore.Mvc.Razor.IRazorViewEngine engine)
        {
            _apiExplorer = apiExplorer;
             this.engine = engine;
        }

        [HttpGet("{viewName}")]
        public IActionResult Index(string viewName)
        {

            var scenario = Scenarii.Instance.Load(viewName);
            scenario.Generate(this, this.engine, _apiExplorer);
            var result =  scenario.Save();

            this.HttpContext.Response.ContentType = "text/html";
            return this.Ok(result);
        }

        private readonly IApiDescriptionGroupCollectionProvider _apiExplorer;
        private readonly IRazorViewEngine engine;


    }
}
