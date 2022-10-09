using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.Projects;
using Moasher.Application.Features.Projects.Commands.CreateProject;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Common.Mappings;

public class ProjectMappings : Profile
{
    public ProjectMappings()
    {
        CreateMap<InitiativeProject, ProjectDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>();
        
        CreateMap<CreateProjectCommand, InitiativeProject>();
    }
}