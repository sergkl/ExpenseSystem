using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoqLib = Moq;
using Machine.Specifications;
using ExpenseSystem.ViewModels.Account;
using ExpenseSystem.Common;

namespace ExpenseSystem.MSpecTests.As_an.unregistered_user.I_want.to_register.When
{
    public class I_provide_different_password_and_confirmation_password : concern_for_account_controller
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
            registrationViewModel.ConfirmPassword = "password111";

            mockUserRepository.Setup(p => p.IsLoginExists(MoqLib.It.IsAny<string>())).Returns(false);
            controller.UserRepository = mockUserRepository.Object;
        };

        Because of = () => { result = controller.Registration(registrationViewModel); };

        It should_return_error_message = () => controller.ModelState["Password"].Errors[0].ErrorMessage.ShouldEqual(Error.PasswordsMismatch);

        static RegistrationViewModel registrationViewModel;
    }
}
