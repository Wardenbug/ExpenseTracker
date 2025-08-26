using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Domain.Expenses;
namespace ExpenseTracker.Application.Expenses.CreateExpense;

public record CreateExpenseCommand(
    string Name,
    CategoryType CategoryType,
    decimal Amount) : ICommand;
