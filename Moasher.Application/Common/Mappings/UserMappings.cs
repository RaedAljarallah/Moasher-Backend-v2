using AutoMapper;
using Moasher.Application.Features.Users;
using Moasher.Application.Features.Users.Commands.CreateUser;
using Moasher.Application.Features.Users.Queries.EditUser;
using Moasher.Domain.Entities;

namespace Moasher.Application.Common.Mappings;

public class UserMappings : Profile
{
    public UserMappings()
    {
        CreateMap<User, UserDto>()
            .ForMember(u => u.EntityName, opt => opt.MapFrom(u => u.Entity.Name))
            .ForMember(u => u.IsActive, opt => opt.MapFrom(u => u.IsActive()))
            .ForMember(u => u.IsSuspended, opt => opt.MapFrom(u => u.IsSuspended()));

        CreateMap<CreateUserCommand, User>();

        CreateMap<User, EditUserDto>();
    }
}