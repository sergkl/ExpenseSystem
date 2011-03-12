using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Moq;
using ExpenseSystem.Repositories.Interfaces;
using System.Web.Mvc;
using ExpenseSystem.Controllers;

namespace ExpenseSystem.Behaviour
{
    /// <summary>
    /// Class configures main settings for tag controller, such as controller, repositories, etc.
    /// </summary>
    public abstract class concern_for_expense_controller : concern_controller
    {
        /// <summary>
        /// Establish for test
        /// </summary>
        Establish context = () =>
        {
            mockExpenseRecordRepository = new Mock<IExpenseRecordRepository>();
            controller = new ExpenseController();
        };

        /// <summary>
        /// Mockup for expense repository
        /// </summary>
        protected static Mock<IExpenseRecordRepository> mockExpenseRecordRepository;

        /// <summary>
        /// Result which we verify in test
        /// </summary>
        protected static ActionResult result;

        /// <summary>
        /// Controller for testing
        /// </summary>
        protected static ExpenseController controller;
    }
}
