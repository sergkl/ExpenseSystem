using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpenseSystem.Model;
using ExpenseSystem.Entities;
using ExpenseSystem.Repositories;
using ExpenseSystem.Repositories.Responses;

namespace ExpenseSystem.Tests.Repositories
{
    [TestClass()]
    public class TagRepositoryTest
    {
        public ExpenseSystemEntities Context { get; set; }
        TagRepository TagRepository;

        public TagRepositoryTest()
        {
            Context = new ExpenseSystemEntities();
            TagRepository = new TagRepository(Context);
        }

        [TestMethod()]
        public void GetParentTagByUserIdTest()
        {
            GetObjectResponse<Tag> response = new GetObjectResponse<Tag>();
            response = TagRepository.GetParentTagByUserId(1);
            Assert.AreEqual(response.IsError, false);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            int userId = 1;
            int tagId = 1;

            GetObjectResponse<Tag> response = new GetObjectResponse<Tag>();
            response = TagRepository.GetById(userId, tagId);
            Assert.AreEqual(response.IsError, false);
        }

        [TestMethod()]
        public void DeleteByIdTest()
        {
            int userId = 1;
            int tagId = 1;
            Response response = TagRepository.DeleteById(userId, tagId);
            Context.Save();
            Assert.AreEqual(response.IsError, false);
        }
    }
}
