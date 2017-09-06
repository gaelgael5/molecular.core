using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchemaApi.Models.Structures;
using Microsoft.Extensions.Logging;
using SchemaApi.Models;
using Molecular.FileStore;
using Molecular;
using Molecular.Helpers;

namespace SchemaApi.Controllers
{

    // [Produces("application/json")]
    // [ProducesResponseType(typeof(TodoItem), 201)]

    /// <summary>
    /// Generic crud controller that store on hard disk
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public abstract class GenericCrudInApplicationController<T> : Controller
        where T : DataModelBase, new()
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCrudController{T}"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public GenericCrudInApplicationController(ILogger<GenericCrudInApplicationController<T>> logger)
        {
            this.logger = logger;
        }
        
        /// <summary>
        /// Item's list stored on disk
        /// </summary>
        /// <param name="application">The application.</param>
        /// <returns></returns>
        [HttpGet("index/{application}")]
        public IndexModelList Index(string application)
        {
            try
            {
                var items = Repositories<T>.Instance.Index(application);
                return items;
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
        }

        /// <summary>
        /// read the specified item
        /// </summary>
        /// <param name="application">path where the model is stored</param>
        /// <param name="name">name of the model</param>
        /// <returns></returns>
        [HttpGet("read/{application}/{name}")]
        public T Read(string application, string name)
        {
            try
            {

                return Repositories<T>.Instance.Read(name, application);

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
        }

        /// <summary>
        /// Create a new empty item with the specified for key
        /// </summary>
        /// <param name="application"></param>
        /// <param name="name">name is the name pprperty of the item you want accessing.</param>
        [HttpGet("create/{application}/{name}")]
        public void Create(string application, string name)
        {

            try
            {

                T item = Repositories<T>.Instance.Create(name);
                item.UpdatedBy = this.HttpContext.User.Identity.Name;
                if (Repositories<T>.Instance.Create(item, application))
                {

                }

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

        }

        /// <summary>
        /// Update the specifed item
        /// </summary>
        /// <param name="application">path where the model is stored</param>
        /// <param name="lockid">unique lockid that the method lock has returned.</param>
        /// <param name="item"></param>
        [HttpPost("update/{application}/{lockid}")]
        public void Update(string application, string lockid, [FromBody]T item)
        {
            try
            {
                string userName = this.HttpContext.User.Identity.Name;
                Repositories<T>.Instance.Update(item, lockid, userName, application);
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
        }

        /// <summary>
        /// Locks item for the specified name.
        /// </summary>
        /// <param name="application">path where the model is stored</param>
        /// <param name="name">name is the name pprperty of the item you want accessing.</param>
        /// <returns>method return a unique lockid that you must return with method that change model</returns>
        [HttpGet("lock/{application}/{name}")]
        public string Lock(string application, string name)
        {
            try
            {
                string userName = this.HttpContext.User.Identity.Name;
                return Repositories<T>.Instance.Lock(name, userName, application);
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
        }

        /// <summary>
        /// unlock specifed item.
        /// if lock id is null you must be Administrator
        /// </summary>
        /// <param name="application">path where the model is stored</param>
        /// <param name="name">name is the name pprperty of the item you want accessing.</param>
        /// <param name="lockid">unique lockid that the method lock has returned.</param>
        [HttpGet("unlock/{application}/{name}/{lockid}")]
        public void Unlock(string application, string name, string lockid)
        {
            try
            {
                string userName = this.HttpContext.User.Identity.Name;
                Repositories<T>.Instance.Unlock(name, lockid, userName, this.HttpContext.User.IsInRole(Constants.Roles.Administrator), application);
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
        }

        /// <summary>
        /// delete item by specified name
        /// </summary>
        /// <param name="application">path where the model is stored</param>
        /// <param name="name">name is the name pprperty of the item you want accessing.</param>
        /// <param name="lockid">unique lockid that the method lock has returned.</param>
        [HttpDelete("{application}/{name}/{lockid}")]
        public void Delete(string application, string name, string lockid)
        {
            try
            {
                string userName = this.HttpContext.User.Identity.Name;
                Repositories<T>.Instance.Delete(name, lockid, userName, application);
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
        }

        private readonly ILogger<GenericCrudInApplicationController<T>> logger;

    }
}
