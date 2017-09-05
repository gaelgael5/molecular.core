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
using System.IO;
using SchemaApi.Models.Stores;
using Molecular.FileStore;
using Molecular.Helpers;
using Molecular;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Swashbuckle.AspNetCore.Swagger;

namespace SchemaApi
{
    public class Startup
    {
        private readonly Info swaggerInfo;

        public Startup(IHostingEnvironment env)
        {

            this.swaggerInfo = new Swashbuckle.AspNetCore.Swagger.Info
            {
                Title = "molecular api",
                Version = "v1",
                Description = "Backend molecular api",
            };

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", this.swaggerInfo);

                var basePath = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "molecular.api.xml");
                c.IncludeXmlComments(xmlPath);

                c.DescribeAllEnumsAsStrings();
                c.DescribeAllParametersInCamelCase();
                c.DescribeStringEnumsInCamelCase();
                
            });

         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            var section = Configuration.GetSection("Schemas");
            InitializeRepository(app, env, section);

            InitializeStructure();

            Repositories<StoreServerData>.Initialize(false);
            Repositories<EnvironmentConfig>.Initialize(false);

            string targetKeyPath = Path.Combine(Repository.Instance.Folder.Parent.FullName, "TargetKeys.json");
            List<KeyValuePair<string, string>> _paths = Serializer.LoadFile<List<KeyValuePair<string, string>>>(targetKeyPath);
            Scenarii.Initialize(@"Scenarii", _paths);

            app.UseMvc();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{this.swaggerInfo.Version}/swagger.json", this.swaggerInfo.Title);
                c.ShowJsonEditor();
                c.ShowRequestHeaders();
                
            });

        }

        private static void InitializeStructure()
        {
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
        }

        private static void InitializeRepository(IApplicationBuilder app, IHostingEnvironment env, IConfigurationSection section)
        {

            var serverAddress = app.ServerFeatures.FirstOrDefault(c => c.Key == typeof(IServerAddressesFeature)).Value;
            if (serverAddress != null)
                foreach (string item in (serverAddress as IServerAddressesFeature).Addresses)
                {
                    Constants.Roots.Add(item);
                    System.Diagnostics.Debug.WriteLine($"exposed to {item}", Constants.Logs.Debug);
                }


            var path = new DirectoryInfo(Path.Combine(env.ContentRootPath, section.GetValue<string>("RootFolder")));

            bool isMaster = false;

            string urlMaster = section.GetValue<string>("UrlMaster");
            if (string.IsNullOrEmpty(urlMaster))
                urlMaster = Constants.Roots.First();

            Repository.Initialize(path.FullName, urlMaster, urlMaster == Constants.Roots.First());

        }
    }
}
