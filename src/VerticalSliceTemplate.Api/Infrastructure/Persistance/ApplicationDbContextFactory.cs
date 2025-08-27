using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Design;

namespace VerticalSliceTemplate.Api.Infrastructure.Persistance;

[ExcludeFromCodeCoverage]
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        var config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

        optionsBuilder.UseSqlServer(
            config.GetConnectionString("SqlDatabase"));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}