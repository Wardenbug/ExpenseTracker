using ExpenseTracker.Application.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ExpenseTracker.Application;

public static class ApplicationLayer
{

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        AddCommandAndQueryHandlers(services);

        services.AddValidatorsFromAssembly(typeof(ApplicationLayer).Assembly, includeInternalTypes: true);

        return services;
    }

    private static void AddCommandAndQueryHandlers(IServiceCollection services)
    {
        Assembly assembly = typeof(ApplicationLayer).Assembly;

        var types =
            assembly.GetTypes()
                .Where(type => !type.IsInterface && !type.IsAbstract)
            .SelectMany(
                type => type.GetInterfaces(),
                (t, i) => new { Type = t, Interface = i })
            .Where(x => x.Interface.IsGenericType
                    && (x.Interface.GetGenericTypeDefinition() == typeof(ICommandHandler<,>) ||
                        x.Interface.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))
                  )
            .ToList();

        foreach (var type in types)
        {
            services.AddScoped(type.Interface, type.Type);
        }
    }
}
