namespace Moasher.Application.Features.Expenditures.Commands.CreateContractExpenditure;

public class CreateContractExpenditureCommandValidator : ExpenditureCommandValidatorBase<CreateContractExpenditureCommand>
{
    public void SetValidationArguments(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
}