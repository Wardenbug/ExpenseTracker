using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Application;

public static class ApplicationLayer
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
