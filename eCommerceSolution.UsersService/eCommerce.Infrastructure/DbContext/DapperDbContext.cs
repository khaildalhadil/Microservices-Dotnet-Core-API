using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace eCommerce.Infrastructure.DbContext;

// Holds one open Npgsql connection per scope; Dapper runs on top of it.
public class DapperDbContext : IDisposable
{
    public IDbConnection Connection { get; }

    public DapperDbContext(IConfiguration configuration)
    {
        var connectionStringInit = configuration.GetConnectionString("DefaultConnection")!;

        var connectionString = connectionStringInit.Replace("$NG_HOST", Environment.GetEnvironmentVariable("NG_HOST") ?? "localhost")
            .Replace("$NG_PASSWORD", Environment.GetEnvironmentVariable("NG_PASSWORD") ?? "1111");
        Connection = new NpgsqlConnection(connectionString);
        Connection.Open();
    }

    public void Dispose() => Connection.Dispose();
}
