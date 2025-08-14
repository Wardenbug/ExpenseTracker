using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses;

public sealed class Expense : Entity
{
    private Expense(Guid id,
        Guid userId,
        string name,
        Category category,
        decimal amount,
        DateTime createdOnUtc) : base(id)
    {
        UserId = userId;
        Name = name;
        Category = category;
        Amount = amount;
        CreatedOnUtc = createdOnUtc;
    }

    private Expense() { }

    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public Category Category { get; private set; }
    public decimal Amount { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? UpdatedOnUtc { get; private set; }


    public static Expense Create(
        Guid userId,
        string name,
        Category category,
        decimal amount,
        DateTime createdOnUtc)
    {
        return new Expense(Guid.NewGuid(),
            userId,
            name,
            category,
            amount,
            createdOnUtc);
    }
}