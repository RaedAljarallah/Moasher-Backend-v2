using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.Programs;
using Moasher.Application.Features.Programs.Commands.CreateProgram;
using Moasher.Application.Features.Programs.Commands.UpdateProgram;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Extensions;
using Moasher.Domain.Entities;

namespace Moasher.Application.Common.Mappings;

public class ProgramMappings : Profile
{
    public ProgramMappings()
    {
        CreateMap<Program, ProgramDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>()
            .ForMember(p => p.InitiativesCount, opt => opt.MapFrom(p => p.Initiatives.Count))
            .ForMember(p => p.KPIsCount, opt => opt.MapFrom(p => p.GetKPIs().Count()))
            .ForMember(p => p.StrategicObjectivesCount, opt => opt.MapFrom(p => p.GetStrategicObjectives().Count()))
            .ForMember(p => p.EntitiesCount, opt => opt.MapFrom(p => p.GetEntities().Count()));

        CreateMap<CreateProgramCommand, Program>();

        CreateMap<UpdateProgramCommand, Program>()
            .ForMember(p => p.Id, opt => opt.Ignore());
    }
}