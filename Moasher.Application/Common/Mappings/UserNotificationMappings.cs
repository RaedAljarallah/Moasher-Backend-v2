using AutoMapper;
using Moasher.Application.Features.UserNotifications;
using Moasher.Domain.Entities;

namespace Moasher.Application.Common.Mappings;

public class UserNotificationMappings : Profile
{
    public UserNotificationMappings()
    {
        CreateMap<UserNotification, UserNotificationDto>();
    }
}