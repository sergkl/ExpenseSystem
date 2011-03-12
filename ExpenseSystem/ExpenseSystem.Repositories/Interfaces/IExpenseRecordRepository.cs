using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpenseSystem.Entities;
using ExpenseSystem.Repositories.Responses;

namespace ExpenseSystem.Repositories.Interfaces
{
    public interface IExpenseRecordRepository : IEntityRepository<ExpenseRecord>
    {
        /// <summary>
        /// Get expense records by choosen tag
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="tagId">Tag identifier</param>
        /// <returns>List of expense records which is in response object</returns>
        GetObjectResponse<List<ExpenseRecord>> GetExpenseRecordsByTag(int userId, int tagId);

        /// <summary>
        /// Add expense record to the system
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="description">Description for expense record</param>
        /// <param name="price">Price</param>
        /// <param name="tagId">Tag identifier for expense record</param>
        /// <param name="dateStamp">Date stamp which shows when money were spent</param>
        /// <returns>Executin result as Response object</returns>
        Response Add(int userId, string description, decimal price, int tagId, DateTime? dateStamp);

        /// <summary>
        /// Method deletes expense record from the system
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="expenseRecordId">Expense record identifier. By this identifier we will delete record from the system</param>
        /// <returns>Executin result as Response object</returns>
        Response Delete(int userId, int expenseRecordId);

        /// <summary>
        /// Method updates the expense record
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="expenseRecordId">Expense record identifier. By this identifier we will find record for changing</param>
        /// <param name="description">Description for expense record</param>
        /// <param name="price">Price</param>
        /// <param name="tagId">Tag identifier for expense record</param>
        /// <param name="dateStamp">Date stamp which shows when money were spent</param>
        /// <returns></returns>
        Response Edit(int userId, int expenseRecordId, string description, decimal price, int tagId, DateTime? dateStamp);


    }
}
