using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using ExpenseSystem.Behaviour;
using ExpenseSystem.Repositories.Responses;
using ExpenseSystem.Common;
using System.Web.Mvc;

namespace When
{
    /// <summary>
    /// Test class: When I don't select tag
    /// </summary>
    [Subject("When deleting a tag")]
    public class I_do_not_select_tag : concern_for_tag_controller
    {
        Establish context = () =>
        {
            //Setup init values
            tagId = 1;

            //Init mock data
            Response response = new Response();
            response.IsError = true;
            response.Errors.Add(Error.TagHasNotBeenSelected);
            controller.SessionVars.UserId = 1;
            mockTagRepository.Setup(p => p.DeleteById(controller.SessionVars.UserId, tagId)).Returns(response);
            controller.TagRepository = mockTagRepository.Object;
        };

        Because of = () => { result = controller.DeleteTag(tagId); };

        It user_should_receive_error_message_that_tag_has_not_been_selected = () => ((result as JsonResult).Data as Response).Errors[0].ShouldEqual(Error.TagHasNotBeenSelected);

        static int tagId;
    }
}
