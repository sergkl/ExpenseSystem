using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpenseSystem.Entities;
using ExpenseSystem.Repositories.Responses;

namespace ExpenseSystem.Repositories.Interfaces
{
    public interface ITagRepository : IEntityRepository<Tag>
    {
        GetObjectResponse<Tag> GetParentTagByUserId(int userId);

        Response ChangeTagName(int userId, int tagId, string tagName);

        AddResponse Add(int userId, string name, int parentId);

        Response DeleteById(int userId, int tagId);

        GetObjectResponse<string> GetTagFullName(int userId, int tagId);

        GetObjectResponse<decimal> GetSpentAmountByTag(int userId, int tagId, DateTime startDate, DateTime endDate);
    }
}
