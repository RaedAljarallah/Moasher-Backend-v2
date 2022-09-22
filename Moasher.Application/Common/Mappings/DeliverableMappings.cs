using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.Deliverables;
using Moasher.Application.Features.Deliverables.Commands.CreateDeliverable;
using Moasher.Application.Features.Deliverables.Commands.UpdateDeliverable;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Common.Mappings;

public class DeliverableMappings : Profile
{
    public DeliverableMappings()
    {
        CreateMap<InitiativeDeliverable, DeliverableDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>();
        
        CreateMap<CreateDeliverableCommand, InitiativeDeliverable>();

        CreateMap<UpdateDeliverableCommand, InitiativeDeliverable>()
            .ForMember(e => e.Initiative, opt => opt.Ignore())
            .ForMember(e => e.InitiativeId, opt => opt.Ignore())
            .ForMember(c => c.Id, opt => opt.Ignore());
    }
}