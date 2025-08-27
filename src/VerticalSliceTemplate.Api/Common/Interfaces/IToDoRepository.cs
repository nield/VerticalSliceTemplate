namespace VerticalSliceTemplate.Api.Common.Interfaces;

public interface IToDoRepository : IRepository<ToDoItem>
{
    Task DeleteAll(CancellationToken cancellationToken = default);
}
