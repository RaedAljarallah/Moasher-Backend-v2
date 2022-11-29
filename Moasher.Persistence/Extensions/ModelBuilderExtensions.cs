using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Constants;
using Moasher.Domain.Entities;
using Moasher.Domain.Types;

namespace Moasher.Persistence.Extensions;

public static class ModelBuilderExtensions
{
    public static void SeedInitialData(this ModelBuilder builder)
    {
        // Seeding Organizer Entity
        var organizerEntityId = Guid.NewGuid();
        builder.Entity<Entity>().HasData(new Entity
        {
            Id = organizerEntityId,
            Code = "VRO",
            Name = "مكتب تحقيق الرؤية",
            IsOrganizer = true,
            CreatedAt = LocalDateTime.Now,
            CreatedBy = "System"
        });
        
        // Seeding Roles
        var superAdminRoleId = Guid.NewGuid();
        var appRoles = AppRoles.AllRoles
            .Where(r => r != AppRoles.SuperAdmin)
            .Select(r => new Role(r)
            {
                Id = Guid.NewGuid(), 
                NormalizedName = r.ToUpper(),
                LocalizedName = AppRoles.GetLocalizedName(r)
            })
            .ToList();
        appRoles.Add(new Role(AppRoles.SuperAdmin)
        {
            Id = superAdminRoleId,
            NormalizedName = AppRoles.SuperAdmin.ToUpper(),
            LocalizedName = AppRoles.GetLocalizedName(AppRoles.SuperAdmin)
        });
        builder.Entity<Role>().HasData(appRoles);
        
        // Seeding SuperAdmin User
        const string superAdminEmail = "SuperAdmin@Moasher.com";
        var superAdminUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = superAdminEmail,
            NormalizedUserName = superAdminEmail.ToUpper(),
            Email = superAdminEmail,
            NormalizedEmail = superAdminEmail.ToUpper(),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            PhoneNumber = "0555555555",
            LockoutEnabled = true,
            FirstName = "Super",
            LastName = "Admin",
            Role = AppRoles.SuperAdmin.ToUpper(),
            MustChangePassword = true,
            EntityId = organizerEntityId
        };
        var passwordHasher = new PasswordHasher<User>();
        superAdminUser.PasswordHash = passwordHasher.HashPassword(superAdminUser, superAdminEmail);

        builder.Entity<User>().HasData(superAdminUser);
        
        // Seeding UserRoles
        builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
        {
            UserId = superAdminUser.Id,
            RoleId = superAdminRoleId
        });
    }
}