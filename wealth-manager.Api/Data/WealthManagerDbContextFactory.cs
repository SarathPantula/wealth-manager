using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace wealth_manager.Api.Data;

/// <summary>
/// Used by EF Core tools (e.g. dotnet ef migrations add) at design time.
/// </summary>
public class WealthManagerDbContextFactory : IDesignTimeDbContextFactory<WealthManagerDbContext>
{
    /// <summary>Creates a configured DbContext for design-time tools (migrations, scaffolding).</summary>
    /// <param name="args">Command-line arguments (unused; configuration is read from appsettings).</param>
    /// <returns>A new instance of <see cref="WealthManagerDbContext"/> with connection from appsettings.</returns>
    public WealthManagerDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        var optionsBuilder = new DbContextOptionsBuilder<WealthManagerDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new WealthManagerDbContext(optionsBuilder.Options);
    }
}
