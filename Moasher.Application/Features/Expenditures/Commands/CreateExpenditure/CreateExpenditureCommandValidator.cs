using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Features.Expenditures.Commands.CreateExpenditure;

public class CreateExpenditureCommandValidator : ExpenditureCommandValidatorBase<CreateExpenditureCommand>
{
    public void SetValidationArguments(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
}