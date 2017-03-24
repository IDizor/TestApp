using System.Collections.Generic;
using System.Linq;

using MongoDB.Driver;
using TestApp.Core.Interfaces.Data;
using TestApp.Core.Interfaces.Repositories;
using TestApp.Core.Models;

namespace TestApp.Data.Repositories
{
    /// <summary>
    /// Implements repository for users DB collection.
    /// </summary>
    /// <seealso cref="TestApp.Data.Repositories.RepositoryBase{TestApp.Core.Models.User}" />
    /// <seealso cref="TestApp.Core.Interfaces.Repositories.IUsersRepository" />
    public class UsersRepository : RepositoryBase<User>, IUsersRepository
    {
        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersRepository"/> class.
        /// </summary>
        /// <param name="dataAccess">The database access provider.</param>
        public UsersRepository(IDataAccess dataAccess) : base(dataAccess)
        {
        }
        #endregion

        #region Public_Methods
        /// <summary>
        /// Gets single user by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>User document.</returns>
        public User GetUser(int userId)
        {
            return this.Documents
                .Find(u => u.UserId == userId)
                .SingleOrDefault();
        }

        /// <summary>
        /// Gets the users who have at least one of specified tags.
        /// </summary>
        /// <param name="tags">The tags array.</param>
        /// <returns>Array of users.</returns>
        public IEnumerable<User> GetUsersWithTags(string[] tags)
        {
            return this.Documents
                .Find(u => u.Tags.Any(t => tags.Contains(t)))
                .ToList();
        }

        /// <summary>
        /// Deletes the user from database.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void DeleteUser(int userId)
        {
            this.Documents.DeleteOne(u => u.UserId == userId);
        }

        /// <summary>
        /// Determines whether the user exists.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public bool UserExists(int userId)
        {
            return this.Documents
                .Find(u => u.UserId == userId)
                .Any();
        }
        #endregion
    }
}
