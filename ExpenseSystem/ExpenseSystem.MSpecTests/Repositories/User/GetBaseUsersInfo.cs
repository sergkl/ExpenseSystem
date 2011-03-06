using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using ExpenseSystem.Repositories;
using ExpenseSystem.Model;
using ExpenseSystem.Entities;

namespace ExpenseSystem.MSpecTests.Repositories.User.GetBaseUsersInfo
{
    public class when_get_list_of_users
    {
        Establish a_user_repository = () => userRepository = new UserRepository(new ExpenseSystemEntities());

        Because of = () => result = userRepository.GetBaseUsersInfo();

        It should_contains_list_of_users = () => result.ShouldNotBeNull();

        static UserRepository userRepository;
        static List<BaseUserInfo> result;
    }
}
