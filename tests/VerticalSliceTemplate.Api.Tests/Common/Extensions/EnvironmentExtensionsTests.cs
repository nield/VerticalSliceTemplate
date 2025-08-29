﻿using Microsoft.Extensions.Hosting;
using Constants = VerticalSliceTemplate.Api.Common.Constants;

namespace VerticalSliceTemplate.Api.Tests.Common.Extensions;

public class EnvironmentExtensionsTests
{
    private readonly IHostEnvironment _environmentMock = Substitute.For<IHostEnvironment>();

    [Fact]
    public void Given_EnvironmentIsTest_When_CheckingIsTest_Then_IsTrue()
    {
        _environmentMock.EnvironmentName
            .Returns(Constants.Environments.Test);

        var sut = _environmentMock.IsTest();

        Assert.True(sut);
    }
}