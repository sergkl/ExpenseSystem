using System.Web.Mvc;
using ExpenseSystem.Controllers;
using ExpenseSystem.Behaviour;
using ExpenseSystem.Repositories.Responses;
using Machine.Specifications;
using MoqLib = Moq;

namespace When
{
    /// <summary>
    /// Test class: When I provide tag name
    /// </summary>
    [Subject("When adding new tag")]
    public class I_provide_tag_name : concern_for_tag_controller
    {
        Establish context = () =>
        {
            //Setup init values
            tagName = "New tag";
            parentId = 1;

            //Init mock data
            AddResponse addResponse = new AddResponse();
            addResponse.Id = 2;
            mockTagRepository.Setup(p => p.Add(MoqLib.It.IsAny<int>(), tagName, (int)parentId)).Returns(addResponse);
            controller.TagRepository = mockTagRepository.Object;
            controller.SessionVars.UserId = 1;
        };

        Because of = () => { result = controller.AddTag(tagName, parentId); };

        It tag_should_be_added = () => ((result as JsonResult).Data as AddResponse).Id.ShouldBeGreaterThan(0);

        static string tagName;
        static int? parentId;
    }
}
