﻿namespace VerticalSliceTemplate.Api.Common.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? UserProfileId { get; }
    string? CorrelationId { get; }
    string? Token { get; }
}