using static VerticalSliceTemplate.Api.Common.Constants;

namespace VerticalSliceTemplate.Api.Endpoints.V1.Weather;

public class GetForecast : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGetRoute("/weather/forecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
        .WithDescription("Get weather forecast")
        .WithTags(OpenApi.Tags.Weather);
    }

    record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary);
}
