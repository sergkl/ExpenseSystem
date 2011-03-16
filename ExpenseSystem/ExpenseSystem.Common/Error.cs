
namespace ExpenseSystem.Common
{
    /// <summary>
    /// Class contains error messages which can be used in entire application
    /// </summary>
    public class Error
    {
        //User messages
        public const string UserObjectCantBeNull = "User object can't be null";
        public const string UserProvidedNotFullNeededInformation = "User has provided not full needed information";
        public const string CredentialsDontExistsInTheSystem = "Credentials don't exists in the system";
        public const string UserHasNotBeenFound = "User hasn't been found";
        public const string PasswordsMismatch = "Password and confirm. password aren't the same";
        public const string CurrentLoginExists = "Current login is busy. Choose another one";
        
        //Messsages for tag
        public const string TagNameHasNotBeenSet = "Tag name hasn't been set";
        public const string TagHasNotBeenSelected = "Tag hasn't been selected";

        //Messages for expense record
        public const string ExpenseRecordHasNotBeenSet = "Expense record hasn't been set";
        public const string ExpenseRecordHasNotBeenSelected = "Expense record hasn't been selected";
        public const string UserDoesNotHaveAccess = "User doesn't have access to work with the object(s)";
    }
}
