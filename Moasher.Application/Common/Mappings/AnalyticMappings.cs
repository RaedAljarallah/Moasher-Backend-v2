using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.Analytics;
using Moasher.Application.Features.Analytics.Commands.CreateAnalytic;
using Moasher.Application.Features.Analytics.Commands.UpdateAnalytic;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities;

namespace Moasher.Application.Common.Mappings;

public class AnalyticMappings : Profile
{
    public AnalyticMappings()
    {
        CreateMap<Analytic, AnalyticDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>();
        
        CreateMap<CreateAnalyticCommand, Analytic>();

        CreateMap<UpdateAnalyticCommand, Analytic>()
            .ForMember(c => c.Id, opt => opt.Ignore())
            .ForMember(a => a.Initiative, opt => opt.Ignore())
            .ForMember(a => a.InitiativeId, opt => opt.Ignore())
            .ForMember(a => a.KPI, opt => opt.Ignore())
            .ForMember(a => a.KPIId, opt => opt.Ignore());
    }
}