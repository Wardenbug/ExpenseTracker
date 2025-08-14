using ExpenseTracker.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }
}
