using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpenseSystem.Entities;
using ExpenseSystem.Model;
using ExpenseSystem.Repositories.Responses;
using System.Data.Objects;
using ExpenseSystem.Common;
using ExpenseSystem.Repositories.Interfaces;

namespace ExpenseSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ExpenseSystemEntities context;

        public UserRepository(ExpenseSystemEntities context)
        {
            this.context = context;
        }

        public GetObjectResponse<User> GetById(int userId, int id)
        {
            GetObjectResponse<User> response = new GetObjectResponse<User>();
            if (userId == id)
            {
                response.Object = context.Users.FirstOrDefault(a => a.Id == id);
                if (response.Object == null)
                {
                    response.IsError = true;
                    response.Errors.Add(Error.UserHasNotBeFound);
                }
            }
            else
            {
                response.IsError = true;
                response.Errors.Add(Error.UserHasNotBeFound);
            }
            return response;
        }

        public AddResponse Add(int userId, User entity)
        {
            AddResponse addResponse = new AddResponse();
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
                addResponse.Errors.Add(Error.UserHasntProvidedNotFullNeededInformation);
            }
            else
            {
                context.Users.AddObject(entity);
                context.Save();
                addResponse.Id = entity.Id;
            }
            return addResponse;
        }

        public Response Delete(int userId, User entity)
        {
            Response response = new Response();
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
            Response response = new Response();
            if (context.Users.Where(a => a.Login == login).Count() > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Method checks user credentials
        /// </summary>
        /// <param name="login">Login (user name)</param>
        /// <param name="password">Password</param>
        /// <returns>Execution result: if it is true then user has been passed validation otherwise it returns false</returns>
        public GetObjectResponse<User> GetUserByCredentials(string login, string password)
        {
            GetObjectResponse<User> response = new GetObjectResponse<User>();
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
