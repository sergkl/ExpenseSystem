using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpenseSystem.Entities;
using ExpenseSystem.Model;
using ExpenseSystem.Repositories.Responses;

namespace ExpenseSystem.Repositories
{
    public class ExpenseRecordRepository : IEntityRepository<ExpenseRecord>
    {
        private readonly ExpenseSystemEntities context;

        public ExpenseRecordRepository(ExpenseSystemEntities context)
        {
            this.context = context;
        }

        public GetObjectResponse<ExpenseRecord> GetById(int userId, int id)
        {
            GetObjectResponse<ExpenseRecord> response = new GetObjectResponse<ExpenseRecord>();
            response.Object = context.ExpenseRecords.FirstOrDefault(a => a.Id == id);
            return response;
        }

        public AddResponse Add(int userId, ExpenseRecord entity)
        {
            AddResponse response = new AddResponse();
            context.ExpenseRecords.AddObject(entity);
            context.Save();
            response.Id = entity.Id;
            return response;
        }

        public Response Delete(int userId, ExpenseRecord entity)
        {
            Response response = new Response(); //TODO: Implement validation for it and other repository methods. I didn't have enough time to do it :(
            context.ExpenseRecords.DeleteObject(entity);
            context.Save();
            return response;
        }

        public List<ExpenseRecord> GetExpenseRecordsByTag(int userId, int tagId)
        {
            TagRepository tagRepository = new TagRepository(context);
            return tagRepository.GetById(userId, tagId).Object.ExpenseRecords.ToList();
        }


        public Response Add(int userId, string description, decimal price, int tagId, DateTime dateStamp)
        {
            AddResponse response = new AddResponse();
            ExpenseRecord expenseRecord = new ExpenseRecord();
            TagRepository tagRepository = new TagRepository(context);
            expenseRecord.Tag = tagRepository.GetById(userId, tagId).Object;
            expenseRecord.Description = description;
            expenseRecord.Price = price;
            expenseRecord.DateStamp = dateStamp;
            response = Add(userId, expenseRecord);
            return response;
        }

        public Response Delete(int userId, int expenseRecordId)
        {
            Response response = new Response(); //TODO: Implement validation for it and other repository methods. I didn't have enough time to do it :(
            context.ExpenseRecords.DeleteObject(GetById(userId, expenseRecordId).Object);
            return response;
        }

        public Response Edit(int userId, int expenseRecordId, string description, decimal price, int tagId, DateTime dateStamp)
        {
            Response response = new Response();
            if (expenseRecordId > 0)
            {
                ExpenseRecord expenseRecord = GetById(userId, expenseRecordId).Object;
                TagRepository tagRepository = new TagRepository(context);
                expenseRecord.Tag = tagRepository.GetById(userId, tagId).Object;
                expenseRecord.Description = description;
                expenseRecord.Price = price;
                expenseRecord.DateStamp = dateStamp;
                context.Save();
            }
            else
            {
                response.IsError = true;
                response.Errors.Add("Record for changing hasn't beed found");
            }
            return response;
        }
    }
}
