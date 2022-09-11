using FluentValidation;
using FluentValidation.Results;

namespace Moasher.Application.Common.Abstracts;

public abstract class ValidatorBase<TCommand> : AbstractValidator<TCommand>
{
    public override async Task<ValidationResult> ValidateAsync(ValidationContext<TCommand> context, CancellationToken cancellation = new())
    {
        await SetUpFiltersAsync(context, cancellation);
        return await base.ValidateAsync(context, cancellation);
    }

    protected virtual Task SetUpFiltersAsync(ValidationContext<TCommand> validationContext, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}