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
            .ForMember(p => p.ActualAmount, opt => opt.Ignore());

        CreateMap<CreateContractExpenditureCommand, InitiativeExpenditure>();

        CreateMap<InitiativeExpenditure, CreateProjectExpenditureCommand>();
        CreateMap<InitiativeExpenditure, CreateContractExpenditureCommand>();
    }
}