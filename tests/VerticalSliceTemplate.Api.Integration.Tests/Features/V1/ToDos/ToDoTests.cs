using System.Net;
using System.Net.Http.Json;
using FizzWare.NBuilder;
using VerticalSliceTemplate.Api.Endpoints.V1.Todos;

namespace VerticalSliceTemplate.Api.Integration.Tests.Features.V1.ToDos;

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
            "/api/v1/todos");

        Assert.NotNull(sut);
        Assert.NotEmpty(sut);
    }

    [Fact]
    public async Task CreateToDoItem_GivenValidRequest_Should_ReturnCreated()
    {
        var payload = Builder<Create.Request>.CreateNew().Build();

        var sut = await _webApplicationFixture.HttpClient.PostAsJsonAsync("/api/v1/todos", payload);

        Assert.NotNull(sut);
        Assert.Equal(HttpStatusCode.Created, sut.StatusCode);

        await _webApplicationFixture.ResetDatabaseAsync();
    }

    [Fact]
    public async Task GetById_GivenValidId_Should_ReturnData()
    {
        var id = 1L;

        var sut = await GetToDoById(id, shouldExists: true);

        Assert.Equal(id, sut!.Id);
    }

    [Fact]
    public async Task GetById_GivenInValidId_Should_ReturnNotFound()
    {
        await GetToDoById(99999, shouldExists: false);
    }

    [Fact]
    public async Task UpdateToDoItem_GivenInValidId_Should_ReturnNotFound()
    {
        var id = 99999L;

        var payload = Builder<Update.Request>.CreateNew().Build();

        var sut = await _webApplicationFixture.HttpClient.PutAsJsonAsync(
            $"/api/v1/todos/{id}",
            payload);

        Assert.NotNull(sut);
        Assert.Equal(HttpStatusCode.NotFound, sut.StatusCode);
    }

    [Fact]
    public async Task UpdateToDoItem_GivenValidRequest_Should_ReturnNoContent()
    {
        var id = 1L;

        var payload = Builder<Update.Request>.CreateNew()
            .With(x => x.Title, "Updated Title")
            .Build();

        var sut = await _webApplicationFixture.HttpClient.PutAsJsonAsync(
            $"/api/v1/todos/{id}",
            payload);

        Assert.NotNull(sut);
        Assert.Equal(HttpStatusCode.NoContent, sut.StatusCode);

        var updatedToDo = await GetToDoById(id, shouldExists: true);

        Assert.Equal(payload.Title, updatedToDo!.Title);

        await _webApplicationFixture.ResetDatabaseAsync();
    }

    [Fact]
    public async Task DeleteToDoItem_GivenInValidId_Should_ReturnNotFound()
    {
        var id = 99999L;

        var sut = await _webApplicationFixture.HttpClient.DeleteAsync(
            $"/api/v1/todos/{id}");

        Assert.NotNull(sut);
        Assert.Equal(HttpStatusCode.NotFound, sut.StatusCode);
    }

    [Fact]
    public async Task DeleteToDoItem_GivenValidRequest_Should_ReturnNoContent()
    {
        var id = 1L;

        var sut = await _webApplicationFixture.HttpClient.DeleteAsync(
            $"/api/v1/todos/{id}");

        Assert.NotNull(sut);
        Assert.Equal(HttpStatusCode.NoContent, sut.StatusCode);

        await GetToDoById(id, shouldExists: false);

        await _webApplicationFixture.ResetDatabaseAsync();
    }

    private async Task<GetById.Response?> GetToDoById(long id, bool shouldExists)
    {
        var sut = await _webApplicationFixture.HttpClient.GetAsync(
            $"/api/v1/todos/{id}");

        Assert.NotNull(sut);

        if (shouldExists)
        {
            Assert.Equal(HttpStatusCode.OK, sut.StatusCode);

            var response = await sut.Content.ReadFromJsonAsync<GetById.Response>();

            Assert.NotNull(response);

            return response;
        }

        Assert.Equal(HttpStatusCode.NotFound, sut.StatusCode);

        return null;
    }
}
