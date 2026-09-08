using VerticalSliceTemplate.Api.Endpoints.V1.Todos;

namespace VerticalSliceTemplate.Api.Tests.Endpoints.V1.ToDos.GetAllToDos;

public class EndpointTests : BaseTestFixture
{
    [Fact]
    public async Task Given_Data_Exists_Should_ReturnData()
    {
        var items = Builder<ToDoItem>.CreateListOfSize(1)
            .Build().AsQueryable().BuildMockDbSet();

        ApplicationDbContextMock.TodoItems
            .Returns(items);

        var sut = await GetAll.Handler(ApplicationDbContextMock, CancellationToken.None);

        sut.Should().NotBeNullOrEmpty();
    }
}
