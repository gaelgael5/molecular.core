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

    /// <summary>
    /// Entry program class
    /// </summary>
    public class Program
    {

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static Program Instance { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Program"/> is canceled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if canceled; otherwise, <c>false</c>.
        /// </value>
        public bool Canceled { get; private set; }

        /// <summary>
        /// Mains of the program with specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
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
        /// <summary>
        /// The task
        /// </summary>
        public Task task;

    }
}
