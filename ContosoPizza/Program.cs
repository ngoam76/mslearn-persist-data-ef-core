using ContosoPizza.Data; // namespace
using ContosoPizza.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Registers PizzaContext with the ASP.NET Core dependency injection system.
// Defines a SQLite connection string that points to a local file, ContosoPizza.db.
// Specifies that PizzaContext will use the SQLite database provider.
builder.Services.AddSqlite<PizzaContext>("Data Source=ContosoPizza.db");

builder.Services.AddSqlite<PromotionsContext>("Data Source=Promotions/Promotions.db");

builder.Services.AddScoped<PizzaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.CreateDbIfNotExists();

app.MapGet("/", () => @"Contoso Pizza management API. Navigate to /swagger to open the Swagger test UI.");

app.Run();

// in console: create database schema

// create migration named InitialCreate
// dotnet ef migrations add InitialCreate --context PizzaContext
// apply migration
// dotnet ef database update --context PizzaContext

// scaffold DbContext and model classes from Data
// using Sqlite database provider
// resulting DbContext and model classes in Models directories
// dotnet ef dbcontext scaffold "Data Source=Promotions/Promotions.db" Microsoft.EntityFrameworkCore.Sqlite --context-dir Data --output-dir Models