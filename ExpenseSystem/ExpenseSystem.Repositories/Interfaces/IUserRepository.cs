using ExpenseSystem.Entities;
using ExpenseSystem.Repositories.Responses;

namespace ExpenseSystem.Repositories.Interfaces
{
    /// <summary>
    /// Interface for user repository
    /// </summary>
    public interface IUserRepository : IEntityRepository<User>
    {
        /// <summary>
        /// Method verifies present this login in the system or no
        /// </summary>
        /// <param name="login">Login</param>
        /// <returns>Execution result</returns>
        bool IsLoginExists(string login);

        /// <summary>
        /// Method checks user credentials
        /// </summary>
        /// <param name="login">Login (user name)</param>
        /// <param name="password">Password</param>
        /// <returns>Execution result: if it is true then user has been passed validation otherwise it returns false</returns>
        GetObjectResponse<User> GetUserByCredentials(string login, string password);
    }
}
