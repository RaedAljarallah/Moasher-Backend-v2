using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Constants;
using Moasher.Domain.Entities;

namespace Moasher.Persistence.Extensions;

public static class ModelBuilderExtensions
{
    public static void SeedRoles(this ModelBuilder builder)
    {
        var appRoles = AppRoles.AllRoles.Select(r => new Role(r)
        {
            Id = Guid.NewGuid(), 
            NormalizedName = r.ToUpper(),
            LocalizedName = AppRoles.GetLocalizedName(r)
        });
        builder.Entity<Role>().HasData(appRoles);
    }
}