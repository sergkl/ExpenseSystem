using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpenseSystem.Entities;
using ExpenseSystem.Model;
using ExpenseSystem.Repositories.Responses;
using ExpenseSystem.Repositories.Interfaces;

namespace ExpenseSystem.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ExpenseSystemEntities context;

        public TagRepository(ExpenseSystemEntities context)
        {
            this.context = context;
        }

        public GetObjectResponse<Tag> GetById(int userId, int id)
        {
            GetObjectResponse<Tag> response = new GetObjectResponse<Tag>();
            response.Object = context.Tags.FirstOrDefault(a => a.Id == id);
            return response;
        }

        public AddResponse Add(int userId, string name, int parentId)
        {
            AddResponse addResponse = new AddResponse();
            Tag tag = new Tag();
            Tag parent = context.Tags.Where(a => a.Id == parentId).First();
            tag.Parent = GetById(userId, parentId).Object;
            tag.Name = name;
            tag.User = new UserRepository(context).GetById(userId, userId).Object;
            context.Tags.AddObject(tag);
            context.Save();
            addResponse.Id = tag.Id;
            return addResponse;
        }

        public Response Delete(int userId, Tag entity)
        {
            while (entity.Children.Count > 0)
            {
                Delete(userId, entity.Children.First());
            }
            context.Tags.DeleteObject(entity);
            context.Save();
            Response response = new Response();
            return response;
        }

        public GetObjectResponse<Tag> GetParentTagByUserId(int userId)
        {
            GetObjectResponse<Tag> response = new GetObjectResponse<Tag>();
            response.Object = context.Tags.FirstOrDefault(a => a.Parent == null && a.User.Id == userId);
            return response;
        }

        public Response ChangeTagName(int userId, int tagId, string tagName)
        {
            Tag tag = GetById(userId, tagId).Object;
            tag.Name = tagName;
            context.Save();
            return new Response();
        }

        public AddResponse Add(int userId, Tag entity)
        {
            AddResponse addResponse = new AddResponse();
            entity.User = new UserRepository(context).GetById(userId, userId).Object;
            context.Tags.AddObject(entity);
            context.Save();
            addResponse.Id = entity.Id;
            return addResponse;
        }

        public Response DeleteById(int userId, int tagId)
        {
            Tag tag = context.Tags.Where(a => a.Id == tagId).First();
            Delete(userId, tag);
            context.Save();
            Response response = new Response();
            return response;
        }

        public GetObjectResponse<string> GetTagFullName(int userId, int tagId)
        {
            GetObjectResponse<string> response = new GetObjectResponse<string>();
            Tag tag = GetById(userId, tagId).Object;
            response.Object = tag.Name;
            Tag parent = tag.Parent;
            while (parent != null && parent.Name != "ExpensesTag")
            {
                response.Object = string.Format("{0}->", parent.Name) + response.Object;
                parent = parent.Parent;
            }
            return response;
        }

        /// <summary>
        /// Get spent money amount by tag. Method returns result including amount by all children branches
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns></returns>
        public GetObjectResponse<decimal> GetSpentAmountByTag(int userId, int tagId, DateTime startDate, DateTime endDate)
        {
            Tag tag = GetById(userId, tagId).Object;
            GetObjectResponse<decimal> response = new GetObjectResponse<decimal>();
            response.Object = tag.ExpenseRecords.Where(a => a.DateStamp >= startDate && a.DateStamp <= endDate).Sum(a => a.Price);
            foreach (Tag childTag in tag.Children)
            {
                response.Object += GetSpentAmountByTag(userId, childTag.Id, startDate, endDate).Object;
            }
            return response;
        }
    }
}
