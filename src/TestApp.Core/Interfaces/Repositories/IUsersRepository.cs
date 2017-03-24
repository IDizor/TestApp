using System.Collections.Generic;

using TestApp.Core.Models;

namespace TestApp.Core.Interfaces.Repositories
{
    /// <summary>
    /// Represents methods for users repository.
    /// </summary>
    /// <seealso cref="TestApp.Core.Interfaces.Repositories.IRepositoryBase{TestApp.Core.Models.User}" />
    public interface IUsersRepository : IRepositoryBase<User>
    {
        /// <summary>
        /// Gets single user by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>User document.</returns>
        User GetUser(int userId);

        /// <summary>
        /// Gets the users who have at least one of specified tags.
        /// </summary>
        /// <param name="tags">The tags array.</param>
        /// <returns>Array of users.</returns>
        IEnumerable<User> GetUsersWithTags(string[] tags);

        /// <summary>
        /// Deletes the user from database.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        void DeleteUser(int userId);

        /// <summary>
        /// Determines whether the user exists.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        bool UserExists(int userId);
    }
}
