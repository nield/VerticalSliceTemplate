using static VerticalSliceTemplate.Api.Common.Constants;

namespace VerticalSliceTemplate.Api.Endpoints.V1.Todos;

public sealed class DeleteById : IEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDeleteRoute(ApiRoutes.Todos + "/{id}", Handler)
            .WithTags(ApiTags.Todos)
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
