using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.InitiativeTeams;
using Moasher.Application.Features.InitiativeTeams.Commands.CreateInitiativeTeam;
using Moasher.Application.Features.InitiativeTeams.Commands.UpdateInitiativeTeam;
using Moasher.Application.Features.InitiativeTeams.Queries.EditInitiativeTeam;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Common.Mappings;

public class InitiativeTeamMappings : Profile
{
    public InitiativeTeamMappings()
    {
        CreateMap<InitiativeTeam, InitiativeTeamDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>();
        
        CreateMap<CreateInitiativeTeamCommand, InitiativeTeam>();

        CreateMap<InitiativeTeam, EditInitiativeTeamDto>()
            .ForMember(m => m.Role, opt => opt.MapFrom(m => m.RoleEnum));

        CreateMap<UpdateInitiativeTeamCommand, InitiativeTeam>()
            .ForMember(m => m.Initiative, opt => opt.Ignore())
            .ForMember(m => m.InitiativeId, opt => opt.Ignore())
            .ForMember(m => m.Id, opt => opt.Ignore());
    }
}