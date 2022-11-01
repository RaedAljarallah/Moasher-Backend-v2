using Moasher.Application.Common.Constants;
using Moasher.Application.Features.Entities;
using Moasher.Application.Features.Users.Commands;

namespace Moasher.Application.Features.Users.Queries.EditUser;

public record EditUserDto : UserCommandBase
{
    public Guid Id { get; set; }
    public EntityDto Entity { get; set; } = default!;
    public string LocalizedRole => AppRoles.GetLocalizedName(Role);
    public bool Suspended { get; set; }
}