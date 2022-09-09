using AutoMapper;
using Moasher.Application.Features.Initiatives;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Common.Mappings;

public class InitiativeMappings : Profile
{
    public InitiativeMappings()
    {
        CreateMap<Initiative, InitiativeDto>();
    }
}