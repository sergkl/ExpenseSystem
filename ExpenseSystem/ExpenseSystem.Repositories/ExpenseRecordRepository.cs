using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseSystem.Common;
using ExpenseSystem.Entities;
using ExpenseSystem.Model;
using ExpenseSystem.Repositories.Interfaces;
using ExpenseSystem.Repositories.Responses;

namespace ExpenseSystem.Repositories
{
    /// <summary>
    /// Repository to work with expense records
    /// </summary>
    public class ExpenseRecordRepository : IExpenseRecordRepository
    {
        /// <summary>
        /// Context for repository
        /// </summary>
        private readonly ExpenseSystemEntities context;

        public ExpenseRecordRepository(ExpenseSystemEntities context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get expense record by identifier
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="id">Expense record identifier</param>
        /// <returns>Expense record</returns>
        public GetObjectResponse<ExpenseRecord> GetById(int userId, int id)
        {
            var response = new GetObjectResponse<ExpenseRecord>();
            if (HasUserAccess(userId, id))
            {
                response.Object = context.ExpenseRecords.FirstOrDefault(a => a.Id == id);
            }
            else
            {
                response.IsError = true;
                response.Errors.Add(Error.UserDoesNotHaveAccess);
            }
            return response;
        }

        /// <summary>
        /// Add new expense record to database
        /// </summary>
        /// <param name="userId">User Identifier</param>
        /// <param name="entity">Expense record</param>
        /// <returns>Expense record identifier</returns>
        public AddResponse Add(int userId, ExpenseRecord entity)
        {
            var response = new AddResponse();
            if (entity == null)
            {
                response.Errors.Add(Error.ExpenseRecordHasNotBeenSet);
                response.IsError = true;
            }
            else
            {
                context.ExpenseRecords.AddObject(entity);
                context.Save();
                response.Id = entity.Id;
            }
            return response;
        }

        /// <summary>
        /// Delete expense record from datase
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="entity">Expense record identifier</param>
        /// <returns>Execution result</returns>
        public Response Delete(int userId, ExpenseRecord entity)
        {
            var response = new Response();
            if (HasUserAccess(userId, entity.Id))
            {
                context.ExpenseRecords.DeleteObject(entity);
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
        /// Get expense records by choosen tag
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="tagId">Tag identifier</param>
        /// <returns>List of expense records which is in response object</returns>
        public GetObjectResponse<List<ExpenseRecord>> GetExpenseRecordsByTag(int userId, int tagId)
        {
            var response = new GetObjectResponse<List<ExpenseRecord>>();
            
            var tagRepository = new TagRepository(context);
            
            if (tagRepository.HasUserAccess(userId, tagId))
            {
                response.Object = tagRepository.GetById(userId, tagId).Object.ExpenseRecords.ToList();
            }
            else
            {
                response.IsError = true;
                response.Errors.Add(Error.UserDoesNotHaveAccess);
            }
            return response;
        }

        /// <summary>
        /// Add expense record to the system
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="description">Description for expense record</param>
        /// <param name="price">Price</param>
        /// <param name="tagId">Tag identifier for expense record</param>
        /// <param name="dateStamp">Date stamp which shows when money were spent</param>
        /// <returns>Executin result as Response object</returns>
        public Response Add(int userId, string description, decimal price, int tagId, DateTime? dateStamp)
        {
            var response = new AddResponse();

            if (string.IsNullOrEmpty(description) || price == 0 || tagId == 0 || dateStamp == null)
            {
                response.IsError = true;
                response.Errors.Add(Error.ExpenseRecordHasNotBeenSet);
            }
            else
            {
                var expenseRecord = new ExpenseRecord();
                var tagRepository = new TagRepository(context);
                expenseRecord.Tag = tagRepository.GetById(userId, tagId).Object;
                expenseRecord.Description = description;
                expenseRecord.Price = price;
                expenseRecord.DateStamp = (DateTime)dateStamp;
                response = Add(userId, expenseRecord);
            }
            return response;
        }

        /// <summary>
        /// Method deletes expense record from the system
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="expenseRecordId">Expense record identifier. By this identifier we will delete record from the system</param>
        /// <returns>Executin result as Response object</returns>
        public Response Delete(int userId, int expenseRecordId)
        {
            var response = new Response();
            if (HasUserAccess(userId, expenseRecordId))
            {
                Delete(userId, GetById(userId, expenseRecordId).Object);
            }
            else
            {
                response.IsError = true;
                response.Errors.Add(Error.UserDoesNotHaveAccess);
            }
            return response;
        }

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
        public Response Edit(int userId, int expenseRecordId, string description, decimal price, int tagId, DateTime? dateStamp)
        {
            var response = new Response();
            if (HasUserAccess(userId, expenseRecordId))
            {
                if (string.IsNullOrEmpty(description) || price == 0 || tagId == 0 || dateStamp == null)
                {
                    response.IsError = true;
                    response.Errors.Add(Error.ExpenseRecordHasNotBeenSet);
                }
                else
                {
                    var expenseRecord = GetById(userId, expenseRecordId).Object;
                    var tagRepository = new TagRepository(context);
                    expenseRecord.Tag = tagRepository.GetById(userId, tagId).Object;
                    expenseRecord.Description = description;
                    expenseRecord.Price = price;
                    expenseRecord.DateStamp = (DateTime)dateStamp;
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
        /// Method verifies does user have permissions to edit tag or no
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="expenseRecordId">Tag identifier</param>
        /// <returns>Exeuction result. If true then user has access to edit tag, otherwise it returns false</returns>
        public bool HasUserAccess(int userId, int expenseRecordId)
        {
            return (from user in context.Users
                    from tag in user.Tags
                    from expenseRecord in tag.ExpenseRecords
                    where user.Id == userId && expenseRecord.Id == expenseRecordId
                    select expenseRecord).Count() > 0 ? true : false;
        }
    }
}
