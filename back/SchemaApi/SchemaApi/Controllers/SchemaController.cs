﻿using System;
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
        [HttpGet("list")]
        public IEnumerable<string> GetList()
        {
            var applications = Repositories<ApplicationStructure>.Instance.GetList();
            return applications;
        }

        // GET api/values/5
        [HttpGet("detail/{applicationName}")]
        public ApplicationStructure Get(string applicationName)
        {            
            return Repositories<ApplicationStructure>.Instance.Get(applicationName, applicationName);
        }

        // POST api/values
        [HttpPost("save/{lockid}")]
        public void Post([FromBody]ApplicationStructure application, string lockid)
        {
            string userName = this.HttpContext.User.Identity.Name;
            Repositories<ApplicationStructure>.Instance.Save(application, lockid, userName, application.Name);
        }

        // POST api/values
        [HttpGet("lock")]
        public string Lock([FromBody]string applicationName)
        {
            string userName = this.HttpContext.User.Identity.Name;
            return Repositories<ApplicationStructure>.Instance.Lock(applicationName, userName, applicationName);
        }

        // POST api/values
        [HttpGet("unlock/{lockid}")]
        public void UnLock([FromBody]string applicationName, string lockid)
        {
            string userName = this.HttpContext.User.Identity.Name;
            Repositories<ApplicationStructure>.Instance.Unlock(applicationName, lockid, userName, this.HttpContext.User.IsInRole(Constants.Roles.Administrator));
        }

        // DELETE api/values/5
        [HttpDelete("{applicationName}/lockid")]
        public void Delete(string applicationName, string lockid)
        {
            string userName = this.HttpContext.User.Identity.Name;
            Repositories<ApplicationStructure>.Instance.Delete(applicationName, lockid, userName, applicationName);
        }

        private readonly ILogger<SchemaController> logger;

    }
}