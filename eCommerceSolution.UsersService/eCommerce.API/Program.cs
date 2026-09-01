using eCommerce.API.Middlewares;
using eCommerce.Core;
using eCommerce.Infrastructure;

namespace eCommerce.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddInfrastructure();
        builder.Services.AddCore();

        builder.Services.AddControllers();

        // Swagger / OpenAPI. Feed the generated XML doc file so the /// comments show up.
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            var xmlPath = Path.Combine(AppContext.BaseDirectory,
                $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml");
            if (File.Exists(xmlPath))
                options.IncludeXmlComments(xmlPath);
        });

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();
        app.UseExceptionHandlingMiddlewares();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();

        app.Run();
    }
}
