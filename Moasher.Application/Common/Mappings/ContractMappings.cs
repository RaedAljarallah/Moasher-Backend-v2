using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.Contracts;
using Moasher.Application.Features.Contracts.Commands.CreateContract;
using Moasher.Application.Features.Contracts.Commands.UpdateContract;
using Moasher.Application.Features.Contracts.Queries.EditContract;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Extensions;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Common.Mappings;

public class ContractMappings : Profile
{
    public ContractMappings()
    {
        CreateMap<InitiativeContract, ContractDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>()
            .ForMember(c => c.Status, opt => opt.MapFrom(c => new EnumValue(c.StatusName, c.StatusStyle)))
            .ForMember(c => c.PlannedExpenditureToDate, opt => opt.MapFrom(c => c.GetPlannedExpenditureToDate()));

        CreateMap<CreateContractCommand, InitiativeContract>()
            .ForMember(c => c.Expenditures, opt => opt.Ignore())
            .ForMember(c => c.ExpendituresBaseline, opt => opt.Ignore());

        CreateMap<InitiativeContract, EditContractDto>()
            .ForMember(c => c.Status, opt => opt.MapFrom(c => c.StatusEnum));
        
        CreateMap<UpdateContractCommand, InitiativeContract>()
            .ForMember(c => c.Initiative, opt => opt.Ignore())
            .ForMember(c => c.InitiativeId, opt => opt.Ignore())
            .ForMember(c => c.Expenditures, opt => opt.Ignore())
            .ForMember(c => c.ExpendituresBaseline, opt => opt.Ignore())
            .ForMember(c => c.Id, opt => opt.Ignore());
    }
}