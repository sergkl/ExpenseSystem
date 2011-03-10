using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpenseSystem.Common
{
    public class Error
    {
        public static string UserObjectCantBeNull = "User object can't be null";
        public static string UserHasntProvidedNotFullNeededInformation = "User hasn't provided not full needed information";
        public static string CredentialsDontExistsInTheSystem = "Credentials don't exists in the system";
        public static string ValueMustBeInteger = "Value must be integer";
        public static string UserHasNotBeFound = "User hasn't been found";
        public static string PasswordsMismatch = "Password and confirm. password aren't the same";
        public static string CurrentLoginExists = "Current login is busy. Choose another one";
    }
}
