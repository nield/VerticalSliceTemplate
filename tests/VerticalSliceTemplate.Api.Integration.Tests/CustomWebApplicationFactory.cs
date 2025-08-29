using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using VerticalSliceTemplate.Api.Common;
using VerticalSliceTemplate.Api.Integration.Tests.Containers;
using VerticalSliceTemplate.Api.Integration.Tests.Mocks;

namespace VerticalSliceTemplate.Api.Integration.Tests;

[ExcludeFromCodeCoverage]
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public string DefaultUserId { get; set; } = "1";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable(
            "DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE",
            "false"
        );

        Environment.SetEnvironmentVariable(
            "OTEL_EXPORTER_OTLP_ENDPOINT",
            "");

        Environment.SetEnvironmentVariable(
            "SEQ_SERVER_URL",
            "http://dummy-url");

        Environment.SetEnvironmentVariable(
            "ConnectionStrings__SqlDatabase",
            DatabaseContainer.Instance.GetConnectionString()
        );

        Environment.SetEnvironmentVariable(
            "ConnectionStrings__Redis",
            CacheContainer.Instance.GetConnectionString()
        );

        builder.UseEnvironment(Constants.Environments.Test);

        builder.ConfigureTestServices(services =>
        {
            services.AddScoped<ICurrentUserService, MockCurrentUserService>();

            services.Configure<TestAuthHandlerOptions>(
                options => options.DefaultUserId = DefaultUserId
            );

            services
                .AddAuthentication(TestAuthHandler.AuthenticationScheme)
                .AddScheme<TestAuthHandlerOptions, TestAuthHandler>(
                    TestAuthHandler.AuthenticationScheme,
                    options => { }
                );
        });

        base.ConfigureWebHost(builder);
    }
}