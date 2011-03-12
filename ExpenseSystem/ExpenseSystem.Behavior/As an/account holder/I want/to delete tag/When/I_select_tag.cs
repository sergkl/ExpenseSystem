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
    /// Test class: When I select tag
    /// </summary>
    [Subject("When deleting a tag")]
    public class I_select_tag : concern_for_tag_controller
    {
        Establish context = () =>
        {
            //Setup init values
            tagId = 1;

            //Init mock data
            Response response = new Response();
            controller.SessionVars.UserId = 1;
            mockTagRepository.Setup(p => p.DeleteById(controller.SessionVars.UserId, tagId)).Returns(response);
            controller.TagRepository = mockTagRepository.Object;
        };

        Because of = () => { result = controller.DeleteTag(tagId); };

        It tag_should_be_deleted = () => ((result as JsonResult).Data as Response).IsError.ShouldEqual(false);

        static int tagId;
    }
}
