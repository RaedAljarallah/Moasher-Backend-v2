using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.Issues;
using Moasher.Application.Features.Issues.Commands.CreateIssue;
using Moasher.Application.Features.Issues.Commands.UpdateIssue;
using Moasher.Application.Features.Issues.Queries.EditIssue;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Common.Mappings;

public class IssueMappings : Profile
{
    public IssueMappings()
    {
        CreateMap<InitiativeIssue, IssueDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>()
            .ForMember(i => i.Scope, opt => opt.MapFrom(i => new EnumValue(i.ScopeName, i.ScopeStyle)))
            .ForMember(i => i.Status, opt => opt.MapFrom(i => new EnumValue(i.StatusName, i.StatusStyle)))
            .ForMember(i => i.Impact, opt => opt.MapFrom(i => new EnumValue(i.ImpactName, i.ImpactStyle)));
            
        
        // CreateMap<InitiativeIssue, IssueSummaryDto>();

        CreateMap<CreateIssueCommand, InitiativeIssue>();

        CreateMap<InitiativeIssue, EditIssueDto>()
            .ForMember(i => i.Scope, opt => opt.MapFrom(i => i.ScopeEnum))
            .ForMember(i => i.Status, opt => opt.MapFrom(i => i.StatusEnum))
            .ForMember(i => i.Impact, opt => opt.MapFrom(i => i.ImpactEnum));

        CreateMap<UpdateIssueCommand, InitiativeIssue>()
            .ForMember(i => i.Initiative, opt => opt.Ignore())
            .ForMember(i => i.InitiativeId, opt => opt.Ignore())
            .ForMember(i => i.Id, opt => opt.Ignore());
    }
}