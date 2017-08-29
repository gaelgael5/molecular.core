using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchemaApi.Models.Structures;
using Microsoft.Extensions.Logging;
using SchemaApi.Models;

namespace SchemaApi.Controllers
{

    // http://localhost:5000/api.Schema

    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(SchemaController))]
    public class SchemaController : Controller
    {

        public SchemaController(ILogger<SchemaController> logger)
        {
            this.logger = logger;
        }

        // GET api/values
        [HttpGet("index")]
        public IEnumerable<string> GetAll()
        {
            var applications = ApplicationStructures.Instance.GetApplications();
            return applications;
        }

        // GET api/values/5
        [HttpGet("detail/{applicationName}")]
        public ApplicationStructure Get(string applicationName)
        {
            return ApplicationStructures.Instance.GetApplication(applicationName);
        }

        // POST api/values
        [HttpPost("save")]
        public void Post([FromBody]ApplicationStructure application, string lockId)
        {

            string userName = this.HttpContext.User.Identity.Name;

            ApplicationStructures.Instance.SaveApplication(application, lockId, userName);

        }

        // POST api/values
        [HttpGet("lock")]
        public string Lock([FromBody]string applicationName)
        {
            string userName = this.HttpContext.User.Identity.Name;
            return ApplicationStructures.Instance.Lock(applicationName, userName);
        }

        // POST api/values
        [HttpGet("unlock")]
        public void UnLock([FromBody]string applicationName)
        {
            string userName = this.HttpContext.User.Identity.Name;
            ApplicationStructures.Instance.Unlock(applicationName, userName, this.HttpContext.User.IsInRole(Constants.Roles.Administrator));
        }

        //// DELETE api/values/5
        //[HttpDelete("{applicationName}")]
        //public void Delete(string applicationName)
        //{
        //}


        private readonly ILogger<SchemaController> logger;

    }
}
