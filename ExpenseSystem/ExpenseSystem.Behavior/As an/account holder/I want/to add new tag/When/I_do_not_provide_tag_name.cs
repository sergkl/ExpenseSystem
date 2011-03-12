using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using ExpenseSystem.Controllers;
using ExpenseSystem.Repositories.Responses;
using MoqLib = Moq;
using System.Web.Mvc;
using ExpenseSystem.Common;
using ExpenseSystem.Behaviour;

namespace When
{
    /// <summary>
    /// Test class: When I don't provide tag name
    /// </summary>
    [Subject("When adding new tag")]
    public class I_do_not_provide_tag_name : concern_for_tag_controller
    {
        Establish context = () =>
        {
            //Setup init values
            tagName = string.Empty;
            parentId = 1;

            //Init mock data
            AddResponse addResponse = new AddResponse();
            addResponse.Id = 0;
            addResponse.IsError = true;
            addResponse.Errors.Add(Error.TagNameHasNotBeenSet);
            mockTagRepository.Setup(p => p.Add(MoqLib.It.IsAny<int>(), tagName, (int)parentId)).Returns(addResponse);
            controller.TagRepository = mockTagRepository.Object;
            controller.SessionVars.UserId = 1;
        };

        Because of = () => { result = controller.AddTag(tagName, parentId); };

        It user_should_receive_error_message_that_tag_name_has_not_been_provided = () => ((result as JsonResult).Data as AddResponse).Errors[0].ShouldEqual(Error.TagNameHasNotBeenSet);

        static string tagName;
        static int? parentId;
    }
}
