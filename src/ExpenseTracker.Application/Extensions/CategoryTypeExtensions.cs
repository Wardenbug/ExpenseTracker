using ExpenseTracker.Domain.Expenses;

namespace ExpenseTracker.Application.Extensions;

internal static class CategoryTypeExtensions
{
    public static Category ToCategory(this CategoryType categoryType) => categoryType switch
    {
        CategoryType.Groceries => Category.Groceries,
        CategoryType.Leisure => Category.Leisure,
        CategoryType.Electronics => Category.Electronics,
        CategoryType.Utilities => Category.Utilities,
        CategoryType.Clothing => Category.Clothing,
        CategoryType.Health => Category.Health,
        _ => Category.Others
    };
}