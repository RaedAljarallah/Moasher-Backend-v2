using AutoMapper;
using Moasher.Application.Features.Roles;
using Moasher.Domain.Entities;

namespace Moasher.Application.Common.Mappings;

public class RoleMappings : Profile
{
    public RoleMappings()
    {
        CreateMap<Role, RoleDto>()
            .ForMember(r => r.Id, opt => opt.MapFrom(r => r.Name));
    }
}