using System.Web;
using System.Web.SessionState;

namespace ExpenseSystem.Helpers
{
    /// <summary>
    /// Session variables which are used to get session names
    /// </summary>
    public enum SessionVariable
    {
        UserId,
        UserName
    }

    /// <summary>
    /// Class helps working with sessions 
    /// </summary>
    public class SessionHelper
    {
        /// <summary>
        /// Current session state
        /// </summary>
        private static HttpSessionState Session
        {
            get
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Session;
                return null;
            }
        }

        /// <summary>
        /// User identifier
        /// </summary>
        public int UserId
        {
            get
            {
                if (Session != null && Session[SessionVariable.UserId.ToString()] != null)
                    return (int)Session[SessionVariable.UserId.ToString()];
                else
                    return 0;
            }
            set
            {
                Session[SessionVariable.UserId.ToString()] = value;
            }
        }

        /// <summary>
        /// User full name. This field include First + Middle + Last name.
        /// </summary>
        public string UserName
        {
            get
            {
                if (Session != null && Session[SessionVariable.UserName.ToString()] != null)
                    return (string)Session[SessionVariable.UserName.ToString()];
                else
                    return null;
                
            }
            set
            {
                Session[SessionVariable.UserName.ToString()] = value;
            }
        }

        /// <summary>
        /// Clear session
        /// </summary>
        public static void Clear()
        {
            Session.Abandon();
        }
    }
}