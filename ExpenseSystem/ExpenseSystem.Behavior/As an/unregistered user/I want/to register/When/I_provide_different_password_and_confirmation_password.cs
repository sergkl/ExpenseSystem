using ExpenseSystem.Common;
using ExpenseSystem.ViewModels.Account;
using Machine.Specifications;
using MoqLib = Moq;
using ExpenseSystem.Behaviour;

namespace When
{
    /// <summary>
    /// Test class for testing registration process when user provides different password and confirmation assword
    /// </summary>
    [Subject("When registering user")]
    public class I_provide_different_password_and_confirmation_password : concern_for_account_controller
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
            registrationViewModel.ConfirmPassword = "password111";

            //Init mock data
            mockUserRepository.Setup(p => p.IsLoginExists(MoqLib.It.IsAny<string>())).Returns(false);
            controller.UserRepository = mockUserRepository.Object;
        };

        Because of = () => { result = controller.Registration(registrationViewModel); };

        It should_return_error_message = () => controller.ModelState["Password"].Errors[0].ErrorMessage.ShouldEqual(Error.PasswordsMismatch);

        static RegistrationViewModel registrationViewModel;
    }
}
