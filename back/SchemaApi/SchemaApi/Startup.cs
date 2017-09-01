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
using SchemaApi.Models.Stores;
using SchemaApi.Models;

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

            var path = new DirectoryInfo(section.GetValue<string>("RootFolder"));
            Repository.Initialize(path.FullName);
            // ApplicationStructures.Initialize(@"Applications");
            Repositories<Structure>.Initialize(true, null, (name) =>
            {
                return new Structure()
                {
                    Name = name,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                    Objects = new List<StructureObject>()
                {
                    new StructureObject() { Name = Constants.Types.String, Kind = TypeKind.Value },
                    new StructureObject() { Name = Constants.Types.DateTime, Kind = TypeKind.Value },
                    new StructureObject() { Name = Constants.Types.Integer, Kind = TypeKind.Value },
                    new StructureObject() { Name = Constants.Types.Decimal , Kind = TypeKind.Value},
                    new StructureObject() { Name = Constants.Types.Guid , Kind = TypeKind.Value},
                },

                };
            });

            Repositories<StoreServerData>.Initialize(false);
            Repositories<EnvironmentConfig>.Initialize(false);

            string targetKeyPath = Path.Combine(Repository.Instance.Folder.Parent.FullName, "TargetKeys.json");
            List<KeyValuePair<string, string>> _paths = Serializer.LoadFile<List<KeyValuePair<string, string>>>(targetKeyPath);
            Scenarii.Initialize(@"Scenarii", _paths);


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
