using MediatR;
using Moasher.Application.Common.Exceptions;
using Moasher.Domain.Common.Interfaces;

namespace Moasher.Application.Common.Extensions;

public static class CommandExtensions
{
    public static void ValidateAndThrow<TResponse>(this IRequest<TResponse> command, IDomainValidator validator)
    {
        var failures = validator.Validate();
        if (failures.Any())
        {
            throw new ValidationException(failures);
        }
    }
}