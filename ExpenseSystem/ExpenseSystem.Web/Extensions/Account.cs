using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseSystem.Entities;
using ExpenseSystem.ViewModels.Account;

namespace ExpenseSystem.Extensions
{
    /// <summary>
    /// Static class for Account to help working with extensions
    /// </summary>
    public static class Account
    {
        /// <summary>
        /// Extension transformate User to RegistrationViewModel object
        /// </summary>
        /// <param name="user">User DTO object</param>
        /// <returns>RegistrationViewModel object which contains data from User DTO object</returns>
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

        /// <summary>
        /// Extension transformate RegistrationViewModel to User object
        /// </summary>
        /// <param name="userViewModel">UserViewModel object which will be used to create User DTO object</param>
        /// <returns>User DTO object which will be contain data from UserViewModel object</returns>
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