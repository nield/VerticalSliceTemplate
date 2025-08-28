namespace VerticalSliceTemplate.Api.Endpoints.V1.Todos;

public sealed class Create : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPostRoute("/todos", Handler)
            .WithTags(Constants.OpenApi.Tags.Todos)
            .WithDescription("Create new todo");
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

    public sealed class Response
    {
        public required long Id { get; set; }
    }


    public static async Task<CreatedAtRoute<Response>> Handler(
        [Validate]Request request, 
        IToDoRepository toDoRepository, 
        CancellationToken cancellationToken)
    {
        var newTodoItem = new ToDoItem
        {
            Title = request.Title,
            Tags = request.Tags
        };

        await toDoRepository.AddAsync(newTodoItem, cancellationToken);

        return TypedResults.CreatedAtRoute<Response>(
            new Response { Id = newTodoItem.Id }, "GetToDoById", new { id = newTodoItem.Id });
    }
}
