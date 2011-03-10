using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using ExpenseSystem.Entities;

namespace ExpenseSystem.Helpers
{
    public enum SessionVariables
    {
        UserId,
        UserName
    }

    public class SessionHelper
    {
        private static HttpSessionState Session
        {
            get
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Session;
                return null;
            }
        }

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

        public void Clear()
        {
            Session.Abandon();
        }
    }
}