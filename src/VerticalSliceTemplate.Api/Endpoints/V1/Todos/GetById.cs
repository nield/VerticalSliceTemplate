using static VerticalSliceTemplate.Api.Common.Constants;

namespace VerticalSliceTemplate.Api.Endpoints.V1.Todos;

public sealed class GetById : IEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGetRoute(ApiRoutes.Todos + "/{id}", Handler)
            .WithTags(ApiTags.Todos)
            .WithDescription("Get a single ToDo")
            .WithName("GetToDoById")
            .Produces(StatusCodes.Status200OK, typeof(Response))
            .Produces(StatusCodes.Status404NotFound);
    }
    
    public static async Task<Response> Handler(
        [Required]long id, 
        IToDoRepository toDoRepository, 
        CancellationToken cancellationToken)
    {
        var todo = await toDoRepository.GetByIdAsync(id, cancellationToken);

        if (todo is null)
        {
            throw new NotFoundException(nameof(ToDoItem), id);
        }

        return new Response(todo.Id, todo.Title, todo.Tags);
    }

    public sealed record Response(long Id, string Title, List<string> Tags);
}
