using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using ExpenseSystem.Model;
using ExpenseSystem.Repositories;

namespace ExpenseSystem.MSpecTests.Repositories.User
{
    public abstract class UserBaseTester
    {
        protected Establish a_user_repository = () => userRepository = new UserRepository(new ExpenseSystemEntities());
        protected static UserRepository userRepository;
    }
}
