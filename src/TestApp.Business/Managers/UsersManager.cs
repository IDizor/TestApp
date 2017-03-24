using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using TestApp.Core.Interfaces.Managers;
using TestApp.Core.Interfaces.Repositories;
using TestApp.Core.Models;
using TestApp.Data.Repositories;

namespace TestApp.Business.Managers
{
    public class UsersManager : IUsersManager
    {
        private readonly IUsersRepository usersRepository;

        public UsersManager(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = this.usersRepository.GetAllDocuments();

            return users;
        }

        public User GetUser(int userId)
        {
            var user = this.usersRepository.GetUser(userId);

            return user;
        }

        public IEnumerable<User> GetUsersWithTags(string[] tags)
        {
            var users = this.usersRepository.GetUsersWithTags(tags);

            return users;
        }

        public User CreateUser(User user)
        {
            var newUser = this.usersRepository.CreateDocument(user);

            return newUser;
        }

        public void UpdateUser(ObjectId id, User user)
        {
            this.usersRepository.UpdateDocument(id, user);
        }

        public void DeleteUser(int userId)
        {
            this.usersRepository.DeleteUser(userId);
        }
    }
}
