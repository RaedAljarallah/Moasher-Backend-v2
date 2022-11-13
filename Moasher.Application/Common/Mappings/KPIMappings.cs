using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.KPIs;
using Moasher.Application.Features.KPIs.Commands.CreateKPI;
using Moasher.Application.Features.KPIs.Commands.UpdateKPI;
using Moasher.Application.Features.KPIs.Queries.EditKPI;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.KPIEntities;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Common.Mappings;

public class KPIMappings : Profile
{
    public KPIMappings()
    {
        CreateMap<KPI, KPIDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>()
            .ForMember(i => i.Status,
                opt => opt.MapFrom(i => i.StatusName != null && i.StatusStyle != null
                    ? new EnumValue(i.StatusName, i.StatusStyle)
                    : null));

        CreateMap<CreateKPICommand, KPI>();

        CreateMap<KPI, EditKPIDto>()
            .ForMember(k => k.Status, opt => opt.MapFrom(k => k.StatusEnum));

        CreateMap<UpdateKPICommand, KPI>()
            .ForMember(k => k.Id, opt => opt.Ignore());

        // CreateMap<KPI, KPIDetailsDto>()
        //     .IncludeBase<KPI, KPIDto>();
    }
}