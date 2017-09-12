using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Molecular;
using Molecular.Helpers;
using SchemaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaApi.Controllers
{


    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class HeartBeatController : Controller
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="HeartBeatController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public HeartBeatController(ILogger<HeartBeatController> logger)
        {
            this.logger = logger;
            this.server = Constants.Roots.FirstOrDefault() ?? "http://[serverroot]/";
        }


        /// <summary>
        /// Checks the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        [HttpGet("{key}")]
        public HeartbeatResponseList Check(string key)
        {

            HeartbeatResponseList result = new HeartbeatResponseList();

            try
            {

                // Placer les tests ici et les ajouter dans la liste de retour.
                
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

            return result;

        }


        private ILogger<HeartBeatController> logger;
        private readonly string server;

    }

}
