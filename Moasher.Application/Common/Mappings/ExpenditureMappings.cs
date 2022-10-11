using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.Expenditures;
using Moasher.Application.Features.Expenditures.Commands.CreateExpenditure;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Common.Mappings;

public class ExpenditureMappings : Profile
{
    public ExpenditureMappings()
    {
        CreateMap<InitiativeExpenditure, ExpenditureDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>();

        CreateMap<CreateExpenditureCommand, InitiativeExpenditure>();

        CreateMap<InitiativeExpenditure, CreateExpenditureCommand>();
    }
}