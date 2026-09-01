using Dapper;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;

namespace eCommerce.Infrastructure.Repositories;

public class UsersRepository(DapperDbContext dbContext) : IUserRepository
{
    // Insert a new user and return the stored row. Column names are lowercase (see Data/schema.sql).
    public async Task<ApplicationUser?> AddUser(ApplicationUser user)
    {
        user.UserID = Guid.NewGuid();

        const string sql = """
            INSERT INTO users (userid, email, password, personname, gender)
            VALUES (@UserID, @Email, @Password, @PersonName, @Gender)
            """;

        var rows = await dbContext.Connection.ExecuteAsync(sql, user);

        return rows > 0 ? user : null;
    }

    // Look up a user by email + password. Returns null when there is no match (bad login).
    public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
    {
        const string sql = """
            SELECT userid, email, password, personname, gender
            FROM users
            WHERE email = @Email AND password = @Password
            """;

        return await dbContext.Connection.QueryFirstOrDefaultAsync<ApplicationUser>(
            sql, new { Email = email, Password = password });
    }
}
