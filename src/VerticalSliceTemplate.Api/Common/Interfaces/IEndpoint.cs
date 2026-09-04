namespace VerticalSliceTemplate.Api.Common.Interfaces;

public interface IEndpoint
{
    static abstract void AddRoute(IEndpointRouteBuilder app);
}
