using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses;
using ExpenseTracker.Infrastructure.Data;
using ExpenseTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ExpenseTracker.Domain.Users;
using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Infrastructure.Authentication;

namespace ExpenseTracker.Infrastructure;

public static class InfrastuctureLayer
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database") ??
            throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options
            .UseNpgsql(
                connectionString,
                options => options
                    .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemes.Expenses)
                )
            .UseSnakeCaseNamingConvention();
        });


        services.AddDbContext<ApplicationIdentityDbContext>(options =>
        {
            options
            .UseNpgsql(
                connectionString,
                options => options
                    .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemes.Identity)
                )
            .UseSnakeCaseNamingConvention();
        });

        services.AddIdentityCore<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

        services.AddScoped<IUnitOfWork>(serviceProvider =>
            serviceProvider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}
