using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using ExpenseSystem.Repositories;
using ExpenseSystem.Model;
using ExpenseSystem.Repositories.Responses;
using ExpenseSystem.Common;

namespace ExpenseSystem.MSpecTests.Repositories.User.Add
{
    public class when_user_provide_all_needed_information_we_should_receive_identifier_for_this_user
    {
        Establish a_user_repository = () => userRepository = new UserRepository(new ExpenseSystemEntities());

        Because of = () => addResponse = userRepository.Add(0, new Entities.User() { Login = "sergey", FirstName = "Sergey", LastName = "Kliuev", MiddleName = "Sergeevich", Email = "sergkl@bk.ru", Password = "qwer", Phone = "095850389" });

        It response_should_be = () => addResponse.ShouldNotBeNull();
        It response_should_be_with_identifier = () => addResponse.Id.ShouldBeGreaterThan(0);
        static AddResponse addResponse;
        static UserRepository userRepository;
    }

    public class when_user_provide_null_instead_correct_user_object
    {
        Establish a_user_repository = () => userRepository = new UserRepository(new ExpenseSystemEntities());

        Because of = () => addResponse = userRepository.Add(0, null);

        It response_should_be = () => addResponse.ShouldNotBeNull();
        It response_should_be_with_identifier_0 = () => addResponse.Id.ShouldEqual(0);
        It response_should_contain_error_status = () => addResponse.IsError.ShouldEqual(true);
        It response_should_contain_error_description_that_user_can_not_be_null = () => addResponse.Errors[0].ShouldEqual(Error.UserObjectCantBeNull);
        static AddResponse addResponse;
        static UserRepository userRepository;
    }

    public class when_user_provide_not_full_needed_information
    {
        Establish a_user_repository = () => userRepository = new UserRepository(new ExpenseSystemEntities());

        Because of = () => addResponse = userRepository.Add(0, new Entities.User() { FirstName = "Sergey" });

        It response_should_be = () => addResponse.ShouldNotBeNull();
        It response_should_be_with_identifier_0 = () => addResponse.Id.ShouldEqual(0);
        It response_should_contain_error_status = () => addResponse.IsError.ShouldEqual(true);
        It response_should_contain_error_description_that_user_provided_not_full_needed_info = () => addResponse.Errors[0].ShouldEqual(Error.UserHasntProvidedNotFullNeededInformation);
        static AddResponse addResponse;
        static UserRepository userRepository;
    }
}
