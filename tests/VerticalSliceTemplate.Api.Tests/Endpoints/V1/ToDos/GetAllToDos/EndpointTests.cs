using VerticalSliceTemplate.Api.Endpoints.V1.Todos;

namespace VerticalSliceTemplate.Api.Tests.Endpoints.V1.ToDos.GetAllToDos;

public class EndpointTests : BaseTestFixture
{
    [Fact]
    public async Task Given_Data_Exists_Should_ReturnData()
    {
        var items = Builder<ToDoItem>.CreateListOfSize(1)
            .Build().AsQueryable().BuildMockDbSet();

        _applicationDbContextMock.TodoItems
            .Returns(items);

        var sut = await GetAll.Handler(_applicationDbContextMock, CancellationToken.None);

        sut.Should().NotBeNullOrEmpty();
    }
}
