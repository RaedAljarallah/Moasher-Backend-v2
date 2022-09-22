using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.ApprovedCosts;
using Moasher.Application.Features.ApprovedCosts.Commands.CreateApprovedCost;
using Moasher.Application.Features.ApprovedCosts.Commands.UpdateApprovedCost;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Common.Mappings;

public class ApprovedCostMappings : Profile
{
    public ApprovedCostMappings()
    {
        CreateMap<InitiativeApprovedCost, ApprovedCostDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>();
        
        CreateMap<CreateApprovedCostCommand, InitiativeApprovedCost>();

        CreateMap<UpdateApprovedCostCommand, InitiativeApprovedCost>()
            .ForMember(e => e.Initiative, opt => opt.Ignore())
            .ForMember(e => e.InitiativeId, opt => opt.Ignore())
            .ForMember(c => c.Id, opt => opt.Ignore());
    }
}