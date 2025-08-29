using System.Net.Http.Json;
using VerticalSliceTemplate.Api.Endpoints.V2.Todos;

namespace VerticalSliceTemplate.Api.Integration.Tests.Features.V2.ToDos;

[Collection("WebApplicationCollection")]
public class ToDoTests
{
    private readonly WebApplicationFixture _webApplicationFixture;

    public ToDoTests(WebApplicationFixture webApplicationFixture)
    {
        _webApplicationFixture = webApplicationFixture;
    }

    [Fact]
    public async Task GetAll_Should_ReturnData()
    {
        var sut = await _webApplicationFixture.HttpClient.GetFromJsonAsync<IEnumerable<GetAll.Response>>(
            "/api/v2/todos");

        Assert.NotNull(sut);
        Assert.NotEmpty(sut);
    }
}
