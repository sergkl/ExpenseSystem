using System.Linq;
using ExpenseSystem.Common;
using ExpenseSystem.Entities;
using ExpenseSystem.Model;
using ExpenseSystem.Repositories.Interfaces;
using ExpenseSystem.Repositories.Responses;

namespace ExpenseSystem.Repositories
{
    /// <summary>
    /// Repository which helps working with users set
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Context for repository
        /// </summary>
        private readonly ExpenseSystemEntities context;

        public UserRepository(ExpenseSystemEntities context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="id">Entity identifier</param>
        /// <returns>Returned entity</returns>
        public GetObjectResponse<User> GetById(int userId, int id)
        {
            var response = new GetObjectResponse<User>();
            if (userId == id)
            {
                response.Object = context.Users.FirstOrDefault(a => a.Id == id);
                if (response.Object == null)
                {
                    response.IsError = true;
                    response.Errors.Add(Error.UserHasNotBeenFound);
                }
            }
            else
            {
                response.IsError = true;
                response.Errors.Add(Error.UserHasNotBeenFound);
            }
            return response;
        }

        /// <summary>
        /// Add new entity to database
        /// </summary>
        /// <param name="userId">User Identifier</param>
        /// <param name="entity">Entity</param>
        /// <returns>Entity identifier</returns>
        public AddResponse Add(int userId, User entity)
        {
            var addResponse = new AddResponse();
            if (entity == null)
            {
                addResponse.IsError = true;
                addResponse.Id = 0;
                addResponse.Errors.Add(Error.UserObjectCantBeNull);
            }
            else if (string.IsNullOrEmpty(entity.Login) ||
                     string.IsNullOrEmpty(entity.Password) ||
                     string.IsNullOrEmpty(entity.FirstName) ||
                     string.IsNullOrEmpty(entity.LastName) ||
                     string.IsNullOrEmpty(entity.Email))
            {
                addResponse.IsError = true;
                addResponse.Id = 0;
                addResponse.Errors.Add(Error.UserProvidedNotFullNeededInformation);
            }
            else
            {
                context.Users.AddObject(entity);
                context.Save();
                addResponse.Id = entity.Id;
            }
            return addResponse;
        }

        /// <summary>
        /// Delete entity from datase
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="entity">Entity identifier</param>
        /// <returns>Execution result</returns>
        public Response Delete(int userId, User entity)
        {
            var response = new Response();
            context.Users.DeleteObject(entity);
            context.Save();
            return response;
        }

        /// <summary>
        /// Method verifies present this login in the system or no
        /// </summary>
        /// <param name="login">Login</param>
        /// <returns>Execution result</returns>
        public bool IsLoginExists(string login)
        {
            return context.Users.Where(a => a.Login == login).Count() > 0;
        }

        /// <summary>
        /// Method checks user credentials
        /// </summary>
        /// <param name="login">Login (user name)</param>
        /// <param name="password">Password</param>
        /// <returns>Execution result: if it is true then user has been passed validation otherwise it returns false</returns>
        public GetObjectResponse<User> GetUserByCredentials(string login, string password)
        {
            var response = new GetObjectResponse<User>();
            var users = context.Users.Where(a => a.Login == login && a.Password == password);
            if (users.Count() == 1)
            {
                response.Object = users.First();
            }
            else
            {
                response.IsError = true;
                response.Errors.Add(Error.CredentialsDontExistsInTheSystem);
            }
            return response;
        }
    }
}
