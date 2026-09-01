using BusinessLogicLayer;
using DataAccessLayer;
using ProductsMicroService.API.APIEndpoints;
using ProductsMicroService.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();

// Swagger / OpenAPI.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseExceptionHandlingMiddleware();

app.MapProductAPIEndpoints();

app.Run();
