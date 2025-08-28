namespace VerticalSliceTemplate.Api.Endpoints.V1.Todos;

public sealed class DeleteById : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDeleteRoute("/todos/{id}", Handler)
            .WithTags(Constants.OpenApi.Tags.Todos)
            .WithDescription("Used to delete a single todo");
    }

    public static async Task<NoContent> Handler(
        [Required]long id, 
        IToDoRepository toDoRepository,
        CancellationToken cancellationToken)
    {
        var todo = await toDoRepository.GetByIdAsync(id, cancellationToken);

        if (todo is null)
        {
            throw new NotFoundException(nameof(ToDoItem), id);
        }

        await toDoRepository.DeleteAsync(todo, cancellationToken);

        return TypedResults.NoContent();
    }
}
