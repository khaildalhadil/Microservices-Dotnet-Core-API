using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;
using eCommerce.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // One connection per use via DapperDbContext; the repository builds Dapper queries on it.
        services.AddTransient<DapperDbContext>();

        services.AddScoped<IUserRepository, UsersRepository>();

        return services;
    }
}
