using ExpenseSystem.Controllers;
using ExpenseSystem.Entities;
using ExpenseSystem.Behaviour;
using ExpenseSystem.Repositories.Responses;
using Machine.Specifications;
using Machine.Specifications.Mvc;
using MoqLib = Moq;

namespace When
{
    /// <summary>
    /// Test class: When I want to get tags tree
    /// </summary>
    [Subject("When getting tags tree")]
    public class I_get_tags_tree : concern_for_tag_controller
    {
        Establish context = () =>
        {
            //Init mock data
            GetObjectResponse<Tag> response = new GetObjectResponse<Tag>();
            response.Object = new Tag() { Id = 1, Name = "Main tag", User = new User() { Id = 1, FirstName = "UserName" } };
            mockTagRepository.Setup(p => p.GetParentTagByUserId(MoqLib.It.IsAny<int>())).Returns(response);
            controller.TagRepository = mockTagRepository.Object;
            controller.SessionVars.UserId = 1;
        };

        Because of = () => { result = controller.GetTagsTree(); };

        It we_should_see_tags_tree = () => (result).ShouldBeAView();
    }
}
