namespace VerticalSliceTemplate.Api.Infrastructure.Persistance.Repositories;

public class ToDoRepository : BaseRepository<ToDoItem>, IToDoRepository
{
    public ToDoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task DeleteAll(CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE dbo.ToDo", cancellationToken);
    }
}
