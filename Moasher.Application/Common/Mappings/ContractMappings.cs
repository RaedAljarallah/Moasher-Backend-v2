using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.Contracts;
using Moasher.Application.Features.Contracts.Commands.CreateContract;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Extensions;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Common.Mappings;

public class ContractMappings : Profile
{
    public ContractMappings()
    {
        CreateMap<InitiativeContract, ContractDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>()
            .ForMember(c => c.PlannedExpenditureToDate, opt => opt.MapFrom(c => c.PlannedExpenditureToDate()));

        CreateMap<CreateContractCommand, InitiativeContract>();
    }
}