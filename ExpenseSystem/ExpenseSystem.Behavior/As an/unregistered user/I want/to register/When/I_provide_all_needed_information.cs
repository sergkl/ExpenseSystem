using ExpenseSystem.Controllers;
using ExpenseSystem.Entities;
using ExpenseSystem.Helpers;
using ExpenseSystem.Behaviour;
using ExpenseSystem.Repositories.Interfaces;
using ExpenseSystem.Repositories.Responses;
using ExpenseSystem.ViewModels.Account;
using Machine.Specifications;
using Machine.Specifications.Mvc;
using MoqLib = Moq;

namespace When
{
    /// <summary>
    /// Test class for testing registration process when user provides all needed information
    /// </summary>
    [Subject("When registering user")]
    public class I_provide_all_needed_information : concern_for_account_controller
    {
        Establish context = () =>
        {
            //Setup init values
            registrationViewModel = new RegistrationViewModel();
            registrationViewModel.FirstName = "FirstName";
            registrationViewModel.MiddleName = "MiddleName";
            registrationViewModel.LastName = "LastName";
            registrationViewModel.Email = "test@yahoo.com";
            registrationViewModel.Phone = "123456789";
            registrationViewModel.Login = "login";
            registrationViewModel.Password = "password";
            registrationViewModel.ConfirmPassword = "password";

            //Init mock data
            AddResponse response = new AddResponse();
            response.IsError = false;
            response.Id = 1;

            mockUserRepository.Setup(p => p.IsLoginExists(MoqLib.It.IsAny<string>())).Returns(false);
            mockUserRepository.Setup(p => p.Add(0, MoqLib.It.IsAny<User>())).Returns(response);

            var mockTagRepository = new MoqLib.Mock<ITagRepository>();
            mockTagRepository.Setup(p => p.Add(MoqLib.It.IsAny<int>(), new Tag())).Returns(new AddResponse());

            controller.UserRepository = mockUserRepository.Object;
            controller.TagRepository = mockTagRepository.Object;
        };

        Because of = () => { result = controller.Registration(registrationViewModel); };

        It user_should_be_registered = () => (new SessionHelper()).UserId.ShouldBeGreaterThan(0);
        It user_should_be_redirected_to_home_page = () => result.ShouldRedirectToAction<TagController>(a => a.GetTagsTree());

        static RegistrationViewModel registrationViewModel;
    }
}
