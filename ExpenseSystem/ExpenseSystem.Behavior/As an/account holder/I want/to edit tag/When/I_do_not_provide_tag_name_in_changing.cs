using ExpenseSystem.Behaviour;
using Machine.Specifications;
using System.Web.Mvc;
using ExpenseSystem.Common;
using ExpenseSystem.Repositories.Responses;
using MoqLib = Moq;

namespace When
{
    /// <summary>
    /// Test class: When I don't provide tag name
    /// </summary>
    [Subject("When editing a tag")]
    public class I_do_not_provide_tag_name_in_changing : concern_for_tag_controller
    {
        Establish context = () =>
        {
            //Setup init values
            tagId = 1;
            tagName = string.Empty;

            //Init mock data
            Response response = new Response();
            response.IsError = true;
            response.Errors.Add(Error.TagNameHasNotBeenSet);
            controller.SessionVars.UserId = 1;
            mockTagRepository.Setup(p => p.ChangeTagName(controller.SessionVars.UserId, tagId, tagName)).Returns(response);
            controller.TagRepository = mockTagRepository.Object;
        };

        Because of = () => { result = controller.ChangeTagName(tagId, tagName); };

        It user_should_receive_error_message_that_tag_name_has_not_been_provided = () => ((result as JsonResult).Data as Response).Errors[0].ShouldEqual(Error.TagNameHasNotBeenSet);

        static int tagId;
        static string tagName;
    }
}
