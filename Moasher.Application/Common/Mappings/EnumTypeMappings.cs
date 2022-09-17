using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.EnumTypes;
using Moasher.Application.Features.EnumTypes.Commands.CreateEnumType;
using Moasher.Application.Features.EnumTypes.Commands.UpdateEnumType;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities;

namespace Moasher.Application.Common.Mappings;

public class EnumTypeMappings : Profile
{
    public EnumTypeMappings()
    {
        CreateMap<EnumType, EnumTypeDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>();
        
        CreateMap<CreateEnumTypeCommand, EnumType>();

        CreateMap<UpdateEnumTypeCommand, EnumType>()
            .ForMember(c => c.Id, opt => opt.Ignore());
    }
}