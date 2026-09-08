namespace VerticalSliceTemplate.Api.Infrastructure.Persistance.Repositories;

public class ToDoRepository : BaseRepository<ToDoItem>, IToDoRepository
{
    public ToDoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task DeleteAll(CancellationToken cancellationToken = default)
    {
        await DbSet.ExecuteDeleteAsync(cancellationToken);
    }
}
