using ExpenseTracker.Domain.Expenses;
using ExpenseTracker.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Infrastructure.Configurations;

internal sealed class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("expenses");
        builder.HasKey(expense => expense.Id);
        builder.Property(expense => expense.Name)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(expense => expense.Category)
            .HasConversion(
                category => category.Type,
                categoryType => Category.AllCategories.First(c => c.Type == categoryType)
            )
            .IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(expense => expense.UserId)
            .IsRequired();
    }
}
