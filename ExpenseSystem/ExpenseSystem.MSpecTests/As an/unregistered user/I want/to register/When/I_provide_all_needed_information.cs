using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoqLib = Moq;
using Machine.Specifications;
using ExpenseSystem.ViewModels.Account;
using ExpenseSystem.Entities;
using ExpenseSystem.Repositories.Responses;
using ExpenseSystem.Repositories.Interfaces;
using System.Web;
using System.Security.Principal;
using ExpenseSystem.Helpers;

namespace ExpenseSystem.MSpecTests.As_an.unregistered_user.I_want.to_register.When
{
    public class I_provide_all_needed_information : concern_for_account_controller
    {
        Establish context = () =>
        {
            registrationViewModel = new RegistrationViewModel();
            registrationViewModel.FirstName = "FirstName";
            registrationViewModel.MiddleName = "MiddleName";
            registrationViewModel.LastName = "LastName";
            registrationViewModel.Email = "test@yahoo.com";
            registrationViewModel.Phone = "123456789";
            registrationViewModel.Login = "login";
            registrationViewModel.Password = "password";
            registrationViewModel.ConfirmPassword = "password";
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

        It user_should_be_registered_and_authenticated_immediately = () => (new SessionHelper()).UserId.ShouldBeGreaterThan(0);

        static RegistrationViewModel registrationViewModel;
    }
}
