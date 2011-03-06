using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpenseSystem.Repositories.Responses;

namespace ExpenseSystem.Repositories
{
    /// <summary>
    /// Interface declares common methods like CRUD (Create, Read, Update, Delete), etc...
    /// </summary>
    /// <typeparam name="TEntity">It is the class of entity</typeparam>
    public interface IEntityRepository<TEntity>
    {
        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="id">Entity identifier</param>
        /// <returns>Returned entity</returns>
        GetObjectResponse<TEntity> GetById(int userId, int id);
        
        /// <summary>
        /// Add new entity to database
        /// </summary>
        /// <param name="userId">User Identifier</param>
        /// <param name="entity">Entity</param>
        /// <returns>Entity identifier</returns>
        AddResponse Add(int userId, TEntity entity);
        
        /// <summary>
        /// Delete entity from datase
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="entity">Entity identifier</param>
        /// <returns>Execution result</returns>
        Response Delete(int userId, TEntity entity);
    }
}
