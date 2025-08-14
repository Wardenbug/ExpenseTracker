namespace ExpenseTracker.Domain.Expenses;

public record Category(CategoryType Type, string DisplayName)
{
    public static readonly Category Groceries = new(CategoryType.Groceries, "Groceries");
    public static readonly Category Leisure = new(CategoryType.Leisure, "Leisure");
    public static readonly Category Electronics = new(CategoryType.Electronics, "Electronics");
    public static readonly Category Utilities = new(CategoryType.Utilities, "Utilities");
    public static readonly Category Clothing = new(CategoryType.Clothing, "Clothing");
    public static readonly Category Health = new(CategoryType.Health, "Health");
    public static readonly Category Others = new(CategoryType.Others, "Others");

    public static IReadOnlyList<Category> AllCategories => new[]
    {
        Groceries, Leisure, Electronics, Utilities, Clothing, Health, Others
    };
};

