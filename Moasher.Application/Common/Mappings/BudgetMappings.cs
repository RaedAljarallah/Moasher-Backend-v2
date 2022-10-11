using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.Budgets;
using Moasher.Application.Features.Budgets.Commands.CreateBudget;
using Moasher.Application.Features.Budgets.Commands.UpdateBudget;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Common.Mappings;

public class BudgetMappings : Profile
{
    public BudgetMappings()
    {
        CreateMap<InitiativeBudget, BudgetDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>();

        CreateMap<CreateBudgetCommand, InitiativeBudget>()
            .ForMember(b => b.InitialAmount, opt => opt.MapFrom(b => b.Amount));

        CreateMap<UpdateBudgetCommand, InitiativeBudget>()
            .ForMember(e => e.Initiative, opt => opt.Ignore())
            .ForMember(e => e.InitiativeId, opt => opt.Ignore())
            .ForMember(e => e.InitialAmount, opt => opt.Ignore())
            .ForMember(c => c.Id, opt => opt.Ignore());
    }
}