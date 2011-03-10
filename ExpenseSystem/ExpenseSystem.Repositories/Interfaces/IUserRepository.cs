using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpenseSystem.Entities;
using ExpenseSystem.Repositories.Responses;

namespace ExpenseSystem.Repositories.Interfaces
{
    public interface IUserRepository : IEntityRepository<User>
    {
        bool IsLoginExists(string login);

        GetObjectResponse<User> GetUserByCredentials(string login, string password);
    }
}
