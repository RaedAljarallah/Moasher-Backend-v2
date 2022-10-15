namespace Moasher.Application.Features.Expenditures.Commands.CreateProjectExpenditure;

public class CreateProjectExpenditureCommandValidator : ExpenditureCommandValidatorBase<CreateProjectExpenditureCommand>
{
    public void SetValidationArguments(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
}