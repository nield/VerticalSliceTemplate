﻿namespace VerticalSliceTemplate.Api.Common.ExceptionHandlers;

public class ValidationExceptionHandler : BaseExceptionHandler<DataValidationFailureException, ValidationProblemDetails>
{
    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public ValidationExceptionHandler(IProblemDetailsService problemDetailsService)
        : base(problemDetailsService)
    {
        
    }

    public override ValidationProblemDetails GenerateProblemDetails(DataValidationFailureException exception)
    {
        return new ValidationProblemDetails(exception.Errors)
        {
            Status = (int)HttpStatusCode,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };
    }
}
