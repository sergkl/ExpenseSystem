using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using ExpenseSystem.Repositories;
using ExpenseSystem.Model;
using ExpenseSystem.Repositories.Responses;
using ExpenseSystem.Common;

namespace ExpenseSystem.MSpecTests.Repositories.User
{
    public class when_user_provide_incorrect_credentials : UserBaseTester
    {
        Because of = () => result = userRepository.GetUserByCredentials("abrakadabra", "abrakadabra");

        It should_returns_response_with_message = () => result.Errors[0].ShouldEqual(Error.CredentialsDontExistsInTheSystem);
        It should_contain_error_indicator = () => result.IsError.ShouldEqual(true);
        It user_object_should_be_null = () => result.Object.ShouldBeNull();

        static GetObjectResponse<Entities.User> result;
    }

    public class when_user_provide_correct_credentials : UserBaseTester
    {
        Because of = () => result = userRepository.GetUserByCredentials("s", "s");

        It should_be_no_errors = () => result.Errors.Count.ShouldEqual(0);
        It error_indicator_should_be_false = () => result.IsError.ShouldEqual(false);
        It user_object_should_not_be_null = () => result.Object.ShouldNotBeNull();

        static GetObjectResponse<Entities.User> result;
    }
}
