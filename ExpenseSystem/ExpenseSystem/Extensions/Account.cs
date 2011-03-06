using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseSystem.Entities;
using ExpenseSystem.ViewModels.Account;

namespace ExpenseSystem.Extensions
{
    public static class Account
    {
        public static RegistrationViewModel ToViewModel(this User user)
        {
            RegistrationViewModel userViewModel = new RegistrationViewModel();
            userViewModel.Id = user.Id;
            userViewModel.FirstName = user.FirstName;
            userViewModel.MiddleName = user.MiddleName;
            userViewModel.LastName = user.LastName;
            userViewModel.Email = user.Email;
            userViewModel.Phone = user.Phone;
            userViewModel.Login = user.Login;
            userViewModel.Password = user.Password;
            userViewModel.ConfirmPassword = user.Password;
            return userViewModel;
        }

        public static User ToEntity(this RegistrationViewModel userViewModel)
        {
            User user = new User();
            user.Id = userViewModel.Id;
            user.FirstName = userViewModel.FirstName;
            user.MiddleName = userViewModel.MiddleName;
            user.LastName = userViewModel.LastName;
            user.Email = userViewModel.Email;
            user.Phone = userViewModel.Phone;
            user.Login = userViewModel.Login;
            user.Password = userViewModel.Password;
            return user;
        }
    }
}