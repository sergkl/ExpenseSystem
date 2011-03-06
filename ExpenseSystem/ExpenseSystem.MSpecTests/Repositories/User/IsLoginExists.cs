using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using ExpenseSystem.Repositories;
using ExpenseSystem.Model;

namespace ExpenseSystem.MSpecTests.Repositories.User
{
    public class when_login_does_not_exist : UserBaseTester
    {
        Because of = () => result = userRepository.IsLoginExists("abrakadabra");    //dummy values for login

        It should_return_false = () => result.ShouldEqual(false);
        static bool result;
    }

    public class when_login_exists : UserBaseTester
    {
        Because of = () => result = userRepository.IsLoginExists("s");    //value which contains in database. TODO: Change to use mockup to don't test database real data

        It should_return_true = () => result.ShouldEqual(true);
        static bool result;
    }
}
