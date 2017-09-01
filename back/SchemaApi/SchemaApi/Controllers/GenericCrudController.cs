﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchemaApi.Models.Structures;
using Microsoft.Extensions.Logging;
using SchemaApi.Models;
using SchemaApi.Helpers;

namespace SchemaApi.Controllers
{

    [Route("api/[controller]")]
    public class GenericCrudController<T> : Controller
        where T : DataModelBase, new()
    {

        public GenericCrudController(ILogger<GenericCrudController<T>> logger)
        {
            this.logger = logger;
            this.server = SchemaApi.Models.Constants.Roots.FirstOrDefault() ?? "http://[serverroot]/";
        }

        [HttpGet("list/{application}")]
        public IEnumerable<string> ListInApplication(string application)
        {
            try
            {
                var items = Repositories<T>.Instance.GetList(application);
                return items;
            }
            catch (UnauthorizedAccessException a)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnauthorizedAccessException), a, $" unauthorized access to execute GET '{this.server}api/list/{application}' on controller {this.GetType().Name}");
                throw;
            }
            catch (Exception e)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnspecifiedException), e, $" failed to execute GET '{this.server}api/list' on controller {this.GetType().Name}");
                throw;
            }
        }

        [HttpGet("list")]
        public IEnumerable<string> List()
        {
            try
            {
                var items = Repositories<T>.Instance.GetList(null);
                return items;
            }
            catch (UnauthorizedAccessException a)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnauthorizedAccessException), a, $" unauthorized access to execute GET '{this.server}api/list' on controller {this.GetType().Name}");
                throw;
            }
            catch (Exception e)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnspecifiedException), e, $" failed to execute GET '{this.server}api/list' on controller {this.GetType().Name}");
                throw;
            }
        }

        [HttpGet("read/{application}/{name}")]
        public T ReadInApplication(string application, string name)
        {
            try
            {

                return Repositories<T>.Instance.Read(name, application);

            }
            catch (UnauthorizedAccessException a)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnauthorizedAccessException), a, $" unauthorized access to execute GET '{this.server}api/read/{application}/{name}' on controller {this.GetType().Name}");
                throw;
            }
            catch (Exception e)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnspecifiedException), e, $" failed to execute GET '{this.server}api/read/{application}/{name}' on controller {this.GetType().Name}");
                throw;
            }
        }

        [HttpGet("read/{name}")]
        public T Read(string name)
        {
            try
            {

                return Repositories<T>.Instance.Read(name);

            }
            catch (UnauthorizedAccessException a)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnauthorizedAccessException), a, $" unauthorized access to execute GET '{this.server}api/read/{name}' on controller {this.GetType().Name}");
                throw;
            }
            catch (Exception e)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnspecifiedException), e, $" failed to execute GET '{this.server}api/read/{name}' on controller {this.GetType().Name}");
                throw;
            }
        }

        [HttpGet("create/{application}/{name}")]
        public void CreateInApplication(string application, string name)
        {

            try
            {

                T item = Repositories<T>.Instance.Create(name);
                item.UpdatedBy = this.HttpContext.User.Identity.Name;
                if (Repositories<T>.Instance.Add(item, application))
                {

                }

            }
            catch (UnauthorizedAccessException a)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnauthorizedAccessException), a, $" unauthorized access to execute GET '{this.server}api/create/{application}/{name}' on controller {this.GetType().Name}");
                throw;
            }
            catch (Exception e)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnspecifiedException), e, $" failed to execute GET '{this.server}api/create/{application}/{name}' on controller {this.GetType().Name}");
                throw;
            }

        }

        [HttpGet("create/{name}")]
        public void Create(string name)
        {

            try
            {

                T item = Repositories<T>.Instance.Create(name);
                item.UpdatedBy = this.HttpContext.User.Identity.Name;
                if (Repositories<T>.Instance.Add(item, name))
                {

                }

            }
            catch (UnauthorizedAccessException a)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnauthorizedAccessException), a, $" unauthorized access to execute GET '{this.server}api/create/{name}' on controller {this.GetType().Name}");
                throw;
            }
            catch (Exception e)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnspecifiedException), e, $" failed to execute GET '{this.server}api/create/{name}' on controller {this.GetType().Name}");
                throw;
            }

        }


        [HttpPost("update/{application}/{lockid}")]
        public void UpdateInApplication(string application, string lockid, [FromBody]T item)
        {
            try
            {
                string userName = this.HttpContext.User.Identity.Name;
                Repositories<T>.Instance.Update(item, lockid, userName, application);
            }
            catch (UnauthorizedAccessException a)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnauthorizedAccessException), a, $" unauthorized access to execute POST '{this.server}api/update/{application}/{lockid}' on controller {this.GetType().Name}");
                throw;
            }
            catch (Exception e)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnspecifiedException), e, $" failed to execute POST '{this.server}api/update/{application}/{lockid}' on controller {this.GetType().Name}");
                throw;
            }
        }

        [HttpPost("update/{lockid}")]
        public void Update(string lockid, [FromBody]T item)
        {
            try
            {
                string userName = this.HttpContext.User.Identity.Name;
                Repositories<T>.Instance.Update(item, lockid, userName);
            }
            catch (UnauthorizedAccessException a)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnauthorizedAccessException), a, $" unauthorized access to execute POST '{this.server}api/update/{lockid}' on controller {this.GetType().Name}");
                throw;
            }
            catch (Exception e)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnspecifiedException), e, $" failed to execute POST '{this.server}api/update/{lockid}' on controller {this.GetType().Name}");
                throw;
            }
        }

        [HttpGet("lock/{application}/{name}")]
        public string LockInApplication(string application, string name)
        {
            try
            {
                string userName = this.HttpContext.User.Identity.Name;
                return Repositories<T>.Instance.Lock(name, userName, application);
            }
            catch (UnauthorizedAccessException a)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnauthorizedAccessException), a, $" unauthorized access to execute GET '{this.server}api/lock/{application}/{name}' on controller {this.GetType().Name}");
                throw;
            }
            catch (Exception e)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnspecifiedException), e, $" failed to execute GET '{this.server}api/lock/{application}/{name}' on controller {this.GetType().Name}");
                throw;
            }
        }

        [HttpGet("lock/{name}")]
        public string Lock(string name)
        {
            try
            {
                string userName = this.HttpContext.User.Identity.Name;
                return Repositories<T>.Instance.Lock(name, userName);
            }
            catch (UnauthorizedAccessException a)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnauthorizedAccessException), a, $" unauthorized access to execute GET '{this.server}api/lock/{name}' on controller {this.GetType().Name}");
                throw;
            }
            catch (Exception e)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnspecifiedException), e, $" failed to execute GET '{this.server}api/lock/{name}' on controller {this.GetType().Name}");
                throw;
            }
        }

        [HttpGet("unlock/{application}/{name}/{lockid}")]
        public void UnLockInApplication(string application, string name, string lockid)
        {
            try
            {
                string userName = this.HttpContext.User.Identity.Name;
                Repositories<T>.Instance.Unlock(name, lockid, userName, this.HttpContext.User.IsInRole(Constants.Roles.Administrator), application);
            }
            catch (UnauthorizedAccessException a)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnauthorizedAccessException), a, $" unauthorized access to execute GET '{this.server}api/unlock/{application}/{name}/{lockid}' on controller {this.GetType().Name}");
                throw;
            }
            catch (Exception e)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnspecifiedException), e, $" failed to execute GET '{this.server}api/unlock/{application}/{name}/{lockid}' on controller {this.GetType().Name}");
                throw;
            }
        }

        [HttpGet("unlock/{name}/{lockid}")]
        public void UnLock(string name, string lockid)
        {
            try
            {
                string userName = this.HttpContext.User.Identity.Name;
                Repositories<T>.Instance.Unlock(name, lockid, userName, this.HttpContext.User.IsInRole(Constants.Roles.Administrator));
            }
            catch (UnauthorizedAccessException a)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnauthorizedAccessException), a, $" unauthorized access to execute GET '{this.server}api/unlock/{name}/{lockid}' on controller {this.GetType().Name}");
                throw;
            }
            catch (Exception e)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnspecifiedException), e, $" failed to execute GET '{this.server}api/unlock/{name}/{lockid}' on controller {this.GetType().Name}");
                throw;
            }
        }

        [HttpDelete("{application}/{name}/{lockid}")]
        public void DeleteInApplication(string application, string name, string lockid)
        {
            try
            {
                string userName = this.HttpContext.User.Identity.Name;
                Repositories<T>.Instance.Delete(name, lockid, userName, application);
            }
            catch (UnauthorizedAccessException a)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnauthorizedAccessException), a, $" unauthorized access to execute DELETE '{this.server}api/{application}/{name}/{lockid}' on controller {this.GetType().Name}");
                throw;
            }
            catch (Exception e)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnspecifiedException), e, $" failed to execute DELETE '{this.server}api/{application}/{name}/{lockid}' on controller {this.GetType().Name}");
                throw;
            }
        }

        [HttpDelete("{name}/{lockid}")]
        public void Delete(string name, string lockid)
        {
            try
            {
                string userName = this.HttpContext.User.Identity.Name;
                Repositories<T>.Instance.Delete(name, lockid, userName);
            }
            catch (UnauthorizedAccessException a)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnauthorizedAccessException), a, $" unauthorized access to execute DELETE '{this.server}api/{name}/{lockid}' on controller {this.GetType().Name}");
                throw;
            }
            catch (Exception e)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnspecifiedException), e, $" failed to execute DELETE '{this.server}api/{name}/{lockid}' on controller {this.GetType().Name}");
                throw;
            }
        }





        private readonly ILogger<GenericCrudController<T>> logger;
        private readonly string server;
    }
}