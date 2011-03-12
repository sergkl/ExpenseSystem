using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using ExpenseSystem.Repositories.Responses;
using System.Web.Mvc;
using ExpenseSystem.Behaviour;

namespace When
{
    /// <summary>
    /// Test class: When I don't provide tag name
    /// </summary>
    [Subject("When editing a tag")]
    public class I_provide_tag_name_in_changing : concern_for_tag_controller
    {
        Establish context = () =>
        {
            //Setup init values
            tagId = 1;
            tagName = "New tag name";

            //Init mock data
            Response response = new Response();
            response.IsError = false;
            controller.SessionVars.UserId = 1;
            mockTagRepository.Setup(p => p.ChangeTagName(controller.SessionVars.UserId, tagId, tagName)).Returns(response);
            controller.TagRepository = mockTagRepository.Object;
        };

        Because of = () => { result = controller.ChangeTagName(tagId, tagName); };

        It user_should_receive_error_message_that_tag_name_has_not_been_provided = () => ((result as JsonResult).Data as Response).IsError.ShouldEqual(false);

        static int tagId;
        static string tagName;
    }
}
