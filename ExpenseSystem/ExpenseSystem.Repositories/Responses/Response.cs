using System.Collections.ObjectModel;

namespace ExpenseSystem.Repositories.Responses
{
    public class Response
    {
        private readonly Collection<string> errors;

        public bool IsError { get; set; }
        public Collection<string> Errors
        {
            get { return errors; }
        }

        public Response()
        {
            IsError = false;
            errors = new Collection<string>();
        }
    }
}
