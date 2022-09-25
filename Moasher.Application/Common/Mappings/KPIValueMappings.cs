using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.KPIValues;
using Moasher.Application.Features.KPIValues.Commands.CreateKPIValue;
using Moasher.Application.Features.KPIValues.Commands.UpdateKPIValue;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.KPIEntities;

namespace Moasher.Application.Common.Mappings;

public class KPIValueMappings : Profile
{
    public KPIValueMappings()
    {
        CreateMap<KPIValue, KPIValueDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>();
        
        CreateMap<CreateKPIValueCommand, KPIValue>();

        CreateMap<UpdateKPIValueCommand, KPIValue>()
            .ForMember(v => v.KPI, opt => opt.Ignore())
            .ForMember(v => v.KPIId, opt => opt.Ignore())
            .ForMember(v => v.Id, opt => opt.Ignore());
    }
}