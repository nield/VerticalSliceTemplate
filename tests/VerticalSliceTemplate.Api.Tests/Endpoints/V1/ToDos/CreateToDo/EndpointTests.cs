using VerticalSliceTemplate.Api.Endpoints.V1.Todos;

namespace VerticalSliceTemplate.Api.Tests.Endpoints.V1.ToDos.CreateToDo;

public class EndpointTests : BaseTestFixture
{
    [Fact]
    public async Task Handle_Success()
    {
        var request = Builder<Create.Request>.CreateNew().Build();

        var sut = await Create.Handler(request, _toDoRepositoryMock, CancellationToken.None);

        sut.Should().NotBeNull();

        sut.StatusCode.Should().Be(StatusCodes.Status201Created);
    }
}
