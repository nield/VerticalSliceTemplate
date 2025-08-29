namespace VerticalSliceTemplate.Api.Endpoints.V1.Todos;

public sealed class GetAll : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGetRoute("/todos", Handler)
            .WithTags(Constants.OpenApi.Tags.Todos)
            .WithDescription("Get all todos");
    }

    public sealed record Response(long Id, string Title, List<string> Tags);

    public static async Task<IEnumerable<Response>> Handler(
        IApplicationDbContext applicationDbContext, 
        CancellationToken cancellationToken)
    {
        var data = await applicationDbContext.TodoItems
            .AsNoTracking()
            .Select(x => new Response(x.Id, x.Title, x.Tags))
            .ToListAsync(cancellationToken);

        return data;
    }
}
