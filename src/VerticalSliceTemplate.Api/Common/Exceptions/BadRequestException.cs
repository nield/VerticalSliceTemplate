﻿namespace VerticalSliceTemplate.Api.Common.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message)
        : base(message)
    {
    }

    private BadRequestException()
    : base()
    {
    }
}
