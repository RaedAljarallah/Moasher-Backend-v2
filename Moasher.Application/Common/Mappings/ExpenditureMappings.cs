using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.Expenditures;
using Moasher.Application.Features.Expenditures.Commands.CreateContractExpenditure;
using Moasher.Application.Features.Expenditures.Commands.CreateProjectExpenditure;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Common.Mappings;

public class ExpenditureMappings : Profile
{
    public ExpenditureMappings()
    {
        CreateMap<InitiativeExpenditure, ExpenditureDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>();

        CreateMap<CreateProjectExpenditureCommand, InitiativeExpenditure>()
            .ForMember(e => e.ActualAmount, opt => opt.Ignore())
            .ForMember(e => e.Contract, opt => opt.Ignore())
            .ForMember(e => e.ContractId, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<CreateContractExpenditureCommand, InitiativeExpenditure>()
            .ForMember(e => e.Project, opt => opt.Ignore())
            .ForMember(e => e.ProjectId, opt => opt.Ignore())
            .ReverseMap();
    }
}