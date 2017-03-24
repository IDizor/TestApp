using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TestApp.Core.Interfaces.Managers;
using TestApp.Core.Models;
using TestApp.Extensions;

namespace TestApp.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersManager usersManager;

        public UsersController(IUsersManager usersManager)
        {
            this.usersManager = usersManager;
        }

        [HttpGet]
        public JsonResult GetAllUsers()
        {
            var users = this.usersManager.GetAllUsers();

            return this.JsonSuccess(users);
        }

        [HttpGet("{userId:int}")]
        public JsonResult GetUser(int userId)
        {
            var user = this.usersManager.GetUser(userId);

            return this.JsonSuccess(user);
        }

        [HttpPost]
        public JsonResult CreateUser([FromBody]User user)
        {
            this.usersManager.CreateUser(user);

            return this.JsonSuccess();
        }

        [HttpPut]
        public JsonResult UpdateUser([FromBody]User user)
        {
            this.usersManager.UpdateUser(user.Id, user);

            return this.JsonSuccess();
        }

        [HttpDelete("{userId:int}")]
        public JsonResult DeleteUser(int userId)
        {
            this.usersManager.DeleteUser(userId);

            return this.JsonSuccess();
        }
    }
}
