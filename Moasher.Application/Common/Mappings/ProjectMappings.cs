using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.Projects;
using Moasher.Application.Features.Projects.Commands.CreateProject;
using Moasher.Application.Features.Projects.Commands.UpdateProject;
using Moasher.Application.Features.Projects.Queries.EditProject;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Common.Mappings;

public class ProjectMappings : Profile
{
    public ProjectMappings()
    {
        CreateMap<InitiativeProject, ProjectDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>()
            .ForMember(p => p.Phase, opt => opt.MapFrom(p => new EnumValue(p.PhaseName, p.PhaseStyle)));

        CreateMap<CreateProjectCommand, InitiativeProject>()
            .ForMember(p => p.Expenditures, opt => opt.Ignore());

        CreateMap<InitiativeProject, EditProjectDto>()
            .ForMember(p => p.Phase, opt => opt.MapFrom(p => p.PhaseEnum))
            .ForMember(p => p.Milestones, opt => opt.MapFrom(p => p.ContractMilestones.Select(cm => cm.Milestone)));

        CreateMap<UpdateProjectCommand, InitiativeProject>()
            .ForMember(p => p.Initiative, opt => opt.Ignore())
            .ForMember(p => p.InitiativeId, opt => opt.Ignore())
            .ForMember(p => p.Expenditures, opt => opt.Ignore())
            .ForMember(p => p.ExpendituresBaseline, opt => opt.Ignore())
            .ForMember(p => p.Baseline, opt => opt.Ignore())
            .ForMember(p => p.Id, opt => opt.Ignore());
    }
}