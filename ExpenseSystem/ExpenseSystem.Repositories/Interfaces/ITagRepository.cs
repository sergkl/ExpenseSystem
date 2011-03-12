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
        /// <summary>
        /// Method get the main tag for all tree of tagg.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>Execution result with object as main tag</returns>
        GetObjectResponse<Tag> GetParentTagByUserId(int userId);

        /// <summary>
        /// Changes tag name
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="tagId">Tag identifier</param>
        /// <param name="tagName">Tag name</param>
        /// <returns>Execution result of changing name</returns>
        Response ChangeTagName(int userId, int tagId, string tagName);

        /// <summary>
        /// Add tag to database
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="name">Caption for new tag</param>
        /// <param name="parentId">Parent identifier for tag</param>
        /// <returns>Execution result with identifier for new tag</returns>
        AddResponse Add(int userId, string name, int parentId);

        /// <summary>
        /// Deletes tag from the system
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="tagId">Tag identifier</param>
        /// <returns>Execution result</returns>
        Response DeleteById(int userId, int tagId);

        /// <summary>
        /// Get tag full name
        /// </summary>
        /// <example>
        /// TagLevel1->TagLevel2->TagLevel3...etc...
        /// </example>>
        /// <param name="userId">User identifier</param>
        /// <param name="tagId">Tag identifier</param>
        /// <returns>Tag full name</returns>
        GetObjectResponse<string> GetTagFullName(int userId, int tagId);

        /// <summary>
        /// Get spent amount by tag
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="tagId">Tag identifier</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Execution result with sum. by choosen tag</returns>
        GetObjectResponse<decimal> GetSpentAmountByTag(int userId, int tagId, DateTime startDate, DateTime endDate);
    }
}
