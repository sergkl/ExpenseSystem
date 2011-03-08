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
        List<ExpenseRecord> GetExpenseRecordsByTag(int userId, int tagId);

        Response Add(int userId, string description, decimal price, int tagId, DateTime dateStamp);

        Response Delete(int userId, int expenseRecordId);

        Response Edit(int userId, int expenseRecordId, string description, decimal price, int tagId, DateTime dateStamp);


    }
}
