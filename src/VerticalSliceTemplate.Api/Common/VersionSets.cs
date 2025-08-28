using Asp.Versioning;
using Asp.Versioning.Builder;
using System.Collections.Concurrent;

namespace VerticalSliceTemplate.Api.Common;

public static class VersionSets
{
    private static readonly ConcurrentDictionary<ApiVersion, ApiVersionSet> VersionSetStore = [];

    public static ApiVersionSet GetVersionSet(int majorVersion = 1)
    {
        ApiVersion key = new(majorVersion);

        if (!VersionSetStore.TryGetValue(key, out ApiVersionSet? value))
        {
            value = CreateVersionSet(majorVersion);

            VersionSetStore[key] = value;
        }

        return value!;
    }

    private static ApiVersionSet CreateVersionSet(int majorVersion)
    {
        return new ApiVersionSetBuilder(null)
            .ReportApiVersions()
            .HasApiVersion(new ApiVersion(majorVersion))
            .Build();
    }
}
