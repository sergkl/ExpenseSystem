using ExpenseSystem.ViewModels.Account;
using Machine.Specifications;
using MoqLib = Moq;
using ExpenseSystem.Behaviour;

namespace When
{
    /// <summary>
    /// Test class for testing registration process when user provides not full information
    /// </summary>
    [Subject("When registering user")]
    public class I_provide_not_full_information : concern_for_account_controller
    {
        Establish context = () =>
            {
                //Init mock data
                mockUserRepository.Setup(p => p.IsLoginExists(MoqLib.It.IsAny<string>())).Returns(false);
                controller.ModelState.AddModelError("Login", "Login is empty. Put login into the field");
                controller.UserRepository = mockUserRepository.Object;
            };

        Because of = () => { result = controller.Registration(new RegistrationViewModel()); };

        It should_return_error_message = () => controller.ModelState.IsValid.ShouldEqual(false);
    }
}
