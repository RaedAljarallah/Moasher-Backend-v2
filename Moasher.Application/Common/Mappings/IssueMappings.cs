using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.Issues;
using Moasher.Application.Features.Issues.Commands.CreateIssue;
using Moasher.Application.Features.Issues.Commands.UpdateIssue;
using Moasher.Application.Features.Issues.Queries.EditIssue;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Common.Mappings;

public class IssueMappings : Profile
{
    public IssueMappings()
    {
        CreateMap<InitiativeIssue, IssueDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>();
        
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