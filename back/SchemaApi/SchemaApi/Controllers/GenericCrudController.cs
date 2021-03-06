﻿using System;
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
    public abstract class GenericCrudController<T> : Controller
        where T : DataModelBase, new()
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCrudController{T}"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public GenericCrudController(ILogger<GenericCrudController<T>> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Indexes of stored items.
        /// </summary>
        /// <returns></returns>
        [HttpGet("index")]
        public IndexModelList Index()
        {
            try
            {
                var items = Repositories<T>.Instance.Index(null);
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
        /// Reads item by the specified name.
        /// </summary>
        /// <param name="name">name is the name pprperty of the item you want accessing.</param>
        /// <returns></returns>
        [HttpGet("read/{name}")]
        public T Read(string name)
        {
            try
            {

                return Repositories<T>.Instance.Read(name);

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
        /// Creates empty item by specified name.
        /// </summary>
        /// <param name="name">name is the name pprperty of the item you want accessing.</param>
        [HttpGet("create/{name}")]
        public void Create(string name)
        {

            try
            {

                T item = Repositories<T>.Instance.Create(name);
                item.UpdatedBy = this.HttpContext.User.Identity.Name;
                if (Repositories<T>.Instance.Create(item, name))
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
        /// Updates the specified item.
        /// </summary>
        /// <param name="lockid">unique lockid that the method lock has returned.</param>
        /// <param name="item">The item.</param>
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
        /// <param name="name">name is the name pprperty of the item you want accessing.</param>
        /// <returns>method return a unique lockid that you must return with method that change model</returns>
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
        /// Unlocks the specified name.
        /// </summary>
        /// <param name="name">name is the name pprperty of the item you want accessing.</param>
        /// <param name="lockid">unique lockid that the method lock has returned.</param>
        [HttpGet("unlock/{name}/{lockid}")]
        public void Unlock(string name, string lockid)
        {
            try
            {
                string userName = this.HttpContext.User.Identity.Name;
                Repositories<T>.Instance.Unlock(name, lockid, userName, this.HttpContext.User.IsInRole(Constants.Roles.Administrator));
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
        /// Deletes the specified name.
        /// </summary>
        /// <param name="name">name is the name pprperty of the item you want accessing.</param>
        /// <param name="lockid">unique lockid that the method lock has returned.</param>
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
                logger.LogError(new EventId((int)ExceptionEnm.UnauthorizedAccessException), a, $" unauthorized access to execute {this.Request.Method} '{this.Request.Path}' on controller {this.GetType().Name}");
                throw;
            }
            catch (Exception e)
            {
                logger.LogError(new EventId((int)ExceptionEnm.UnspecifiedException), e, $" failed to execute {this.Request.Method} '{this.Request.Path}' on controller {this.GetType().Name}");
                throw;
            }
        }

        private readonly ILogger<GenericCrudController<T>> logger;

    }
}
