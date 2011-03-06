using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using ExpenseSystem.Repositories;
using ExpenseSystem.Model;
using ExpenseSystem.Repositories.Responses;
using Dto = ExpenseSystem.Entities;

namespace ExpenseSystem.MSpecTests.Repositories.User
{
    public class when_user_provide_userId_which_does_not_exist : UserBaseTester
    {
        Because of = () => result = userRepository.GetById(-5, -5);

        It should_contains_response = () => result.ShouldNotBeNull();
        It should_return_null_for_user = () => result.Object.ShouldBeNull();
        It should_contains_error_indicator_as_true = () => result.IsError.ShouldEqual(true);
        static GetObjectResponse<Dto.User> result;
    }

    public class when_user_provide_userId_which_exists : UserBaseTester
    {
        Because of = () => result = userRepository.GetById(1, 1);

        It should_contains_response = () => result.ShouldNotBeNull();
        It should_return_null_for_user = () => result.Object.ShouldNotBeNull();
        It should_contains_error_indicator_as_true = () => result.IsError.ShouldEqual(false);
        static GetObjectResponse<Dto.User> result;
    }
}
