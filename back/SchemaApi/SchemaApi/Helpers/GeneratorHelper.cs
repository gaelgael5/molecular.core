using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molecular.Helpers
{
    public static class GeneratorHelper
    {

        public static bool RenderViewToString(this Controller controller, IRazorViewEngine engine, string viewName, object model, out StringBuilder output)
        {
            output = new StringBuilder();
            controller.ViewData.Model = model;
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = engine.FindView(controller.ControllerContext, viewName, false);
                    Microsoft.AspNetCore.Mvc.Rendering.ViewContext viewContext = new Microsoft.AspNetCore.Mvc.Rendering.ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw, new Microsoft.AspNetCore.Mvc.ViewFeatures.HtmlHelperOptions() { });
                    Task t = viewResult.View.RenderAsync(viewContext);
                    // viewResult.View..ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);

                    var a = t.GetAwaiter();
                    while (!a.IsCompleted)
                        Task.Yield();

                    if (t.IsFaulted)
                    {
                        output.Append(t.Exception.InnerException.ToString());
                        return false;
                    }

                    output.Append(sw.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
               output.Append(ex.ToString());
            }

            return false;

        }


    }
}
