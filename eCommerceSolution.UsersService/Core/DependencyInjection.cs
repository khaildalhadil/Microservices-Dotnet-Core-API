using eCommerce.Core.ServiceContracts;
using eCommerce.Core.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        // Register core services here
        services.AddTransient<IUserService, UserService>();

        // Register all FluentValidation validators in this assembly.
        services.AddValidatorsFromAssemblyContaining<UserService>();

        return services;
    }
}
