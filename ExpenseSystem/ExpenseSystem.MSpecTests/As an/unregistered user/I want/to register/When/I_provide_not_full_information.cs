using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using ExpenseSystem.Controllers;
using ExpenseSystem.ViewModels.Account;
using System.Web.Mvc;
using MoqLib = Moq;
using System.Web;
using ExpenseSystem.Repositories.Interfaces;

namespace ExpenseSystem.MSpecTests.As_an.unregistered_user.I_want.to_register.When
{
    public class I_provide_not_full_information : concern_for_account_controller
    {
        Establish context = () =>
            {
                mockUserRepository.Setup(p => p.IsLoginExists(MoqLib.It.IsAny<string>())).Returns(false);
                controller.ModelState.AddModelError("Login", "Login is empty. Put login into the field");
                controller.UserRepository = mockUserRepository.Object;
            };

        Because of = () => { result = controller.Registration(new RegistrationViewModel()); };

        It should_return_error_message = () => controller.ModelState.IsValid.ShouldEqual(false);
    }
}
