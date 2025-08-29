using VerticalSliceTemplate.Api.Endpoints.V1.Todos;

namespace VerticalSliceTemplate.Api.Tests.Endpoints.V1.ToDos.DeleteToDo;

public class EndpointTests : BaseTestFixture
{
    [Fact]
    public async Task Given_InvalidId_Should_ThrowException()
    {
        var id = 1L;

        _toDoRepositoryMock.GetByIdAsync(id, Arg.Any<CancellationToken>())
            .ReturnsNull();

        await Assert.ThrowsAsync<NotFoundException>(() => 
            DeleteById.Handler(id, _toDoRepositoryMock, CancellationToken.None));
    }

    [Fact]
    public async Task Given_ValidId_Should_ReturnNoContent()
    {
        var id = 1L;

        var item = Builder<ToDoItem>.CreateNew().Build();

        _toDoRepositoryMock.GetByIdAsync(id, Arg.Any<CancellationToken>())
            .Returns(item);

        var sut = await DeleteById.Handler(id, _toDoRepositoryMock, CancellationToken.None);

        sut.Should().NotBeNull();
        sut.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }
}
