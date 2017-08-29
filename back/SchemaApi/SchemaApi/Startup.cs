using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SchemaApi.Models.Structures;
using SchemaApi.Models.Environments;
using SchemaApi.Models.Generators;
using SchemaApi.Helpers;
using System.IO;

namespace SchemaApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();


            var section = Configuration.GetSection("Schemas");
            var folderName = section.GetValue<string>("FolderStructures");
            if (string.IsNullOrEmpty(folderName))
                folderName = @"Applications\Structures";
            ApplicationStructures.Initialize(folderName);


            folderName = section.GetValue<string>("FolderEnvironments");
            if (string.IsNullOrEmpty(folderName))
                folderName = @"Applications\Environments";
            EnvironmentsConfig.Initialize(folderName);


            section = Configuration.GetSection("Scenarii");
            folderName = section.GetValue<string>("Path");
            if (string.IsNullOrEmpty(folderName))
                folderName = @"Scenarii";
            string targetKeyPath = Path.Combine(Directory.GetCurrentDirectory(), "TargetKeys.json");
            List<KeyValuePair<string, string>> _paths = Serializer.LoadFile<List<KeyValuePair<string, string>>>(targetKeyPath);
            Scenarii.Initialize(folderName, _paths);


        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

        }

    }
}
