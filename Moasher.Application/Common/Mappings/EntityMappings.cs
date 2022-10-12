using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.Entities;
using Moasher.Application.Features.Entities.Commands.CreateEntity;
using Moasher.Application.Features.Entities.Commands.UpdateEntity;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Extensions;
using Moasher.Domain.Entities;

namespace Moasher.Application.Common.Mappings;

public class EntityMappings : Profile
{
    public EntityMappings()
    {
        CreateMap<Entity, EntityDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>()
            .ForMember(e => e.InitiativesCount, opt => opt.MapFrom(e => e.Initiatives.Count))
            .ForMember(e => e.KPIsCount, opt => opt.MapFrom(e => e.KPIs.Count))
            .ForMember(e => e.StrategicObjectivesCount, opt => opt.MapFrom(e => e.GetStrategicObjectives().Count()));

        CreateMap<CreateEntityCommand, Entity>();

        CreateMap<UpdateEntityCommand, Entity>()
            .ForMember(c => c.Id, opt => opt.Ignore());
    }
}