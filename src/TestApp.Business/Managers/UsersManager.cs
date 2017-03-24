using System;
using System.Collections.Generic;
using System.Linq;

using TestApp.Core.Helpers.Validation;
using TestApp.Core.Interfaces.Managers;
using TestApp.Core.Interfaces.Repositories;
using TestApp.Core.Models;

namespace TestApp.Business.Managers
{
    /// <summary>
    /// Implements functionality to manage users.
    /// </summary>
    /// <seealso cref="TestApp.Core.Interfaces.Managers.IUsersManager" />
    public class UsersManager : IUsersManager
    {
        #region Private_FIelds        
        /// <summary>
        /// The users repository.
        /// </summary>
        private readonly IUsersRepository usersRepository;
        #endregion

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersManager"/> class.
        /// </summary>
        /// <param name="usersRepository">The users repository.</param>
        public UsersManager(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }
        #endregion

        #region Public_Methods
        /// <summary>
        /// Gets all users from repository.
        /// </summary>
        /// <returns>Array of users.</returns>
        public IEnumerable<User> GetAllUsers()
        {
            var users = this.usersRepository.GetAllDocuments();

            return users;
        }

        /// <summary>
        /// Gets single user by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>User document.</returns>
        public User GetUser(int userId)
        {
            Validator.Requires<ArgumentOutOfRangeException>(userId > 0,
                ErrorTemplate.ShouldBeGreaterThanZero, nameof(userId));

            Validator.Requires<ArgumentException>(this.usersRepository.UserExists(userId),
                ErrorTemplate.DoesNotExists, nameof(userId));

            var user = this.usersRepository.GetUser(userId);

            return user;
        }

        /// <summary>
        /// Gets the users who have at least one of specified tags.
        /// </summary>
        /// <param name="tags">The tags array.</param>
        /// <returns>Array of users.</returns>
        public IEnumerable<User> GetUsersWithTags(string[] tags)
        {
            Validator.Requires<ArgumentNullException>(tags?.Length > 0,
                ErrorTemplate.ShouldHaveValue, nameof(tags));

            Validator.Requires<ArgumentException>(!tags.Any(t => string.IsNullOrWhiteSpace(t)),
                ErrorTemplate.Invalid, nameof(tags));

            var users = this.usersRepository.GetUsersWithTags(tags);

            return users;
        }

        /// <summary>
        /// Adds the new user to the database.
        /// </summary>
        /// <param name="user">The user object.</param>
        /// <returns>Created user.</returns>
        public User CreateUser(User user)
        {
            Validator.Requires<ArgumentNullException>(user != null,
                ErrorTemplate.ShouldHaveValue, nameof(user));

            Validator.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(user.FirstName),
                ErrorTemplate.ShouldHaveValue, nameof(user.FirstName));

            Validator.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(user.LastName),
                ErrorTemplate.ShouldHaveValue, nameof(user.LastName));

            Validator.Requires<ArgumentException>(!this.usersRepository.UserExists(user.UserId),
                ErrorTemplate.AlreadyExists, nameof(user.UserId));

            var newUser = this.usersRepository.CreateDocument(user);

            return newUser;
        }

        /// <summary>
        /// Updates the user by user identifier.
        /// </summary>
        /// <param name="user">The user object.</param>
        public void UpdateUser(User user)
        {
            Validator.Requires<ArgumentNullException>(user != null,
                ErrorTemplate.ShouldHaveValue, nameof(user));

            Validator.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(user.FirstName),
                ErrorTemplate.ShouldHaveValue, nameof(user.FirstName));

            Validator.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(user.LastName),
                ErrorTemplate.ShouldHaveValue, nameof(user.LastName));

            User existingUser = this.usersRepository.GetUser(user.UserId);

            Validator.Requires<ArgumentException>(existingUser != null,
                ErrorTemplate.DoesNotExists, nameof(user.UserId));

            user.Id = existingUser.Id;
            this.usersRepository.UpdateDocument(user.Id, user);
        }

        /// <summary>
        /// Deletes the user from database.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void DeleteUser(int userId)
        {
            Validator.Requires<ArgumentOutOfRangeException>(userId > 0,
                ErrorTemplate.ShouldBeGreaterThanZero, nameof(userId));

            Validator.Requires<ArgumentException>(this.usersRepository.UserExists(userId),
                ErrorTemplate.DoesNotExists, nameof(userId));

            this.usersRepository.DeleteUser(userId);
        }
        #endregion
    }
}
