using Microsoft.EntityFrameworkCore;
using wealth_manager.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, ct) =>
    {
        document.Info.Title = "Wealth Manager API";
        document.Info.Version = "1.0";
        document.Info.Description = "API for managing gold assets. Provides CRUD operations for gold assets with value and BIS karat purity.";
        return Task.CompletedTask;
    });
});
builder.Services.AddControllers();

// Add PostgreSQL DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<WealthManagerDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// Code First: ensure database and tables exist (creates GoldAsset table from model)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WealthManagerDbContext>();
    dbContext.Database.EnsureCreated();
}

app.MapOpenApi(); // OpenAPI spec at /openapi/v1.json

app.UseHttpsRedirection();

// Swagger UI (reads the OpenAPI spec above)
app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Wealth Manager API v1"));

app.MapControllers();
app.MapGet("/", () => new { message = "Wealth Manager API", openApi = "/openapi/v1.json", swagger = "/swagger" });

app.Run();
