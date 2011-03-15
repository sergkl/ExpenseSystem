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
