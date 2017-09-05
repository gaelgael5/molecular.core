using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Molecular;

namespace SchemaApi
{
    public class Program
    {

        public static Program Instance { get; private set; }

        public bool Canceled { get; private set; }

        public static void Main(string[] args)
        {

            Program.Instance = new Program();

            Program.Instance.Start(args);

        }

        private void Start(string[] args)
        {

            string initialDirectory = Directory.GetCurrentDirectory();

            if (args.Length == 1)
                initialDirectory = args[0];

            this.host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(initialDirectory)
                .UseStartup<Startup>()
                // .UseApplicationInsights()
                //.ConfigureLogging()                
                .Build();

            if (args.Length == 1)
                this.task = Task.Run(() =>
                {
                    host.Run();
                });

            else
                host.Run();

        }


        private IWebHost host;
        public Task task;

    }
}
