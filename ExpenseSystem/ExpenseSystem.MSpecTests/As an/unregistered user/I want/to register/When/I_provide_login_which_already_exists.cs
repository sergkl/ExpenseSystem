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
    public class I_provide_login_which_already_exists : concern_for_account_controller
    {
        Establish context = () =>
        {
            registrationViewModel = new RegistrationViewModel();
            registrationViewModel.Login = "Login";
            mockUserRepository.Setup(p => p.IsLoginExists(MoqLib.It.IsAny<string>())).Returns(true);
            controller.UserRepository = mockUserRepository.Object;
        };

        Because of = () => { result = controller.Registration(registrationViewModel); };

        It should_return_error_message = () => controller.ModelState["Login"].Errors[0].ErrorMessage.ShouldEqual(Error.CurrentLoginExists);

        static RegistrationViewModel registrationViewModel;
    }
}
