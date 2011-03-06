using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using ExpenseSystem.Repositories;
using ExpenseSystem.Entities;
using ExpenseSystem.Model;

namespace ExpenseSystem.MSpecTests.Repositories.User
{
    public class when_user_exists_we_return_user_object : UserBaseTester
    {
        Because of = () => result = userRepository.GetUserByLogin("test").First();

        It should_contains_user = () => result.ShouldNotBeNull();
        static Entities.User result;
    }

    public class when_user_doesnt_exist_we_return_empty_set : UserBaseTester
    {
        Because of = () => result = userRepository.GetUserByLogin("abrakadabra");

        It should_contains_empty_set = () => result.Count().ShouldEqual(0);
        static IQueryable<Entities.User> result;
    }
}
