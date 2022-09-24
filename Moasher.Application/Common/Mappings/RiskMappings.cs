using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.Risks;
using Moasher.Application.Features.Risks.Commands.CreateRisk;
using Moasher.Application.Features.Risks.Commands.UpdateRisk;
using Moasher.Application.Features.Risks.Queries.EditRisk;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Common.Mappings;

public class RiskMappings : Profile
{
    public RiskMappings()
    {
        CreateMap<InitiativeRisk, RiskDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>();
        
        // CreateMap<InitiativeRisk, RiskSummaryDto>();

        CreateMap<CreateRiskCommand, InitiativeRisk>();

        CreateMap<InitiativeRisk, EditRiskDto>()
            .ForMember(i => i.Type, opt => opt.MapFrom(i => i.TypeEnum))
            .ForMember(i => i.Priority, opt => opt.MapFrom(i => i.PriorityEnum))
            .ForMember(i => i.Probability, opt => opt.MapFrom(i => i.ProbabilityEnum))
            .ForMember(i => i.Impact, opt => opt.MapFrom(i => i.ImpactEnum))
            .ForMember(i => i.Scope, opt => opt.MapFrom(i => i.ScopeEnum));

        CreateMap<UpdateRiskCommand, InitiativeRisk>()
            .ForMember(m => m.Initiative, opt => opt.Ignore())
            .ForMember(m => m.InitiativeId, opt => opt.Ignore())
            .ForMember(m => m.Id, opt => opt.Ignore());
    }
}