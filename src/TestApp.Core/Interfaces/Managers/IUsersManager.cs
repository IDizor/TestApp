using System.Collections.Generic;

using TestApp.Core.Models;

namespace TestApp.Core.Interfaces.Managers
{
    /// <summary>
    /// Represents methods for UsersManager. 
    /// </summary>
    public interface IUsersManager
    {
        /// <summary>
        /// Gets all users from repository.
        /// </summary>
        /// <returns>Array of users.</returns>
        IEnumerable<User> GetAllUsers();

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
        /// Adds the new user to the database.
        /// </summary>
        /// <param name="user">The user object.</param>
        /// <returns>Created user.</returns>
        User CreateUser(User user);

        /// <summary>
        /// Updates the user by user identifier.
        /// </summary>
        /// <param name="user">The user object.</param>
        void UpdateUser(User user);

        /// <summary>
        /// Deletes the user from database.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        void DeleteUser(int userId);
    }
}
