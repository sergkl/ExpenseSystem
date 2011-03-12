using ExpenseSystem.Common;
using ExpenseSystem.ViewModels.Account;
using Machine.Specifications;
using MoqLib = Moq;
using ExpenseSystem.Behaviour;

namespace When
{
    /// <summary>
    /// Test class for testing registration process when user provides login which already exists
    /// </summary>
    [Subject("When registering user")]
    public class I_provide_login_which_already_exists : concern_for_account_controller
    {
        Establish context = () =>
        {
            //Setup init values
            registrationViewModel = new RegistrationViewModel();
            registrationViewModel.Login = "Login";

            //Init mock data
            mockUserRepository.Setup(p => p.IsLoginExists(MoqLib.It.IsAny<string>())).Returns(true);
            controller.UserRepository = mockUserRepository.Object;
        };

        Because of = () => { result = controller.Registration(registrationViewModel); };

        It should_return_error_message_that_login_is_already_exists = () => controller.ModelState["Login"].Errors[0].ErrorMessage.ShouldEqual(Error.CurrentLoginExists);

        static RegistrationViewModel registrationViewModel;
    }
}
