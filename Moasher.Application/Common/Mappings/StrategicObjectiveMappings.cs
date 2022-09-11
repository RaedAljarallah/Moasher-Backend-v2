using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Services;
using Moasher.Application.Features.StrategicObjectives;
using Moasher.Application.Features.StrategicObjectives.Commands.CreateStrategicObjective;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.StrategicObjectiveEntities;

namespace Moasher.Application.Common.Mappings;

public class StrategicObjectiveMappings : Profile
{
    public StrategicObjectiveMappings()
    {
        CreateMap<StrategicObjective, StrategicObjectiveDtoBase>()
            .IncludeBase<AuditableDbEntity<Guid>, DtoBase>()
            .ForMember(o => o.Level, opt => opt.MapFrom(o => o.HierarchyId.GetLevel()));

        CreateMap<StrategicObjectiveLevelOne, StrategicObjectiveLevelOneDto>()
            .IncludeBase<AuditableDbEntity<Guid>, DtoBase>()
            .ForMember(o => o.Level, opt => opt.MapFrom(o => o.HierarchyId.GetLevel()));

        CreateMap<StrategicObjectiveLevelTwo, StrategicObjectiveLevelTwoDto>()
            .IncludeBase<AuditableDbEntity<Guid>, DtoBase>()
            .ForMember(o => o.Level, opt => opt.MapFrom(o => o.HierarchyId.GetLevel()));

        CreateMap<StrategicObjectiveLevelThree, StrategicObjectiveLevelThreeDto>()
            .IncludeBase<AuditableDbEntity<Guid>, DtoBase>()
            .ForMember(o => o.Level, opt => opt.MapFrom(o => o.HierarchyId.GetLevel()));

        CreateMap<StrategicObjectiveLevelFour, StrategicObjectiveLevelFourDto>()
            .IncludeBase<AuditableDbEntity<Guid>, DtoBase>()
            .ForMember(o => o.Level, opt => opt.MapFrom(o => o.HierarchyId.GetLevel()));
        
        CreateMap<CreateStrategicObjectiveCommand, StrategicObjective>()
            .ForMember(o => o.HierarchyId, opt => opt.MapFrom(o => HierarchyIdService.Parse(o.HierarchyId)));
        
        // CreateMap<UpdateStrategicObjectiveCommand, StrategicObjective>()
        //     .ForMember(c => c.Id, opt => opt.Ignore())
        //     .ForMember(o => o.HierarchyId, opt => opt.MapFrom(o => HierarchyIdService.Parse(o.HierarchyId)));
    }
}