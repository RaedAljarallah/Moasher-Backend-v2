﻿namespace Moasher.Application.Common.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException(string? message) : base(message) { }
}