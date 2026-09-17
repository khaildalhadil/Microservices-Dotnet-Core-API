using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(
        this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")!;

        string finalConnectionString = connectionString.Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST") ?? "localhost")
            .Replace("$MYSQL_PASSWORD", Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? "1111");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySQL(finalConnectionString));

        services.AddScoped<IProductsRepository, ProductsRepository>();

        return services;
    }
}
