using System.Collections.Generic;

using TestApp.Core.Models;

namespace TestApp.Core.Interfaces.Repositories
{
    public interface IUsersRepository : IRepositoryBase<User>
    {
        User GetUser(int userId);

        IEnumerable<User> GetUsersWithTags(string[] tags);

        void DeleteUser(int userId);

        bool UserExists(int userId);
    }
}
