using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.Milestones;
using Moasher.Application.Features.Milestones.Commands.CreateMilestone;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Common.Mappings;

public class MilestoneMappings : Profile
{
    public MilestoneMappings()
    {
        CreateMap<InitiativeMilestone, MilestoneDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>();
        
        CreateMap<CreateMilestoneCommand, InitiativeMilestone>();
        
        // CreateMap<UpdateMilestoneCommand, InitiativeMilestone>()
        //     .ForMember(e => e.Initiative, opt => opt.Ignore())
        //     .ForMember(e => e.InitiativeId, opt => opt.Ignore())
        //     .ForMember(c => c.Id, opt => opt.Ignore());
    }
}