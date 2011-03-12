using System.Web.Mvc;
using ExpenseSystem.Controllers;
using ExpenseSystem.Repositories.Interfaces;
using Machine.Specifications;
using Moq;

namespace ExpenseSystem.Behaviour
{
    /// <summary>
    /// Class configures main settings for tag controller, such as controller, repositories, etc.
    /// </summary>
    public abstract class concern_for_tag_controller : concern_controller
    {
        /// <summary>
        /// Establish for test
        /// </summary>
        Establish context = () =>
        {
            mockTagRepository = new Mock<ITagRepository>();
            controller = new TagController();
        };

        /// <summary>
        /// Mockup for tag repository
        /// </summary>
        protected static Mock<ITagRepository> mockTagRepository;

        /// <summary>
        /// Result which we verify in test
        /// </summary>
        protected static ActionResult result;

        /// <summary>
        /// Controller for testing
        /// </summary>
        protected static TagController controller;
    }
}
