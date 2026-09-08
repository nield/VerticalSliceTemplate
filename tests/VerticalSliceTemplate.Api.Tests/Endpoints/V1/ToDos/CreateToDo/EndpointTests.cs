using VerticalSliceTemplate.Api.Endpoints.V1.Todos;

namespace VerticalSliceTemplate.Api.Tests.Endpoints.V1.ToDos.CreateToDo;

public class EndpointTests : BaseTestFixture
{
    [Fact]
    public async Task Handle_Success()
    {
        const int newId = 1;
        
        ToDoRepositoryMock.AddAsync(Arg.Any<ToDoItem>(), Arg.Any<CancellationToken>())
            .Returns(callInfo =>
            {
                var entity = callInfo.Arg<ToDoItem>();
                
                entity.Id = newId;
                
                return entity;
            });
        
        var request = Builder<Create.Request>.CreateNew().Build();

        var handler = new Create.Handler(ToDoRepositoryMock);
        
        var sut = await  handler.Handle(request, CancellationToken.None);

        sut.Should().NotBeNull();

        sut.Id.Should().Be(newId);
    }
}
