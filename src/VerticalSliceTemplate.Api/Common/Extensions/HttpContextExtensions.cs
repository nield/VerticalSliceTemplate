﻿using Microsoft.Extensions.Primitives;
using static VerticalSliceTemplate.Api.Common.Constants;

namespace VerticalSliceTemplate.Api.Common.Extensions;

public static class HttpContextExtensions
{
    public static StringValues GetCorrelationId(this HttpContext context, bool allowEmpty = false)
    {
        if (context.Request.Headers.TryGetValue(
            Headers.CorrelationId, out StringValues requestCorrelationId))
        {
            return requestCorrelationId;
        }

        if (context.Response.Headers.TryGetValue(
            Headers.CorrelationId, out StringValues responseCorrelationId))
        {
            return responseCorrelationId;
        }

        return allowEmpty
            ? StringValues.Empty
            : new StringValues(Guid.NewGuid().ToString());
    }
}

