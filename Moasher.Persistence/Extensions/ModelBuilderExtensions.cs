using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Constants;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Entities.KPIEntities;
using Moasher.Domain.Types;

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

    public static void SeedOrganizerEntity(this ModelBuilder builder)
    {
        builder.Entity<Entity>().HasData(new Entity
        {
            Id = Guid.NewGuid(),
            Code = "VRO",
            Name = "مكتب تحقيق الرؤية",
            IsOrganizer = true,
            CreatedAt = LocalDateTime.Now,
            CreatedBy = "System"
        });
    }
}