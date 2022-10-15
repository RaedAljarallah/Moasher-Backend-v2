using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.Projects;
using Moasher.Application.Features.Projects.Commands.CreateProject;
using Moasher.Application.Features.Projects.Commands.UpdateProject;
using Moasher.Application.Features.Projects.Queries.EditProject;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Common.Mappings;

public class ProjectMappings : Profile
{
    public ProjectMappings()
    {
        CreateMap<InitiativeProject, ProjectDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>();

        CreateMap<CreateProjectCommand, InitiativeProject>()
            .ForMember(p => p.Expenditures, opt => opt.Ignore());

        CreateMap<InitiativeProject, EditProjectDto>()
            .ForMember(p => p.Phase, opt => opt.MapFrom(p => p.PhaseEnum));

        CreateMap<UpdateProjectCommand, InitiativeProject>()
            .ForMember(p => p.Initiative, opt => opt.Ignore())
            .ForMember(p => p.InitiativeId, opt => opt.Ignore())
            .ForMember(p => p.Expenditures, opt => opt.Ignore())
            .ForMember(p => p.ExpendituresBaseline, opt => opt.Ignore())
            .ForMember(p => p.Id, opt => opt.Ignore());
    }
}