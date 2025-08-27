using System.IO.Compression;
using Microsoft.AspNetCore.ResponseCompression;

namespace VerticalSliceTemplate.Api.Configurations;

internal static class Compression
{
    internal static void ConfigureCompression(this IServiceCollection services)
    {
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });

        services.Configure<BrotliCompressionProviderOptions>(options
            => options.Level = CompressionLevel.SmallestSize);

        services.Configure<GzipCompressionProviderOptions>(options
            => options.Level = CompressionLevel.SmallestSize);
    }

    internal static void UseCompression(this WebApplication app)
    {
        app.UseResponseCompression();
    }
}
