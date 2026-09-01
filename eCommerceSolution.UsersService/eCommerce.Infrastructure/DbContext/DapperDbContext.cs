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
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        Connection = new NpgsqlConnection(connectionString);
        Connection.Open();
    }

    public void Dispose() => Connection.Dispose();
}
