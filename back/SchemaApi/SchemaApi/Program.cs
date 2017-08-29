using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace SchemaApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                // .UseApplicationInsights()
                // .ConfigureLogging()                
                .Build();

            TraceHostListener(host);

            // Microsoft.AspNetCore.Mvc.ViewEngines.ICompositeViewEngine}, ImplementationType = {Microsoft.AspNetCore.Mvc.ViewEngines.CompositeViewEngine
            // Microsoft.AspNetCore.Mvc.Razor.IRazorViewEngine}, ImplementationType = {Microsoft.AspNetCore.Mvc.Razor.RazorViewEngine
            // Microsoft.AspNetCore.Mvc.Razor.IRazorPageFactoryProvider}, ImplementationType = {Microsoft.AspNetCore.Mvc.Razor.Internal.DefaultRazorPageFactoryProvider
            // Microsoft.AspNetCore.Mvc.Razor.Compilation.IRazorCompilationService}, ImplementationType = {Microsoft.AspNetCore.Mvc.Razor.Internal.RazorCompilationService

            host.Run();

        }

        private static void TraceHostListener(IWebHost host)
        {
            var serverAddress = host.ServerFeatures.FirstOrDefault(c => c.Key == typeof(IServerAddressesFeature)).Value;
            if (serverAddress != null)
                foreach (var item in (serverAddress as IServerAddressesFeature).Addresses)
                {
                    System.Diagnostics.Debug.WriteLine($"exposed to {item}");
                    SchemaApi.Models.Constants.Roots.Add(item);
                }
        }
    }
}
