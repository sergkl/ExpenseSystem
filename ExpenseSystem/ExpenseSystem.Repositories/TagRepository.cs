using System.Linq;
using ExpenseSystem.Common;
using ExpenseSystem.Entities;
using ExpenseSystem.Model;
using ExpenseSystem.Repositories.Interfaces;
using ExpenseSystem.Repositories.Responses;
using System;

namespace ExpenseSystem.Repositories
{
    /// <summary>
    /// Repository to work with tags
    /// </summary>
    public class TagRepository : ITagRepository
    {
        /// <summary>
        /// Context for repository
        /// </summary>
        private readonly ExpenseSystemEntities context;

        public TagRepository(ExpenseSystemEntities context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get tag by identifier
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="id">Tag identifier</param>
        /// <returns>Returned tag</returns>
        public GetObjectResponse<Tag> GetById(int userId, int id)
        {
            var response = new GetObjectResponse<Tag>();

            if (HasUserAccess(userId, id))
            {
                response.Object = context.Tags.FirstOrDefault(a => a.Id == id);
            }
            else
            {
                response.IsError = true;
                response.Errors.Add(Error.UserDoesNotHaveAccess);
            }

            return response;
        }

        /// <summary>
        /// Add tag to database
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="name">Caption for new tag</param>
        /// <param name="parentId">Parent identifier for tag</param>
        /// <returns>Execution result with identifier for new tag</returns>
        public AddResponse Add(int userId, string name, int parentId)
        {
            var addResponse = new AddResponse();
            if (HasUserAccess(userId, parentId))
            {
                if (string.IsNullOrEmpty(name))
                {
                    addResponse.IsError = true;
                    addResponse.Errors.Add(Error.TagNameHasNotBeenSet);
                }
                else
                {
                    var tag = new Tag();
                    tag.Parent = GetById(userId, parentId).Object;
                    tag.Name = name;
                    tag.User = new UserRepository(context).GetById(userId, userId).Object;
                    context.Tags.AddObject(tag);
                    context.Save();
                    addResponse.Id = tag.Id;
                }
            }
            else
            {
                addResponse.IsError = true;
                addResponse.Errors.Add(Error.UserDoesNotHaveAccess);
            }

            return addResponse;
        }

        /// <summary>
        /// Delete tag from datase
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="entity">Tag identifier</param>
        /// <returns>Execution result</returns>
        public Response Delete(int userId, Tag entity)
        {
            var response = new Response();
            if (entity != null && HasUserAccess(userId, entity.Id))
            {
                while (entity.Children.Count > 0)
                {
                    Delete(userId, entity.Children.First());
                }
                context.Tags.DeleteObject(entity);
                context.Save();
            }
            else
            {
                response.IsError = true;
                response.Errors.Add(Error.UserDoesNotHaveAccess);
            }
            
            return response;
        }

        /// <summary>
        /// Method get the main tag for all tree of tagg.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>Execution result with object as main tag</returns>
        public GetObjectResponse<Tag> GetParentTagByUserId(int userId)
        {
            var response = new GetObjectResponse<Tag>();
            response.Object = context.Tags.FirstOrDefault(a => a.Parent == null && a.User.Id == userId);
            return response;
        }

        /// <summary>
        /// Changes tag name
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="tagId">Tag identifier</param>
        /// <param name="tagName">Tag name</param>
        /// <returns>Execution result of changing name</returns>
        public Response ChangeTagName(int userId, int tagId, string tagName)
        {
            var response = new Response();
            if (HasUserAccess(userId, tagId))
            {
                if (string.IsNullOrEmpty(tagName))
                {
                    response.IsError = true;
                    response.Errors.Add(Error.TagNameHasNotBeenSet);
                }
                else
                {
                    var tag = GetById(userId, tagId).Object;
                    tag.Name = tagName;
                    context.Save();
                }
            }
            else
            {
                response.IsError = true;
                response.Errors.Add(Error.UserDoesNotHaveAccess);
            }

            return response;
        }

        /// <summary>
        /// Add new tag to database
        /// </summary>
        /// <param name="userId">User Identifier</param>
        /// <param name="entity">Tag</param>
        /// <returns>Tag identifier</returns>
        public AddResponse Add(int userId, Tag entity)
        {
            var addResponse = new AddResponse();
            if (entity == null)
            {
                addResponse.IsError = true;
                addResponse.Errors.Add(Error.TagNameHasNotBeenSet);
            }
            else
            {
                entity.User = new UserRepository(context).GetById(userId, userId).Object;
                context.Tags.AddObject(entity);
                context.Save();
                addResponse.Id = entity.Id;
            }
            return addResponse;
        }

        /// <summary>
        /// Deletes tag from the system
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="tagId">Tag identifier</param>
        /// <returns>Execution result</returns>
        public Response DeleteById(int userId, int tagId)
        {
            var response = new Response();
            if (HasUserAccess(userId, tagId))
            {
                var tag = context.Tags.Where(a => a.Id == tagId).FirstOrDefault();
                if (tag == null)
                {
                    response.IsError = true;
                    response.Errors.Add(Error.TagHasNotBeenSelected);
                }
                else
                {
                    Delete(userId, tag);
                    context.Save();
                }  
            }
            else
            {
                response.IsError = true;
                response.Errors.Add(Error.UserDoesNotHaveAccess);
            }

          
            return response;
        }

        /// <summary>
        /// Get tag full name
        /// </summary>
        /// <example>
        /// TagLevel1->TagLevel2->TagLevel3...etc...
        /// </example>>
        /// <param name="userId">User identifier</param>
        /// <param name="tagId">Tag identifier</param>
        /// <returns>Tag full name</returns>
        public GetObjectResponse<string> GetTagFullName(int userId, int tagId)
        {
            var response = new GetObjectResponse<string>();
            if (HasUserAccess(userId, tagId))
            {
                var tag = GetById(userId, tagId).Object;
                response.Object = tag.Name;
                var parent = tag.Parent;
                while (parent != null && parent.Name != "ExpensesTag")
                {
                    response.Object = string.Format("{0}->", parent.Name) + response.Object;
                    parent = parent.Parent;
                }
            }
            else
            {
                response.IsError = true;
                response.Errors.Add(Error.UserDoesNotHaveAccess);
            }
            
            return response;
        }

        /// <summary>
        /// Get spent amount by tag
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="tagId">Tag identifier</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Execution result with sum. by choosen tag</returns>
        public GetObjectResponse<decimal> GetSpentAmountByTag(int userId, int tagId, DateTime startDate, DateTime endDate)
        {
            var response = new GetObjectResponse<decimal>();
            if (HasUserAccess(userId, tagId))
            {
                var tag = GetById(userId, tagId).Object;
                response.Object = tag.ExpenseRecords.Where(a => a.DateStamp >= startDate && a.DateStamp <= endDate).Sum(a => a.Price);
                foreach (var childTag in tag.Children)
                {
                    response.Object += GetSpentAmountByTag(userId, childTag.Id, startDate, endDate).Object;
                }
            }
            else
            {
                response.IsError = true;
                response.Errors.Add(Error.UserDoesNotHaveAccess);
            }

            return response;
        }

        /// <summary>
        /// Method verifies does user have permissions to edit tag or no
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="tagId">Tag identifier</param>
        /// <returns>Exeuction result. If true then user has access to edit tag, otherwise it returns false</returns>
        public bool HasUserAccess(int userId, int tagId)
        {
            return (from user in context.Users
                    from tag in user.Tags
                    where user.Id == userId && tag.Id == tagId
                    select tag).Count() > 0 ? true : false;
        }
    }
}
