using BusinessLogicLayer.Services;
using BusinessLogicLayer.ServiceContracts;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddScoped<IOrdersService, OrdersService>();

        // Register all FluentValidation validators in this assembly.
        services.AddValidatorsFromAssemblyContaining<OrdersService>();

        return services;
    }
}
