﻿using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseSystem.Entities;
using ExpenseSystem.Model;
using ExpenseSystem.Repositories.Interfaces;
using ExpenseSystem.Repositories.Responses;
using ExpenseSystem.Common;

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
            GetObjectResponse<ExpenseRecord> response = new GetObjectResponse<ExpenseRecord>();
            response.Object = context.ExpenseRecords.FirstOrDefault(a => a.Id == id);
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
            AddResponse response = new AddResponse();
            context.ExpenseRecords.AddObject(entity);
            context.Save();
            response.Id = entity.Id;
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
            Response response = new Response(); //TODO: Implement validation for it and other repository methods. I didn't have enough time to do it :(
            context.ExpenseRecords.DeleteObject(entity);
            context.Save();
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
            GetObjectResponse<List<ExpenseRecord>> response = new GetObjectResponse<List<ExpenseRecord>>();
            TagRepository tagRepository = new TagRepository(context);
            response.Object = tagRepository.GetById(userId, tagId).Object.ExpenseRecords.ToList();
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
            AddResponse response = new AddResponse();

            if (string.IsNullOrEmpty(description) || price == 0 || tagId == 0 || dateStamp == null)
            {
                response.IsError = true;
                response.Errors.Add(Error.ExpenseRecordDataIsNotSet);
            }
            else
            {
                ExpenseRecord expenseRecord = new ExpenseRecord();
                TagRepository tagRepository = new TagRepository(context);
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
            Response response = new Response(); //TODO: Implement validation for it and other repository methods. I didn't have enough time to do it :(
            Delete(userId, GetById(userId, expenseRecordId).Object);
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
            Response response = new Response();
            if (string.IsNullOrEmpty(description) || price == 0 || tagId == 0 || dateStamp == null)
            {
                response.IsError = true;
                response.Errors.Add(Error.ExpenseRecordDataIsNotSet);
            }
            else
            {
                ExpenseRecord expenseRecord = GetById(userId, expenseRecordId).Object;
                TagRepository tagRepository = new TagRepository(context);
                expenseRecord.Tag = tagRepository.GetById(userId, tagId).Object;
                expenseRecord.Description = description;
                expenseRecord.Price = price;
                expenseRecord.DateStamp = (DateTime)dateStamp;
                context.Save();
            }
            return response;
        }
    }
}
