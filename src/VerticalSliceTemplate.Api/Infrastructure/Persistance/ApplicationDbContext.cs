using System.Reflection;

namespace VerticalSliceTemplate.Api.Infrastructure.Persistance;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public static readonly string MigrationTableName = "__EFMigrationsHistory";

    #region DbSets

    public DbSet<ToDoItem> TodoItems => Set<ToDoItem>();

    #endregion

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
