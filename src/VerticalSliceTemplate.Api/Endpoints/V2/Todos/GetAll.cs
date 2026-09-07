namespace VerticalSliceTemplate.Api.Endpoints.V2.Todos;

public sealed class GetAll : IEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGetRoute("/todos", Handler, 2)
            .WithTags(Constants.OpenApi.Tags.Todos)
            .WithDescription("Used to get all todos");
    }

    public static async Task<IEnumerable<Response>> Handler(
        IApplicationDbContext applicationDbContext,
        CancellationToken cancellationToken)
    {
        var data = await applicationDbContext.TodoItems
            .AsNoTracking()
            .Select(x => new Response(x.Id, x.Title, x.Tags, x.CreatedBy))
            .ToListAsync(cancellationToken);

        return data;
    }
    
    public sealed record Response(long Id, string Title, List<string> Tags, string? CreatedBy);
}
