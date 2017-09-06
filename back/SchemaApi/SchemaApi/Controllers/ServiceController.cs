using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Molecular;
using Molecular.FileStore;
using Molecular.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaApi.Controllers
{


    /// <summary>
    /// Service controller provide list a available server
    /// </summary>
    [Route("api/[controller]")]
    public class ServiceController : Controller
    {

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger"></param>
        public ServiceController(ILogger<ServiceController> logger)
        {
            this.logger = logger;
            this.server = Constants.Roots.FirstOrDefault() ?? "http://[serverroot]/";
        }

        /// <summary>
        /// Return the list of servers
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public IndexModelList Index(string key)
        {

            IndexModelList servers = null;

            try
            {

                var i = Repositories<SchemaApi.Models.ApiServer>.Instance;
                servers = i.Index(null);
                
            }
            catch (UnauthorizedAccessException a)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnauthorizedAccessException), a, $" unauthorized access to execute {this.Request.Method} '{this.Request.Path}' on controller {this.GetType().Name}");
                throw;
            }
            catch (Exception e)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnspecifiedException), e, $" failed to execute {this.Request.Method} '{this.Request.Path}' on controller {this.GetType().Name}");
                throw;
            }

            return servers;

        }

        private ILogger<ServiceController> logger;
        private readonly string server;

    }

}
