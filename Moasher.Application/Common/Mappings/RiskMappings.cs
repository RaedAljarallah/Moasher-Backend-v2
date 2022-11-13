using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.Risks;
using Moasher.Application.Features.Risks.Commands.CreateRisk;
using Moasher.Application.Features.Risks.Commands.UpdateRisk;
using Moasher.Application.Features.Risks.Queries.EditRisk;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Common.Mappings;

public class RiskMappings : Profile
{
    public RiskMappings()
    {
        CreateMap<InitiativeRisk, RiskDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>()
            .ForMember(r => r.Type, opt => opt.MapFrom(r => new EnumValue(r.TypeName, r.TypeStyle)))
            .ForMember(r => r.Priority, opt => opt.MapFrom(r => new EnumValue(r.PriorityName, r.PriorityStyle)))
            .ForMember(r => r.Probability,
                opt => opt.MapFrom(r => new EnumValue(r.ProbabilityName, r.ProbabilityStyle)))
            .ForMember(r => r.Impact, opt => opt.MapFrom(r => new EnumValue(r.ImpactName, r.ImpactStyle)))
            .ForMember(r => r.Scope, opt => opt.MapFrom(r => new EnumValue(r.ScopeName, r.ScopeStyle)));
        
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