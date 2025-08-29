using VerticalSliceTemplate.Api.Endpoints.V1.Todos;

namespace VerticalSliceTemplate.Api.Tests.Endpoints.V1.ToDos.UpdateToDo;

public class EndpointTests : BaseTestFixture
{
    [Fact]
    public async Task Given_InvalidId_Should_ThrowException()
    {
        var id = 1L;

        var request = Builder<Update.Request>.CreateNew().Build();

        _toDoRepositoryMock.GetByIdAsync(id, Arg.Any<CancellationToken>())
            .ReturnsNull();

        await Assert.ThrowsAsync<NotFoundException>(() =>
            Update.Handler(id, request, _toDoRepositoryMock, CancellationToken.None));
    }

    [Fact]
    public async Task Given_ValidId_Should_ReturnNoContent()
    {
        var id = 1L;

        var request = Builder<Update.Request>.CreateNew().Build();

        var item = Builder<ToDoItem>.CreateNew().Build();

        _toDoRepositoryMock.GetByIdAsync(id, Arg.Any<CancellationToken>())
            .Returns(item);

        var sut = await Update.Handler(id, request, _toDoRepositoryMock, CancellationToken.None);

        sut.Should().NotBeNull();
        sut.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }
}
