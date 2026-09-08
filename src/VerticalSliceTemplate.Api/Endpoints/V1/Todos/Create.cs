using static VerticalSliceTemplate.Api.Common.Constants;

namespace VerticalSliceTemplate.Api.Endpoints.V1.Todos;

public sealed class Create : IEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        // This endpoint/handler example should be used for more complex endpoints with in depth business logic.
        // This example is simple, but this shows how the framework is intended to be used. 
        app.MapPostRoute(ApiRoutes.Todos,
            async ([Validate] Request request, Handler handler, CancellationToken cancellationToken) =>
            {
                var response = await handler.Handle(request, cancellationToken);

                return TypedResults.CreatedAtRoute(
                    new Response { Id = response.Id }, "GetToDoById", new { id = response.Id });
            })
            .WithTags(ApiTags.Todos)
            .WithDescription("Create new todo")
            .Produces<Response>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
    }

    public sealed class Handler(IToDoRepository toDoRepository) : IEndpointHandler
    {
        public async Task<Response> Handle(
            Request request, CancellationToken cancellationToken)
        {
            var newTodoItem = new ToDoItem
            {
                Title = request.Title,
                Tags = request.Tags
            };

            await toDoRepository.AddAsync(newTodoItem, cancellationToken);

            return new Response { Id = newTodoItem.Id };
        }
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
}
