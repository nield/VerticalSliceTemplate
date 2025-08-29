using FluentValidation.TestHelper;
using VerticalSliceTemplate.Api.Endpoints.V1.Todos;

namespace VerticalSliceTemplate.Api.Tests.Endpoints.V1.ToDos.CreateToDo;

public class ValidatorTests
{
    private readonly Create.Validator _validator = new();

    [Fact]
    public async Task Given_EmptyTitle_Should_Fail()
    {
        var sut = await _validator.TestValidateAsync(new Create.Request
        {
            Title = ""
        });

        sut.ShouldHaveValidationErrorFor(x => x.Title);
    }
}
