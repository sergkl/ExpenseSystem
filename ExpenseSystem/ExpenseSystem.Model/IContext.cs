using System.Data.Objects;
using ExpenseSystem.Entities;

namespace ExpenseSystem.Model
{
    public interface IContext
    {
        IObjectSet<User> Users { get; }
        IObjectSet<Tag> Tags { get; }
        IObjectSet<ExpenseRecord> ExpenseRecords { get; }

        string Save();
    }
}
