
namespace ExpenseSystem.Common
{
    /// <summary>
    /// Class contains error messages which can be used in entire application
    /// </summary>
    public class Error
    {
        //Common messages
        public static string ValueMustBeInteger = "Value must be integer";
        public static string RecordHasNotBeenFound = "Record hasn't been found";

        //User messages
        public static string UserObjectCantBeNull = "User object can't be null";
        public static string UserProvidedNotFullNeededInformation = "User has provided not full needed information";
        public static string CredentialsDontExistsInTheSystem = "Credentials don't exists in the system";
        public static string UserHasNotBeenFound = "User hasn't been found";
        public static string PasswordsMismatch = "Password and confirm. password aren't the same";
        public static string CurrentLoginExists = "Current login is busy. Choose another one";
        
        //Messsages for tag
        public static string TagNameHasNotBeenSet = "Tag name hasn't been set";
        public static string TagHasNotBeenSelected = "Tag hasn't been selected";

        //Messages for expense record
        public static string ExpenseRecordDataIsNotSet = "Expense record data hasn't been filled completely";
        public static string ExpenseRecordHasNotBeenSelected = "Expense record hasn't been selected";
    }
}
