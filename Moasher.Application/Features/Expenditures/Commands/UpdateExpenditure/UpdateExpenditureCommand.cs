namespace Moasher.Application.Features.Expenditures.Commands.UpdateExpenditure;

public record UpdateExpenditureCommand : ExpenditureCommandBase
{
    public Guid Id { get; set; }
}