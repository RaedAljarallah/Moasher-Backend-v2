using Moasher.Application.Features.Contracts.Commands;
using Moasher.Application.Features.EnumTypes;
using Moasher.Application.Features.Expenditures.Commands.CreateContractExpenditure;

namespace Moasher.Application.Features.Contracts.Queries.EditContract;

public record EditContractDto : ContractCommandBase
{
    public Guid Id { get; set; }
    public EnumTypeDto Status { get; set; } = default!;
    public IEnumerable<CreateContractExpenditureCommand> Expenditures { get; set; } =
        Enumerable.Empty<CreateContractExpenditureCommand>();
}