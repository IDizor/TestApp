using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using TestApp.Core.Models;

namespace TestApp.Core.Interfaces.Managers
{
    public interface IUsersManager
    {
        IEnumerable<User> GetAllUsers();

        User GetUser(int userId);

        IEnumerable<User> GetUsersWithTags(string[] tags);

        User CreateUser(User user);

        void UpdateUser(ObjectId id, User user);

        void DeleteUser(int userId);
    }
}
