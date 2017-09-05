using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Molecular;
using Molecular.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaApi.Controllers
{


    [Route("api/[controller]")]
    public class HeartBeatController : Controller
    {

        public HeartBeatController(ILogger<HeartBeatController> logger)
        {
            this.logger = logger;
            this.server = Constants.Roots.FirstOrDefault() ?? "http://[serverroot]/";
        }


        [HttpGet("{key}")]
        public List<KeyValuePair<string, string>> Check(string key)
        {

            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

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
