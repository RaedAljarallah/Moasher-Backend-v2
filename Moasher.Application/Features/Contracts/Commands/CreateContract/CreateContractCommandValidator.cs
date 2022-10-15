using FluentValidation;
using Moasher.Application.Common.Constants;

namespace Moasher.Application.Features.Contracts.Commands.CreateContract;

public class CreateContractCommandValidator : ContractCommandValidatorBase<CreateContractCommand>
{
    public CreateContractCommandValidator()
    {
        RuleFor(command => command.ProjectId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("المشروع"));
    }
}