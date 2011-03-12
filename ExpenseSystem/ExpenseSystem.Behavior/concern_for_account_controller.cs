using System.Web.Mvc;
using ExpenseSystem.Controllers;
using ExpenseSystem.Repositories.Interfaces;
using Machine.Specifications;
using Moq;

namespace ExpenseSystem.Behaviour
{
    /// <summary>
    /// Class configures main settings for account controller, such as controller, repositories, etc.
    /// </summary>
    public abstract class concern_for_account_controller : concern_controller
    {
        /// <summary>
        /// Establish for test
        /// </summary>
        Establish context = () =>
        {
            mockUserRepository = new Mock<IUserRepository>();
            controller = new AccountController();
            controller.ControllerContext = mockControllerContext.Object;
        };

        /// <summary>
        /// Mockup for user repository
        /// </summary>
        protected static Mock<IUserRepository> mockUserRepository;

        /// <summary>
        /// Mockup for tag repository
        /// </summary>
        protected static Mock<ITagRepository> mockTagRepository;

        /// <summary>
        /// Result which we will verify in test
        /// </summary>
        protected static ActionResult result;

        /// <summary>
        /// Controller for testing
        /// </summary>
        protected static AccountController controller;
    }
}
