using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using SchemaApi.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace SchemaApi.Models.Generators
{

    public class Scenario
    {

        public Scenario()
        {

        }

        public ViewStep[] Views { get; set; }

        public string Target { get; set; }

        public void Generate(Controller controller, IRazorViewEngine engine, object model)
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder(1024);

            foreach (var item in this.Views)
            {

                var txt = controller.RenderViewToString(engine, item.ViewName, model);
                controller.HttpContext.Response.ContentType = "application/text; charset=utf-8";

                var reader = new StreamReader(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(txt)));
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line.StartsWith("!!cutefile - - - - - - "))
                    {
                        var filename = line.Substring(10).Trim(' ', '-');
                        item.AddFile(filename, sb);
                        sb = new System.Text.StringBuilder(1024);
                    }
                    else
                        sb.AppendLine(line);
                }

                if (sb.Length > 0 && !string.IsNullOrEmpty(sb.ToString().Trim()))
                    item.AddFile("nofilename", sb);

                sb = new System.Text.StringBuilder(1024);

            }

        }

        public void Save()
        {

            if (string.IsNullOrEmpty(this.Target))
                throw new Exception("target folder can't be null");

            foreach (var item in this.Views)
            {
                var p = this.Target;

                if (!string.IsNullOrEmpty(item.Target))
                    p = Path.Combine(p, item.Target);

                item.Save(p);

            }

        }

    }

}
