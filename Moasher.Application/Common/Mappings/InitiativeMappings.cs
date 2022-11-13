using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.Initiatives;
using Moasher.Application.Features.Initiatives.Commands.CreateInitiative;
using Moasher.Application.Features.Initiatives.Commands.UpdateInitiative;
using Moasher.Application.Features.Initiatives.Queries.EditInitiative;
using Moasher.Application.Features.Initiatives.Queries.GetInitiativeDetails;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Common.Mappings;

public class InitiativeMappings : Profile
{
    public InitiativeMappings()
    {
        CreateMap<Initiative, InitiativeDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>()
            .ForMember(i => i.Status,
                opt => opt.MapFrom(i => i.StatusName != null && i.StatusStyle != null
                    ? new EnumValue(i.StatusName, i.StatusStyle)
                    : null))
            .ForMember(i => i.FundStatus, opt => opt.MapFrom(i => new EnumValue(i.FundStatusName, i.FundStatusStyle)));

        CreateMap<Initiative, EditInitiativeDto>()
            .ForMember(i => i.FundStatus, opt => opt.MapFrom(i => i.FundStatusEnum))
            .ForMember(i => i.Status, opt => opt.MapFrom(i => i.StatusEnum));

        CreateMap<CreateInitiativeCommand, Initiative>();
        CreateMap<UpdateInitiativeCommand, Initiative>()
            .ForMember(i => i.Id, opt => opt.Ignore());

        CreateMap<Initiative, InitiativeDetailsDto>()
            .IncludeBase<Initiative, InitiativeDto>();
    }
}