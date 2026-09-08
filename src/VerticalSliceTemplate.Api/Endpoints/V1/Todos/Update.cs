using static VerticalSliceTemplate.Api.Common.Constants;

namespace VerticalSliceTemplate.Api.Endpoints.V1.Todos;

public sealed class Update : IEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPutRoute(ApiRoutes.Todos + "/{id}", Handler)
            .WithTags(ApiTags.Todos)
            .WithDescription("Used to update a single todo")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
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
    
    public sealed class Request
    {
        public required string Title { get; set; }
        public List<string> Tags { get; set; } = [];
    }

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}
