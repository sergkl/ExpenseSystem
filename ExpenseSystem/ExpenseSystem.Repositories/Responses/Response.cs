using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpenseSystem.Repositories.Responses
{
    public class Response
    {
        public bool IsError;
        public List<string> Errors;

        public Response()
        {
            IsError = false;
            Errors = new List<string>();
        }
    }
}
