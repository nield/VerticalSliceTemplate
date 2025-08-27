using System.ComponentModel.DataAnnotations;

namespace VerticalSliceTemplate.Api.Endpoints.V1.Todos;

public class Update : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPutRoute("/todos/{id}", Handler)
            .WithTags(Constants.OpenApi.Tags.Todos)
            .WithDescription("Used to update a single todo");
    }

    public sealed class Request
    {
        public required string Title { get; set; }
        public List<string> Tags { get; set; } = [];
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }

    public static async Task<NoContent> Handler(
        [Required]long id,
        [Validate]Request request,
        IToDoRepository toDoRepository, 
        CancellationToken cancellationToken)
    {
        var todo = await toDoRepository.GetByIdAsync(id, cancellationToken);

        if (todo is null)
        {
            throw new NotFoundException(nameof(ToDoItem), id);
        }

        todo.Title = request.Title;
        todo.Tags = request.Tags;

        await toDoRepository.UpdateAsync(todo, cancellationToken);

        return TypedResults.NoContent();
    }
}
