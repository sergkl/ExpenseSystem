using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using ExpenseSystem.Entities;

namespace ExpenseSystem.Helpers
{
    /// <summary>
    /// Session variables which are used to get session names
    /// </summary>
    public enum SessionVariables
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
                if (Session != null && Session[SessionVariables.UserId.ToString()] != null)
                    return (int)Session[SessionVariables.UserId.ToString()];
                else
                    return 0;
            }
            set
            {
                Session[SessionVariables.UserId.ToString()] = value;
            }
        }

        /// <summary>
        /// User full name. This field include First + Middle + Last name.
        /// </summary>
        public string UserName
        {
            get
            {
                if (Session != null && Session[SessionVariables.UserName.ToString()] != null)
                    return (string)Session[SessionVariables.UserName.ToString()];
                else
                    return null;
                
            }
            set
            {
                Session[SessionVariables.UserName.ToString()] = value;
            }
        }

        /// <summary>
        /// Clear session
        /// </summary>
        public void Clear()
        {
            Session.Abandon();
        }
    }
}