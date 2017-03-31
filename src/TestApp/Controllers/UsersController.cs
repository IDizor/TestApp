using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using TestApp.Attributes;
using TestApp.Core.Interfaces.Managers;
using TestApp.Core.Models;
using TestApp.Extensions;

namespace TestApp.Controllers
{
    /// <summary>
    /// Implemets API controller to manage users.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("[controller]")]
    public class UsersController : Controller
    {
        #region Private_Fields        
        /// <summary>
        /// The users manager.
        /// </summary>
        private readonly IUsersManager usersManager;
        #endregion

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="usersManager">The users manager.</param>
        public UsersController(IUsersManager usersManager)
        {
            this.usersManager = usersManager;
        }
        #endregion

        /// <summary>
        /// Gets all existing users.
        /// </summary>
        [HttpGet]
        public JsonResult GetAllUsers()
        {
            var users = this.usersManager.GetAllUsers();

            return this.JsonSuccess(users);
        }

        /// <summary>
        /// Gets the user by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        [HttpGet("{userId:int}")]
        public JsonResult GetUser(int userId)
        {
            try
            {
                var user = this.usersManager.GetUser(userId);

                return this.JsonSuccess(user);
            }
            catch (ArgumentException ex)
            {
                return this.JsonError(ex.Message);
            }
        }

        /// <summary>
        /// Gets the users who have at least one of specified tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <returns></returns>
        [HttpGet("with-tags/{tags}")]
        public JsonResult GetUsersWithTags([ModelBinder(BinderType = typeof(ArrayModelBinder))]int[] tags)
        {
            try
            {
                //var users = this.usersManager.GetUsersWithTags(tags);

                return this.JsonSuccess(tags);
            }
            catch (ArgumentException ex)
            {
                return this.JsonError(ex.Message);
            }
        }

        /// <summary>
        /// Creates the new user.
        /// </summary>
        /// <param name="user">The user object.</param>
        [HttpPost]
        public JsonResult CreateUser([FromBody]User user)
        {
            try
            { 
                this.usersManager.CreateUser(user);

                return this.JsonSuccess();
            }
            catch (ArgumentException ex)
            {
                return this.JsonError(ex.Message);
            }
        }

        /// <summary>
        /// Updates the user by user identifier.
        /// </summary>
        /// <param name="user">The user object.</param>
        [HttpPut]
        public JsonResult UpdateUser([FromBody]User user)
        {
            try
            {
                this.usersManager.UpdateUser(user);

                return this.JsonSuccess();
            }
            catch (ArgumentException ex)
            {
                return this.JsonError(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the user by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        [HttpDelete("{userId:int}")]
        public JsonResult DeleteUser(int userId)
        {
            try
            {
                this.usersManager.DeleteUser(userId);

                return this.JsonSuccess();
            }
            catch (ArgumentException ex)
            {
                return this.JsonError(ex.Message);
            }
        }
    }
}
