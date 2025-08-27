namespace VerticalSliceTemplate.Api.Endpoints.V1.Todos;

public class GetAll : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGetRoute("/todos", Handler)
            .WithTags(Constants.OpenApi.Tags.Todos)
            .WithDescription("Get all todos");
    }

    public record Response(long Id, string Title, List<string> Tags);

    public static async Task<IEnumerable<Response>> Handler(IApplicationDbContext applicationDbContext)
    {
        var data = await applicationDbContext.TodoItems
            .Select(x => new Response(x.Id, x.Title, x.Tags))
            .ToListAsync();

        return data;
    }
}
