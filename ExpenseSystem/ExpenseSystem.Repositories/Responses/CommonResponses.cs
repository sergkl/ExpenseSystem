using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpenseSystem.Entities;

namespace ExpenseSystem.Repositories.Responses
{
    public class GetObjectResponse<T> : Response
    {
        public T Object { get; set; }
    }

    public class AddResponse : Response
    {
        public int Id { get; set; }
    }
}
