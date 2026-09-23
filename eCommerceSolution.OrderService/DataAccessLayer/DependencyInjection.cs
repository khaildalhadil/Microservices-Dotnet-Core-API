using DataAccessLayer.Repositories;
using DataAccessLayer.RepositoryContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(
        this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDB")!;

        string finalConnectionString = connectionString
            .Replace("$MONGODB_HOST", Environment.GetEnvironmentVariable("MONGODB_HOST") ?? "localhost")
            .Replace("$MONGODB_PORT", Environment.GetEnvironmentVariable("MONGODB_PORT") ?? "27017");
         
        services.AddSingleton<IMongoClient>(new MongoClient(finalConnectionString));
        services.AddScoped<IMongoDatabase>(provider =>
            provider.GetRequiredService<IMongoClient>().GetDatabase(
                Environment.GetEnvironmentVariable("MONGODB_DATABASE") ?? "OrdersDatabase"));

        services.AddScoped<IOrdersRepository, OrdersRepository>();

        return services;
    }
}
