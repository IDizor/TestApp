using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using TestApp.Core.Interfaces.Data;
using TestApp.Core.Interfaces.Repositories;
using TestApp.Core.Models;

namespace TestApp.Data.Repositories
{
    public class UsersRepository : RepositoryBase<User>, IUsersRepository
    {
        public UsersRepository(IDataAccess dataAccess) : base(dataAccess)
        {
        }

        public User GetUser(int userId)
        {
            return this.Documents
                .Find(u => u.UserId == userId)
                .SingleOrDefault();
        }

        public IEnumerable<User> GetUsersWithTags(string[] tags)
        {
            return this.Documents
                .Find(u => u.Tags.Any(t => tags.Contains(t)))
                .ToList();
        }

        public void DeleteUser(int userId)
        {
            this.Documents.DeleteOne(u => u.UserId == userId);
        }

        public bool UserExists(int userId)
        {
            return this.Documents
                .Find(u => u.UserId == userId)
                .Any();
        }
    }
}
