using System.ComponentModel.DataAnnotations;

namespace VerticalSliceTemplate.Api.Endpoints.V1.Todos;

public class GetById : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGetRoute("/todos/{id}", Handler)
            .WithTags(Constants.OpenApi.Tags.Todos)
            .WithDescription("Get a single ToDo")
            .WithName("GetToDoById");
    }

    public record Response(long Id, string Title, List<string> Tags);

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
}
