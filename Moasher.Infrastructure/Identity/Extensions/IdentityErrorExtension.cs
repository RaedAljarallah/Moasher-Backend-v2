using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace Moasher.Infrastructure.Identity.Extensions;

public static class IdentityErrorExtension
{
    public static IEnumerable<ValidationFailure> ToValidationErrors(this IEnumerable<IdentityError> identityErrors)
    {
        var validationErrors = new List<ValidationFailure>();
        identityErrors.ToList().ForEach(e => validationErrors.Add(new ValidationFailure("", e.Description)));

        return validationErrors;
    }
}