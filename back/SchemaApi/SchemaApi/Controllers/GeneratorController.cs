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
        public string Index(string viewName)
        {

            //Scenario s = new Scenario()
            //{
            //    Target = @"::front",
            //    Views = new ViewStep[]
            //    {
            //        new ViewStep()
            //        {
            //             ViewName = "angular2-typescript-service-models",
            //        },
            //        new ViewStep()
            //        {
            //             ViewName = "angular2-typescript-service-interface",
            //        }
            //    }
            //};
            //Scenarii.Instance.Save(s, "schemaApi");


            var scenario = Scenarii.Instance.Load(viewName);
            scenario.Generate(this, this.engine, _apiExplorer);
            scenario.Save();

            return "";
        }

        private readonly IApiDescriptionGroupCollectionProvider _apiExplorer;
        private readonly IRazorViewEngine engine;


    }
}
